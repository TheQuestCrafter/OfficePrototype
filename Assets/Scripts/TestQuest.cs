using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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
