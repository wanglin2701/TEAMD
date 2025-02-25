using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Strikes : MonoBehaviour
{
    
    public GameObject[] strikeIcons; // Strike UI Icons
    private int currentStrikes = 0;
    private UIManager uiManager;

    void Start()
    {
        uiManager = FindObjectOfType<UIManager>();
    }

    public void AddStrike()
    {
        if (currentStrikes < strikeIcons.Length)
        {
            strikeIcons[currentStrikes].SetActive(true);
            currentStrikes++;

            if (currentStrikes >= strikeIcons.Length)
            {
                GameOver();
            }
        }
    }

    void GameOver()
    {
        uiManager.ShowLevelFail();
        Time.timeScale = 0f; // Stop game
    }
}
