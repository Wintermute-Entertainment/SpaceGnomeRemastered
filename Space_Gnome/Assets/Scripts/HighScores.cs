using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScores : MonoBehaviour
{
    public float newScore;
    public float currentHighScore;
    public float previousHighScore;
    private void Awake()
    {
        //currentHighScore = PlayerPrefs.GetFloat("HighScore");
      
    }


    private void Update()
    {
        
        if (newScore > (PlayerPrefs.GetFloat("HighScore")))
        {
            previousHighScore = PlayerPrefs.GetFloat("HighScore");
            PlayerPrefs.SetFloat("PreviousHighScore", previousHighScore);
            PlayerPrefs.SetFloat("HighScore", newScore);
        }
        Toolbox.instance.m_uIManager.highScoreText.text = PlayerPrefs.GetFloat("HighScore").ToString("f0");
        Toolbox.instance.m_uIManager.previousHighScoreText.text = PlayerPrefs.GetFloat("PreviousHighScore").ToString("f0");
    }

}
