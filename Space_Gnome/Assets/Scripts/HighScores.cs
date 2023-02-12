using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScores : MonoBehaviour
{
    public float newScore;
    public float currentHighScore;
    private void Awake()
    {
        currentHighScore = PlayerPrefs.GetFloat("HighScore");
    }


    private void Update()
    {
        newScore = Toolbox.instance.m_playerManager.score;
        if (newScore > (PlayerPrefs.GetFloat("HighScore",0)))
        {
            PlayerPrefs.SetFloat("HighScore", newScore);
        }
        Toolbox.instance.m_uIManager.highScoreText.text = PlayerPrefs.GetFloat("HighScore").ToString("f0");
    }

}
