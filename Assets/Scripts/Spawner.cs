using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private GameObject chiliPrefab;
    [SerializeField]
    private float inBetweenTime = 3;
    [SerializeField]
    private float totalTime = 15;
    [SerializeField]
    private int spawnX=7;
    [SerializeField]
    Transform spawnerPoint;

    private System.Random random;
    private float xHolderFloat;
    private Vector3 spawnPoint;
    private float timeDelay;

    void Awake()
    {
        timeDelay = Time.time + inBetweenTime;
        random = new System.Random(); 
    }
    void Start()
    {
        InvokeRepeating("SpawnChili", 0, inBetweenTime);
    }
    void FixedUpdate()
    {
        //if (Time.time > timeDelay)
        
        if (Time.time >= totalTime)
        {
            CancelInvoke();
        }
    }

    void SpawnChili()
    {
        SetSpawnX();
        Spawn();
    }

    private void Spawn()
    {
        var chili = Instantiate(chiliPrefab, spawnPoint,spawnerPoint.rotation,this.transform);
    }

    private void SetSpawnX()
    {
        xHolderFloat = random.Next(-spawnX, spawnX);
        spawnPoint = spawnerPoint.position;
        spawnPoint.Set(spawnerPoint.position.x+xHolderFloat,spawnerPoint.position.y,spawnerPoint.position.z);
    }
}
