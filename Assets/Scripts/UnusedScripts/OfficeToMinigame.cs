using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OfficeToMinigame 
{

    public static void StartMiniGame(string sceneToLoad)
    {
            SceneManager.LoadScene(sceneToLoad);
    }
}
