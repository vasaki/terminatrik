using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpMenu : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) OnBack();
    }
    public void OnBack()
    {
        MenuManager.GoTo(MenuOptions.PlayAgain);
    }

    public void OnGlossary()
    {
        MenuManager.GoTo(MenuOptions.Glossary);
    }
}
