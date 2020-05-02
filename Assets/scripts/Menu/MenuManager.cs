using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class MenuManager
{
    static GameObject pauseMenu = null;
    static GameObject endMenu = null;
    static GameObject shop = null;
    static GameDifficulty difficulty;
    public static GameDifficulty Difficulty => difficulty;
    public static void GoTo(MenuOptions option, EventParameter param)
    {
        switch (option)
        {
            case MenuOptions.PlayAgainMenu:
                endMenu = (GameObject)Object.Instantiate(Resources.Load("Menus/EndGameMenu"));
                endMenu.GetComponent<EndGameMenu>().SetFinalStatistics(
                    ((EndGameParam)param).score, ((EndGameParam)param).time);
                break;
            case MenuOptions.Shop:
                if (pauseMenu == null && endMenu == null && shop == null)
                {
                    shop = (GameObject)Object.Instantiate(Resources.Load("Equipment/Shop"));
                    shop.GetComponent<Shop>().DisplayItems(((EquipmentParam)param).equipment);
                }
                break;
        }
    }
    public static void GoTo(MenuOptions option)
    {
        switch (option)
        {
            case MenuOptions.PlayAgain:
                SceneManager.LoadScene("start");
                break;
            case MenuOptions.Quit:
                Application.Quit();
                break;
            case MenuOptions.PauseMenu:
                if (pauseMenu == null && endMenu == null && shop == null)
                    pauseMenu = (GameObject)Object.Instantiate(
                        Resources.Load("Menus/PauseMenu"));
                else if (pauseMenu != null) pauseMenu.GetComponent<PauseMenu>().OnResume();
                break;
            case MenuOptions.Easy:
                difficulty = GameDifficulty.Easy;
                SceneManager.LoadScene("gameplay");
                break;
            case MenuOptions.Normal:
                difficulty = GameDifficulty.Normal;
                SceneManager.LoadScene("gameplay");
                break;
            case MenuOptions.Hard:
                difficulty = GameDifficulty.Hard;
                SceneManager.LoadScene("gameplay");
                break;
            case MenuOptions.Help:
                SceneManager.LoadScene("help");
                break;
            case MenuOptions.Glossary:
                SceneManager.LoadScene("glossary1");
                break;
            case MenuOptions.GlossaryPage2:
                SceneManager.LoadScene("glossary2");
                break;
            case MenuOptions.GlossaryPage3:
                SceneManager.LoadScene("glossary3");
                break;
        }
    }
}
