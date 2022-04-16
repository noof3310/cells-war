using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthBar : MonoBehaviour
{
    private int maxHealth;
    private int curHealth;

    public float healthBarLength;
    private Enemy enemy;


    // Use this for initialization
    void Start()
    {
        enemy = gameObject.GetComponent(typeof(Enemy)) as Enemy;
        curHealth = enemy.GetCurrentHealth();
        maxHealth = enemy.GetMaxHealth();
        healthBarLength = Screen.width / 6;
    }

    // Update is called once per frame
    void OnGUI()
    {

        Vector2 targetPos;
        targetPos = Camera.main.WorldToScreenPoint(transform.position);

        GUI.Box(new Rect(targetPos.x, Screen.height - targetPos.y, 60, 20), curHealth + "/" + maxHealth);

    }

    public void AddjustCurrentHealth(int adj)
    {
        curHealth += adj;

        if (curHealth < 0)
            curHealth = 0;

        if (curHealth > maxHealth)
            curHealth = maxHealth;

        if (maxHealth < 1)
            maxHealth = 1;

        healthBarLength = (Screen.width / 6) * (curHealth / (float)maxHealth);
    }
}
