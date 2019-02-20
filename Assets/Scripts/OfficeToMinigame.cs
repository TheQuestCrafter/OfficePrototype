using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OfficeToMinigame : MonoBehaviour
{
    [SerializeField]
    private int sceneNum;
    Scene scene = new Scene();
    private Collider2D ProximityCheck = new Collider2D();
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(sceneNum);
        }
    }
}
