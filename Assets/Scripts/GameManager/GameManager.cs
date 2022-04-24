using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static GameState State;
    public static int level = 1;
    public static bool canChangeState = true;
    public static float restTime = 10f;
    public static float rushTime = 10f;
    public static int levelBossSpawn = 3;
    // Start is called before the first frame update

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        UpdateGameState(GameState.SpawnState);

    }

    void Update()
    {
    }

    public static void UpdateGameState(GameState newState)
    {
        State = newState;
        Debug.Log(newState);

        switch (newState)
        {
            case GameState.StartState:
                break;
            case GameState.FightState:
                HandleFightState();
                break;
            case GameState.RestState:
                HandleRestState();
                break;
            case GameState.RushState:
                HandleRushState();
                break;
            case GameState.Victory:
                break;
            case GameState.Lose:
                HandleLoseState();
                break;
            default:
                break;
        }
    }

    public static void HandleStartState()
    {
        SceneManager.LoadScene(1);
        UpdateGameState(GameState.RushState);
    }
    public static void HandleRestState()
    {
        Timer.SetCurrentTime(restTime);
    }
    public static void HandleRushState()
    {
        Timer.SetCurrentTime(rushTime);
    }
    public static void HandleFightState()
    {
    }
    public static void HandleLoseState()
    {

        level = 1;
        canChangeState = true;
        PlayerDetails.ResetAll();
        Objective.ResetAll();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        UpdateGameState(GameState.StartState);
    }


    public static GameState GetCurrentGameState()
    {
        return State;
    }

}

public enum GameState
{
    StartState,
    FightState,
    SpawnState,
    RestState,
    RushState,
    Victory,
    Lose,
}