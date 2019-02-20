using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OfficePlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 10f;
    [SerializeField]
    Text text;
    
    private GlobalInformation GM = new GlobalInformation();
    private float horizontal;
    private float vertical;

    [SerializeField]
    Rigidbody2D rb;

    void Awake()
    {
        GM = (GlobalInformation)FindObjectOfType(typeof(GlobalInformation));
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * moveSpeed, vertical * moveSpeed);
        text.text = "Schrutebucks: " + GM.Shrutebucks;
    }
}
