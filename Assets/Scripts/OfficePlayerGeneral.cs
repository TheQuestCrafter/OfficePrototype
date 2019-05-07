using System.Collections;
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

    [SerializeField]
    PopupText popupText;

    private GlobalInformation GameMasterBrain;

    private bool FreezePlayer;
    private Rigidbody2D MyRigidBody2D;
    private Animator playerAnim;
    private GlobalInformation GM = new GlobalInformation();
    private float horizontal;
    private float vertical;
    private Collider2D[] QuestHitResults = new Collider2D[100];
    private Collider2D[] InteractableHitResults = new Collider2D[100];
    private Collider2D[] FireHitResults = new Collider2D[100];
    private Collider2D[] MinigameHitResults = new Collider2D[100];
    private List<PlayerPrompts> promptToSay = new List<PlayerPrompts>();
    private string StuffToSayThisFrame;

    void Awake()
    {
        FreezePlayer = false;
        speech.text = "";
        StuffToSayThisFrame = "";
        GM = (GlobalInformation)FindObjectOfType(typeof(GlobalInformation));
        MyRigidBody2D = this.GetComponent<Rigidbody2D>();
        playerAnim = this.transform.GetChild(0).GetComponent<Animator>();
    }

    private void Start()
    {
        GameMasterBrain = GameObject.Find("TheGameMaster").GetComponent<GlobalInformation>();
        HandOutMinigameRewards();
    }

    void HandOutMinigameRewards()
    {
        //when the scene reloads, if there's a saved position, return to it
        if (GameMasterBrain.playerPos != null && GameMasterBrain.playerPos != Vector3.zero)//if they have a saved position (that isn't null or zero), put the player back in that spot
        {
            gameObject.transform.position = GameMasterBrain.playerPos;
        }
        if (GameMasterBrain.cameBackFromChiliMinigame == true)//if they just came back from the chili game, check their score and hand out the reward
        {
            GameMasterBrain.cameBackFromChiliMinigame = false;
            if (GameMasterBrain.chiliWin == 0)
            {
                popupText.DisplayPopupText("\nYou earned the bronze medal!", 3, 5);
            }
            if (GameMasterBrain.chiliWin == 1)
            {
                popupText.DisplayPopupText("\nYou earned the silver medal!", 2, 5);
            }
            if (GameMasterBrain.chiliWin == 2)
            {
                popupText.DisplayPopupText("\nYou earned the gold medal!", 1, 5);
            }
        }
        if (GameMasterBrain.cameBackFromCreedMinigame == true)
        {
            GameMasterBrain.cameBackFromCreedMinigame = false;
            if (GameMasterBrain.larryWin == 0)
            {
                popupText.DisplayPopupText("\nYou earned the bronze medal!", 3, 5);
            }
            if (GameMasterBrain.larryWin == 1)
            {
                popupText.DisplayPopupText("\nYou earned the silver medal!", 2, 5);
            }
            if (GameMasterBrain.larryWin == 2)
            {
                popupText.DisplayPopupText("\nYou earned the gold medal!", 1, 5);
            }
        }
    }

    void Update()
    {
        //updates input from pudate
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        SenseAround();
        UpdateAnimation();
    }

    private void FixedUpdate()
    {
        //StuffToSayThisFrame = ""; // resets stuff to say this frame every update, if there is something to say, it is filled in again later this update.

       if(!FreezePlayer)
       MyRigidBody2D.velocity = new Vector2(horizontal * moveSpeed, vertical * moveSpeed);

        //this function includes input monitoring, which should never be in FixedUpdate because many inputs will be missed due to the fixed rate of updates
        //it has been moved to Update
        HandlePrompts();
        //TimerTick();
    }

    private void UpdateAnimation()
    {
        playerAnim.SetFloat("xVelocity", Mathf.Abs(MyRigidBody2D.velocity.x));
        playerAnim.SetFloat("yVelocity", MyRigidBody2D.velocity.y);

        //Sprite Flipping
        if (MyRigidBody2D.velocity.x > 0.1)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (MyRigidBody2D.velocity.x < -0.1)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
    }

    private void SenseAround()
    {
        if(FreezePlayer)//don't let the player look for more things to do while they're busy doing something else
        {
            return;
        }
        if (QuestDetected() && questSystem.haveActiveQuest == false)
        {
            AddPrompt("Press E to take quest", 10);

            //QuestText = "Press E to take quest\n";
            //QuestTextTimer = 100;

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

        if (InteractableDetected())
        {
            //Debug.Log("dafasdfasdff");
            foreach (Collider2D i in InteractableHitResults)
            {
               if(!i) { continue; }
                if (Input.GetButtonDown("Fire1"))
                {
                    Interactiables thing = i.GetComponentInParent<Interactiables>();
                    FreezePlayer = true;
                    thing.giveprompt();
                }
                else
                {
                    AddPrompt("Press E to interact with " + i.name, 10);

                    //InteractableText = "Press E to interact with " + i.name + "\n";
                    //InteractableTimer = 100;
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
                    i.GetComponentInParent<NPCScript>().TalkToNPC();
                }
                else
                {
                    AddPrompt("Press E to accept task from " + i.name, 10);
                    break;
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

    /*
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
    */

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
            AddPrompt(prompt,10);
        }
    }

    private void HandlePrompts()
    {
        return;//removing prompts due to feedback
        speech.text = "";
        for (int i = 0; i < promptToSay.Count; i++)
        {
            speech.text += ":";
            promptToSay[i].Tick();
            if (promptToSay[i].prompt == "")
            {
                promptToSay.Remove(promptToSay[i]);
            }
            else
            {
                speech.text += promptToSay[i].prompt + "\n";
            }

            
        }
    }

    private void AddPrompt(string prompt,int duration)
    {
        return;//removing prompts due to feedback
        bool repeated = false;
        if (promptToSay.Count > 0)
        {
            for(int i = 0;i<promptToSay.Count;i++)
            {
                if (prompt == promptToSay[i].prompt)
                {
                    repeated = true;
                    promptToSay[i].timer = duration;
                }
                    
            }
            if(!repeated)
                promptToSay.Add(new PlayerPrompts(prompt, duration));

            return;
        }
        else
        {
            promptToSay.Add(new PlayerPrompts(prompt, duration));
            return;
        }
       
    }
}
