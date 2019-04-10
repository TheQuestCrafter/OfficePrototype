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

    DaySystem daySystemScript;

    private void Start()
    {
        daySystemScript = GameObject.FindGameObjectWithTag("Player").GetComponent<DaySystem>();
    }

    public void giveprompt()
    {

        if(name == "ExitDoor")//if this is the door to leave the office
        {
            if(daySystemScript.dayComplete)
            {
                ObjectDescriptionChart.ExecuteBlock("Next Day");
            }
            else
            {
                ObjectDescriptionChart.ExecuteBlock("Day Not Complete");
            }
        }

        else
        {
            //Calls the block from the object flowchart with the string title given through objectBlockName
            ObjectDescriptionChart.ExecuteBlock(objectBlockName);
        }
    }
}
