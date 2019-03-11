using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;


public class TestQuest : QuestParent 
{
    [SerializeField]
    private NPCScript angela;
    [SerializeField]
    private string questStep2Text;
    [SerializeField]
    private string updatedAngelaText;
    [SerializeField]
    private float timeToFinish;
    [SerializeField]
    private string prompt1;

    //Fungus Variables
    [SerializeField]
    private Flowchart questFlowchart; //reference to this quest's Flowchart
    [SerializeField]
    private string Step1BlockName;
    [SerializeField]
    private string Step2BlockName;

    private float timerForFinish;
    private bool active;

    void FixedUpdate()
    {
        if (questStep == 1)
        {
            QuestStep1();
        }
        else if (Time.time == timerForFinish + timeToFinish && questStep == 2)
        {
            FinishQuest();
        }
    }

    public override void StartQuest()
    {
        if (questSystem.StartQuest(questName, questTextInitial) == false)
        {
            questStep = 1;

            //CALL FLOWCHART BLOCK ONE
            questFlowchart.ExecuteBlock(Step1BlockName);
        }
        else
        {

        }
    }

    private void QuestStep1()
    {
        

        if (angela.playerTouching == true)
        {
            active = true;
            if (angela.playerTouching == true && Input.GetButtonDown("Fire1"))
            {
                //CALL FLOWCHART BLOCK TWO
                questFlowchart.ExecuteBlock(Step2BlockName);

                angela.UpdateProxSpeech(updatedAngelaText);
                questSystem.UpdateQuest(questStep, questStep2Text);
                timerForFinish = Time.time;
                questStep++;
                active = false;
            }
        }
        else
        {
            active = false;
        }
        PromptPlayer();
    }

    private void PromptPlayer()
    {

        questSystem.PromptPlayer(prompt1, active);
    }

    private void FinishQuest()
    {
        questStep = 0;
        questSystem.FinishQuest();
    }
}
