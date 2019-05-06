using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///     Directions: This Script is meant to be attached to both the PickupPrefab
/// and the ObstaclePrefab that will be spawned by the ObjectSpawner.cs
///     Purpose: Handles the Movement of Prefabs that are spawned in 
/// by the ObjectSpawner.cs
/// </summary>
public class ObstacleScroller : MonoBehaviour
{    
    [SerializeField]
    private float obstacleSpeed;

    private LeisureSuitScoreKeeper scoreKeeper;

    private void Start()
    {
        scoreKeeper = GameObject.Find("ScoreText").GetComponent<LeisureSuitScoreKeeper>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (scoreKeeper.LeisureSuitGameEndState == false)
        {
            //Moves Objects To The Left
            transform.position += new Vector3(-(obstacleSpeed * Time.deltaTime), 0f, 0f);
        }
    }
}
