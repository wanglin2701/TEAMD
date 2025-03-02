using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneNavigator : MonoBehaviour
{
    private GameObject HowtoPlay_screen;
    private Button[] TitleScreen_BTNS;
    private Button PlayBTN;
    private Button HowtoPlayBTN;
    private Button QuitBTN;

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

    public void PlayGame()
    {
        SceneManager.LoadScene("GameUI");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
