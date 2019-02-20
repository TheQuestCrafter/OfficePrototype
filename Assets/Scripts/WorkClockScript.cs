using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WorkClockScript : MonoBehaviour
{
    public Text WorkClockText;
    string ClockString;
    float WorkClock = 0f;
    int minutes;
    int hours;

    void Update()
    {
       WorkClock += Time.deltaTime * 10; //Sped up for testing

       minutes = (int)(WorkClock % 60);
       hours = (int)(11 + ((WorkClock / 60) % 60));

        UpdateClock();     
    }

    public void UpdateClock()
    {
        if (hours < 12) // AM to PM
        {
            ClockString = string.Format("{0:0}:{1:00} AM", hours, minutes);
        }
        else
        {
            ClockString = string.Format("{0:0}:{1:00} PM", hours, minutes);

        }
        
        //if (hours > 12) //Trying to get hours to roll over from 12 to 1
        //{
        //    hours = (int)(1 + ((WorkClock / 60) % 60));
        //}
        
        WorkClockText.text = ClockString;

        CheckLoadLevel();
    }

    public void CheckLoadLevel()
    {
        if (hours == 15) //Hour limit to trigger next day, Default "9 to 5"
        {
            SceneManager.LoadScene("TheOffice");
        }
    }
}
