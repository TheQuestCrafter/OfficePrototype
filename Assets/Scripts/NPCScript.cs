using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
using UnityEngine.SceneManagement;

public class NPCScript : MonoBehaviour
{

    public AudioSource winSound;

    [SerializeField]
    private TextMesh speech;
    [SerializeField]
    private string ProximitySpeech;
    [SerializeField]
    private string identifier;

    private GlobalInformation GameMasterBrain;
    private GameObject player;

    [SerializeField]
    PopupText popupText;

    [SerializeField]
    [Tooltip("A referene to the Quest system")]
    private GameObject MinigameDetector;

    [Header("Minigame Settings")]

    [SerializeField]
    [Tooltip("The chart that holds the NPC's minigame, if they have one")]
    private Flowchart minigameChart;

    [SerializeField]
    [Tooltip("The day during which their minigame is open for play. 0 = available every day")]
    private int minigameDay = 0;

    [SerializeField]
    [Tooltip("The name of the block that starts their minigame, such as DwightFireMinigameStart")]
    private string minigameBlockName;



    [Tooltip("When set to true, interacting with the NPC will start the minigame flowchart block")]
    public bool readyToStartMinigame = false;

    [SerializeField]
    [Tooltip("What the NPC's proximity text will say if they're ready to start the minigame")]
    private string minigameProximityText = "";

    //Fungus Variables for NPC random dialogue
    [SerializeField]
    private Flowchart barkFlowchart;
    [SerializeField]
    private string barkBlockName;

    private int score = 0;
    private int bestScore = 0;
    [SerializeField]
    private int scoreOneStar = 3;
    [SerializeField]
    private int scoreTwoStar = 6;
    [SerializeField]
    private int scoreThreeStar = 8;


    public bool playerTouching = false;
    DaySystem daySystem;

    //private Collider2D ProximityCheck = new Collider2D();

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        GameMasterBrain = GameObject.Find("TheGameMaster").GetComponent<GlobalInformation>();
        daySystem = GameObject.FindGameObjectWithTag("Player").GetComponent<DaySystem>();

        speech.text = "";
        ProximitySpeech = "";//removing prompts due to feedback

        if (minigameChart==null)
            MinigameDetector.SetActive(false);
    }

    public void TalkToNPC()
    {
        Debug.Log("A");
        if (readyToStartMinigame && (minigameDay == 0 || minigameDay == daySystem.currentDay))//if the NPC is ready to start the game and it's the correct day for the game
        {
            Debug.Log("B");

            if (minigameChart)//make sure they actually have a flowchart assigned
            {
                GameMasterBrain.playerPos = player.transform.position;
                minigameChart.ExecuteBlock(minigameBlockName);
            }
            else
            {
                Debug.Log(gameObject + " tried to start a minigame but had no flowchart assigned.");
            }
        }
        /*else if(barkFlowchart != null)
        {
            barkFlowchart.ExecuteBlock(barkBlockName);
        }*/
    }

    public void StartMinigame()
    {
        if (gameObject.name == "Dwight")
        {
            GameObject fireSpawner = GameObject.Find("FireSpawnLocations");
            FireSpawning fireSpawningScript = fireSpawner.GetComponent<FireSpawning>();
            if (fireSpawningScript.CanStartMinigame() && readyToStartMinigame)
            {
                readyToStartMinigame = false;
                MinigameDetector.SetActive(false);
                fireSpawningScript.StartFireMinigame(8);
            }
        }
    }

    public void ResetMinigame(int earnedScore)//resets settings so the player can attempt the minigame again, and reports the score they got
    {
        readyToStartMinigame = true;
        MinigameDetector.SetActive(true);
        score = earnedScore;
        if(score > bestScore)
        {
            bestScore = score;
        }
        PostMinigameChat();
    }

    void PostMinigameChat()//the correct block is found by using the NPC's name, make sure they're named correctly in the heirarchy and in the flowchart blocks!
    {
        if (score < scoreOneStar)
        {
            minigameChart.ExecuteBlock(name + "NoMedal");
        }
        if (score >= scoreOneStar && score < scoreTwoStar)
        {
            minigameChart.ExecuteBlock(name + "BronzeMedal");
        }
        if (score >= scoreTwoStar && score < scoreThreeStar)
        {
            minigameChart.ExecuteBlock(name + "SilverMedal");
        }
        if (score >= scoreThreeStar)
        {
            minigameChart.ExecuteBlock(name + "GoldMedal");
        }
    }

    void ReportScoreResults()
    {
        if(!popupText)
        {
            Debug.Log("No popup text found!");
            return;
        }

        if (score < scoreOneStar)
        {
            popupText.DisplayPopupText("Score: " + score + "\nYou did not earn a medal.", 0, 5);
            return;
        }
        daySystem.dayComplete = true;
        //winSound.Play();
        if (score >= scoreOneStar && score < scoreTwoStar)
        {
            popupText.DisplayPopupText("Score: " + score + "\nYou earned the bronze medal!", 3, 5);
        }
        if (score >= scoreTwoStar && score < scoreThreeStar)
        {
            popupText.DisplayPopupText("Score: " + score + "\nYou earned the silver medal!", 2, 5);
        }
        if (score >= scoreThreeStar)
        {
            popupText.DisplayPopupText("Score: " + score + "\nYou earned the gold medal!", 1, 5);
        }
    }

    public void UpdateProxSpeech(string updatedLine)
    {
        return;//removing prompts due to feedback
        ProximitySpeech = updatedLine;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerTouching = true;
            //speech.text = ProximitySpeech;
            if(readyToStartMinigame)
            {
                //speech.text = minigameProximityText;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        speech.text = "";
        playerTouching = false;
    }
}
