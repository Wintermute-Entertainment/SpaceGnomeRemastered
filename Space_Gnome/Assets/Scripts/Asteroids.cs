using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroids : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Asteroid"))
        {
            Toolbox.instance.m_coins.hP -= 1;
            Debug.Log("Hit an asteroid.");
        }
      

    }
    //private void Update()
    //{
    //    if (Toolbox.instance.m_playerManager.hP <= 0 || Toolbox.instance.m_playerManager.time <= 0) { Toolbox.instance.m_playerManager.GameOver(); }
    //}
}
