using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Earth : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject earth;
    public GnomeMovement m_gnomeMovement;
   // [SerializeField] float earthAdjustmentThreshold;
   // [SerializeField] Vector3 earthOffset;
   // [SerializeField] bool hasMoved;
    private void FixedUpdate()
    {
        if (m_gnomeMovement.isFallingIdle && !m_gnomeMovement.isJumping)
        {
            //if (player.transform.position.y + earth.transform.position.y < earthAdjustmentThreshold)
            //{
            //    earth.transform.position -= earthOffset;
            // //  hasMoved = true;
            //}
            //// else { hasMoved = false; }
            //else if (player.transform.position.y + earth.transform.position.y > earthAdjustmentThreshold)
            //{
            //    earth.transform.position = player.transform.position - earthOffset;
            //   // hasMoved = false;
            //}
            earth.transform.Translate(m_gnomeMovement.fallSpeed * m_gnomeMovement.gravity * Time.deltaTime * Vector3.down);
        }
        else if (m_gnomeMovement.isJumping)
        {
            earth.transform.Translate(m_gnomeMovement.fallSpeed * m_gnomeMovement.gravity * Time.deltaTime * Vector3.up);
        }
    }
}
