using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCScript : MonoBehaviour
{

    [SerializeField]
    private TextMesh speech;
    [SerializeField]
    private string ProximitySpeech;
    [SerializeField]
    private string identifier;

    public bool playerTouching = false;

    //private Collider2D ProximityCheck = new Collider2D();

    // Start is called before the first frame update
    void Start()
    {
        speech.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateProxSpeech(string updatedLine)
    {
        ProximitySpeech = updatedLine;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerTouching = true;
            speech.text = ProximitySpeech;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        speech.text = "";
        playerTouching = false;
    }
}
