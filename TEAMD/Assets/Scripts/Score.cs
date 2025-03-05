using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    private int score = 0;

    public void AddPoints(int points)
    {
        score += points;
        scoreText.text = score.ToString();
    }
}
