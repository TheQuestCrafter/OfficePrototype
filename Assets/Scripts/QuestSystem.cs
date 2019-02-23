using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestSystem : MonoBehaviour
{
    public string questName;
    public bool haveActiveQuest = false;
    public int questStep = 0;
    [SerializeField]
    private Text questText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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

    public void FinishQuest(string finalText)
    {
        questStep = 0;
        questName = null;
        haveActiveQuest = false;
        questText.text = null;
    }
}
