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


    private Collider2D ProximityCheck = new Collider2D();

    // Start is called before the first frame update
    void Start()
    {
        speech.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            speech.text = ProximitySpeech;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        speech.text = "";
    }
}
