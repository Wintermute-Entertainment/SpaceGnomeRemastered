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

    [Header("Time Coin Bonuses")]
    [SerializeField] float timeBonus; //Bonuses awarded for picking up time coin.
    [SerializeField] float timeBonus2;
    [SerializeField] float timeBonus3;

    [SerializeField] int danceBonus; //Total Dance Combos executed (1-4), determines coin pickup bonus.

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

    [Header("Coin Pick Up Bonus Sprite GOs")]
    // Reference to the plus1, plus2, plus3, plus4, and plus5 GameObjects in the scene
    public GameObject plus1; 
    public GameObject plus2; 
    public GameObject plus3; 
    public GameObject plus4; 
    public GameObject plus5; 

    // Reference to the UI elements for points, hP, and time coins
    public GameObject pointsCoinTextObject;
    public GameObject hPCoinTextObject;
    public GameObject timeCoinTextObject;
    private void Awake()
    {
        points = 0;
        danceBonus = 0;

        waitOneSecond = Toolbox.instance.m_gnomeMovement.WaitOneSecond(1f);
        //   StartCoroutine(waitOneSecond);
        plus1 = Toolbox.instance.m_uIManager.plus1.gameObject;
        plus2 = Toolbox.instance.m_uIManager.plus2.gameObject;
        plus3 = Toolbox.instance.m_uIManager.plus3.gameObject;
        plus4 = Toolbox.instance.m_uIManager.plus4.gameObject;
        plus5 = Toolbox.instance.m_uIManager.plus5.gameObject;

}
    private void Start()
    {
        // Deactivate all the plus GameObjects at the start
        plus1.SetActive(false);
        plus2.SetActive(false);
        plus3.SetActive(false);
        plus4.SetActive(false);
        plus5.SetActive(false);

        // Deactivate all the UI elements at the start
        pointsCoinTextObject.SetActive(false);
        hPCoinTextObject.SetActive(false);
        timeCoinTextObject.SetActive(false);
    }
    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collided object is a coin
        if (collision.gameObject.CompareTag("Coin1") ||
            collision.gameObject.CompareTag("Coin2") ||
            collision.gameObject.CompareTag("Coin3"))

        {
            // Increment the dance bonus by 1 and adjust if it's lower than 1 or greater than 5.
            danceBonus++;
            if (danceBonus < 1) { danceBonus = 1; }
            if (danceBonus > 5) { danceBonus = 5; }

            // Stop all existing tweens on the plus GameObjects
            //DOTween.Kill(plus1);
            //DOTween.Kill(plus2);
            //DOTween.Kill(plus3);
            //DOTween.Kill(plus4);
            //DOTween.Kill(plus5);

            // Activate the appropriate plus GameObject based on the dance bonus
            switch (danceBonus)
            {
                case 1:
                    plus1.SetActive(true);
                    plus1.transform.DOScale(Vector3.one, 1f).From().OnComplete(() => plus1.SetActive(false));
                    plus1.GetComponent<SpriteRenderer>().DOFade(0f, 0.5f).From();
                    break;
                case 2:
                    plus2.SetActive(true);
                    plus2.transform.DOScale(Vector3.one, 1f).From().OnComplete(() => plus2.SetActive(false));
                    plus2.GetComponent<SpriteRenderer>().DOFade(0f, 0.5f).From();
                    break;
                case 3:
                    plus3.SetActive(true);
                    plus3.transform.DOScale(Vector3.one, 1f).From().OnComplete(() => plus3.SetActive(false));
                    plus3.GetComponent<SpriteRenderer>().DOFade(0f, 0.5f).From();
                    break;
                case 4:
                    plus4.SetActive(true);
                    plus4.transform.DOScale(Vector3.one, 1f).From().OnComplete(() => plus4.SetActive(false));
                    plus4.GetComponent<SpriteRenderer>().DOFade(0f, 0.5f).From();
                    break;
                case 5:
                    plus5.SetActive(true);
                    plus5.transform.DOScale(Vector3.one, 1f).From().OnComplete(() => plus5.SetActive(false));
                    plus5.GetComponent<SpriteRenderer>().DOFade(0f, 0.5f).From();
                    break;
            }

            // Get the collided coin's tag
            string collidedCoinTag = collision.gameObject.tag;

            // Activate the corresponding UI element based on the collided coin's tag
            switch (collidedCoinTag)
            {
                case "Coin1":
                    pointsCoinTextObject.SetActive(true);
                    pointsCoinTextObject.transform.DOScale(Vector3.one, 0.5f).From().OnComplete(() => pointsCoinTextObject.SetActive(false));
                    pointsCoinTextObject.GetComponent<SpriteRenderer>().DOFade(0f, 0.5f).From();
                    break;
                case "Coin2":
                    hPCoinTextObject.SetActive(true);
                    hPCoinTextObject.transform.DOScale(Vector3.one, 0.5f).From().OnComplete(() => hPCoinTextObject.SetActive(false));
                    hPCoinTextObject.GetComponent<SpriteRenderer>().DOFade(0f, 0.5f).From();
                    break;
                case "Coin3":
                    timeCoinTextObject.SetActive(true);
                    timeCoinTextObject.transform.DOScale(Vector3.one, 0.5f).From().OnComplete(() => timeCoinTextObject.SetActive(false));
                    timeCoinTextObject.GetComponent<SpriteRenderer>().DOFade(0f, 0.5f).From();
                    break;
            }
            // Deactivate the previous plus GameObject (if any)
            int previousBonus = danceBonus - 1;
            switch (previousBonus)
            {
                case 1:
                    plus1.SetActive(false);
                    break;
                case 2:
                    plus2.SetActive(false);
                    break;
                case 3:
                    plus3.SetActive(false);
                    break;
                case 4:
                    plus4.SetActive(false);
                    break;
                case 5:
                    plus5.SetActive(false);
                    break;
            }
        }
        //POINTS (GOLD) COIN
        if (collision.gameObject.CompareTag("Coin1"))
        {

            //START PASTED CODE

            int pointsGained = Mathf.Clamp(danceBonus, 1, 5);
            points += pointsGained;

            SpriteRenderer plusText = null;
            SpriteRenderer coinTypeTextSprite = null;

            switch (pointsGained)
            {
                case 1:
                    plusText = Toolbox.instance.m_uIManager.plus1;
                    break;
                case 2:
                    plusText = Toolbox.instance.m_uIManager.plus2;
                    break;
                case 3:
                    plusText = Toolbox.instance.m_uIManager.plus3;
                    break;
                case 4:
                    plusText = Toolbox.instance.m_uIManager.plus4;
                    break;
                case 5:
                    plusText = Toolbox.instance.m_uIManager.plus5;
                    break;
            }

            if (plusText != null)
            {
                plusText.gameObject.SetActive(true);
                plusText.DOFade(0f, 2.5f).SetDelay(1.5f).OnComplete(() =>
                {
                    plusText.gameObject.SetActive(false);
                });
            }
            if (coinTypeTextSprite != null)
            {
                coinTypeTextSprite.gameObject.SetActive(true);
                coinTypeTextSprite.DOFade(0f, 2.5f).SetDelay(1.5f).OnComplete(() =>
                {
                    coinTypeTextSprite.gameObject.SetActive(false);
                });
            }

            //END PASTED CODE
            if (danceBonus >= 1)
            {
                pointsGained = danceBonus;
                points += danceBonus;
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

        if (collision.gameObject.CompareTag("Coin1")) {
            Destroy(collision.gameObject);

            //FX
            Toolbox.instance.m_audio.pointsCoin.Play();
        }


        //Reset dance bonus and combo count after collecting a coin.
        danceBonus = 0;
        m_gnomeMovement.comboCount = 0;
        combo1 = false; combo2 = false; combo3 = false; combo4 = false; combo5 = false;





        //HEALTH (SILVER) COIN
        if (collision.gameObject.CompareTag("Coin2"))
        {
            //START PASTED CODE

            int pointsGained = Mathf.Clamp(danceBonus, 1, 5);
            points += pointsGained;

            SpriteRenderer plusText = null;
            SpriteRenderer coinTypeTextSprite = null;

            switch (pointsGained)
            {
                case 1:
                    plusText = Toolbox.instance.m_uIManager.plus1;
                    break;
                case 2:
                    plusText = Toolbox.instance.m_uIManager.plus2;
                    break;
                case 3:
                    plusText = Toolbox.instance.m_uIManager.plus3;
                    break;
                case 4:
                    plusText = Toolbox.instance.m_uIManager.plus4;
                    break;
                case 5:
                    plusText = Toolbox.instance.m_uIManager.plus5;
                    break;
            }

            if (plusText != null)
            {
                plusText.gameObject.SetActive(true);
                plusText.DOFade(1f, 2.5f).SetDelay(1.5f).OnComplete(() =>
                {
                    plusText.gameObject.SetActive(false);
                });
            }
            if (coinTypeTextSprite != null)
            {
                coinTypeTextSprite.gameObject.SetActive(true);
                coinTypeTextSprite.DOFade(0f, 2.5f).SetDelay(1.5f).OnComplete(() =>
                {
                    coinTypeTextSprite.gameObject.SetActive(false);
                });
            }

            //END PASTED CODE

            if (danceBonus >= 1)
            {
                int hpGained = danceBonus;
                hP += danceBonus;


                Debug.Log("Gained " + hP + " HP. Dance bonus currently " + danceBonus + ".");
            }



            else
            {
                hP += 1;
                Debug.Log("Gained a single HP (Dance bonus is " + danceBonus + ".");
            }



            totalHealthCoinsCollected += 1;
            Debug.Log("Got a HP.");
            gotAHP = true;
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
                //START PASTED CODE

                int pointsGained = Mathf.Clamp(danceBonus, 1, 5);
                points += pointsGained;

                SpriteRenderer plusText = null;
                SpriteRenderer coinTypeTextSprite = null;

                switch (pointsGained)
                {
                    case 1:
                        plusText = Toolbox.instance.m_uIManager.plus1;
                        break;
                    case 2:
                        plusText = Toolbox.instance.m_uIManager.plus2;
                        break;
                    case 3:
                        plusText = Toolbox.instance.m_uIManager.plus3;
                        break;
                    case 4:
                        plusText = Toolbox.instance.m_uIManager.plus4;
                        break;
                    case 5:
                        plusText = Toolbox.instance.m_uIManager.plus5;
                        break;
                }

                if (plusText != null)
                {
                    plusText.gameObject.SetActive(true);
                    plusText.DOFade(0f, 2.5f).SetDelay(1.5f).OnComplete(() =>
                    {
                        plusText.gameObject.SetActive(false);
                    });
                }
                if (coinTypeTextSprite != null)
                {
                    coinTypeTextSprite.gameObject.SetActive(true);
                    coinTypeTextSprite.DOFade(0f, 2.5f).SetDelay(1.5f).OnComplete(() =>
                    {
                        coinTypeTextSprite.gameObject.SetActive(false);
                    });
                }

                //END PASTED CODE
                //Toolbox.instance.m_uIManager.timeUIText.gameObject.SetActive(true);

                //StartCoroutine(waitOneSecond);
                //Debug.Log("Waiting 1.5 seconds...");
                /////StopCoroutine(waitOneSecond);

                //Toolbox.instance.m_uIManager.timeUIText.gameObject.SetActive(false);

                Debug.Log("Got a time.");
                gotATime = true;

                Toolbox.instance.m_audio.timeCoin.Play();
                Destroy(collision.gameObject);

                totalTimeCoinsCollected += 1;

                if (totalTimeCoinsCollected <= 25) { Toolbox.instance.m_timer.timerTime += timeBonus; }
                else if (totalTimeCoinsCollected > 25 && totalTimeCoinsCollected <= 50) { Toolbox.instance.m_timer.timerTime += timeBonus2; }
                else if (totalTimeCoinsCollected > 50 && totalTimeCoinsCollected <= 100) { Toolbox.instance.m_timer.timerTime += timeBonus3; }

                //Reset coin pickup bonuses from dance combos after collecting a coin.
                danceBonus = 0;
                m_gnomeMovement.comboCount = 0;
                combo1 = false; combo2 = false; combo3 = false; combo4 = false; combo5 = false;
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

