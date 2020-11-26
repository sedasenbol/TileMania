using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text livesText;
    [SerializeField]
    private Text scoreText;
    [SerializeField]
    private Button pauseButton;
    [SerializeField]
    private Button playButton;
    [SerializeField]
    private Button replayButton;
    [SerializeField]
    private Text congratsText;
    [SerializeField]
    private Text gameOverText;
    public delegate void ClickedButton();
    public static event ClickedButton PlayButtonClicked;
    public static event ClickedButton PauseButtonClicked;
    public static event ClickedButton ReplayButtonClicked;
    private GameState gameState;
    private void Start()
    {
        gameState = FindObjectOfType<GameManager>().StateOfTheGame;
    }
    private void Update()
    {
        if(gameState.IsAlive)
        {
            ShowTexts();
        }
    }
    private void ShowTexts()
    {
        livesText.text = "Lives: " + gameState.Lives;
        scoreText.text = gameState.Score.ToString("00000");
    }
    public void StartGame()
    {
        if (PlayButtonClicked != null)
        {
            PlayButtonClicked();
        }
        livesText.gameObject.SetActive(true);
        scoreText.gameObject.SetActive(true);
        playButton.gameObject.SetActive(false);
        pauseButton.gameObject.SetActive(true);
        replayButton.gameObject.SetActive(false);
    }
    public void ReplayGame()
    {
        if (ReplayButtonClicked != null)
        {
            ReplayButtonClicked();
        }
    }
    public void PauseGame()
    {
        if (PauseButtonClicked != null)
        {
            PauseButtonClicked();
        }
    }
    private void OnEnable()
    {
        GameManager.Success += Congrats;
        GameManager.GameOver += GameIsOver;
    }
    private void OnDisable()
    {
        GameManager.Success -= Congrats;
        GameManager.GameOver -= GameIsOver;
    }
    private void Congrats()
    {
        congratsText.text = "Congratulations !"+ System.Environment.NewLine +"Your Score Is: " + gameState.Score.ToString();
        congratsText.gameObject.SetActive(true);
        pauseButton.gameObject.SetActive(false);
        replayButton.gameObject.SetActive(true);
    }
    private void GameIsOver()
    {
        gameOverText.gameObject.SetActive(true);
        pauseButton.gameObject.SetActive(false);
        replayButton.gameObject.SetActive(true);
    }
}