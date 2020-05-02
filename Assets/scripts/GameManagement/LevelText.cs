using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelText : MonoBehaviour
{
    public void OnEndAnimation()
    {
        Destroy(gameObject);
    }
}
