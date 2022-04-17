using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 1000;
    public int damage;
    public float moveSpeed;
    public float timer;

    public string gameObjectName;
    public float chanceForBuff = 0.3f;
    public int maximumBuffNumber = 3;
    private int currentHealth;
    private bool died;



    // Start is called before the first frame update
    void Start()
    {
        gameObject.name = gameObjectName;
        died = false;
        SetCurrentHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {

        // if (Enemy_behavior.attackMode)
        // {
        //     Debug.Log("Attack !!");
        // }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SetCurrentHealth(0);
        }
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
    public void SetDied(bool value)
    {
        died = value;
    }

    public bool GetDied()
    {
        return died;
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }

    public void TakenDamage(int value)
    {
        int resultHp = currentHealth - value;
        SetCurrentHealth(resultHp);
    }

}

public enum EnemyBuff
{
    Attack,
    Speed,
    Hp
}