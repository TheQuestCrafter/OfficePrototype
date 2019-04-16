using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndCrawlText : MonoBehaviour
{
    [Tooltip("This is the opening crawl text animation")]
    [SerializeField]
    private Animator textCrawl;

    [SerializeField]
    private string dayScene;

    // Update is called once per frame
    void Update()
    {
        if(textCrawl.GetCurrentAnimatorStateInfo(0).IsName("TextCrawlDone")) //if animation clip has ended
        {
            SceneManager.LoadScene(dayScene);
        }
    }
}
