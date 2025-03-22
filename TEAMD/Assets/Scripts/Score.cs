using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    SoundManager soundManaager;
    public TextMeshProUGUI scoreText;
    private int score = 0;

    public void Start()
    {
        soundManaager = GameObject.Find("SFXManager").GetComponent<SoundManager>();
    }

    public void AddPoints(int points)
    {
        soundManaager.PlaySound("Correct");
        score += points;
        scoreText.text = score.ToString();
    }
}
