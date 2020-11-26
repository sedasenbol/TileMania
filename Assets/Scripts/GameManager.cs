using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private const int LAST_SCENE_INDEX = 4;
    private const float LEVEL_LOAD_DELAY = 2f;
    private const float LEVEL_EXIT_SLOW_MOTION_FACTOR = 0.2f;
    private Vector3 waterStartPos = new Vector3(0, -6.3f, 0);
    private GameState gameState = new GameState();
    private GameObject waterPrefab;
    public delegate void EndOfTheGame();
    public static event EndOfTheGame Success;
    public static event EndOfTheGame GameOver;
    public void IsPlayerDead()
    {
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
        gameState.Lives--;
        if ((int)gameState.CurrentScene==4)
        {
            Destroy(GameObject.Find("Water"));
            Instantiate(waterPrefab, waterStartPos,Quaternion.identity);
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
            if (Success != null)
            {
                Success();
            }
            Time.timeScale = 0;
        }
    }
    private IEnumerator LoadNextLevel()
    {
        Time.timeScale = LEVEL_EXIT_SLOW_MOTION_FACTOR;
        int currentScene = (int)gameState.CurrentScene;
        yield return new WaitForSecondsRealtime(LEVEL_LOAD_DELAY);
        Time.timeScale = 1f;
        gameState.CurrentScene = (GameState.Scene)(currentScene + 1);
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
        waterPrefab = Resources.Load<GameObject>("Prefabs/Water");
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
