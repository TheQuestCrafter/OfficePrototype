using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public PongManager PM;
    
    private float currentSpeedX, currentSpeedY;
    private float Angle;
    [SerializeField]
    private float maxSpeed;
    [SerializeField]
    private float initalSpeed;
    [SerializeField]
    private float incrementForce;

    private Vector2 startPosition;
    private Rigidbody2D ballRigidbody2D;
    private SpriteRenderer ballSprite;

    public bool justDied = false;
    public bool hitWall = false;

    // Start is called before the first frame update
    void Start()
    {        
        ballRigidbody2D = GetComponent<Rigidbody2D>();
        PM = FindObjectOfType<PongManager>();
        ballSprite = GetComponent<SpriteRenderer>();
        ResetBall();
        ServeBall();
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {        
        KeepBallMoving();
        ManageScore();        
    }


    private void Update()
    {
        OffScreen();
        currentSpeedX = ballRigidbody2D.velocity.x;
        currentSpeedY = ballRigidbody2D.velocity.y;
        Debug.Log("current xVelocity = " + ballRigidbody2D.velocity.x.ToString());
        Debug.Log("current yVelocity = " + ballRigidbody2D.velocity.y.ToString());
        FlipSprite();
    }

    private void FlipSprite()
    {
        if (ballRigidbody2D.velocity.x > 0)
            ballSprite.flipX = true;
        if (ballRigidbody2D.velocity.x < 0)
            ballSprite.flipX = false;
    }

    private void ManageScore()
    {
        if (hitWall)
            PM.score++;
    }

    private void KeepBallMoving()
    {
        Vector2 clampedVelocity = ballRigidbody2D.velocity;

        if (ballRigidbody2D.velocity.x >= clampedVelocity.x || ballRigidbody2D.velocity.x <= clampedVelocity.x)
        {
            ballRigidbody2D.velocity = clampedVelocity;
        }

        clampedVelocity.x = Mathf.Clamp(ballRigidbody2D.velocity.x, -maxSpeed, maxSpeed);
        clampedVelocity.y = Mathf.Clamp(ballRigidbody2D.velocity.y, -maxSpeed, maxSpeed);        
    }

    private void ServeBall()
    {
        ballRigidbody2D.AddForce(Vector2.left * initalSpeed, ForceMode2D.Impulse);
        ballRigidbody2D.AddForce(Vector2.down * initalSpeed, ForceMode2D.Impulse);
    }

    void ResetBall()
    {
        justDied = false;
        ballRigidbody2D.velocity = new Vector2(0,0);
        transform.position = Vector2.zero;
    }

    void RestartGame()
    {
        if(PM.score >= PM.winScore)
        {
            this.enabled = false;
            Destroy(this);
        }
        else
        {
            if (justDied)
                PM.lives = PM.lives - 1;
            ResetBall();
            Invoke("ServeBall", 1);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Paddle"))
        {
            hitWall = true;            
        }
    }

    void OnCollisionExit2D(Collision2D coll)
    {
        if (coll.collider.CompareTag("Paddle"))
        {
            hitWall = false;            

            if (ballRigidbody2D.velocity.x < 0 && ballRigidbody2D.velocity.x != maxSpeed)
            {
                ballRigidbody2D.AddForce(new Vector2(-incrementForce, 0));
            }
            else if (ballRigidbody2D.velocity.x > 0 && ballRigidbody2D.velocity.x != maxSpeed)
            {
                ballRigidbody2D.AddForce(new Vector2(incrementForce, 0));
            }
            else if (ballRigidbody2D.velocity.x == maxSpeed)
            {
                ballRigidbody2D.AddForce(Vector2.zero, ForceMode2D.Impulse);
            }
        }
    }

    void OffScreen()
    {        
        if (this.GetComponent<Renderer>().isVisible == false)
        {
            justDied = true;

            RestartGame();
        }
    }

    void CheckForHorizontal()
    {
        if (ballRigidbody2D.velocity.y == 0)
        {
            ballRigidbody2D.AddForce(Vector2.up);
        }
    }

    void CheckForVertical()
    {
        if(ballRigidbody2D.velocity.x == 0)
        {
            ballRigidbody2D.AddForce(Vector2.right);
        }
    }
}