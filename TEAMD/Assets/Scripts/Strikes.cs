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
    public GameObject redOverlay;

    void Start()
    {
        soundManaager = GameObject.Find("SFXManager").GetComponent<SoundManager>();
        uiManager = FindObjectOfType<UIManager>();
    }

    public void AddStrike()
    {
        if (currentStrikes < strikeIcons.Length)
        {
            //soundManaager.PlaySound("Wrong");
            strikeIcons[currentStrikes].SetActive(true);
            currentStrikes++;

            if (redOverlay != null)
            {
                StartCoroutine(FlashRed(currentStrikes >= strikeIcons.Length));
            }

            if (currentStrikes >= strikeIcons.Length)
            {
                GameOver();
            }
        }
    }

    IEnumerator FlashRed(bool keepOnScreen = false)
    {
        redOverlay.SetActive(true);

        if (!keepOnScreen)
        {
            yield return new WaitForSeconds(0.3f);
            redOverlay.SetActive(false);
        }
    }

    void GameOver()
    {
        soundManaager.PlaySound("Gameover");
        uiManager.ShowLevelFail();
        Time.timeScale = 0f; // Stop game
    }
}
