//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Valve.VR
{
    using System;
    using UnityEngine;
    
    
    public partial class SteamVR_Actions
    {
        
        private static SteamVR_Input_ActionSet_Mech p_Mech;
        
        public static SteamVR_Input_ActionSet_Mech Mech
        {
            get
            {
                return SteamVR_Actions.p_Mech.GetCopy<SteamVR_Input_ActionSet_Mech>();
            }
        }
        
        private static void StartPreInitActionSets()
        {
            SteamVR_Actions.p_Mech = ((SteamVR_Input_ActionSet_Mech)(SteamVR_ActionSet.Create<SteamVR_Input_ActionSet_Mech>("/actions/Mech")));
            Valve.VR.SteamVR_Input.actionSets = new Valve.VR.SteamVR_ActionSet[] {
                    SteamVR_Actions.Mech};
        }
    }
}
