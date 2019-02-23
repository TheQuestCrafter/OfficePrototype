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
    [SerializeField]
    private TextMesh speech;

    private GlobalInformation GM = new GlobalInformation();
    private float horizontal;
    private float vertical;


    [SerializeField]
    Rigidbody2D rb;

    void Awake()
    {
        speech.text = "";
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

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Quest"))
        {
            speech.text = "Press E to take quest";

            if (Input.GetButtonDown("Fire1"))
            {
                collision.GetComponentInParent<QuestParent>().StartQuest();
                collision.gameObject.SetActive(false);
            }
        }
        else
        {
            speech.text = "";
        }
    }









}
