using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles the player input for the LeisureSuit minigame.
/// </summary>
public class LeisureSuitPlayerController : MonoBehaviour
{
    // v = Vertical & h = Horizontal
    private float v_MovementInput, h_MovementInput;
    private Rigidbody2D playerRigidBody;    

    [SerializeField]
    private float moveSpeed;
       
    private LeisureSuitScoreKeeper scoreKeeper;

    // Start is called before the first frame update
    private void Start()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
        scoreKeeper = GameObject.Find("ScoreText").GetComponent<LeisureSuitScoreKeeper>();
    }

    private void Update()
    {
        if (scoreKeeper.LeisureSuitGameEndState == false)
        {
            v_MovementInput = Input.GetAxis("Vertical");
            h_MovementInput = Input.GetAxis("Horizontal");
        }
    }

    private void FixedUpdate()
    {
        if (scoreKeeper.LeisureSuitGameEndState == false)
        {
            Vector2 movement = new Vector2(h_MovementInput * moveSpeed, v_MovementInput * moveSpeed);
            playerRigidBody.velocity = movement;
        }
        else
        {
            playerRigidBody.velocity = Vector3.zero;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (scoreKeeper.LeisureSuitGameEndState == false)
        {
            if (collision.tag == "Obstacle")
            {
                Destroy(collision.gameObject);
                scoreKeeper.DecreaseScore();
            }
            else if (collision.tag == "Pickup")
            {
                Destroy(collision.gameObject);
                scoreKeeper.IncreaseScore();
            }
        }
    }
}
