using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective : MonoBehaviour
{
    // Start is called before the first frame update
    public static HealthBar healthBar;
    public int maxHealth = 1000;
    private int currentHealth;
    void Start()
    {
        SetCurrentHealth(maxHealth);
        healthBar = FindObjectOfType(typeof(HealthBar)) as HealthBar;
        healthBar.SetMaxHealthBar(maxHealth);
        healthBar.SetHealthBar(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        // if(Input.GetKeyDown(KeyCode.Space)){
        //     TakenDamage(100);
        // }
        // hp.SetHealth(currentHealth);
        // if(hp.GetHealth() < 900){
        //     Debug.Log("HP Less than 900");
        // }
    }

    public void SetCurrentHealth(int value)
    {
        currentHealth = value;
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    public void TakenDamage(int value)
    {
        int resultHp = currentHealth - value;
        healthBar.SetHealthBar(resultHp);
        Debug.Log(resultHp);
        SetCurrentHealth(resultHp);
    }
}
