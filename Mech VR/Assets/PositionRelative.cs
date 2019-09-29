using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionRelative : MonoBehaviour
{
    Transform parent;
    Vector3 offset;

    void Start()
    {
        parent = transform.parent;
        offset = parent.position - transform.position;
    }

    void Update()
    {
        transform.position = parent.position - offset;
    }
}
