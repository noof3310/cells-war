using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public static float currentTime = 0f;

    [SerializeField] Text countdownText;
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
                default:
                    break;

            }
        }
        if (currentTime <= 0 && !GameManager.canChangeState)
        {
            GameManager.canChangeState = true;
        }
        if (currentTime > 0)
        {
            currentTime -= 1 * Time.deltaTime;
            countdownText.text = currentTime.ToString("0");
            GameManager.canChangeState = false;
        }


    }

    public static void SetCurrentTime(float newTime)
    {
        currentTime = newTime;
    }
}
