using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScroller : MonoBehaviour
{
    /*SCRIPT DESCRIPTION:
     *  Directions: This Script is meant to be attached to both the PickupPrefab
     * and the ObstaclePrefab that will be spawned by the ObjectSpawner.cs
     *  Purpose: Handles the Movement of Prefabs that are spawned in 
     * by the ObjectSpawner.cs
     */
    
    [SerializeField]
    private float obstacleSpeed;

    // Update is called once per frame
    private void Update()
    {
        //Moves Objects To The Left
        transform.position += new Vector3(-(obstacleSpeed * Time.deltaTime), 0f, 0f);
    }
}
