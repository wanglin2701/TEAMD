using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    
    public GameObject levelCompleteScreen;
    public GameObject levelFailScreen;
    private GameManager gameManager;

    public void Start()
    {
        gameManager = FindObjectOfType<GameManager>();

    }

    public void ShowLevelComplete()
    {
        gameManager.isGamePause = true;
        levelCompleteScreen.SetActive(true);
    }

    public void ShowLevelFail()
    {
        gameManager.isGamePause = true;
        levelFailScreen.SetActive(true);
    }
}
