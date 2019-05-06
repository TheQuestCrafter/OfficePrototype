using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// This acts as the Game Manager for the LeisureSuit game. However, it hasn't been renamed and refactored to convey that yet due to time constraints.
/// </summary>
public class LeisureSuitScoreKeeper : MonoBehaviour
{
    //Variable for the GameMaster's GlobalInformation script;
    private GlobalInformation GameMasterBrain;
    private Text scoreText, timerText;
    private GameObject winCanvas, loseCanvas;
    //3 strikes (hits) and the player's out
    private int startingHealth = 3, currentHealth;
    private int scoreCounter;
    private float currentTime = 0f;
    private bool hasTimeLeft, isAlive;

    [SerializeField]
    [Tooltip("The scene to change to.")]
    private int sceneNum;

    [SerializeField]
    [Tooltip("The minimum score required to win.")]
    private int minimumScoreToWin;

    [SerializeField]
    [Tooltip("The highest possible score the player can achieve. Reaching this score will cause the game to end.")]
    private int highestPossibleScore;

    [SerializeField]
    [Tooltip("This is the length of time that the game will play for.")]
    private float minigameTimer;

    public bool HasTimeLeft => hasTimeLeft ? currentTime > 0 : currentTime <= 0;
    public bool IsAlive
    {
        get
        {
            if (currentHealth > 0)
                return isAlive = true;
            else
                return isAlive = false;
        }
    }

    private bool leisureSuitGameEndState;
    /// <summary>
    /// Used to check if either the win or lose state canvas objects are active or not
    /// </summary>
    public bool LeisureSuitGameEndState
    {
        get
        {
            //Check all possible conditions that can que a win & loss state
            if (winCanvas.activeSelf == false && loseCanvas.activeSelf == false)
            {
                //TODO: Debug LiesureSuitScoreKeeper
                Debug.Log($"The current value of LeisureSuitEndState is: {leisureSuitGameEndState}");

                return leisureSuitGameEndState = false;
            }
            else
                return leisureSuitGameEndState = true;
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        InitializeLeisureGame();

        //TODO: Debug LiesureSuitScoreKeeper
        Debug.Log($"The current value of LeisureSuitEndState is: {LeisureSuitGameEndState}");
    }

    private void InitializeLeisureGame()
    {
        //Set the Win and Lose state canvas objects to inactive
        winCanvas = GameObject.Find("WinState_Panel");
        winCanvas.SetActive(false);
        loseCanvas = GameObject.Find("LoseState_Panel");
        loseCanvas.SetActive(false);

        GameMasterBrain = GameObject.Find("TheGameMaster").GetComponent<GlobalInformation>();

        scoreText = GetComponent<Text>();
        timerText = GameObject.Find("MiniGameTimerText").GetComponent<Text>();
        scoreCounter = 0;
        currentHealth = startingHealth;
        currentTime = minigameTimer;
        UpdateScore();
    }

    private void Update()
    {
        if (!LeisureSuitGameEndState)
        {
            CheckLeisureSuitMinigameTimer();
            CheckCurrentPlayerHealth();
        }

        //TODO: Debug.Log to display the player's current health
        Debug.Log($"The Player's current health is: {startingHealth}");
        Debug.Log($"The IsAlive bool is: {IsAlive}");
        Debug.Log($"The HasTimeLeft bool is: {HasTimeLeft}");
        Debug.Log($"The current value of LeisureSuitEndState is: {LeisureSuitGameEndState}");
    }

    private void CheckCurrentPlayerHealth()
    {
        isAlive = (currentHealth > 0);

        if (!isAlive)
        {
            //TODO: Add the UI text update code here...
            currentHealth = 0;

            if (scoreCounter >= minimumScoreToWin)
                winCanvas.SetActive(true);
            else
                loseCanvas.SetActive(true);
        }
    }

    /// <summary>
    /// Used to track and display the timer for the Minigame.
    /// </summary>
    private void CheckLeisureSuitMinigameTimer()
    {
        currentTime -= 1 * Time.deltaTime;
        timerText.text = currentTime.ToString("0");
        hasTimeLeft = (currentTime > 0);

        if (!hasTimeLeft)
        {
            //Keeps the text display from going negative
            currentTime = 0;

            if (scoreCounter >= minimumScoreToWin)
                winCanvas.SetActive(true);
            else
                loseCanvas.SetActive(true);
        }
    }

    private void UpdateScore()
    {
        //Keep score from going negative
        if (scoreCounter <= 0)
        {
            scoreCounter = 0;
            scoreText.text = $"Score: {scoreCounter}";
        }
        else if ((0 < scoreCounter) && (scoreCounter < highestPossibleScore))
        {
            scoreText.text = $"Score: {scoreCounter}";
        }
        else if (scoreCounter >= highestPossibleScore)
        {
            //TODO: StopCoroutine(MiniGameTimer());
            scoreText.text = $"Score: {scoreCounter}";
            winCanvas.SetActive(true);
        }
    }

    public void DecreaseScore()
    {
        scoreCounter--;
        currentHealth--;
        UpdateScore();
    }

    public void IncreaseScore()
    {
        scoreCounter++;
        UpdateScore();
    }

    #region End State Button Handler
    public void RetryLeisureSuitGame()
    {
        //Reload the Current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ExitToOffice()
    {
        //TODO: Debug.Log ExitGame Click
        Debug.Log("Exit Button Clicked!");

        if (scoreCounter < minimumScoreToWin || loseCanvas.activeSelf == true)
        {
            //Player HAS NOT met the minimum requirements to win
            GameMasterBrain.larryWin = 0;
        }
        else if (scoreCounter >= minimumScoreToWin && scoreCounter < highestPossibleScore)
        {
            //Player HAS met the minimum requirements to win, but HAS NOT achieved the highest possible score
            GameMasterBrain.larryWin = 1;
        }
        else if (scoreCounter >= highestPossibleScore)
        {
            //Player HAS reached the maximum score obtainable and triggered an auto-win state
            GameMasterBrain.larryWin = 2;
        }
        SceneManager.LoadScene(sceneNum);
        //TODO: Debug.Log ExitGame State
        Debug.Log($"GameMasterBrain.larryWin == {GameMasterBrain.larryWin}");
    }
    #endregion
}
