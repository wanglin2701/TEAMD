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

    // Start is called before the first frame update
    void Start()
    {
        if(SceneManager.GetActiveScene().name == "TitleScreen")
        {
            HowtoPlay_screen = GameObject.Find("HowtoPlay");
            HowtoPlay_screen.SetActive(false);
        }

        else if(SceneManager.GetActiveScene().name == "GameUI")
        {
            Gameover_screen = GameObject.Find("LevelFailScreen");
            Gameover_screen.SetActive(false);

            CompleteLevel_screen = GameObject.Find("LevelCompleteScreen");
            CompleteLevel_screen.SetActive(false);
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

    public void PlayGame()
    {
        Gameover_screen.SetActive(false);
        CompleteLevel_screen.SetActive(false);
        SceneManager.LoadScene("GameUI");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void GoTitleScreen()
    {
        SceneManager.LoadScene("TitleScreen");
    }
}
