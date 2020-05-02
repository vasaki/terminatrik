using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
    }
    public void OnResume()
    {
        Time.timeScale = 1;
        Destroy(gameObject);
    }
    public void OnRestart()
    {
        Time.timeScale = 1;
        MenuManager.GoTo(MenuOptions.PlayAgain);
    }
    public void OnQuit()
    {
        Time.timeScale = 1;
        MenuManager.GoTo(MenuOptions.Quit);
    }
}
