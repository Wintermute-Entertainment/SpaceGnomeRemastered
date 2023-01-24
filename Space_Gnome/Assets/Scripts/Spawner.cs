using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Game Objects")]
    public GameObject objectInstantiator;   //This game object assigned in Inspector.
    [SerializeField] GameObject playerPrefab;
    public GameObject[] objects;
    private GameObject spawnedObject;
    [SerializeField] GameObject launchplatform;

    [Header("Fire Speed")]
    [SerializeField] int fireSpeed;  //Speed at which instanstiated objects are fired at from firePoint.

    [SerializeField] Transform firePoint;
    [SerializeField] Transform playerSpawnTransform;
    [SerializeField] Transform spawnedObjectParentTransform;
    [SerializeField] Transform spawnedSpawnerParentTransform;

    [SerializeField] bool isColliding;
    [SerializeField] bool spawningAllowed;
    private bool isSpawned;

    [Header("Number of Objects Instantiated")]
    public int objectCount;                 //Current number of objects that have been instantiated.
    [Header("Max Objects Allowed")]
    public int maxObjectCount;              //Maximum number of objects that can be instantiated at once.
    [Header("Max Spawners Allowed")]
    [SerializeField] int maxSpawnerCount;

    [Header("Random Distance Range From Player")]       //Minimum and maximum distances from player at which objects can be instantiated for each axis.
    public float xDistanceFromPlayerMin;
    public float xDistanceFromPlayerMax;
    public float yDistanceFromPlayerMin;
    public float yDistanceFromPlayerMax;
    public float zDistanceFromPlayerMin;
    public float zDistanceFromPlayerMax;

    [SerializeField] float spawnerSpawnDistance;

    Vector3 randomPos;


    private void Awake()
    {
        isSpawned = false;
        spawningAllowed = true;
    }
    private void Update()
    {
        if (spawningAllowed) { if (isColliding == true) { SpawnObject(); } }
        if (objectCount <= maxObjectCount) { spawningAllowed = true; }
        else if (objectCount > maxObjectCount) { spawningAllowed = false; spawnedObject.SetActive(false); }

        randomPos.x = Random.Range(xDistanceFromPlayerMin, xDistanceFromPlayerMax);
        randomPos.y = Random.Range(yDistanceFromPlayerMin, yDistanceFromPlayerMax);
        randomPos.z = Random.Range(zDistanceFromPlayerMin, zDistanceFromPlayerMax);

        Vector3 position = new(randomPos.x, randomPos.y, randomPos.z);
        firePoint.transform.position = playerPrefab.transform.position + position; ;
        objectInstantiator = gameObject;
    }
    void SpawnObject()
    {

        //int i;
        objectCount += 1;
        Instantiate(spawnedObject = objects[Random.Range(0, objects.Length)], firePoint.position, firePoint.rotation, spawnedObjectParentTransform);
        // GameObject[] spawnedCoins = new GameObject[3];
        // GameObject spawnedCoin = spawnedCoins[Random.Range(0, spawnedCoins.Length +1)];
        //  if (spawnedCoin != null) { Instantiate(spawnedCoin = spawnedCoins[Random.Range(0, spawnedCoins.Length)], spawnedObjectParentTransform, true); }
        //   if (spawnedCoin != null) { Instantiate(spawnedCoin, spawnedObjectParentTransform, true); }

        //for (i = 0; i >= 0; i++) ;
        //Instantiate(spawnedCoins[i],spawnedObjectParentTransform,true);

        if (spawnedObject != null) { if (spawnedObject.activeInHierarchy) { isSpawned = true; Debug.Log("Instantaited Object: " + spawnedObject.name); } }
        else { isSpawned = false; }

        isColliding = false;

       // if (spawnedObjectParentTransform.childCount >= maxObjectCount) { spawnedObjectParentTransform.GetChild(1).gameObject.SetActive(false); }
        //else { spawnedObject.SetActive(true); }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            Instantiate(objectInstantiator, spawnedSpawnerParentTransform, true);
            //collision.gameObject.transform.position = playerSpawnTransform.transform.position;
            isColliding = true;

        }
        else { collision.gameObject.SetActive(false); }
    }
    public void OnTriggerEnter(Collider collision)
    {


        if (collision.gameObject.CompareTag("Player"))
        {
            int i;
            Destroy(launchplatform);

            isColliding = true;
            GameObject clone = Instantiate(objectInstantiator, spawnedSpawnerParentTransform, true);
            objectInstantiator.SetActive(false);
            clone.transform.Translate(new Vector3(0, spawnerSpawnDistance, 0), Space.World);

            for (i = 0; i < maxSpawnerCount; i++) ;
            Debug.Log("Spawned Spawner, index " + i);

            if (i >= maxSpawnerCount)
            {
                if (spawnedSpawnerParentTransform.childCount >= maxSpawnerCount)
                {
                    Destroy(spawnedSpawnerParentTransform.GetChild(i).gameObject);
                    Debug.Log("Deactivated spawned Spawner " + i + ".");
                }
                //else if (i <= maxObjectCount) { spawnedSpawnerParentTransform.GetChild(i).gameObject.SetActive(true); }
            }
            else if (i <= maxObjectCount) { spawnedSpawnerParentTransform.GetChild(i).gameObject.SetActive(true); }
            //Instantiate(objectInstantiator, playerPrefab.transform.position - new Vector3(0, -10, 0), objectInstantiator.transform.rotation, spawnedSpawnerParentTransform);
        }
    }
}
