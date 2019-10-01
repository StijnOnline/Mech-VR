using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public Transform IKhand;
    private Rigidbody rigidB;

    private void Start()
    {
        rigidB = GetComponent<Rigidbody>();
    }


    void FixedUpdate()
    {
        rigidB.MovePosition(IKhand.position);
        rigidB.MoveRotation(IKhand.rotation);


    }
}
