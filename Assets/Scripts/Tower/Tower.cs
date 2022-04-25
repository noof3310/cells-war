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
    public TowerBuff typeOfTowerBuff;

    public string gameObjectName;
    public float chanceForBuff = 0.3f;
    public int maximumBuffNumber = 3;
    [SerializeField] private float currentHealth;
    private bool died;
    public List<TowerBuff> towerBuffs;

    // Start is called before the first frame update
    private void Awake()
    {
        gameObject.name = gameObjectName;
        died = false;
        damage = baseDamage;
        maxHealth = baseMaxHealth;
        buffedTimer = baseTimer;
        timer = baseTimer;
        SetCurrentHealth(baseMaxHealth);
        if (isBuffTower)
        {
            RandomBuff();
        }
    }
    void Start()
    {


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
        TowerBuff buff = (TowerBuff)Random.Range(1, System.Enum.GetValues(typeof(TowerBuff)).Length);
        typeOfTowerBuff = buff;
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
        Debug.Log(this.gameObjectName + " " + buff);
        switch (buff)
        {
            case TowerBuff.Attack:
                damage = baseDamage * 2;
                break;
            case TowerBuff.Speed:
                timer = baseTimer / 2;
                break;
            case TowerBuff.Hp:
                if (currentHealth + (baseMaxHealth * 2) >= maxHealth + (baseMaxHealth * 2))
                {
                    currentHealth = baseMaxHealth * 2;

                }
                else
                {
                    currentHealth += baseMaxHealth * 2;

                }
                maxHealth = baseMaxHealth * 2;

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
                    timer = baseTimer;
                    break;
                case TowerBuff.Hp:
                    maxHealth = baseMaxHealth;
                    currentHealth -= baseMaxHealth;
                    break;
            }
        }

    }
}

public enum TowerBuff
{
    Unknown,
    Attack,
    Speed,
    Hp
}