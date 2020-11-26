using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public delegate void EndOfTheGame();
    public static event EndOfTheGame Success;
    public static event EndOfTheGame GameOver;
    private const int LAST_SCENE_INDEX = 4;
    private const float SLOW_MOTION_DELAY = 2f;
    private const float SLOW_MOTION_FACTOR = 0.2f;
    private GameObject waterPrefab;
    private Vector3 waterStartPos = new Vector3(0, -6.3f, 0);
    private GameState gameState = new GameState();
    private void IsPlayerDead()
    {
        gameState.Lives--;
        if (gameState.Lives == 0)
        {
            gameState.CurrentState = GameState.State.GameOver;
            gameState.IsAlive = false;
            if (GameOver != null)
            {
                GameOver();
                Time.timeScale = 0;
            }
            return;
        };
        StartCoroutine(DieSlowMotion());
    }
    private IEnumerator DieSlowMotion()
    {
        Time.timeScale = 0.2f;
        yield return new WaitForSecondsRealtime(SLOW_MOTION_DELAY);
        Time.timeScale = 1f;
    }
    private void IncreaseCoins()
    {
        gameState.Coins++;
        gameState.Score += 100;
    }
    private void LoadStartMenu()
    {
        SceneManager.LoadScene(0);
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
            gameState.CurrentState = GameState.State.Success;
            if (Success != null)
            {
                Success();
            }
            Time.timeScale = 0;
        }
    }
    private IEnumerator LoadNextLevel()
    {
        Time.timeScale = SLOW_MOTION_FACTOR;
        int currentScene = (int)gameState.CurrentScene;
        yield return new WaitForSecondsRealtime(SLOW_MOTION_DELAY);
        Time.timeScale = 1f;
        gameState.CurrentScene = (GameState.Scene)(currentScene + 1);
        SceneManager.LoadScene(currentScene + 1,LoadSceneMode.Additive);
        Destroy(GameObject.Find("Grid").gameObject);
        Destroy(GameObject.Find("Stage").gameObject);
        Destroy(GameObject.Find("Cameras").gameObject);
        Destroy(GameObject.Find("Player").gameObject);
    }
    private void PauseOrResume()
    {
        if (gameState.CurrentState == GameState.State.Paused)
        {
            gameState.CurrentState = GameState.State.OnPlay;
            Time.timeScale = 1f;
        }
        else
        {
            gameState.CurrentState = GameState.State.Paused;
            Time.timeScale = 0f;
        }
    }
    private void OnEnable()
    {
        UIManager.PlayButtonClicked += LoadNextScene;
        UIManager.PauseButtonClicked += PauseOrResume;
        UIManager.ReplayButtonClicked += LoadStartMenu;
        Player.PlayerIsDead += IsPlayerDead;
        Player.PickedUpCoin += IncreaseCoins;
        LevelExit.ExitLevel += LoadNextScene;
    }
    private void OnDisable()
    {
        UIManager.PlayButtonClicked -= LoadNextScene;
        UIManager.PauseButtonClicked -= PauseOrResume;
        UIManager.ReplayButtonClicked -= LoadStartMenu;
        Player.PlayerIsDead -= IsPlayerDead;
        Player.PickedUpCoin -= IncreaseCoins;
        LevelExit.ExitLevel -= LoadNextScene;
    }
    private void Start()
    {
        gameState.Score = 0;
        gameState.Coins = 0;
        gameState.Lives = 3;
        gameState.IsAlive = false;
        gameState.CurrentScene = GameState.Scene.Start;
        gameState.CurrentState = GameState.State.Start;
        waterPrefab = Resources.Load<GameObject>("Prefabs/Water");
    }
    public GameState StateOfTheGame { get { return gameState; } }
}
