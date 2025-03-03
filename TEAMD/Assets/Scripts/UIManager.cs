using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    
    public GameObject levelCompleteScreen;
    public GameObject levelFailScreen;
    public GameObject pauseScreen; 

    void Start()
    {
        levelCompleteScreen.SetActive(false);
        levelFailScreen.SetActive(false);
        pauseScreen.SetActive(false); // Make sure the pause menu is hidden at start
    }
    
    public void ShowLevelComplete()
    {
        levelCompleteScreen.SetActive(true);
        Time.timeScale = 0f; // Pause game when level is complete
    }

    public void ShowLevelFail()
    {
        levelFailScreen.SetActive(true);
        Time.timeScale = 0f; // Pause game when level is failed

    }

    public void ShowPauseScreen()
    {
        pauseScreen.SetActive(true);
        Time.timeScale = 0f; // Pause game
    }

    public void HidePauseScreen()
    {
        pauseScreen.SetActive(false);
        Time.timeScale = 1f; // Resume game
    }
}
