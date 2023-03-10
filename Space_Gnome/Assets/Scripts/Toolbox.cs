using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toolbox : MonoBehaviour
{
    public PlayerManager m_playerManager;
    public GnomeMovement m_gnomeMovement;
    public Coins m_coins;
    public Asteroids m_asteroids;
    public Timer m_timer;
    public FallDamage m_fallDamage;
    public Audio m_audio;
    public UI_Manager m_uIManager;
    public HighScores m_highScores;



    //START SINGLETON
    public static Toolbox instance;
    public static Toolbox Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<Toolbox>();
                if (instance == null)
                {
                    GameObject singleton = new GameObject();
                    singleton.AddComponent<Toolbox>();
                    singleton.name = "(Singleton) Toolbox";
                }
            }
            return instance;
        }
    }
    //END Singleton
    private void Awake()
    {
        instance = this;
    }
}
