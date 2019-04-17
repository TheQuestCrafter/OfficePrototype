using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class DayDeliveryQuest : QuestParent
{
    [SerializeField]
    private NPCScript NPC1;
    [SerializeField]
    private NPCScript NPC2;
    [SerializeField]
    private string questStep2Text;
    [SerializeField]
    private string questStep3Text;
    [SerializeField]
    private string updatedNPC1Text;
    [SerializeField]
    private string updatedNPC2Text;
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
    [SerializeField]
    private string Step3BlockName;

    private float timerForFinish;
    private bool active;

    void FixedUpdate()
    {
        //If the QuestStart is called then queststep = 1 to start the quest
        if (questStep == 1)
        {
            QuestStep1();
        }
        if (questStep == 2)
        {
            QuestStep2();
        }
        //It will clear and null out the quest system after an alotted amount of time so that the previous step is shown before clearing out the UI
        else if (Time.time == timerForFinish + timeToFinish && questStep == 3)
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
       if(NPC1.playerTouching == true && Input.GetButtonDown("Fire1"))
        {
            questFlowchart.ExecuteBlock(Step2BlockName);
            NPC1.UpdateProxSpeech(updatedNPC1Text);
            questSystem.UpdateQuest(questStep, questStep2Text);
            questStep++;
        }
    }
    private void QuestStep2()
    {
        if (NPC2.playerTouching == true && Input.GetButtonDown("Fire1"))
        {
            questFlowchart.ExecuteBlock(Step3BlockName);
            NPC2.UpdateProxSpeech(updatedNPC2Text);
            questSystem.UpdateQuest(questStep, questStep3Text);
            questStep++;
        }
    }

    private void PromptPlayer()
    {
        questSystem.PromptPlayer(prompt1, active);
    }

    private void FinishQuest()
    {
        //Resets the QuestSystem
        questStep = 0;
        questSystem.FinishQuest();
    }
}
