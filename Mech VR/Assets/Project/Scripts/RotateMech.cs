using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateMech : MonoBehaviour
{
    public Transform head;
    public Transform leftHand;
    public Transform rightHand;

    public float slerp = 0.1f;

    void LateUpdate()
    {
        //Quaternion rotation = Quaternion.LookRotation((head.forward + leftHand.forward + rightHand.forward));
        Quaternion rotation = head.rotation;
        rotation.z = 0;
        rotation.x = 0;
        
        transform.localRotation = Quaternion.Slerp(transform.rotation, rotation, slerp);
    }
}