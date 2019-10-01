using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandTarget : MonoBehaviour
{
    public Transform controller;
    public Transform mech;

    public float scaleFactor = 1.5f;



    void Update()
    {
        transform.position = (controller.position - mech.position) * scaleFactor;
        transform.rotation = controller.rotation * Quaternion.Euler(-20,0,0);
    }
}
