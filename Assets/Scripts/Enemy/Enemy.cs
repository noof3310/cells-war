using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int baseMaxHealth = 100;
    public int baseDamage;
    public float baseTimer;
    public float moveSpeed;
    private int maxHealth;
    private int damage;
    private float timer;

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
        damage = baseDamage;
        maxHealth = baseMaxHealth;
        timer = baseTimer;
        SetCurrentHealth(baseMaxHealth);
        RandomBuff();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.M) && GetCurrentHealth() > 0)
        {
            SetCurrentHealth(GetCurrentHealth() - 10);
        }
    }

    void RandomBuff()
    {
        float randomInt = Random.Range(0f, 1f);
        if (randomInt <= chanceForBuff)
        {
            for (int i = 0; i < maximumBuffNumber; i++)
            {

                EnemyBuff buff = (EnemyBuff)Random.Range(0, System.Enum.GetValues(typeof(EnemyBuff)).Length);
                Debug.Log("Buff: " + buff);
                switch (buff)
                {
                    case EnemyBuff.Attack:
                        damage += baseDamage;
                        break;
                    case EnemyBuff.Hp:
                        maxHealth += baseMaxHealth / 2;
                        SetCurrentHealth(maxHealth);
                        break;
                    case EnemyBuff.Speed:
                        timer += baseTimer / 3;
                        break;
                }
            }
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
    public void SetDamage(int value)
    {
        damage = value;
    }

    public int GetDamage()
    {
        return damage;
    }
    public void SetTimer(float value)
    {
        timer = value;
    }

    public float GetTimer()
    {
        return timer;
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