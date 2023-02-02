using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CamController : MonoBehaviour
{
    SGInput player1Controls;

    [SerializeField] GameObject mainCamera;
    [SerializeField] GameObject fallingCamera;
    [SerializeField] GameObject vCam1;
    [SerializeField] GameObject vCam2;

    public GnomeMovement m_gnomeMovement;
    public FloorCollider m_floorCollider;

    private void Awake()
    {
        player1Controls = new SGInput();
    }

    private void Update()
    {
        if (m_gnomeMovement.isFallingIdle)
        {
            if (mainCamera != null)
            {
                fallingCamera.SetActive(true);
                vCam2.SetActive(true);
                mainCamera.SetActive(false);
                vCam1.SetActive(false);
            }
           
        }
        else if (m_floorCollider.isStanding) 
        {
            if (fallingCamera != null)
            {
                mainCamera.SetActive(true);
                vCam1.SetActive(true);
                fallingCamera.SetActive(false);
                vCam2.SetActive(false);
            }
          
        }
    }

    void OnEnable()
    {
        player1Controls.Player.Enable();

    }
    void OnDisable()
    {
        player1Controls.Player.Disable();
    }
}
