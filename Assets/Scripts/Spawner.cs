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
        GM = (GlobalInformation)FindObjectOfType(typeof(GlobalInformation));
        timeDelay = Time.time + inBetweenTime;
        random = new System.Random(); 
    }
    void Start()
    {
        text.text = "";
        InvokeRepeating("SpawnChili", 0, inBetweenTime);
    }
    void FixedUpdate()
    {
        //if (Time.time > timeDelay)
        
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
            SceneManager.LoadScene(0);
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
