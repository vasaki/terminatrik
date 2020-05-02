using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleText : MonoBehaviour
{
    private void OnGameTitleAnimationEnd()
    {
        EventManager.Invoke(EventNames.GameTitleDisappeared);
        Destroy(gameObject);
    }
}
