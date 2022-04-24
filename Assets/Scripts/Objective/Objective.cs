using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective : MonoBehaviour
{
    // Start is called before the first frame update
    public static HealthBar healthBar;
    public static float maxHealth = 10000;
    private static float currentHealth;
    private static bool died;
    void Start()
    {
        ResetAll();
    }

    // Update is called once per frame
    void Update()
    {
        if (GetCurrentHealth() <= 0)
        {
            died = true;
            GameManager.UpdateGameState(GameState.Lose);
        }
        // if (Input.GetKeyDown(KeyCode.Space))
        // {
        //     TakenDamage(100);
        // }
        // hp.SetHealth(currentHealth);
        // if(hp.GetHealth() < 900){
        //     Debug.Log("HP Less than 900");
        // }
    }

    public static void SetCurrentHealth(float value)
    {
        currentHealth = value;
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    public void TakenDamage(float value)
    {
        float resultHp = currentHealth - value;
        healthBar.SetHealthBar(resultHp);
        Debug.Log(value);
        Debug.Log(resultHp);
        SetCurrentHealth(resultHp);
    }

    public static void ResetAll()
    {
        SetCurrentHealth(maxHealth);
        healthBar = FindObjectOfType(typeof(HealthBar)) as HealthBar;
        healthBar.SetMaxHealthBar(maxHealth);
        healthBar.SetHealthBar(maxHealth);
        died = false;
    }
}
