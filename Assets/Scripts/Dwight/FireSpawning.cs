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

    IEnumerator WaitToEnableFireSpawnLocationCoroutine(GameObject fireSpawnLocation, bool reenableSpawns)
    {
        fireSpawnLocations.Remove(fireSpawnLocation);
        yield return new WaitForSeconds(delayBetweenFires + 5);
        if (reenableSpawns)//add the spawn locations back in
        {
            fireSpawnLocations.Add(fireSpawnLocation);
        }

    }

    void SpawnFire()
    {
        if(fireSpawnLocations.Count == 0)//if all spawn locations have been removed, no more fires will spawn
        {
            canSpawnFire = false;
            return;
        }
        index = Random.Range(0, fireSpawnLocations.Count);
        chosenSpawnPoint = fireSpawnLocations[index];
        Instantiate(fire, chosenSpawnPoint.transform);
        StartCoroutine(WaitToSpawnNextFireCoroutine());

        //remove this fire from the spawn locations list for a while
        StartCoroutine(WaitToEnableFireSpawnLocationCoroutine(chosenSpawnPoint, false));
    }
}
