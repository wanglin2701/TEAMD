using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    
    public GameObject levelCompleteScreen;
    public GameObject levelFailScreen;
    private GameManager gameManager;
    public GameObject pauseScreen;

    SoundManager soundManaager;


    public void Start()
    {
        soundManaager = GameObject.Find("SFXManager").GetComponent<SoundManager>();
        gameManager = FindObjectOfType<GameManager>();
        levelCompleteScreen.SetActive(false);
        levelFailScreen.SetActive(false);
        pauseScreen.SetActive(false);

    }

    public void ShowLevelComplete()
    {
        gameManager.isPaused = true;
        levelCompleteScreen.SetActive(true);
        soundManaager.PlaySound("LevelWin");

    }

    public void ShowLevelFail()
    {
        gameManager.isPaused = true;
        levelFailScreen.SetActive(true);
    }

    public void ShowPauseScreen()
    {
        pauseScreen.SetActive(true);
        Time.timeScale = 0f; // Pause game
        gameManager.isPaused = true;
    }

    public void HidePauseScreen()
    {
        pauseScreen.SetActive(false);
        Time.timeScale = 1f; // Resume game
        gameManager.isPaused = false;
    }
}
