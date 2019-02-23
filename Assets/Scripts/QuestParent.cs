using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestParent : MonoBehaviour
{
    [SerializeField]
    private QuestSystem questSystem = new QuestSystem();
    [SerializeField]
    private string questName;
    [SerializeField]
    string questTextInitial;

    private int questStep = 0;
    
    public void StartQuest()
    {
        if(questSystem.StartQuest(questName,questTextInitial) == false)
        {
            questStep = 1;
        }
        else
        {

        }
    }
}
