﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    /*SCRIPT DESCRIPTION:
     * Directions: This Script is meant to be attached to the object that will act as the container for
     * the 3 potential object spawning locations.
     * 
     * Purpose: This will pick which spawn point to spawn a new object at and whether it will be an
     * object the player will need to pickup (PickupPrefab) or an obstacle to be avoided (ObstaclePrefab)
     */

    #region Serialized Fields
    [SerializeField]
    [Tooltip("The Obstacle Prefab to be Spawned in.")]
    private GameObject obstaclePrefab;

    [SerializeField]
    [Tooltip("The Pickup Prefab to be Spawned in.")]
    private GameObject pickupPrefab;

    [SerializeField]
    [Tooltip("The amount of time to wait before spawning another object.")]
    private float spawnDelay;

    [SerializeField]
    [Tooltip("The amount of time to wait before spawning another pickup.")]
    private float pickupSpawnDelay;

    [SerializeField]
    [Tooltip("The lifetime of the object before destroying it.")]
    private float lifetime;
    #endregion

    //Used for the random number that will be generated in the SpawnPointPicker()
    private float spawnPosition;
    private Transform[] spawnPoints;
    private bool canSpawn = true;
    private bool canSpawnPickups = true;

    #region IEnumerators
    IEnumerator SpawnTimer()
    {
        canSpawn = false;
        yield return new WaitForSecondsRealtime(spawnDelay);
        canSpawn = true;
    }

    IEnumerator PickupSpawnTimer()
    {
        canSpawnPickups = false;
        yield return new WaitForSecondsRealtime(pickupSpawnDelay);
        canSpawnPickups = true;
    }
    #endregion

    // Start is called before the first frame update
    private void Start()
    {
        spawnPoints = GetComponentsInChildren<Transform>();
    }

    // Update is called once per frame
    private void Update()
    {
        if(canSpawn)
            SpawnPointPicker();
    }


    /// <summary>
    /// Decides which spawn point will be used to spawn the next object
    /// </summary>
    private void SpawnPointPicker()
    {
        spawnPosition = Random.Range(0f, 3f);

        if(spawnPosition <= 1) //Spawns @ Top Spawn Point
        {
            //Deciding Whether to Spawn an Obstacle or a Pickup
            if(spawnPosition < 0.5f)
            {
                SpawnObstacles(1);
            }
            else if (canSpawnPickups)
            {
                SpawnPickups(1);
                StartCoroutine(PickupSpawnTimer());
            }

            StartCoroutine(SpawnTimer());
        }
        else if (spawnPosition <= 2 && spawnPosition > 1) //Spawns @ Middle Spawn Point
        {
            //Deciding Whether to Spawn an Obstacle or a Pickup
            if (spawnPosition < 1.5f)
            {
                SpawnObstacles(2);
            }
            else if (canSpawnPickups)
            {
                SpawnPickups(2);
                StartCoroutine(PickupSpawnTimer());
            }

            StartCoroutine(SpawnTimer());
        }
        else if (spawnPosition <= 3 && spawnPosition > 2)//Spawns @ Bottom Spawn Point
        {
            //Deciding Whether to Spawn an Obstacle or a Pickup
            if (spawnPosition < 2.5f)
            {
                SpawnObstacles(3);
            }
            else if (canSpawnPickups)
            {
                SpawnPickups(3);
                StartCoroutine(PickupSpawnTimer());
            }

            StartCoroutine(SpawnTimer());
        }
    }

    /// <summary>
    /// Used for Spawning the Obstacles the player will need to avoid
    /// </summary>
    private void SpawnObstacles(int spawnPoint)
    {
        GameObject clone = Instantiate(obstaclePrefab, spawnPoints[spawnPoint].position, Quaternion.identity);
        Destroy(clone, lifetime);
    }

    /// <summary>
    /// Used for spawning the Pickups the player needs to get
    /// </summary>
    private void SpawnPickups(int spawnPoint)
    {
        GameObject clone = Instantiate(pickupPrefab, spawnPoints[spawnPoint].position, Quaternion.identity);
        Destroy(clone, lifetime);
    }
}