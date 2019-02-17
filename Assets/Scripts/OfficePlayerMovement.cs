using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfficePlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 10f;

    private float horizontal;
    private float vertical;

    [SerializeField]
    Rigidbody2D rb;

    void Start()
    {
        
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * moveSpeed, vertical * moveSpeed);
    }
}
