using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class MechTop : MonoBehaviour {
    [Header("CameraRig")]
    public Transform VRrig;
    public Transform head;
    public Transform leftController;
    public Transform rightController;

    [Header("Calibration")]
    public Vector3 headOffset;
    private Vector3 directionalHeadOffset = new Vector3(0, 1.8f, 0);
    private Vector3 rigOffset;

    public SteamVR_Action_Boolean calibrateAction;
    public SteamVR_Input_Sources inputSource;


    [Header("Arms")]
    public Transform leftTarget;
    public Transform rightTarget;
    public float handPositionScaleFactor = 4f;



    private float slerp = 0.1f;

    void OnEnable() {
        calibrateAction.AddOnStateDownListener(Calibrate, inputSource);

    }

    void Start() {

    }

    void Update() {
        MoveTarget(leftTarget, leftController);
        MoveTarget(rightTarget, rightController);
    }

    void LateUpdate() {
        //Quaternion rotation = Quaternion.LookRotation((head.forward + leftHand.forward + rightHand.forward));
        Quaternion rotation = head.rotation;
        rotation.z = 0;
        rotation.x = 0;

        transform.localRotation = Quaternion.Slerp(transform.rotation, rotation, slerp);

        directionalHeadOffset = transform.forward * headOffset.z;
        directionalHeadOffset += Vector3.Cross(Vector3.up, transform.forward) * headOffset.x;
        directionalHeadOffset += Vector3.up * headOffset.y; //maybe only y needed, not tested on other axis
        VRrig.position = transform.position + rigOffset + directionalHeadOffset;
    }

    //TODO: maybe move elbow target outward based on rotation
    private void MoveTarget(Transform target, Transform controller) {
        Vector3 startpoint = transform.position + directionalHeadOffset;
        Vector3 toContoller = startpoint - controller.position;

        target.position = startpoint - (toContoller * handPositionScaleFactor);
        target.rotation = controller.rotation;
    }

    private void Calibrate(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource) {
        rigOffset = VRrig.position - head.position;
    }
}
