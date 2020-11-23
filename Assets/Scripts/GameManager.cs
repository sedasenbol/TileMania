using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameState gameState = new GameState();

    public void IsPlayerDead()
    {
        gameState.CurrentState = GameState.State.IsDead;
        gameState.IsAlive = false;
    }
    private void Start()
    {

    }
    private void Update()
    {
        
    }
    public GameState StateOfTheGame { get { return gameState; } }
}
