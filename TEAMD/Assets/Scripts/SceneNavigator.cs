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

    SoundManager soundManaager;


    // Start is called before the first frame update
    void Start()
    {
        soundManaager = GameObject.Find("SFXManager").GetComponent<SoundManager>();

        if (SceneManager.GetActiveScene().name == "TitleScreen")
        {
            HowtoPlay_screen = GameObject.Find("HowtoPlay");
            HowtoPlay_screen.SetActive(false);
        }

    }

    public void OpenHowtoPlay()
    {
        soundManaager.PlaySound("BTNClick");

        HowtoPlay_screen.SetActive(true);

    }

    public void CloseHowtoPlay()
    {
        soundManaager.PlaySound("BTNClick");

        HowtoPlay_screen.SetActive(false);

    }

    public void RestartGame()
    {
        soundManaager.PlaySound("BTNClick");

        Time.timeScale = 1f;
        SceneManager.LoadScene("GameUI");
    }

    public void PlayGame()
    {
        soundManaager.ChangeMusic("Gameplay");
        soundManaager.PlaySound("BTNClick");

        Time.timeScale = 1f;
        SceneManager.LoadScene("GameUI");
    }

    public void QuitGame()
    {
        soundManaager.PlaySound("BTNClick");

        Application.Quit();
    }

    public void GoTitleScreen()
    {
        soundManaager.PlaySound("BTNClick");

        Time.timeScale = 1f;
        SceneManager.LoadScene("TitleScreen");
    }
}
