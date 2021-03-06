﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OfficePlayerGeneral : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The Player's movement speed, 10 used for testing")]
    private float moveSpeed = 10f;
    [SerializeField]
    Text text;
    [SerializeField]
    [Tooltip("A referene to the player's speech bubble textmesh")]
    private TextMesh speech;
    [SerializeField]
    [Tooltip("A referene to the Quest system")]
    private QuestSystem questSystem;

    [SerializeField]
    [Tooltip("Collider 2D that detects everything around player and runs them through filters below")]
    private Collider2D ProximityDetectionTrigger;
    [SerializeField]
    [Tooltip("What the player considers Quest objects")]
    private ContactFilter2D QuestContactFilter;
    [SerializeField]
    [Tooltip("What the player considers Interactable objects")]
    private ContactFilter2D InteractableContactFilter;
    [SerializeField]
    [Tooltip("What the player considers Fire")]
    private ContactFilter2D FireContactFilter;
    [SerializeField]
    [Tooltip("What the player considers a repeatable minigame")]
    private ContactFilter2D MinigameContactFilter;

    private bool FreezePlayer;
    Rigidbody2D MyRigidBody2D;
    private GlobalInformation GM = new GlobalInformation();
    private int InteractableTimer;
    private string InteractableText;
    private string QuestText;
    private int QuestTextTimer;
    private float horizontal;
    private float vertical;
    private Collider2D[] QuestHitResults = new Collider2D[100];
    private Collider2D[] InteractableHitResults = new Collider2D[100];
    private Collider2D[] FireHitResults = new Collider2D[100];
    private Collider2D[] MinigameHitResults = new Collider2D[100];
    private string StuffToSayThisFrame;

    void Awake()
    {
        FreezePlayer = false;
        speech.text = "";
        StuffToSayThisFrame = "";
        GM = (GlobalInformation)FindObjectOfType(typeof(GlobalInformation));
        MyRigidBody2D = this.GetComponent<Rigidbody2D>();
    }

    void Update()
    {

        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        SenseAround();

    }

    private void FixedUpdate()
    {
        StuffToSayThisFrame = ""; // resets stuff to say this frame every update, if there is something to say, it is filled in again later this update.

       if(!FreezePlayer)
       MyRigidBody2D.velocity = new Vector2(horizontal * moveSpeed, vertical * moveSpeed);

        text.text = "Schrutebucks: " + GM.Shrutebucks;
        //SenseAround();
        //this function includes input monitoring, which should never be in FixedUpdate because many inputs will be missed due to the fixed rate of updates
        //it has been moved to Update
        speech.text = StuffToSayThisFrame + QuestText + InteractableText;
        TimerTick();
    }

    private void SenseAround()
    {
        if(FreezePlayer)//don't let the player look for more things to do while they're busy doing something else
        {
            return;
        }
        if (QuestDetected() && questSystem.haveActiveQuest == false)
        {
            StuffToSayThisFrame += "Press E to take quest\n";

            if (Input.GetButtonDown("Fire1"))
            {
                FreezePlayer = true;
                foreach (Collider2D i in QuestHitResults)
                {
                    if(!i)//null reference exceptions were being thrown because looping through every collider in QuestHitResults was also returning a null object in addition to the quest
                    {//this skips over any of those null results
                        continue;
                    }
                    if (i.CompareTag("Quest"))
                    {
                        questSystem.RetreiveQuest(i.name);
                        i.gameObject.SetActive(false);
                    }
                }

            }
        }
        else
        {
            speech.text = "";
        }

        if (InteractableDetected() && InteractableTimer == 0)
        {
            foreach (Collider2D i in InteractableHitResults)
            {
                if(!i) { continue; }
                if (Input.GetButtonDown("Fire1"))
                {
                    Interactiables thing = i.GetComponentInParent<Interactiables>();
                    InteractableTimer = 100;
                    FreezePlayer = true;
                    thing.giveprompt();
                }
                else
                {
                    StuffToSayThisFrame += "Press E to interact with " + i.name + "\n";
                    break;
                }
            }
        }
        if (MinigameDetected())
        {
            foreach (Collider2D i in MinigameHitResults)
            {
                if (!i) { continue; }
                if (Input.GetButtonDown("Fire1"))
                {
                    i.GetComponent<NPCScript>().TalkToNPC();
                }
            }
        }
        if (FireDetected())
        {
            //StuffToSayThisFrame += "Press E put out fire \n";

            if (Input.GetButtonDown("Fire1"))
            {
                FireHitResults[0].GetComponentInParent<FireBehavior>().ReduceFire();
            }

        }

    }

    private void TimerTick()
    {
        //this function is called once per update to count down how long each prompt text stays visible.
        //currently has two types of text that needs to have timer.
        InteractableTimer--;
        QuestTextTimer--;

        InteractableTimer = Mathf.Clamp(InteractableTimer, 0, 1000);
        QuestTextTimer = Mathf.Clamp(QuestTextTimer, 0, 1000);

        if (InteractableTimer == 0)
        {
            InteractableText = "";
        }
        if (QuestTextTimer == 0)
        {
            QuestText = "";
        }
    }

    //various detects returning a bool depending on if certain type is detected in the detection collider.
    private bool QuestDetected()
    {
        return ProximityDetectionTrigger.OverlapCollider(QuestContactFilter, QuestHitResults) > 0;
    }
    private bool InteractableDetected()
    {
        return ProximityDetectionTrigger.OverlapCollider(InteractableContactFilter, InteractableHitResults) > 0;
    }
    private bool MinigameDetected()
    {
        return ProximityDetectionTrigger.OverlapCollider(MinigameContactFilter, MinigameHitResults) > 0;
    }
    private bool FireDetected()
    {
        return ProximityDetectionTrigger.OverlapCollider(FireContactFilter, FireHitResults) > 0;
    }

    //freeze and unfreeze player uded for dialogue and fungus plugin to keep player grounded when fungus is running.
    public void UnfreezePlayer()
    {
        FreezePlayer = false;
    }
    public void RefreezePlayer()
    {
        FreezePlayer = true;
    }

    //method used by other scripts and gameobjects to give the player a speech bubble prompt.
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
