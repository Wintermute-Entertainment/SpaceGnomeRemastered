using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

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

    public int danceBonus; //Total Dance Combos executed (1-4), determines coin pickup bonus.

    [Header("Text Objects")]
    [SerializeField] TMP_Text pointsText;
    [SerializeField] TMP_Text hPText;
    [SerializeField] TMP_Text totalTimeCoinsCollectedText;
    [SerializeField] TMP_Text totalHealthCoinsCollectedText;
    [SerializeField] TMP_Text totalPointsCoinsCollectedText;

    [Header("Statistics")]
    public float roundTotalTime;
    public int totalTimeCoinsCollected;
    public int totalHealthCoinsCollected;
    public int totalPointsCoinsCollected;
    

    public GnomeMovement m_gnomeMovement;

    bool combo1;
    bool combo2;
    bool combo3;
    bool combo4;
    bool combo5;

    public IEnumerator waitOneSecond;

    private void Awake()
    {
        points= 0;
        danceBonus = 0;

        waitOneSecond = Toolbox.instance.m_gnomeMovement.WaitOneSecond(1.5f);
        StartCoroutine(waitOneSecond);
    }

    private void OnCollisionEnter(Collision collision)
    {

        //POINTS (GOLD) COIN
            if (collision.gameObject.CompareTag("Coin1"))
            {
                if (danceBonus >= 1)
                {
                int pointsGained = danceBonus;
                points += danceBonus;
                

                if (pointsGained == 1)
                {
                    Debug.Log(pointsGained + "points gained after 1 combo point coin pickup.");
                   
                    Toolbox.instance.m_uIManager.plus1.gameObject.SetActive(true);
                    Toolbox.instance.m_uIManager.pointsUIText.gameObject.SetActive(true);

                    DOTweenModuleSprite.DOFade(Toolbox.instance.m_uIManager.plus1, 1, 0);
                    DOTweenModuleSprite.DOFade(Toolbox.instance.m_uIManager.pointsUIText, 1, 0);

                    DOTweenModuleSprite.DOFade(Toolbox.instance.m_uIManager.plus1, 0, 2.5f);
                    DOTweenModuleSprite.DOFade(Toolbox.instance.m_uIManager.pointsUIText, 0, 2.5f);
                    StartCoroutine(waitOneSecond);
                    
                    Debug.Log("Waiting 1.5 seconds...");
                 

                   Toolbox.instance.m_uIManager.plus1.gameObject.SetActive(false);
                   Toolbox.instance.m_uIManager.pointsUIText.gameObject.SetActive(false);
                    
                }
                if (pointsGained == 2)
                {
                   
                    Toolbox.instance.m_uIManager.plus2.gameObject.SetActive(true);
                    Toolbox.instance.m_uIManager.pointsUIText.gameObject.SetActive(true);

                    DOTweenModuleSprite.DOFade(Toolbox.instance.m_uIManager.plus2, 1, 0);
                    DOTweenModuleSprite.DOFade(Toolbox.instance.m_uIManager.pointsUIText, 1, 0);

                    DOTweenModuleSprite.DOFade(Toolbox.instance.m_uIManager.plus2, 0, 2.5f);
                    DOTweenModuleSprite.DOFade(Toolbox.instance.m_uIManager.pointsUIText, 0, 2.5f);

                    StartCoroutine(waitOneSecond);
                    Debug.Log("Waiting 1.5 seconds...");
                    //StopCoroutine(waitOneSecond);

                    Toolbox.instance.m_uIManager.plus2.gameObject.SetActive(false);
                    Toolbox.instance.m_uIManager.pointsUIText.gameObject.SetActive(false);
                    
                }
                if (pointsGained == 3)
                {
                    Toolbox.instance.m_uIManager.plus3.gameObject.SetActive(true);
                    Toolbox.instance.m_uIManager.pointsUIText.gameObject.SetActive(true);

                    DOTweenModuleSprite.DOFade(Toolbox.instance.m_uIManager.plus3, 1, 0);
                    DOTweenModuleSprite.DOFade(Toolbox.instance.m_uIManager.pointsUIText, 1, 0);

                    DOTweenModuleSprite.DOFade(Toolbox.instance.m_uIManager.plus3, 0, 2.5f);
                    DOTweenModuleSprite.DOFade(Toolbox.instance.m_uIManager.pointsUIText, 0, 2.5f);

                    StartCoroutine(waitOneSecond);
                    Debug.Log("Waiting 1.5 seconds...");
                    //StopCoroutine(waitOneSecond);

                    Toolbox.instance.m_uIManager.plus3.gameObject.SetActive(false);
                    Toolbox.instance.m_uIManager.pointsUIText.gameObject.SetActive(false);

                }
                if ( pointsGained== 4)
                {
                    Toolbox.instance.m_uIManager.plus4.gameObject.SetActive(true);
                    Toolbox.instance.m_uIManager.pointsUIText.gameObject.SetActive(true);

                    DOTweenModuleSprite.DOFade(Toolbox.instance.m_uIManager.plus4, 1, 0);
                    DOTweenModuleSprite.DOFade(Toolbox.instance.m_uIManager.pointsUIText, 1, 0);

                    DOTweenModuleSprite.DOFade(Toolbox.instance.m_uIManager.plus4, 0, 2.5f);
                    DOTweenModuleSprite.DOFade(Toolbox.instance.m_uIManager.pointsUIText, 0, 2.5f);

                    StartCoroutine(waitOneSecond);
                    Debug.Log("Waiting 1.5 seconds...");
                    //StopCoroutine(waitOneSecond);

                    Toolbox.instance.m_uIManager.plus4.gameObject.SetActive(false);
                    Toolbox.instance.m_uIManager.pointsUIText.gameObject.SetActive(false);

                }
                if (pointsGained== 5)
                {
                 
                    Toolbox.instance.m_uIManager.plus5.gameObject.SetActive(true);
                    Toolbox.instance.m_uIManager.pointsUIText.gameObject.SetActive(true);

                    DOTweenModuleSprite.DOFade(Toolbox.instance.m_uIManager.plus5, 1, 0);
                    DOTweenModuleSprite.DOFade(Toolbox.instance.m_uIManager.pointsUIText, 1, 0);

                    DOTweenModuleSprite.DOFade(Toolbox.instance.m_uIManager.plus5, 0, 2.5f);
                    DOTweenModuleSprite.DOFade(Toolbox.instance.m_uIManager.pointsUIText, 0, 2.5f);

                    StartCoroutine(waitOneSecond);
                    Debug.Log("Waiting 1.5 seconds...");
                    //StopCoroutine(waitOneSecond);

                    Toolbox.instance.m_uIManager.plus5.gameObject.SetActive(false);
                    Toolbox.instance.m_uIManager.pointsUIText.gameObject.SetActive(false);

                }

                Debug.Log("Got " + pointsGained + " points. Dance bonus currently " + danceBonus + ".");
                }
                else 
                {
                    points += 1;
                    Debug.Log("Gained a single point (Dance bonus is " + danceBonus + ".");
                }

            
            totalPointsCoinsCollected += 1;
            Debug.Log("Got a point.");
            gotAPoint = true;
            pointsText.text = points.ToString();
            Destroy(collision.gameObject);

            //FX
            Toolbox.instance.m_audio.pointsCoin.Play();

            //Reset dance bonus and combo count after collecting a coin.
            danceBonus = 0;
            m_gnomeMovement.comboCount = 0;
            combo1 = false; combo2 = false; combo3 = false; combo4 = false; combo5 = false;

        }
       
           
        
        //HEALTH (SILVER) COIN
        if (collision.gameObject.CompareTag("Coin2"))
            {
            

            if (danceBonus >= 1)
            {
                int hpGained = danceBonus;
                hP += danceBonus;
               

               
                if (hpGained == 1)
                {
                    Debug.Log("HP Gained on 1 combo bonus hp coin pickup: " + hpGained+ ".");
                    Toolbox.instance.m_uIManager.plus1.gameObject.SetActive(true);
                    Toolbox.instance.m_uIManager.healthUIText.gameObject.SetActive(true);
                    StartCoroutine(waitOneSecond);
                    Debug.Log("Waiting 1.5 seconds...");
                    //StopCoroutine(waitOneSecond);
                    Toolbox.instance.m_uIManager.plus1.gameObject.SetActive(false);
                    Toolbox.instance.m_uIManager.healthUIText.gameObject.SetActive(false);
                }
                else if (hpGained == 2)
                {
                    Toolbox.instance.m_uIManager.plus2.gameObject.SetActive(true);
                    Toolbox.instance.m_uIManager.healthUIText.gameObject.SetActive(true);
                    StartCoroutine(waitOneSecond);
                    Debug.Log("Waiting 1.5 seconds...");
                    //StopCoroutine(waitOneSecond);
                    Toolbox.instance.m_uIManager.plus2.gameObject.SetActive(false);
                    Toolbox.instance.m_uIManager.healthUIText.gameObject.SetActive(false);

                }
                else if (hpGained == 3)
                {
                   
                    Toolbox.instance.m_uIManager.plus3.gameObject.SetActive(true);
                    Toolbox.instance.m_uIManager.healthUIText.gameObject.SetActive(true);
                    StartCoroutine(waitOneSecond);
                    Debug.Log("Waiting 1.5 seconds...");
                    //StopCoroutine(waitOneSecond);
                    Toolbox.instance.m_uIManager.plus3.gameObject.SetActive(false);
                    Toolbox.instance.m_uIManager.healthUIText.gameObject.SetActive(false);

                }
                else if (hpGained == 4)
                {
                    Toolbox.instance.m_uIManager.plus4.gameObject.SetActive(true);
                    Toolbox.instance.m_uIManager.healthUIText.gameObject.SetActive(true);
                    StartCoroutine(waitOneSecond);
                    Debug.Log("Waiting 1.5 seconds...");
                    //StopCoroutine(waitOneSecond);
                    Toolbox.instance.m_uIManager.plus4.gameObject.SetActive(false);
                    Toolbox.instance.m_uIManager.healthUIText.gameObject.SetActive(false);

                }
                else if (hpGained == 5)
                {
                    Toolbox.instance.m_uIManager.plus5.gameObject.SetActive(true);
                    Toolbox.instance.m_uIManager.healthUIText.gameObject.SetActive(true);
                    StartCoroutine(waitOneSecond);
                    Debug.Log("Waiting 1.5 seconds...");
                   // StopCoroutine(waitOneSecond);
                    Toolbox.instance.m_uIManager.plus5.gameObject.SetActive(false);
                    Toolbox.instance.m_uIManager.healthUIText.gameObject.SetActive(false);

                }
                Debug.Log("Gained " + hpGained + " HP. Dance bonus currently " + danceBonus + ".");
            }
            
            else 
            { 
                hP += 1;
                Debug.Log("Gained a single HP (Dance bonus is " + danceBonus + ".");
            }

            

            totalHealthCoinsCollected += 1;
                Debug.Log("Got a HP.");
                gotAHP= true;
                hPText.text = hP.ToString();
                Destroy(collision.gameObject);
                Toolbox.instance.m_audio.hPCoin.Play();
                

            //Reset coin pickup bonuses from dance combos after collecting a coin.
            danceBonus = 0;
            m_gnomeMovement.comboCount = 0;
            combo1 = false; combo2 = false; combo3 = false; combo4 = false; combo5 = false;
        }

        //TIME (BRONZE) COIN
            if (collision.gameObject.CompareTag("Coin3"))
            {
            
            Toolbox.instance.m_uIManager.timeUIText.gameObject.SetActive(true);

            StartCoroutine(waitOneSecond);
            Debug.Log("Waiting 1.5 seconds...");
            ///StopCoroutine(waitOneSecond);

            Toolbox.instance.m_uIManager.timeUIText.gameObject.SetActive(false);

            Debug.Log("Got a time.");
                gotATime= true;
            
                Toolbox.instance.m_audio.timeCoin.Play();
                Destroy(collision.gameObject);

            totalTimeCoinsCollected += 1;

            if (totalTimeCoinsCollected <= 25) { Toolbox.instance.m_timer.timerTime += timeBonus; }
            else if (totalTimeCoinsCollected > 25 && totalTimeCoinsCollected <= 50) { Toolbox.instance.m_timer.timerTime += timeBonus2; }
            else if (totalTimeCoinsCollected > 50 && totalTimeCoinsCollected <= 100) { Toolbox.instance.m_timer.timerTime += timeBonus3; }
           
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
       
        if (m_gnomeMovement.comboCount == 1)
        {
            combo1 = true;
            ApplyCombos();
        }
        else if (m_gnomeMovement.comboCount == 2)
        {
            combo2 = true;
            ApplyCombos();
        }
        else if (m_gnomeMovement.comboCount == 3)
        {
            combo3 = true;
            ApplyCombos();
        }
        else if (m_gnomeMovement.comboCount == 4)
        {
            combo4 = true;
            ApplyCombos();
        }
        else if (m_gnomeMovement.comboCount == 5)
        {
            combo5 = true;
            ApplyCombos();
        }
        else { return; }

     
    }
    void ApplyCombos()
    {
        if (combo1)
        {
            danceBonus = 1;
           ResetCombos();
        }
        if (combo2)
        {
            danceBonus = 2;
            ResetCombos();
        }
        if (combo3)
        {
            danceBonus = 3;
            ResetCombos();
        }
        if (combo4)
        {
            danceBonus = 4;
            ResetCombos();
        }
        if (combo5)
        {
            danceBonus = 5;
            ResetCombos();
        }
    }
    void ResetCombos()
    {
        combo1 = false;
        combo2 = false;
        combo3 = false;
        combo4 = false;
        combo5 = false;
    }
}

