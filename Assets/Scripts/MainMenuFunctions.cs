using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuFunctions : MonoBehaviour
{
    [Tooltip("This int transfers you to the sceneIndex with that number")]
    [SerializeField]
    private int sceneSelected = 1;

    public void MainMenuSceneSelect()
    {
        SceneManager.LoadScene(sceneSelected);
    }

    public void EndGame()
    {
        Application.Quit();
    }
}
