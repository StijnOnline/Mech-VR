using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ThrowPuzzle : MonoBehaviour
{
    private int count = 0;

    public UnityEvent completeEvent;

    public void HitRing(GameObject g, Collider other)
    {
        string ringname = g.GetComponent<Renderer>().material.name;
        string blockname = other.GetComponent<Renderer>().material.name;

        if(ringname == blockname) {
            count++;
            Destroy(other.gameObject);
        }

        if(count == 3) {
            completeEvent.Invoke();
            this.enabled = false;
        }
    }
}
