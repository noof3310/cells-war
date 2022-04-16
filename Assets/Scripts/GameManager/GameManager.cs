using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static GameState State;
    public static float restTime = 20f;
    public float rushTime = 20f;
    // Start is called before the first frame update

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        UpdateGameState(GameState.StartState);

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
                break;
            case GameState.RestState:
                HandleRestState();
                break;
            case GameState.RushState:
                break;
            case GameState.Victory:
                break;
            case GameState.Lose:
                break;
            default:
                break;
        }
    }

    public static void HandleStartState()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        UpdateGameState(GameState.RestState);
    }
    public static void HandleRestState()
    {
        Timer.SetCurrentTime(restTime);
    }

}

public enum GameState
{
    StartState,
    FightState,
    RestState,
    RushState,
    Victory,
    Lose,
}