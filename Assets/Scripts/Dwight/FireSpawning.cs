using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSpawning : MonoBehaviour
{
    List<GameObject> fireSpawnLocations = new List<GameObject>();
    GameObject chosenSpawnPoint;
    public GameObject fire;
    int index;

    bool canSpawnFire = true;

    [SerializeField]
    float delayBetweenFires = 10;

    private void Start()
    {
        foreach (Transform child in transform)
        {
            fireSpawnLocations.Add(child.gameObject);
        }
    }

    private void Update()
    {
        if(canSpawnFire)
        {
            SpawnFire();
        }
    }

    IEnumerator WaitToSpawnNextFireCoroutine()
    {
        canSpawnFire = false;
        yield return new WaitForSeconds(delayBetweenFires);
        canSpawnFire = true;

    }

    void SpawnFire()
    {
        index = Random.Range(1, fireSpawnLocations.Count);
        chosenSpawnPoint = fireSpawnLocations[index];
        Instantiate(fire, chosenSpawnPoint.transform);
        StartCoroutine(WaitToSpawnNextFireCoroutine());
    }
}
