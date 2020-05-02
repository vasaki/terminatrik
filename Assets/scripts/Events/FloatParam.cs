using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct FloatParam : EventParameter
{
    public float Float;
    public FloatParam(float aFloat)
    {
        Float = aFloat;
    }
}
