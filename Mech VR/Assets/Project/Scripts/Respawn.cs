using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    private Vector3 startPos;
    public float limit = 100f;

    private void Start() {
        startPos = transform.position;
    }

    void FixedUpdate()
    {
        if((transform.position - startPos).magnitude > limit) {
            transform.position = startPos;

            Rigidbody rb = GetComponent<Rigidbody>();
            if(rb) {
                rb.velocity = Vector3.zero;
            }
        }        
    }
}