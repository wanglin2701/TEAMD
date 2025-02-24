using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    
    public float levelTime = 181f; // Can change anytime, testing purpose I put 3 minutes in seconds
    public TextMeshProUGUI timerText;
    private bool isLevelOver = false;

    private UIManager uiManager;
    private Strikes strikesManager;


    void Start()
    {
        uiManager = FindObjectOfType<UIManager>();
        strikesManager = FindObjectOfType<Strikes>();
        StartCoroutine(LevelCountdown());
    }

    IEnumerator LevelCountdown()
    {
        while (levelTime > 0 && !isLevelOver)
        {
            yield return new WaitForSeconds(1f);
            levelTime--;
            UpdateTimerUI();
        }

        if (!isLevelOver)
        {
            EndLevel();
        }
    }

    void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(levelTime / 60);
        int seconds = Mathf.FloorToInt(levelTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void EndLevel()
    {
        isLevelOver = true;
        uiManager.ShowLevelComplete();
        Time.timeScale = 0f; // Stop game
    }

    public void AddStrike()
    {
        strikesManager.AddStrike();
    }
}
