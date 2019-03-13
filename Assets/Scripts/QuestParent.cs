using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestParent : MonoBehaviour
{
    [SerializeField]
    public QuestSystem questSystem = new QuestSystem();
    [SerializeField]
    public string questName;
    [SerializeField]
    public string questTextInitial;

    public int questStep = 0;
    
    public virtual void StartQuest()
    {
        //Sets to the next quest step
        if(questSystem.StartQuest(questName,questTextInitial) == false)
        {
            questStep = 1;
        }
    }
}
