using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public Transform rotationCenter;
    public GnomeMovement m_gnomeMovement;
    [SerializeField] float rotationIncrements;
    [SerializeField] GameObject earth;
    [SerializeField] private float asteroidFallSpeed;

    [System.Obsolete]
    private void Update()
    {
        transform.RotateAround(earth.transform.position, rotationCenter.transform.position * m_gnomeMovement.fallSpeed * m_gnomeMovement.gravity * Time.deltaTime, rotationIncrements);
        transform.Translate(asteroidFallSpeed * m_gnomeMovement.fallSpeed * m_gnomeMovement.gravity * Time.deltaTime * Vector3.down);
    }
}
