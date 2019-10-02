using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandRB : MonoBehaviour
{
    public Transform IKhand;
    private Rigidbody rigidB;

    private void Start()
    {
        rigidB = GetComponent<Rigidbody>();
    }


    void FixedUpdate()
    {
        //Debug.Log(IKhand.position - rigidB.position);

        rigidB.MovePosition(IKhand.position);
        rigidB.MoveRotation(IKhand.rotation);
    }
}