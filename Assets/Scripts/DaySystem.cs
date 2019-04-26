using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class DaySystem : MonoBehaviour
{

    //this class is attached to the player

    public int currentDay = 1;//the day the player is currently on

    public bool dayComplete = false;//when this is set to true, interacting with the door will let the player move on to the next day

    public Flowchart objectDescriptionChart;



    private void Start()
    {
        if(currentDay == 1)
        {
            //anything we want to happen on the first day
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F5))
        {
            DebugJumpToNextDay();
        }
    }

    public void GoToNextDay()//call this function when player interacts with the front door of the office
    {
        if (dayComplete)
        {
            if (currentDay >= 5)//if they're on the final day and leaving, end the game
            {
                //end the game, show credits, etc
                Debug.Log("Roll credits!");
                return;
            }
            currentDay++;
            dayComplete = false;//reset this variable when starting next day
            UpdateGameWorld(currentDay);
            Debug.Log("Moved on to day " + currentDay + ".");

        }
        else if(!dayComplete)
        {
            Debug.Log("Current day is not completed!");
        }
    }

    public void DebugJumpToNextDay()//used for skipping to the next day instantly, for testing
    {
        dayComplete = true;
        objectDescriptionChart.ExecuteBlock("DoorReady");
        GoToNextDay();
    }


    void UpdateGameWorld(int newDay)//changes the scene to suit the next day (move characters around, add or remove items, etc)
    {
        if(newDay == 2)
        {
            //do stuff to make the new day different
        }
        if (newDay == 3)
        {
            //do stuff to make the new day different
        }
        if (newDay == 4)
        {
            //do stuff to make the new day different
        }
        if (newDay == 5)
        {
            //do stuff to make the new day different
        }
    }
    
}
