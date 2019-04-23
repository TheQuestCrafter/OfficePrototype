using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    private Text text;
    private bool Opaque = true;

    [Tooltip("This int transfers you to the sceneIndex with that number")]
    [SerializeField]
    private int sceneSelected = 1;

    [SerializeField]
    private Color color1;

    [SerializeField]
    private Color color2;

    [SerializeField]
    private float divisor = 50;

    void Awake()
    {
        text = GetComponent<Text>();
        text.color = color1;
    }

    private void FixedUpdate()
    {
        SetColor2();
        SetColor1();
        DetectInputs();
    }

    private void DetectInputs()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        else if (Input.anyKeyDown)
        {
            SceneManager.LoadScene(sceneSelected);
        }
    }

    private void SetColor1()
    {
        if (Opaque)
        {
            if (text.color.a == 1)
            {
                Opaque = false;
                Debug.Log("Is opaque");
            }
            else
            {
                text.color += color1/divisor;
            }
        }
    }

    private void SetColor2()
    {
        if (!Opaque)
        {
            if (text.color.a <= 0)
            {
                Opaque = true;
                Debug.Log("Is not opaque");
            } 
            else
            {
                text.color -= color2 / divisor;
            }
        }
    }

}
