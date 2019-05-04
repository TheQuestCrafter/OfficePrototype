using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Spawns the Ball for the first time
/// Updates Score and displays Win Text
/// </summary>
public class PongManager : MonoBehaviour
{
    [SerializeField]
    private Ball ball;
    [SerializeField]
    private GameObject ballObj;
    private GameObject cloneBall;
    private Paddle paddle;
    private GlobalInformation globalInformation;

    public int score = 0;
    public int winScore;
    public int lives;

    [SerializeField]
    private Text scoreText;
    [SerializeField]
    private Text livesText;
    [SerializeField]
    private Text failText;
    [SerializeField]
    private Text winText;
    

    // Start is called before the first frame update
    void Start()
    {
        ball = GetComponent<Ball>();        
        StartCoroutine(WaitToStart());
        failText.enabled = false;
        winText.enabled = false;
        globalInformation = FindObjectOfType<GlobalInformation>();
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score: " + score.ToString();
        livesText.text = "Lives: " + lives.ToString();
        Debug.Log(score.ToString());
        EndGame();
    }

    void IncreaseScore()
    {
        if (ball.hitWall)
        {
            score++;
        }
    }

    void EndGame()
    {
        if (score >= winScore)
        {
            winText.enabled = true;
            Destroy(cloneBall, 0f);
            StartCoroutine(LeaveScene());
            globalInformation.jimWin = 2;
        }
        if(lives <= 0)
        {
            globalInformation.jimWin = 0;
            failText.enabled = true;
            Destroy(cloneBall, 0f);
            StartCoroutine(LeaveScene());
        }
    }

    IEnumerator WaitToStart()
    {
        yield return new WaitForSeconds(2f);
        cloneBall = Instantiate(ballObj);
    }

    IEnumerator LeaveScene()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("TheOffice");
    }
}