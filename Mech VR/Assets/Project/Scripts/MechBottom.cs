using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechBottom : MonoBehaviour {

    [Header("Controls")]
    public float acceleration;
    public float maxSpeed;
    public float turnSpeed;
    public float hoverDist;
    public float jumpSpeed;

    [Header("Mech top")]
    public Transform top;
    public float toTopheight;
    public bool connected = true;
    private Rigidbody rigidB;
    public float connectRange;

    float input_hor;
    float input_ver;
    bool input_jump;
    bool input_connect;
    

    void Start() {
        rigidB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update() {
        input_hor = Input.GetAxisRaw("Horizontal");
        input_ver = Input.GetAxisRaw("Vertical");
        input_jump = Input.GetKeyDown(KeyCode.Space);
        input_connect = Input.GetKeyDown(KeyCode.E);
        if(input_connect && !connected) { Connect(); }
        if(input_connect && connected) { Disconnect(); }

        if(connected) {
            top.position = transform.position + toTopheight * Vector3.up;
        }
    }

    private void FixedUpdate() {
        //raycast down and then change ypos slightly above ground
        int mask = ~LayerMask.NameToLayer("Mech");

        RaycastHit hit;
        if(Physics.Raycast(transform.position, Vector3.down, out hit, Mathf.Infinity, mask)) {
            //rigidB.AddForce((transform.position - hit.point - Vector3.up * hoverDist) * hoverForce, ForceMode.Acceleration);
            transform.position = hit.point + Vector3.up * hoverDist;
        }

        //rigidB.AddForce(transform.forward * input_ver * acceleration, ForceMode.Acceleration);
        //rigidB.AddForce(transform.forward * input_ver * speed);  


        rigidB.MoveRotation(rigidB.rotation * Quaternion.Euler(0, input_hor * turnSpeed, 0));

        Vector3 localVel = transform.InverseTransformDirection(rigidB.velocity); //relative velocity
        if(input_ver != 0) {
            if(Mathf.Abs(localVel.z + input_ver * acceleration) > maxSpeed) {
                rigidB.velocity = transform.forward * maxSpeed * Mathf.Sign(localVel.z);
            } else {
                rigidB.velocity = transform.forward * (localVel.z + input_ver * acceleration);
            }
        }

        if(input_jump && !connected) {
            rigidB.AddForce(Vector3.up * jumpSpeed, ForceMode.Acceleration);
        }
    }

    private void Connect() {
        if((top.position - transform.position).magnitude < connectRange) {
            StartCoroutine(Attach());
        }
    }

    private void Disconnect() {
        connected = false;
    }

    public IEnumerator Attach() {
        Vector3 targetpos = transform.position + toTopheight * Vector3.up;
        while((targetpos - top.position).magnitude > 0.1f) {
            targetpos = transform.position + toTopheight * Vector3.up;
            top.position = Vector3.Lerp(top.position, targetpos, 0.1f);
            yield return 0;
        }
        connected = true;
        
        
    } 
}
