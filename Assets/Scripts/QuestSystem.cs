using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public void UpdateQuest(int inputQuestStep, string updatedText)
    {
        questStep = inputQuestStep;
        questText.text = updatedText;
    }

    public void FinishQuest()
    {
        questStep = 0;
        questName = null;
        haveActiveQuest = false;
        questText.text = null;
    }

    public void PromptPlayer(string prompt, bool active)
    {
        player.PromptPlayer(prompt, active);
    }
}
