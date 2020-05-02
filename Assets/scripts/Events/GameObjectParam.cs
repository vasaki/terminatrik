using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct GameObjectParam : EventParameter
{
    public GameObject gameObject;
    public GameObjectParam(GameObject param)
    {
        gameObject = param;
    }
}
