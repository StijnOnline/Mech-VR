using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateMech : MonoBehaviour
{
    public Transform head;
    public Transform leftHand;
    public Transform rightHand;

    void Update()
    {
        Quaternion rotation = Quaternion.LookRotation((head.forward + leftHand.forward + rightHand.forward));
        rotation.x *= 0.1f;
        transform.localRotation = rotation;
    }
}