using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeisureSuitScoreKeeper : MonoBehaviour
{
    private Text scoreText;
    private int scoreCounter;

    // Start is called before the first frame update
    private void Start()
    {
        scoreText = GetComponent<Text>();
        UpdateScore();
    }

    private void UpdateScore()
    {
        //Keep score from going negative
        if (scoreCounter <= 0)
        {
            scoreCounter = 0;
            scoreText.text = $"Score: {scoreCounter}";
        }
        else
        {
            scoreText.text = $"Score: {scoreCounter}";
        }
    }

    public void DecreaseScore()
    {
        scoreCounter--;
        UpdateScore();
    }

    public void IncreaseScore()
    {
        scoreCounter++;
        UpdateScore();
    }
}
