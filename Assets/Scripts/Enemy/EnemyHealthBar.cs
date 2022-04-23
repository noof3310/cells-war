using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    public Slider healthBar;
    public Vector3 offsetY = new Vector3(0f, 0f, 0);
    private float maxHealth;
    private float curHealth;
    private Slider uiUse;

    public float healthBarLength;
    // private Enemy enemy;


    // Use this for initialization
    void Start()
    {
        // enemy = gameObject.GetComponent(typeof(Enemy)) as Enemy;
        // curHealth = enemy.GetCurrentHealth();
        // maxHealth = enemy.GetMaxHealth();
        // SetMaxHealthBar(100);
        // SetHealthBar(50);
        healthBarLength = Screen.width / 6;
        HpBarGenerator();
    }
    void Update()
    {
        // if (maxHealth != enemy.GetMaxHealth())
        // {
        //     maxHealth = enemy.GetMaxHealth();
        //     SetMaxHealthBar(enemy.GetMaxHealth());
        // }
        // if (curHealth != enemy.GetCurrentHealth())
        // {
        //     Debug.Log(enemy.GetCurrentHealth());
        //     Debug.Log(healthBar.value);
        //     curHealth = enemy.GetCurrentHealth();
        //     SetHealthBar(enemy.GetCurrentHealth());
        // }
        if (uiUse != null)
            uiUse.transform.position = Camera.main.WorldToScreenPoint(transform.Find("Head").gameObject.transform.position + offsetY);

    }

    void HpBarGenerator()
    {
        uiUse = Instantiate(healthBar, FindObjectOfType<Canvas>().transform);
        uiUse.transform.position = Camera.main.WorldToScreenPoint(transform.Find("Head").gameObject.transform.position + offsetY);
    }

    public void SetMaxHealthBar(float health)
    {
        uiUse.maxValue = health;
    }

    public void SetHealthBar(float health)
    {
        Debug.Log(health);
        uiUse.value = health;
    }

}
