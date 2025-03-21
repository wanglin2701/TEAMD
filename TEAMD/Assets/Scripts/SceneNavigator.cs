using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneNavigator : MonoBehaviour
{
    private GameObject HowtoPlay_screen;
    private GameObject Gameover_screen;
    private GameObject CompleteLevel_screen;
    private GameObject PauseMenu_screen;

    // Start is called before the first frame update
    void Start()
    {
        if(SceneManager.GetActiveScene().name == "TitleScreen")
        {
            HowtoPlay_screen = GameObject.Find("HowtoPlay");
            HowtoPlay_screen.SetActive(false);
        }

    }

    public void OpenHowtoPlay()
    {
        HowtoPlay_screen.SetActive(true);

    }

    public void CloseHowtoPlay()
    {
        HowtoPlay_screen.SetActive(false);

    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("GameUI");
    }

    public void PlayGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("GameUI");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void GoTitleScreen()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("TitleScreen");
    }
}
