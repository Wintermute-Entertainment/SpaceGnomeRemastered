using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectKiller : MonoBehaviour
{
    [SerializeField] GameObject playerPrefab;
    [SerializeField] Transform playerSpawnTransform;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //collision.gameObject.SetActive(false);
            collision.gameObject.transform.position = playerSpawnTransform.transform.position;
           // playerPrefab.SetActive(true);
          //  Destroy(collision.gameObject);
        }
        else { Destroy(collision.gameObject); }
    }
}
