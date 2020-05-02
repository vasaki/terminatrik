using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenu : MonoBehaviour
{
    public void OnEasy()
    {
        MenuManager.GoTo(MenuOptions.Easy);
    }
    public void OnNormal()
    {
        MenuManager.GoTo(MenuOptions.Normal);
    }
    public void OnHard()
    {
        MenuManager.GoTo(MenuOptions.Hard);
    }
    public void OnQuit()
    {
        MenuManager.GoTo(MenuOptions.Quit);
    }
    public void OnHelp()
    {
        MenuManager.GoTo(MenuOptions.Help);
    }
}
