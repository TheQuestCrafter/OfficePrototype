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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void giveprompt()
    {
        ObjectDescriptionChart.ExecuteBlock(objectBlockName);
    }
}
