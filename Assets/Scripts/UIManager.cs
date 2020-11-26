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
    public delegate void ClickedButton();
    public static event ClickedButton PlayButtonClicked;
    public static event ClickedButton PauseButtonClicked;
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
    public void PauseGame()
    {
        if (PauseButtonClicked != null)
        {
            PauseButtonClicked();
        }
    }
}