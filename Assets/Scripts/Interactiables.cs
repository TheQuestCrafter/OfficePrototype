using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class Interactiables : MonoBehaviour
{
    [SerializeField]
    private string ExamineText;

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


    public string giveprompt()
    {
        ObjectDescriptionChart.ExecuteBlock(objectBlockName);
        return ExamineText;
    }
}
