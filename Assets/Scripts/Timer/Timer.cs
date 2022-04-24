using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public static float currentTime = 0f;

    [SerializeField] Text countdownText;
    private string state;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (currentTime <= 0 && GameManager.canChangeState)
        {
            GameManager.canChangeState = false;
            switch (GameManager.State)
            {
                case GameState.RestState:
                    GameManager.UpdateGameState(GameState.SpawnState);
                    break;
                case GameState.RushState:
                    GameManager.UpdateGameState(GameState.RestState);
                    break;
                case GameState.SpawnState:
                    countdownText.text = "Fight!!";
                    GameManager.UpdateGameState(GameState.FightState);
                    break;
                default:
                    break;

            }
        }
        if (GameManager.State == GameState.FightState || GameManager.State == GameState.SpawnState) state = "Fight!!";
        if (GameManager.State == GameState.RestState) state = "Rest: ";
        if (GameManager.State == GameState.RushState) state = "Rush: ";

        if (currentTime <= 0 && !GameManager.canChangeState)
        {
            GameManager.canChangeState = true;
        }
        if (currentTime > 0)
        {
            currentTime -= 1 * Time.deltaTime;
            countdownText.text = state + currentTime.ToString("0");
            GameManager.canChangeState = false;
        }


    }

    public static void SetCurrentTime(float newTime)
    {
        currentTime = newTime;
    }
}
