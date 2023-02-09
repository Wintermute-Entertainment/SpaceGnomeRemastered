using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDamage : MonoBehaviour
{


    public float height;
    public float landingHeight;
    float fallDistance;


    int fallDamage;
    [SerializeField] int fallDamageThreshHoldMinimum;
    [SerializeField] int fallDamageThreshHold1;
    [SerializeField] int fallDamageThreshHold2;
    [SerializeField] int fallDamageThreshHold3;
    [SerializeField] int fallDamageThreshHold4;
    

    [SerializeField] int fallDamage1;
    [SerializeField] int fallDamage2;
    [SerializeField] int fallDamage3;
    [SerializeField] int fallDamage4;
    [SerializeField] int fallDamage5;

    [SerializeField] int boostPenalty1;
    [SerializeField] int boostPenalty2;
    [SerializeField] int boostPenalty3;
    [SerializeField] int boostPenalty4;
    [SerializeField] int boostPenalty5;

    private void OnCollisionEnter(Collision collision)
    {
        landingHeight = gameObject.transform.position.y;
        if (collision.gameObject.CompareTag("Floor"))
        {
           
            fallDistance = height - landingHeight;
            Debug.Log("Fall distance is " + fallDistance + ".");
            Debug.Log("Landing height is " + landingHeight + " when collidied with 'Floor'.");

            FallDamageHPDrain();
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor") )
        {
            height = transform.position.y; //Set height based on Y position.
            Debug.Log("Height on collision exit is " + height);
        }
       
    }
    private void Update()
    {
        if (Toolbox.instance.m_gnomeMovement.isJumping) { height = transform.position.y; }
    }

    public void FallDamageHPDrain()
    {
           

            if (fallDistance < fallDamageThreshHoldMinimum)
            {
                fallDamage = 0;                             //Set fall damage based on fall distance.
                Debug.Log("Took 0 fall damage.");
            }

            else if (fallDistance > fallDamageThreshHoldMinimum && fallDistance < fallDamageThreshHold1)
            {
                fallDamage = fallDamage1; 
                Toolbox.instance.m_coins.hP -= fallDamage1;
                Toolbox.instance.m_playerManager.boost -= boostPenalty1;
                //Play sounds and PE and subtract boost penalty and points loss.
                Debug.Log("Took fallDamage1");

            }
            else if (fallDistance > fallDamageThreshHold1 && fallDistance < fallDamageThreshHold2)
            {
                fallDamage = fallDamage2;
                Toolbox.instance.m_coins.hP -= fallDamage2;
                Toolbox.instance.m_playerManager.boost -= boostPenalty2;
                Debug.Log("Took fallDamage2");

            }
            else if (fallDistance > fallDamageThreshHold2 && fallDistance < fallDamageThreshHold3)
            {
                fallDamage = fallDamage3;
                Toolbox.instance.m_coins.hP -= fallDamage3;
                Toolbox.instance.m_playerManager.boost -= boostPenalty3;
                Debug.Log("Took fallDamage3");

            }
            else if (fallDistance > fallDamageThreshHold3 && fallDistance < fallDamageThreshHold4)
            {
                fallDamage = fallDamage4;
                Toolbox.instance.m_coins.hP -= fallDamage4;
                Toolbox.instance.m_playerManager.boost -= boostPenalty4;
                Debug.Log("Took fallDamage4");

            }
            else if (fallDistance > fallDamageThreshHold4)
            {
                fallDamage = fallDamage5;
                Toolbox.instance.m_coins.hP -= fallDamage5;
                Toolbox.instance.m_playerManager.boost -= boostPenalty5;
                Debug.Log("Took fallDamage5");

            }
         
    }
    
}
