using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcRenderer : MonoBehaviour {
    LineRenderer lr;
    public float maxLength = 10;
    public int resolution = 2;
    float g; //force of gravity on the y axis 
    float radianAngle;
    void Awake() {
        lr = GetComponent<LineRenderer>();
        lr.positionCount = Mathf.CeilToInt(maxLength) * resolution + 1;
        g = Mathf.Abs(Physics.gravity.y);
    }

    public void RenderArc(float velocity) {        
        lr.SetPositions(CalculateArcArray(velocity));
    }

    Vector3[] CalculateArcArray(float velocity) {
        Vector3[] arcArray = new Vector3[Mathf.CeilToInt(maxLength) * resolution + 1];
        radianAngle = Mathf.Deg2Rad * -transform.rotation.eulerAngles.x;
        
        for(int i = 0; i <= maxLength * resolution; i ++) {            
            arcArray[i] = calculateArcPoint(i/(float)resolution , velocity) + transform.position;
        }
        return arcArray;
    }

    Vector3 calculateArcPoint(float t, float velocity) {
        Vector3 next = transform.forward * t;
        next.y = 0;

        float l = next.magnitude;
        next.y = l * Mathf.Tan(radianAngle) - ((g * l * l) / (2 * velocity * velocity * Mathf.Cos(radianAngle) * Mathf.Cos(radianAngle)));

        return next;
    }
    
}