using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlossaryPage2Menu : MonoBehaviour
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
        MenuManager.GoTo(MenuOptions.GlossaryPage3);
    }
    public void OnPreviousPage()
    {
        MenuManager.GoTo(MenuOptions.Glossary);
    }
}
