using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState
{
    public enum State
    {
        Start,
        CountDown,
        OnPlay,
        Paused,
        Resuming,
        IsDead,
        Replaying,
        GameOver,
        Restarted,
    }
    public enum Scene
    {
        Start = 0,
        Level1 = 1,
        Level2 = 2,
        Level3 = 3,
        Level4 = 4,
        Level5 = 5,
        Success = 6,    
    }
    private int hearts = 3;
    private int coins = 0;
    private int score = 0;
    private bool isAlive = false;
    private State currentState = State.Start;
    private Scene currentScene = Scene.Start;
    public int Hearts { get { return hearts; } set { hearts = value; } }
    public int Coins { get { return coins; } set { coins = value; } }
    public int Score { get { return score; } set { score = value; } }
    public bool IsAlive { get { return isAlive; } set { isAlive = value; } }
    public State CurrentState { get { return currentState; } set { currentState = value; } }
    public Scene CurrentScene { get { return currentScene; } set { currentScene = value; } }
}
