using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    [SerializeField]
    private bool locked;

    [SerializeField]
    private LayerMask playerLayer;


    private SpriteRenderer mySpriteRenderer;
    private BoxCollider2D doorCollider;
    private CapsuleCollider2D checkCollider;

    private void Start()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        doorCollider = GetComponent<BoxCollider2D>();
        checkCollider = GetComponent<CapsuleCollider2D>();
    }





    private void OnTriggerEnter2D(Collider2D collision)
    {
        CheeckPlayerLeft();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        CheeckPlayerLeft();
    }

    private void OpenDoor(bool open)
    {
        if (!locked)
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

    private void CheeckPlayerLeft()
    {
        if (Physics2D.OverlapCapsule(transform.position, checkCollider.size, checkCollider.direction, 0, playerLayer) == null)
            OpenDoor(false);
        else
            OpenDoor(true);
    }
}
