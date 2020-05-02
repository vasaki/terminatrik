using UnityEngine;
using UnityEngine.UI;
using System;

public class EndGameMenu : MonoBehaviour
{
    private void Start()
    {
        Time.timeScale = 0;
    }
    public void OnPlayAgainButton()
    {
        MenuManager.GoTo(MenuOptions.PlayAgain);
    }

    public void OnQuitButton()
    {
        MenuManager.GoTo(MenuOptions.Quit);
    }

    private void OnDestroy()
    {
        Time.timeScale = 1;
    }
    public void SetFinalStatistics(int score, TimeSpan time)
    {
        GameObject.FindGameObjectWithTag("ScoreValueText").GetComponent<Text>().text =
            score.ToString();
        GameObject.FindGameObjectWithTag("TimeValueText").GetComponent<Text>().text =
            time.ToString(@"mm\:ss");
    }
}
