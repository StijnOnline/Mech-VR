using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Shield : MonoBehaviour {
    public SteamVR_Action_Boolean shieldAction;
    public SteamVR_Input_Sources inputSource = SteamVR_Input_Sources.LeftHand;

    private SpriteRenderer spriteR;
    private MeshCollider coll;

    void OnEnable() {
        shieldAction.AddOnChangeListener(UpdateShield, inputSource);
        spriteR = GetComponent<SpriteRenderer>();
        coll = GetComponentInChildren<MeshCollider>();
    }

    private void UpdateShield(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource, bool newState) {
        spriteR.enabled = newState;
        coll.enabled = newState;
    }

    private void OnCollisionEnter(Collision collision) {
        Debug.Log("Something hit the shield", collision.gameObject);
    }
}
