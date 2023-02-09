using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class PlayerManager : MonoBehaviour
{
    public int hP;
    public float time;
    public int points;

    public float boost;
    [SerializeField] float defaultBoost;
    public TMP_Text boostText;
    [SerializeField] float boostCap;


    private void Awake()
    {
        boost = defaultBoost;
    }
    public void GameOver()
    {
        Debug.Log("Game over, man!");
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
    }



}
