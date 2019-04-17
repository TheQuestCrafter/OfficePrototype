using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDayFromIntro : MonoBehaviour
{
    SceneFader sceneFader;

    [SerializeField]
    public string sceneDayToLoad;

    [SerializeField]
    private AudioSource doorOpen;

    private void Start()
    {
        sceneFader = GetComponent<SceneFader>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            doorOpen.Play();
            sceneFader.FadeTo(sceneDayToLoad);
        }
    }
}
