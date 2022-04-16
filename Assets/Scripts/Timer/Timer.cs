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
        if (currentTime > 0)
        {
            currentTime -= 1 * Time.deltaTime;
            countdownText.text = currentTime.ToString("0");
        }
        if (currentTime <= 0)
        {
            currentTime = 0;
        }
    }

    public static void SetCurrentTime(float newTime)
    {
        currentTime = newTime;
    }
}
