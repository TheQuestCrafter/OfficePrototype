using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    [SerializeField]
    Text text;
    [SerializeField]
    PlayerScriptChiliGame player = new PlayerScriptChiliGame();
    [SerializeField]
    private int minWinAmount = 8;

    private GlobalInformation GM = new GlobalInformation();
    private System.Random random;
    private float xHolderFloat;
    private Vector3 spawnPoint;
    private float timeDelay;
    private string EndGameState;
    private bool EndGameStarted = false;

    void Awake()
    {
        //Takes in the GlobalInformation which is part of the game master.
        GM = (GlobalInformation)FindObjectOfType(typeof(GlobalInformation));
        totalTime = totalTime + Time.time;
        timeDelay = Time.time + inBetweenTime;
        random = new System.Random();
        //Starts the Chili Spawning sequence
        InvokeRepeating("SpawnChili", 0, inBetweenTime);
        text.text = "";
    }

    void FixedUpdate()
    {
        ///<summary>
        /// If the timer has finished the desired time given in the serialized field then it will stop spawning chili and determine whether the player won or lost.
        /// </summary>
        if (Time.time >= totalTime && !EndGameStarted)
        {
            CancelInvoke();
            if (player.count >= minWinAmount)
            {
                EndGameState = "You saved the Chili!";
                GM.Shrutebucks += 5;
            }
            else
            {
                EndGameState = "You dropped the Chili!";
                GM.Shrutebucks += 1;
            }
            text.text = EndGameState;
            EndGameStarted = true;
        }
        else if (Time.time >= totalTime + 3)
        {
            EndGameStarted = false;
            //Loads back to the office.
            SceneManager.LoadScene(0);
        }
    }
    /// <summary>
    /// Sets the spawn location then spawns chili there.
    /// </summary>
    void SpawnChili()
    {
        SetSpawnX();
        Spawn();
    }

    private void Spawn()
    {
        //Instantiates the chili at the location set by SetSpawn
        var chili = Instantiate(chiliPrefab, spawnPoint,spawnerPoint.rotation,this.transform);
    }

    private void SetSpawnX()
    {
        //Randomly generates a x location to spawn the chili at since the X is constantly set.
        xHolderFloat = random.Next(-spawnX, spawnX);
        spawnPoint = spawnerPoint.position;
        spawnPoint.Set(spawnerPoint.position.x+xHolderFloat,spawnerPoint.position.y,spawnerPoint.position.z);
    }
}
