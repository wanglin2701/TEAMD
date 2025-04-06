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
    public GameObject sparkleEffectPrefab;

    public void Start()
    {
        soundManaager = GameObject.Find("SFXManager").GetComponent<SoundManager>();
    }

    public void AddPoints(int points)
    {
        soundManaager.PlaySound("Correct");
        score += points;
        scoreText.text = score.ToString();

        // Create sparkle effect
        if (sparkleEffectPrefab != null)
        {
            // Top center in Viewport space = (0.5, 1)
            Vector3 topCenterWorldPos = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.95f, 10f)); 
            GameObject sparkle = Instantiate(sparkleEffectPrefab, topCenterWorldPos, Quaternion.identity);
            sparkle.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f); // Optional size adjustment
            Destroy(sparkle, 2f);
        }
    }
}
