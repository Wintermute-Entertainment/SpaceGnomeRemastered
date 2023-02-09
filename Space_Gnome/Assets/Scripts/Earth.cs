using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Earth : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject earth;
    [SerializeField] Transform earthResetTransform;
    public GnomeMovement m_gnomeMovement;
   // [SerializeField] float earthAdjustmentThreshold;
   // [SerializeField] Vector3 earthOffset;
   // [SerializeField] bool hasMoved;
    private void Update()
    {
        if (m_gnomeMovement.isFallingIdle && !m_gnomeMovement.isJumping)
        {
           
            earth.transform.Translate(m_gnomeMovement.fallSpeed * m_gnomeMovement.gravity * Time.deltaTime * Vector3.down);
        }
        else if (m_gnomeMovement.isJumping)
        {
            earth.transform.Translate(m_gnomeMovement.fallSpeed * m_gnomeMovement.gravity * Time.deltaTime * Vector3.up);
        }

        //if (transform.position.y >= player.transform.position.y - earthResetTransform.position.y) { transform.Translate(player.transform.position - earthResetTransform.position); }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player")) { collision.gameObject.transform.Translate(Time.deltaTime * Toolbox.instance.m_gnomeMovement.jumpSpeed * Toolbox.instance.m_gnomeMovement.jumpHeight.y * Time.deltaTime * Toolbox.instance.m_gnomeMovement.playerSpeed * Vector3.up); }
        
    }
}
