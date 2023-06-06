using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Earth : MonoBehaviour
{
    [SerializeField] GameObject player;
   
    public GnomeMovement m_gnomeMovement;

    [SerializeField] float earthRotationAngle;
   
    private void Update()
    {

        transform.Rotate(0, earthRotationAngle, 0, Space.World);

        if (m_gnomeMovement.isFallingIdle && !m_gnomeMovement.isJumping)
        {
           
            transform.Translate(m_gnomeMovement.fallSpeed * m_gnomeMovement.gravity * Time.deltaTime * Vector3.down);
        }
        else if (m_gnomeMovement.isJumping)
        {
            transform.Translate(m_gnomeMovement.fallSpeed * m_gnomeMovement.gravity * Time.deltaTime * Vector3.up);
        }
      //  transform.Rotate(360, 360, 360);
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.Translate(Time.deltaTime * Toolbox.instance.m_gnomeMovement.jumpSpeed *
            Toolbox.instance.m_gnomeMovement.jumpHeight.y * Time.deltaTime * Toolbox.instance.m_gnomeMovement.playerSpeed * Vector3.up);
            
            Debug.Log("Player bounced off planet."); }
        
    }

   
}
