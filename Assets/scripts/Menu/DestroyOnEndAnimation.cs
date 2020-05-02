﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnEndAnimation : MonoBehaviour
{
    public void OnAnimationEnd()
    {
        Destroy(gameObject);
    }
}
