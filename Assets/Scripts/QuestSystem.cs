using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class QuestSystem : MonoBehaviour
{
    public string questName;
    public bool haveActiveQuest = false;
    public int questStep = 0;
    string[] questList = new string[5] { "TestQuest", "", "", "", "" };

    [SerializeField]
    private TestQuest testQuest;
    [SerializeField]
    OfficePlayerMovement player;
    [SerializeField]
    private Text questText;
    private Scene SceneHistory;

    void Update()
    {
        //finds any missing references once a new scene has started
        if (SceneManager.GetActiveScene() != SceneHistory)
        {
            FindObjects();
        }
    }

    /// <summary>
    /// Looks for replacement components for the questsystem
    /// </summary>
    private void FindObjects()
    {
        testQuest = (TestQuest)FindObjectOfType(typeof(TestQuest));
        player = (OfficePlayerMovement)FindObjectOfType(typeof(OfficePlayerMovement));
        /*
        if(GameObject.Find("Canvas/QuestPanel/QuestText").GetComponent<Text>()!=null)
            questText = GameObject.Find("Canvas/QuestPanel/QuestText").GetComponent<Text>();
            */
    }


    /// <summary>
    /// Searches through questname given and calls the quest that was procted
    /// </summary>
    /// <param name="givenQuestName"></param>
    public void RetreiveQuest(string givenQuestName)
    {
        if (givenQuestName == questList[0])
        {
            testQuest.StartQuest();
        }
        else if (givenQuestName == questList[1])
        {

        }
        else if (givenQuestName == questList[2])
        {

        }
        else if (givenQuestName == questList[3])
        {

        }
        else if (givenQuestName == questList[4])
        {

        }
    }

    //Calls the given quest and initiates it if there is no active quest
    public bool StartQuest(string inputQuestName, string initialText)
    {
        if (haveActiveQuest == false)
        {
            questName = inputQuestName;
            haveActiveQuest = true;
            questText.text = initialText;
            return false;
        }
        else
        {
            return true;
        }
    }

    //Updates the quest text UI
    public void UpdateQuest(int inputQuestStep, string updatedText)
    {
        questStep = inputQuestStep;
        questText.text = updatedText;
    }

    //Resets the questsystem
    public void FinishQuest()
    {
        questStep = 0;
        questName = null;
        haveActiveQuest = false;
        questText.text = null;
    }

    //prompts the OfficePlayerMovement
    public void PromptPlayer(string prompt, bool active)
    {
        player.PromptPlayer(prompt, active);
    }
}
