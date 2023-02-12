using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotations : MonoBehaviour
{
    public GameObject player;
    [SerializeField] float rotationIncrements;
   
    public GameObject earth;


    public GnomeMovement m_gnomeMovememnt;

    [System.Obsolete]
    private void FixedUpdate()
    {



        if (m_gnomeMovememnt.isFallingIdle)
        {

            player.transform.RotateAround(earth.transform.position * Time.deltaTime, rotationIncrements);
        }

    }
}
