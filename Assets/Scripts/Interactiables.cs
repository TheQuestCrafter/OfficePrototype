using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactiables : MonoBehaviour
{
    [SerializeField]
    private string ExamineText;

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
        return ExamineText;
    }
}
