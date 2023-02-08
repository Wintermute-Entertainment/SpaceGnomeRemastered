using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Coins : MonoBehaviour
{

    public int hP;
    public float time;
    public int points;

    public bool gotAPoint;
    public bool gotAHP;
    public bool gotATime;

    [SerializeField] float timeBonus;

    [SerializeField] TMP_Text pointsText;
    [SerializeField] TMP_Text hPText;
    

    private void OnCollisionEnter(Collision collision)
    {
        
            if (collision.gameObject.CompareTag("Coin1"))
            {
                points += 1;
                Debug.Log("Got a point.");
                gotAPoint = true;
                pointsText.text = points.ToString();
                Destroy(collision.gameObject);
            
            }
            if (collision.gameObject.CompareTag("Coin2"))
            {
                hP += 1;
                Debug.Log("Got a HP.");
                gotAHP= true;
                hPText.text = hP.ToString();
                Destroy(collision.gameObject);

            }
            if (collision.gameObject.CompareTag("Coin3"))
            {
                time += 1;
                Debug.Log("Got a time.");
                gotATime= true;
                Toolbox.instance.m_timer.startTime += timeBonus;
                Destroy(collision.gameObject);
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
        if (hP <= 0) { Toolbox.instance.m_playerManager.GameOver(); }
        time = Toolbox.instance.m_timer.timerTime;
        hPText.text = hP.ToString();
        pointsText.text = points.ToString();
    }
}

