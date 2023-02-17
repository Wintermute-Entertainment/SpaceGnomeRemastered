using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] GameObject player;
    
    [Header("Basic Variables")]
    public int hP;
    public float time;
    public int points;
    public float score;
    [Header("Boost Variables")]
    public float boost;
    [SerializeField] float defaultBoost;
    [SerializeField] float defaultScore;
    public TMP_Text boostText;
    [SerializeField] float boostCap;

    private void Awake()
    {
        defaultScore = 0;
        boost = defaultBoost;
        score = defaultScore;
    }
    public void GameOver()
    {   Toolbox.instance.m_uIManager.finalScoreText.text = score.ToString("f0");
        if (score > 1)
        {
            score = Toolbox.instance.m_highScores.newScore;
        }
        


        if (Toolbox.instance.m_highScores.newScore > PlayerPrefs.GetFloat("HighScore"))//float.Parse(Toolbox.instance.m_uIManager.previousHighScoreText.text))
            {
                Toolbox.instance.m_highScores.currentHighScore = score;
                PlayerPrefs.SetFloat("HighScore", score);
            }

        Toolbox.instance.m_uIManager.gameOverPanel.SetActive(true);
        Toolbox.instance.m_uIManager.highScoreText.text = PlayerPrefs.GetFloat("HighScore").ToString("f0");
        Debug.Log("Game over, man!");
        Debug.Log("Score was " + score + " at Game Over.");
       
    }
    private void Update()
    {
        hP = Toolbox.instance.m_coins.hP;
        time = Toolbox.instance.m_coins.time;
        points = Toolbox.instance.m_coins.points;

        boostText.text = boost.ToString();
        if (boost >= boostCap)
        {
            boost = boostCap;
        }
        if (boost<= 0) { boost= 0; }

        score = points + (hP / 2) * Time.time;
    }
}
