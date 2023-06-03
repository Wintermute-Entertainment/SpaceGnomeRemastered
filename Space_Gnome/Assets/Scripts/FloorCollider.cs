using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorCollider : MonoBehaviour
{
    public GameObject player;
    public GnomeMovement m_gnomeMovement;
    public bool isStanding;

    [SerializeField] Rigidbody floorColliderRB;

 
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {

            isStanding = true;
            Debug.Log("Started standing...");
            m_gnomeMovement.ResetStates();
          // m_gnomeMovement.gravity = m_gnomeMovement.defaultGravity;
        }
        else if(!collision.gameObject.CompareTag("Floor")) { isStanding = false; }
    }
    public void OnCollisionExit(Collision collision)
    {

        if (collision.gameObject.CompareTag("Floor"))
        {
            if (m_gnomeMovement.player1Controls.Player.Jump.triggered /*&& Toolbox.instance.m_gnomeMovement.jumpingAllowed*/)
            {
                isStanding = false;
                Debug.Log("Started Jumping...");
                
                m_gnomeMovement.isJumping = true;
                m_gnomeMovement.isIdle = false;
                m_gnomeMovement.isFallingIdle = false;
                m_gnomeMovement.isWalking = false;
                m_gnomeMovement.isSprinting = false;
                m_gnomeMovement.isDancing = false;

            }
            else if (m_gnomeMovement.isJumping==false)
            {
                isStanding = false;
                Debug.Log("Started FallingIdle...");
                
                m_gnomeMovement.FallingIdle();
                m_gnomeMovement.isFallingIdle = true;
                m_gnomeMovement.isIdle = false;
                m_gnomeMovement.isJumping = false;
                m_gnomeMovement.isWalking = false;
                m_gnomeMovement.isSprinting = false;
                m_gnomeMovement.isDancing = false;
            }
            else { isStanding = false; }

        }



    }
    private void Update()
    {
        if (player.transform.position.y - transform.position.y >= 1)
        {
            transform.Translate(Vector3.up, Space.Self);
            Debug.Log("Updated Floor Collider Y position.");
        }




    }

    //private void FixedUpdate()
    //{
    //    Debug.Log("Updated Floor Collider Y velocity from " + floorColliderRB.velocity.y + " to zero in Fixed Update.");
    //    floorColliderRB.velocity = Vector3.zero; 
    //}

}
