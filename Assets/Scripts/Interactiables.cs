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

    public void giveprompt()
    {
        //Calls the block from the object flowchart with the string title given through objectBlockName
        ObjectDescriptionChart.ExecuteBlock(objectBlockName);
    }
}
