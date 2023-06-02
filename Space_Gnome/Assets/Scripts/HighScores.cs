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

        currentHighScore = PlayerPrefs.GetFloat("HighScore",0);
        newScore = 0;
    }


    private void Update()
    {
        newScore = Toolbox.instance.m_playerManager.score;
        
        //if (newScore > (PlayerPrefs.GetFloat("HighScore")))
        //{
        //    previousHighScore = PlayerPrefs.GetFloat("HighScore");
        //    PlayerPrefs.SetFloat("PreviousHighScore", previousHighScore);
        //    PlayerPrefs.SetFloat("HighScore", newScore);
        //}
        
    }

}
