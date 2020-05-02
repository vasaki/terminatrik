using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct IntParam : EventParameter
{
    public int AnInt;
    public IntParam(int param)
    {
        AnInt = param;
    }
}
