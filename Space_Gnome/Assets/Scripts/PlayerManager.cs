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
    public TMP_Text boostText;
    [SerializeField] float boostCap;

    private void Awake()
    {
        boost = defaultBoost;
    }
    public void GameOver()
    {   Toolbox.instance.m_uIManager.finalScoreText.text = score.ToString("f0");
        Toolbox.instance.m_uIManager.gameOverPanel.SetActive(true);

        if (Toolbox.instance.m_highScores.currentHighScore > float.Parse(Toolbox.instance.m_uIManager.previousHighScoreText.text))
        {
            Toolbox.instance.m_highScores.currentHighScore = score;
            PlayerPrefs.SetFloat("HighScore", score);
        }
        player.SetActive(false);
        //Toolbox.instance.m_uIManager.finalScoreText.text = score.ToString("f0");
        Toolbox.instance.m_highScores.newScore = score;
        Toolbox.instance.m_uIManager.highScoreText.text = PlayerPrefs.GetFloat("HighScore", 0).ToString("f0");
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
