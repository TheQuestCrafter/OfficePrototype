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
    private Collider2D ProximityDetectionTrigger;
    [SerializeField]
    private ContactFilter2D QuestContactFilter;
    [SerializeField]
    private ContactFilter2D InteractableContactFilter;

    private GlobalInformation GM = new GlobalInformation();
    private int InteractableTimer;
    private string InteractableText;
    private string QuestText;
    private int QuestTextTimer;
    private float horizontal;
    private float vertical;
    private Collider2D[] QuestHitResults = new Collider2D[100];
    private Collider2D[] InteractableHitResults = new Collider2D[100];
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
        AvaliableQuest();
        speech.text = StuffToSayThisFrame+QuestText+InteractableText;
        TimerTick();
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
                        questSystem.RetreiveQuest(i.name);
                        i.gameObject.SetActive(false);
                    }
                }

            }
        }
        else if (InteractableDetected()&&InteractableTimer==0)
        {



            foreach (Collider2D i in InteractableHitResults)
            {
                if (Input.GetButtonDown("Fire1"))
                {
                    Interactiables thing = i.GetComponentInParent<Interactiables>();
                    InteractableTimer = 100;
                    thing.giveprompt();
                    
                }
                else
                {
                    StuffToSayThisFrame += "Press E to interact with " + i.name + "\n";
                    break;
                }
            } 
        }  
        else
        {
            speech.text = "";
        }
    }

    private void TimerTick()
    {
        InteractableTimer--;
        QuestTextTimer--;

        InteractableTimer = Mathf.Clamp(InteractableTimer, 0, 1000);
        QuestTextTimer = Mathf.Clamp(QuestTextTimer,0,1000);

        if (InteractableTimer == 0)
        {
            InteractableText = "";
        }
        if (QuestTextTimer == 0)
        {
            QuestText = "";
        }



    }


    private bool QuestDetected()
    {
        return ProximityDetectionTrigger.OverlapCollider(QuestContactFilter, QuestHitResults) > 0;
    }
    private bool InteractableDetected()
    {
        return ProximityDetectionTrigger.OverlapCollider(InteractableContactFilter,InteractableHitResults) > 0;
    }


    public void PromptPlayer(string prompt, bool activate)
    {
        if (activate)
        {
            QuestTextTimer = 100;
            QuestText = prompt;
        }
        else
            QuestTextTimer = 0;

    }







}
