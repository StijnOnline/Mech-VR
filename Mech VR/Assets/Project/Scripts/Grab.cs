using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Grab : MonoBehaviour {

    [Header("Ability")]
    public SteamVR_Action_Single grabAction;
    public SteamVR_Input_Sources inputSource = SteamVR_Input_Sources.RightHand;
    public Vector3 holdOffset;
    public float grabRange = 10f;
    public float throwSpeed = 10f;

    [Header("Indicators")]
    public float indicatorThickness = 0.005f;
    public Material grabableMaterial;
    public Material ungrabableMaterial;
    public ArcRenderer arcRenderer;

    private Transform grabbedObject;
    private Rigidbody grabbedRigidB;
    private Collider grabbedcoll;
    private GameObject pointer;
    private Renderer pointerRenderer;


    void OnEnable() {
        grabAction.AddOnAxisListener(GrabUpdate, inputSource);
    }

    void Start() {
        pointer = GameObject.CreatePrimitive(PrimitiveType.Cube);
        pointer.transform.parent = transform;
        pointer.transform.localScale = new Vector3(indicatorThickness, indicatorThickness, grabRange);
        pointer.transform.localPosition = new Vector3(0f, 0f, grabRange / 2);
        pointer.transform.localRotation = Quaternion.identity;
        Destroy(pointer.GetComponent<BoxCollider>());
    }

    void Update() {

        if(grabbedObject == null) {
            pointer.SetActive(true);
            arcRenderer.gameObject.SetActive(false);
            //line indicator
            pointerRenderer = pointer.GetComponent<Renderer>();
            pointerRenderer.material = ungrabableMaterial;

            //change color if targeted object is grabable
            int mask = ~LayerMask.NameToLayer("Mech");
            RaycastHit hit;
            if(Physics.Raycast(transform.position, transform.forward, out hit, grabRange, mask)) {
                if(hit.transform.GetComponent<IGrabable>() && hit.transform.GetComponent<Rigidbody>()) { pointerRenderer.material = grabableMaterial; }
            }
        } else {
            pointer.SetActive(false);
            arcRenderer.gameObject.SetActive(true);
            arcRenderer.RenderArc(throwSpeed);
        }

    }

    private void GrabUpdate(SteamVR_Action_Single fromAction, SteamVR_Input_Sources fromSource, float newAxis, float newDelta) {

        if(grabbedObject == null && newAxis > 0.5f) {
            //raycast
            int mask = ~LayerMask.NameToLayer("Mech");
            RaycastHit hit;
            if(Physics.Raycast(transform.position, transform.forward, out hit, grabRange, mask)) {
                grabbedRigidB = hit.transform.GetComponent<Rigidbody>();

                if(hit.transform.GetComponent<IGrabable>() && grabbedRigidB) {
                    grabbedObject = hit.transform;
                    grabbedRigidB.isKinematic = true;
                    grabbedcoll = grabbedObject.GetComponent<Collider>();
                    grabbedcoll.enabled = false;
                }
            }
        }
        if(grabbedObject != null && newAxis > 0.5f) {
            //grabbedObject.position = Vector3.Lerp(grabbedObject.position, transform.position + holdOffset, 0.3f);
            grabbedObject.position = Vector3.Lerp(grabbedObject.position, transform.position + transform.TransformDirection(holdOffset), 0.3f);
            grabbedObject.rotation = Quaternion.Lerp(grabbedObject.rotation, transform.rotation, 0.3f);
        }
        if(grabbedObject != null && newAxis < 0.3f) {
            grabbedRigidB.isKinematic = false;
            grabbedObject.position = grabbedObject.position + transform.forward;
            grabbedRigidB.velocity = transform.forward * throwSpeed; // change to 'throwing'
            grabbedcoll.enabled = true;

            grabbedObject = null;
            grabbedRigidB = null;
        }
    }
}