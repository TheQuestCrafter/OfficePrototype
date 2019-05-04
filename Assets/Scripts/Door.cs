using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    [SerializeField]
    private bool locked;



    private SpriteRenderer mySpriteRenderer;
    private BoxCollider2D doorCollider;

    private void Start()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        doorCollider = GetComponent<BoxCollider2D>();
    }




    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")&&!locked)
        {
            OpenDoor(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            OpenDoor(false);
        }
    }

    private void OpenDoor(bool open)
    {
        if (open)
        {
            mySpriteRenderer.enabled = false;
            doorCollider.enabled = false;
        }
        else
        {
            mySpriteRenderer.enabled = true;
            doorCollider.enabled = true;
        }
    }
}
