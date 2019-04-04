using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenuFunctions : MonoBehaviour
{
    [Tooltip("This int transfers you to the sceneIndex with that number")]
    [SerializeField]
    private int sceneSelected = 1;
    [Tooltip("Gives the event system to the script to find buttons.")]
    [SerializeField]
    private EventSystem eventSystem;
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip selectedClip;
    [SerializeField]
    private GameObject firstButton;

    private int firstSelected;

    void Start()
    {
        firstSelected = 0;
    }

    public void PlaySelectSound()
    {
        if (firstButton == eventSystem.currentSelectedGameObject && firstSelected == 0)
        {
            firstSelected++;
        }
        else
        {
            Debug.Log("Sound Played");
            audioSource.PlayOneShot(selectedClip);
        }
    }

    public void MainMenuSceneSelect()
    {
        SceneManager.LoadScene(sceneSelected);
    }

    public void EndGame()
    {
        Application.Quit();
    }
}
