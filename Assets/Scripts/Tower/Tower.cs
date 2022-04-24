using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public float baseMaxHealth = 100;
    public float baseDamage;
    public float baseTimer;
    [SerializeField] private float maxHealth;
    [SerializeField] private float damage;
    private float buffedTimer;
    private float timer;

    public bool isBuffTower = false;

    public string gameObjectName;
    public float chanceForBuff = 0.3f;
    public int maximumBuffNumber = 3;
    [SerializeField] private float currentHealth;
    private bool died;
    public List<TowerBuff> towerBuffs;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.name = gameObjectName;
        died = false;
        damage = baseDamage;
        maxHealth = baseMaxHealth;
        buffedTimer = baseTimer;
        timer = baseTimer;
        SetCurrentHealth(baseMaxHealth);
        // RandomBuff();
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

                TowerBuff buff = (TowerBuff)Random.Range(0, System.Enum.GetValues(typeof(TowerBuff)).Length);
                Debug.Log("Buff: " + buff);
                towerBuffs.Add(buff);
                switch (buff)
                {
                    case TowerBuff.Attack:
                        damage += baseDamage;
                        break;
                    case TowerBuff.Hp:
                        maxHealth += baseMaxHealth / 2;
                        SetCurrentHealth(maxHealth);
                        break;
                    case TowerBuff.Speed:
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

    public void SetCurrentHealth(float value)
    {
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

    public void TakenDamage(float value)
    {
        float resultHp = currentHealth - value;
        SetCurrentHealth(resultHp);
    }

    public float GetBuffedTimer()
    {
        return buffedTimer;
    }

    public void GetBuffed(TowerBuff buff)
    {
        towerBuffs.Add(buff);
        switch (buff)
        {
            case TowerBuff.Attack:
                damage = baseDamage * 2;
                break;
            case TowerBuff.Speed:
                buffedTimer = baseTimer / 2;
                break;
        }
    }

    public void CancleBuff(TowerBuff buff)
    {
        towerBuffs.Remove(buff);
        if (!towerBuffs.Contains(buff))
        {
            switch (buff)
            {
                case TowerBuff.Attack:
                    damage = baseDamage;
                    break;
                case TowerBuff.Speed:
                    buffedTimer = baseTimer;
                    break;
            }
        }
    }
}

public enum TowerBuff
{
    Attack,
    Speed,
    Hp
}