using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Coins : MonoBehaviour
{
    [Header("Main Coin Variables")]

    public int points;
    public int hP;
    public float time;
    public float totalTime;

    public bool gotAPoint;
    public bool gotAHP;
    public bool gotATime;

    [SerializeField] float timeBonus; //Bonuses awarded for picking up time coin.
    [SerializeField] float timeBonus2;
    [SerializeField] float timeBonus3;


    [Header("Text Objects")]
    [SerializeField] TMP_Text pointsText;
    [SerializeField] TMP_Text hPText;
    [SerializeField] TMP_Text totalTimeCoinsCollectedText;
    [SerializeField] TMP_Text totalHealthCoinsCollectedText;
    [SerializeField] TMP_Text totalPointsCoinsCollectedText;

    [Header("Statistics")]
    [SerializeField] int totalTimeCoinsCollected;
    [SerializeField] int totalHealthCoinsCollected;
    [SerializeField] int totalPointsCoinsCollected;

    private void Awake()
    {
        points= 0;

    }

    private void OnCollisionEnter(Collision collision)
    {
        
            if (collision.gameObject.CompareTag("Coin1"))
            {
                points += 1;
                totalPointsCoinsCollected+= 1;
                Debug.Log("Got a point.");
                gotAPoint = true;
                pointsText.text = points.ToString();
                Destroy(collision.gameObject);
                Toolbox.instance.m_audio.pointsCoin.Play();
            
            }
            if (collision.gameObject.CompareTag("Coin2"))
            {
                hP += 1;
                totalHealthCoinsCollected+= 1;
                Debug.Log("Got a HP.");
                gotAHP= true;
                hPText.text = hP.ToString();
                Destroy(collision.gameObject);
            Toolbox.instance.m_audio.hPCoin.Play();

        }
            if (collision.gameObject.CompareTag("Coin3"))
            {
          
                Debug.Log("Got a time.");
                gotATime= true;
            
                Toolbox.instance.m_audio.timeCoin.Play();
                Destroy(collision.gameObject);

            totalTimeCoinsCollected += 1;

            if (totalTimeCoinsCollected <= 25) { Toolbox.instance.m_timer.timerTime += timeBonus; }
            else if (totalTimeCoinsCollected > 25 && totalTimeCoinsCollected <= 50) { Toolbox.instance.m_timer.timerTime += timeBonus2; }
            else if (totalTimeCoinsCollected > 50 && totalTimeCoinsCollected <= 100) { Toolbox.instance.m_timer.timerTime += timeBonus3; }
            else { return; }
        }

    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Coin1"))
        {
            gotAPoint = false;
        }
        if (collision.gameObject.CompareTag("Coin2"))
        {
            gotAHP = false;
        }
        if (collision.gameObject.CompareTag("Coin3"))
        {
            gotATime = false;

        }
    }
    private void Update()
    {
        if (points <= 0) { points = 0; }
        time = Toolbox.instance.m_timer.startTime;
        hPText.text = hP.ToString();
        pointsText.text = points.ToString();
        if (hP <= 0) { Toolbox.instance.m_playerManager.GameOver(); }
       
    }
}

