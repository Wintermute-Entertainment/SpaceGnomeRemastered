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
            Toolbox.instance.m_audio.tookDamage.Play();
        }
    }
   
}
