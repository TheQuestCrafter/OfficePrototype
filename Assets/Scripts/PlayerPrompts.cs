using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrompts
{
    public string prompt;
    public int timer;

    public PlayerPrompts(string StuffToSay, int timeStayOnScreen)
    {
        prompt = StuffToSay;
        timer = timeStayOnScreen;
    }

    public void Tick()
    {
        
        if (timer <= 0)
            prompt = "";
        else
            timer--;
    }
}
