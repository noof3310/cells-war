using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float baseMaxHealth = 100;
    public float baseDamage;
    public float baseTimer;
    public float moveSpeed;
    private float maxHealth;
    [SerializeField] private float damage;
    [SerializeField] private float currentHealth = 100f;
    private float timer;

    public string gameObjectName;
    public float chanceForBuff = 0.3f;
    public int maximumBuffNumber = 3;
    private bool died;
    public List<EnemyBuff> enemyBuffs;

    public float hpLevelUpRatio = 0.2f;
    public float damageLevelUpRatio = 0.2f;
    public float timerLevelUpRatio = 0.2f;
    private EnemyHealthBar healthBar;


    // Start is called before the first frame update
    void Awake()
    {
        gameObject.name = gameObjectName;
        healthBar = gameObject.GetComponent(typeof(EnemyHealthBar)) as EnemyHealthBar;

        died = false;
        damage = baseDamage;
        maxHealth = baseMaxHealth;
        timer = baseTimer;
        // LevelPowerUp();
        SetCurrentHealth(baseMaxHealth);
        SetMaxHealth(baseMaxHealth);
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

        for (int i = 0; i < maximumBuffNumber; i++)
        {
            float randomInt = Random.Range(0f, 1f);
            if (randomInt <= chanceForBuff)
            {

                EnemyBuff buff = (EnemyBuff)Random.Range(0, System.Enum.GetValues(typeof(EnemyBuff)).Length);
                enemyBuffs.Add(buff);
                switch (buff)
                {
                    case EnemyBuff.Attack:
                        damage += baseDamage;
                        break;
                    case EnemyBuff.Hp:
                        SetMaxHealth(maxHealth + (baseMaxHealth / 2));
                        SetCurrentHealth(maxHealth + (baseMaxHealth / 2));
                        break;
                    case EnemyBuff.Speed:
                        timer += baseTimer / 3;
                        break;
                }
            }
        }
    }

    void LevelPowerUp()
    {
        int currentLevel = GameManager.level;
        SetDamage(damage + Mathf.Pow(1 + damageLevelUpRatio, currentLevel) * baseDamage);
        SetCurrentHealth(Mathf.Pow(1 + hpLevelUpRatio, currentLevel) * baseMaxHealth);
        SetMaxHealth(Mathf.Pow(1 + hpLevelUpRatio, currentLevel) * baseMaxHealth);
        SetTimer(Mathf.Pow(1 + timerLevelUpRatio, currentLevel) * baseTimer);
    }

    // For testing taken damage. It can be hidden.
    // void OnTriggerEnter2D(Collider2D collider)
    // {
    //     if (collider.gameObject.tag == "Player")
    //     {
    //         hp.takenDamage(100);
    //     }
    // }

    public void SetCurrentHealth(float value)
    {
        healthBar.SetHealthBar(value);
        currentHealth = value;
    }

    public float GetCurrentHealth()
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
    public void SetDamage(float value)
    {
        damage = value;
    }

    public float GetDamage()
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

    public float GetMaxHealth()
    {
        return maxHealth;
    }
    public void SetMaxHealth(float value)
    {
        healthBar.SetMaxHealthBar(value);
        maxHealth = value;
    }

    public void TakenDamage(float value)
    {
        float resultHp = currentHealth - value;
        SetCurrentHealth(resultHp);
    }


}

public enum EnemyBuff
{
    Attack,
    Speed,
    Hp
}