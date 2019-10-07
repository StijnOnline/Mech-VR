using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class ButtonTest : MonoBehaviour
{
    public SteamVR_Action_Boolean testAction;
    public SteamVR_Input_Sources inputSource = SteamVR_Input_Sources.RightHand;
    void OnEnable()
    {
        testAction.AddOnChangeListener(Test2, inputSource);
        
    }

    private void Test2(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource, bool newState)
    {
        Debug.Log( fromAction);
        Debug.Log( fromSource);
        Debug.Log( newState);
    }

    public void Test()
    {

        Debug.Log("BOOP");

    }

}