using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int sceneCount = 7;
    private GameState gameState = new GameState();
    public void IsPlayerDead()
    {
        if (gameState.Hearts == 0)
        {
            gameState.CurrentState = GameState.State.GameOver;
            gameState.IsAlive = false;
            return;
        }
        gameState.CurrentState = GameState.State.IsDead;
        gameState.IsAlive = false;
    }
    void OnEnable()
    {
        UIManager.OnClickedPlay += LoadNextScene;
    }
    void OnDisable()
    {
        UIManager.OnClickedPlay -= LoadNextScene;
    }
    private void LoadNextScene()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        if (currentScene == 0)
        {
            gameState.IsAlive = true;
        }
        if (currentScene != sceneCount)
        {
            gameState.CurrentScene = (GameState.Scene)currentScene+1;
            SceneManager.LoadScene(currentScene + 1);
        }
        else
        {
            gameState.CurrentState = GameState.State.Start;
            gameState.CurrentScene = GameState.Scene.Start;
            SceneManager.LoadScene(0);
        }
    }
    private void Awake()
    {
        
    }
    private void Start()
    {
        gameState.Score = 0;
        gameState.Coins = 0;
        gameState.Hearts = 3;
        gameState.IsAlive = false;
        gameState.CurrentScene = GameState.Scene.Start;
        gameState.CurrentState = GameState.State.Start;
    }
    private void Update()
    {
        
    }
    public GameState StateOfTheGame { get { return gameState; } }
}
