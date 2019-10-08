using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Vector3 moveDist;
    private bool open = false;

    public void Open(Collider other) {
        if(!open) {
            StartCoroutine(moveDoor(transform.position + moveDist));
            open = true;
        }
    }

    private IEnumerator moveDoor(Vector3 targetPos) {
        while((transform.position - targetPos).magnitude > 0.01f) {
            transform.position = Vector3.Lerp(transform.position, targetPos, 0.1f);
            yield return 0;
        }
    }
}