﻿using UnityEngine;

namespace Obvious.Soap.Editor
{
    public class SoapSettings : ScriptableObject
    {
        [Tooltip("Default: displays all the parameters of variables. Minimal : only displays the value.")]
        public EVariableDisplayMode VariableDisplayMode = EVariableDisplayMode.Default;
        
        [Tooltip("")]
        public ENamingCreationMode NamingOnCreationMode = ENamingCreationMode.Auto;
    }

    public enum EVariableDisplayMode
    {
        Default,
        Minimal
    }
    
    public enum ENamingCreationMode
    {
        Auto,
        Manual
    }
}