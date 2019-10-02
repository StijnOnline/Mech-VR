using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechBottom : MonoBehaviour
{
    private Rigidbody rigidB;
    public Transform top;
    public float maxSpeed;
    public float acceleration;

    public float turnSpeed;
    public float stickForce;

    public float jumpSpeed;

    public bool connected = true;

    float input_hor;
    float input_ver;
    bool input_jump;
    bool input_connect;

    void Start()
    {
        rigidB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        input_hor = Input.GetAxis("Horizontal");
        input_ver = Input.GetAxis("Vertical");
        input_jump = Input.GetKeyDown(KeyCode.Space);
        input_connect = Input.GetKeyDown(KeyCode.E);
        if (input_connect ) { Connect(); }        

        


        if (connected)
        {
            top.position = (transform.position);
        }
    }

    private void FixedUpdate()
    {
        //raycast down and then change heigth slightly above ground



        rigidB.AddForce(transform.forward * input_ver * acceleration, ForceMode.Acceleration);
        rigidB.MoveRotation(rigidB.rotation * Quaternion.Euler(0, input_hor * turnSpeed, 0));
        rigidB.AddForce( -1 * transform.up * stickForce, ForceMode.Acceleration);

        if (input_jump && !connected)
        {
            rigidB.AddForce(Vector3.up * jumpSpeed, ForceMode.Acceleration);
        }
    }

    private void Connect()
    {
        
    }
}
