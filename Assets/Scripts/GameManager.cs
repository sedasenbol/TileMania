using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private const int LAST_SCENE_INDEX = 3;
    private const float LEVEL_LOAD_DELAY = 2f;
    private const float LEVEL_EXIT_SLOW_MOTION_FACTOR = 0.2f;
    private GameState gameState = new GameState();
    public void IsPlayerDead()
    {
        if (gameState.Lives == 0)
        {
            gameState.CurrentState = GameState.State.GameOver;
            gameState.IsAlive = false;
            return;
        }
        gameState.CurrentState = GameState.State.IsDead;
        gameState.IsAlive = false;
    }
    private void OnEnable()
    {
        UIManager.PlayButtonClicked += LoadNextScene;
        UIManager.PauseButtonClicked += PauseOrResume;
        Player.PlayerIsDead += IsPlayerDead;
        Player.PickedUpCoin += IncreaseCoins;
        LevelExit.ExitLevel += LoadNextScene;
    }
    private void OnDisable()
    {
        UIManager.PlayButtonClicked -= LoadNextScene;
        UIManager.PauseButtonClicked -= PauseOrResume;
        Player.PlayerIsDead -= IsPlayerDead;
        Player.PickedUpCoin -= IncreaseCoins;
        LevelExit.ExitLevel -= LoadNextScene;
    }
    private void IncreaseCoins()
    {
        gameState.Coins++;
        gameState.Score += 100;
    }
    private void Update()
    {

    }
    private void LoadNextScene()
    {
        int currentScene = (int)gameState.CurrentScene;
        if (currentScene == 0)
        {
            gameState.IsAlive = true;
        }
        if (currentScene != LAST_SCENE_INDEX)
        {
            StartCoroutine(LoadNextLevel());
        }
        else
        {
            gameState.CurrentState = GameState.State.Start;
            gameState.CurrentScene = GameState.Scene.Start;
            SceneManager.LoadScene(0);
        }
    }
    private IEnumerator LoadNextLevel()
    {
        Time.timeScale = LEVEL_EXIT_SLOW_MOTION_FACTOR;
        int currentScene = (int)gameState.CurrentScene;
        yield return new WaitForSecondsRealtime(LEVEL_LOAD_DELAY);
        Time.timeScale = 1f;
        gameState.CurrentScene = (GameState.Scene)currentScene + 1;
        SceneManager.LoadScene(currentScene + 1,LoadSceneMode.Additive);
        Destroy(GameObject.Find("Grid").gameObject);
        Destroy(GameObject.Find("Stage").gameObject);
        Destroy(GameObject.Find("Cameras").gameObject);
        Destroy(GameObject.Find("Player").gameObject);
    }
    private void Start()
    {
        gameState.Score = 0;
        gameState.Coins = 0;
        gameState.Lives = 3;
        gameState.IsAlive = false;
        gameState.CurrentScene = GameState.Scene.Start;
        gameState.CurrentState = GameState.State.Start;
    }
    private void PauseOrResume()
    {
        if (gameState.CurrentState == GameState.State.Paused)
        {
            gameState.CurrentState = GameState.State.OnPlay;
            Time.timeScale = 1;
        }
        else
        {
            gameState.CurrentState = GameState.State.Paused;
            Time.timeScale = 0;
        }
    }
    public GameState StateOfTheGame { get { return gameState; } }
}
