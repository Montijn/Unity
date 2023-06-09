using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreController : MonoBehaviour
{
    public int totalScore;
    public RoundTimer timer;
    public TMP_Text scoreText;

    // Start is called before the first frame update
    void Start()
    {
        totalScore = 0;
        scoreText.text = totalScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddScore(int score)
    {
        totalScore += score;
        scoreText.text = totalScore.ToString();
    }

    public void SubmitScore()
    {
        int highScore = PlayerPrefs.GetInt("highscore");

        int newScore = (totalScore + (int)timer.getCurrentTime()) * 2;
        if(highScore < newScore)
        {
            PlayerPrefs.SetInt("highscore", newScore);
        }
    }

    public string GetHighScore()
    {
        return PlayerPrefs.GetInt("highscore").ToString();
    }
}
