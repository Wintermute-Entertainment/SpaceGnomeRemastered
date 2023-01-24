using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
[System.Serializable]

public class Platforms : MonoBehaviour
{
    [SerializeField] GameObject player;

    [SerializeField] GameObject platformPrefab;

    [SerializeField] Transform platformObjectsParent;

    public GameObject[] spawnablePlatforms;

    GameObject clone;

   public SGInput player1Controls;

    [SerializeField] float platformSpawnDistance;

    private void Awake()
    {

        clone = null;
        player1Controls = new SGInput();

        player1Controls.Player.FirePlatform.performed += ctx => Fire();
    }

    public void Fire()
    {
        Instantiate(clone = platformPrefab, player.transform.position, player.transform.rotation, platformObjectsParent);
        clone.SetActive(true);
        Debug.Log("Spawned platform.");
        clone.transform.Translate(Vector3.down * platformSpawnDistance, Space.World);
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
