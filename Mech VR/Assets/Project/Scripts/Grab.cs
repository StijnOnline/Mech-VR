using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Grab : MonoBehaviour {

    public SteamVR_Action_Single grabAction;
    public SteamVR_Input_Sources inputSource = SteamVR_Input_Sources.RightHand;
    public float indicatorThickness = 0.005f;
    public float range = 10f;
    public Material grabableMaterial;
    public Material ungrabableMaterial;
    public Vector3 holdOffset;


    private Transform grabbedObject;
    private Rigidbody grabbedRigidB;
    private GameObject pointer;
    private Renderer pointerRenderer;


    void OnEnable() {
        grabAction.AddOnAxisListener(GrabUpdate, inputSource);
    }



    void Start() {
        pointer = GameObject.CreatePrimitive(PrimitiveType.Cube);
        pointer.transform.parent = transform;
        pointer.transform.localScale = new Vector3(indicatorThickness, indicatorThickness, range);
        pointer.transform.localPosition = new Vector3(0f, 0f, range / 2);
        pointer.transform.localRotation = Quaternion.identity;
        Destroy(pointer.GetComponent<BoxCollider>());
    }


    void Update() {
        pointerRenderer = pointer.GetComponent<Renderer>();
        pointerRenderer.material = ungrabableMaterial;
        int mask = ~LayerMask.NameToLayer("Mech");
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.forward, out hit, range, mask)) {
            if(hit.transform.GetComponent<IGrabable>() && hit.transform.GetComponent<Rigidbody>()) { pointerRenderer.material = grabableMaterial; }
        }
    }
    
    private void GrabUpdate(SteamVR_Action_Single fromAction, SteamVR_Input_Sources fromSource, float newAxis, float newDelta) {
              
        if(grabbedObject == null && newAxis > 0.5f) {
            //raycast
            int mask = ~LayerMask.NameToLayer("Mech");
            RaycastHit hit;
            if(Physics.Raycast(transform.position, transform.forward, out hit, range, mask)) {
                grabbedRigidB = hit.transform.GetComponent<Rigidbody>();
                if(hit.transform.GetComponent<IGrabable>() && grabbedRigidB) {
                    grabbedObject = hit.transform;
                    grabbedRigidB.isKinematic = true;
                }
            }
        }
        if(grabbedObject != null && newAxis > 0.5f) {
            grabbedObject.position = Vector3.Lerp(grabbedObject.position, transform.position + holdOffset, 0.3f);
            grabbedObject.rotation = Quaternion.Lerp(grabbedObject.rotation, transform.rotation, 0.3f);
        }
        if(grabbedObject != null && newAxis < 0.3f) {
            grabbedRigidB.isKinematic = false;
            grabbedRigidB.velocity = transform.forward * 10f; // change to 'throwing'
            grabbedObject = null;
            grabbedRigidB = null;
        }
    }
}