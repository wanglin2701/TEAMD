using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Strikes : MonoBehaviour
{
    
    public GameObject[] strikeIcons; // Strike UI Icons
    private int currentStrikes = 0;
    private UIManager uiManager;
    SoundManager soundManaager;


    void Start()
    {
        soundManaager = GameObject.Find("SFXManager").GetComponent<SoundManager>();
        uiManager = FindObjectOfType<UIManager>();
    }

    public void AddStrike()
    {
        if (currentStrikes < strikeIcons.Length)
        {
            soundManaager.PlaySound("Wrong");
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
        soundManaager.PlaySound("Gameover");
        uiManager.ShowLevelFail();
        Time.timeScale = 0f; // Stop game
    }
}
