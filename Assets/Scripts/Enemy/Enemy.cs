using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 1000;
    public string gameObjectName;
    private int currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.name = gameObjectName;
        SetCurrentHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {

        // if (Enemy_behavior.attackMode)
        // {
        //     Debug.Log("Attack !!");
        // }
    }

    // For testing taken damage. It can be hidden.
    // void OnTriggerEnter2D(Collider2D collider)
    // {
    //     if (collider.gameObject.tag == "Player")
    //     {
    //         hp.takenDamage(100);
    //     }
    // }

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
        SetCurrentHealth(resultHp);
    }

}
