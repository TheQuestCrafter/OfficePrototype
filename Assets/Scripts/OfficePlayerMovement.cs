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
    [SerializeField]
    private QuestSystem questSystem;

    [SerializeField]
    private Collider2D QuestDetectTrigger;
    [SerializeField]
    private ContactFilter2D QuestContactFilter;

    private GlobalInformation GM = new GlobalInformation();
    private float horizontal;
    private float vertical;
    private Collider2D[] QuestHitResults = new Collider2D[100];
    private string StuffToSayThisFrame;


    [SerializeField]
    Rigidbody2D rb;

    void Awake()
    {
        speech.text = "";
        StuffToSayThisFrame = "";
        GM = (GlobalInformation)FindObjectOfType(typeof(GlobalInformation));
    }

    void Update()
    {
        
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

    }

    private void FixedUpdate()
    {
        StuffToSayThisFrame = "";
        rb.velocity = new Vector2(horizontal * moveSpeed, vertical * moveSpeed);
        text.text = "Schrutebucks: " + GM.Shrutebucks;
        AvaliableQuest();
        speech.text = StuffToSayThisFrame;
    }
    
    private void AvaliableQuest()
    {
        if (QuestDetected() && questSystem.haveActiveQuest == false)
        {
            StuffToSayThisFrame += "Press E to take quest\n";
            if (Input.GetButtonDown("Fire1"))
            {
                foreach (Collider2D i in QuestHitResults)
                {
                    if (i.CompareTag("Quest"))
                    {
                        i.gameObject.SetActive(false);
                        questSystem.RetreiveQuest(i.name);

                    }
                }

            }
        }
        else
        {
            speech.text = "";
        }
    }

    /*

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Quest") && questSystem.haveActiveQuest == false)
        {
            speech.text = "Press E to take quest";

            if (Input.GetButtonDown("Fire1"))
            {
                questSystem.RetreiveQuest(collision.name);
            }
        }
        else
        {
            speech.text = "";
        }
    }
    */
    private bool QuestDetected()
    {
        return QuestDetectTrigger.OverlapCollider(QuestContactFilter, QuestHitResults) > 0;
    }

    public void PromptPlayer(string prompt, bool activate)
    {
        if (activate)
        {
            StuffToSayThisFrame += prompt+"\n";
        }

    }







}
