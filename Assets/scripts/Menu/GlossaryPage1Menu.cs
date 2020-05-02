using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlossaryPage1Menu : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) OnBack();
    }
    public void OnBack()
    {
        MenuManager.GoTo(MenuOptions.PlayAgain);
    }

    public void OnNextPage()
    {
        MenuManager.GoTo(MenuOptions.GlossaryPage2);
    }
}
