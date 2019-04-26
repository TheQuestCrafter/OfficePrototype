using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class Interactiables : MonoBehaviour
{

    //Fungus Variables
    [SerializeField]
    private Flowchart ObjectDescriptionChart;
    [SerializeField]
    private string objectBlockName;
    [SerializeField]
    private bool isExitDoor = false;

    private DaySystem daySystem;

    private void Start()
    {
        daySystem = GameObject.FindGameObjectWithTag("Player").GetComponent<DaySystem>();
    }

    public void giveprompt()
    {
        if(isExitDoor && daySystem.dayComplete == true)
        {
            ObjectDescriptionChart.ExecuteBlock("DoorReady");
            daySystem.GoToNextDay();
        }
        else
        {
            //Calls the block from the object flowchart with the string title given through objectBlockName
            ObjectDescriptionChart.ExecuteBlock(objectBlockName);
        }
    }
}
