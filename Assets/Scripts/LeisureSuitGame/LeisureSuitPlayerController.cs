using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void FixedUpdate()
    {
        v_MovementInput = Input.GetAxis("Vertical");
        h_MovementInput = Input.GetAxis("Horizontal");
        Vector2 movement = new Vector2(h_MovementInput * moveSpeed, v_MovementInput * moveSpeed);
        playerRigidBody.velocity = movement;
    }

    private void OnTriggerEnter2D(Collider2D collision)
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
