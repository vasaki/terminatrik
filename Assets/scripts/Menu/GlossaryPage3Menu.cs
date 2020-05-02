using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlossaryPage3Menu : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) OnBack();
    }
    public void OnBack()
    {
        MenuManager.GoTo(MenuOptions.PlayAgain);
    }

    public void OnPreviousPage()
    {
        MenuManager.GoTo(MenuOptions.GlossaryPage2);
    }
}
