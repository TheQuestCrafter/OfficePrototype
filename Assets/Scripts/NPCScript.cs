using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
using UnityEngine.SceneManagement;

public class NPCScript : MonoBehaviour
{

    [SerializeField]
    private TextMesh speech;
    [SerializeField]
    private string ProximitySpeech;
    [SerializeField]
    private string identifier;

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
    [Tooltip("The name of the block that starts their minigame, such as DwightFireMinigameStart")]
    private string minigameBlockName;

    [Tooltip("When set to true, interacting with the NPC will start the minigame flowchart block")]
    public bool readyToStartMinigame = false;

    [SerializeField]
    [Tooltip("What the NPC's proximity text will say if they're ready to start the minigame")]
    private string minigameProximityText = "";

    private int score = 0;
    private int bestScore = 0;
    [SerializeField]
    private int scoreOneStar = 3;
    [SerializeField]
    private int scoreTwoStar = 6;
    [SerializeField]
    private int scoreThreeStar = 8;


    public bool playerTouching = false;

    //private Collider2D ProximityCheck = new Collider2D();

    // Start is called before the first frame update
    void Start()
    {
        speech.text = "";
        ProximitySpeech = "";//removing prompts due to feedback

        if (minigameChart==null)
            MinigameDetector.SetActive(false);
    }

    public void TalkToNPC()
    {
        if(readyToStartMinigame)
        {

            if (minigameChart)//make sure they actually have a flowchart assigned
            {
                minigameChart.ExecuteBlock(minigameBlockName);
            }
            else
            {
                Debug.Log(gameObject + " tried to start a minigame but had no flowchart assigned.");
            }
        }
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
        ReportScoreResults();
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
        }
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
