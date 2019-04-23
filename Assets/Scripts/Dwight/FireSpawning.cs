using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSpawning : MonoBehaviour
{
    [HideInInspector]
    public List<GameObject> fireSpawnLocations = new List<GameObject>();

    [Tooltip("The NPC or object that is responsible for starting the minigame")]
    public GameObject startingObject;

    public PopupText popupText;

    GameObject chosenSpawnPoint;
    public GameObject fire;
    int index;

    bool canSpawnFire = false;

    [SerializeField]
    float delayBetweenFires = 10;

    int firesExtinguished = 0;
    int firesFailed = 0;
    bool minigameIsRunning = false;
    bool minigameIsFinished = false;

    [HideInInspector]
    public int totalFires = 8;//the total amount of fires to spawn for the minigame
    int firesSpawned = 0;

    private void Start()
    {
        foreach (Transform child in transform)
        {
            fireSpawnLocations.Add(child.gameObject);
        }
    }

    public bool CanStartMinigame()
    {
        if (minigameIsFinished)
        {
            return false;//don't let them start the minigame if they've already completed it
        }
        if (minigameIsRunning)
        {
            return false;//don't let them start the fire minigame while it's already running
        }
        else
        {
            return true;
        }
    }

    public void StartFireMinigame(int numFires=8)//starts the minigame. recommended number of fires to spawn is 8
    {
        if(minigameIsFinished)
        {
            return;//don't let them start the minigame if they've already completed it
        }
        if(minigameIsRunning)
        {
            return;//don't let them start the fire minigame while it's already running
        }
        canSpawnFire = true;
        totalFires = numFires;
        popupText.DisplayPopupText("Put out the fires before the office burns down!", 0, 3);
        minigameIsRunning = true;
    }

    public void UpdateFireTally(bool fireSucceeded)//keeps track of how many fires are extinguished or burned out
    {
        if(fireSucceeded)
        {
            firesExtinguished++;
        }
        else
        {
            firesFailed++;
        }
        //check if the game is complete
        if(firesExtinguished + firesFailed >= totalFires)
        {
            EndFireMinigame();
        }
    }

    public void EndFireMinigame()//ends the game and shows how many fires you extinguished
    {
        //popupText.DisplayPopupText("Fires extinguished: " + firesExtinguished + "/" + totalFires);
        startingObject.GetComponent<NPCScript>().ResetMinigame(firesExtinguished);

        //clear the game in case it's played again
        firesExtinguished = 0;
        firesFailed = 0;
        minigameIsRunning = false;
        minigameIsFinished = true;
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

    IEnumerator WaitToEnableFireSpawnLocationCoroutine(GameObject fireSpawnLocation, bool reenableSpawns)//prevents a fire from spawning in the same location until a certain amount of time has passed
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
        if(firesSpawned >= totalFires)
        {
            canSpawnFire = false;
        }
        
        index = Random.Range(0, fireSpawnLocations.Count);
        chosenSpawnPoint = fireSpawnLocations[index];
        Instantiate(fire, chosenSpawnPoint.transform);
        firesSpawned++;
        StartCoroutine(WaitToSpawnNextFireCoroutine());

        //remove this fire from the spawn locations list for a while
        StartCoroutine(WaitToEnableFireSpawnLocationCoroutine(chosenSpawnPoint, false));
    }
}
