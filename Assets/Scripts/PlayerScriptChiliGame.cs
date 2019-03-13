using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScriptChiliGame : MonoBehaviour
{
    [SerializeField]
    private Text countText;
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private Rigidbody2D rb2d;
    [SerializeField]
    private float slowCoefficient=2;

    private float direction;
    public int count = 0;

    void FixedUpdate()
    {
        Move();
        countText.text = "Score: " + count;
    }

    private void Move()
    {
        //Moves player on input
        direction = Input.GetAxis("Horizontal");
        Vector2 vector = new Vector2(moveSpeed, 0);
        if (!Input.GetKeyDown(KeyCode.A) || !Input.GetKey(KeyCode.D))
        {
            vector = (vector / slowCoefficient);
        }
            rb2d.velocity=(vector * direction);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //increases score if chili is caught
        if (other.tag == "Chili")
        {
            count++;
            other.gameObject.SetActive(false);
        }
    }
}
