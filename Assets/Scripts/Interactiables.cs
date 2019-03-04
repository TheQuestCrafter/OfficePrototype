using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactiables : MonoBehaviour
{
    [SerializeField]
    private string ExamineText;
    [SerializeField]
    private TextMesh speech;
    [SerializeField]
    private 
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

    /*
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") )
        {
            Debug.Log("player examines interactables");
            OfficePlayerMovement player = collision.GetComponent<OfficePlayerMovement>();
            player.PromptPlayer(ExamineText, true);
        }

    }
    */
}
