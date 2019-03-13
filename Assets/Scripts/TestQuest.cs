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
        //If the QuestStart is called then queststep = 1 to start the quest
        if (questStep == 1)
        {
            QuestStep1();
        }
        //It will clear and null out the quest system after an alotted amount of time so that the previous step is shown before clearing out the UI
        else if (Time.time == timerForFinish + timeToFinish && questStep == 2)
        {
            FinishQuest();
        }
    }

    public override void StartQuest()
    {
        //Can be called by the questsystem and will start queststep 1
        if (questSystem.StartQuest(questName, questTextInitial) == false)
        {
            questStep = 1;

            //CALL FLOWCHART BLOCK ONE
            questFlowchart.ExecuteBlock(Step1BlockName);
        }
    }

    private void QuestStep1()
    {
        //if the player is adjacent to angela.
        if (angela.playerTouching == true)
        {
            active = true;

            //If the player interacts while adjacent to angela
            if (angela.playerTouching == true && Input.GetButtonDown("Fire1"))
            {
                //CALL FLOWCHART BLOCK TWO
                questFlowchart.ExecuteBlock(Step2BlockName);

                //Updates to the next part of the quest, updates Angela's proximity speech, and starts the timer to finish.
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
        //Prompts the player to interact if they are adjacent
        PromptPlayer();
    }

    private void PromptPlayer()
    {
        //Prompts the player to interact if they are adjacent
        questSystem.PromptPlayer(prompt1, active);
    }

    private void FinishQuest()
    {
        //Resets the QuestSystem
        questStep = 0;
        questSystem.FinishQuest();
    }
}
