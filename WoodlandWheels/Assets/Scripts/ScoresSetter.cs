using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoresSetter : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    //private float score = 0;
    public const string HIGH_SCORE = "High Score";
    public float finalScore = 0 ;

    private bool powerUpActive = false;
    private float powerUpTimer = 0f;
    private const float powerUpDuration = 5f;
    private const int scoreMultiplier = 4;

    void Update()
    {
         if (powerUpActive)
         {
            powerUpTimer += Time.deltaTime;
            if (powerUpTimer >= powerUpDuration)
            {
                powerUpActive = false;
                powerUpTimer = 0f;
            }
         }

    updateScore(1);
    scoreText.text = "Score: " + Mathf.FloorToInt(finalScore).ToString();
    }

    private void OnDestroy()
    {
        int highScore = PlayerPrefs.GetInt(HIGH_SCORE, 0);
        if (finalScore > highScore)
        {
            PlayerPrefs.SetInt(HIGH_SCORE, Mathf.FloorToInt(finalScore));
        }
    }

    public void updateScore(int score)
{
    finalScore += Time.deltaTime * score * (powerUpActive ? scoreMultiplier : 1);
}

public void ActivatePowerUp()
{
    powerUpActive = true;
}


public float GetScore()
{
    scoreText.text = "Score: " + Mathf.FloorToInt(finalScore).ToString();
    return finalScore;
}
public void ResetScore()
{
    finalScore = 0;
    Debug.Log("Score reset");
    //scoreText.text = "Score: " + Mathf.FloorToInt(finalScore).ToString();
}
}