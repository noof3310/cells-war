using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerHealthBar : MonoBehaviour
{
    private float maxHealth;
    private float curHealth;

    public float healthBarLength;
    private Tower tower;


    // Use this for initialization
    void Start()
    {
        tower = gameObject.GetComponent(typeof(Tower)) as Tower;
        curHealth = tower.GetCurrentHealth();
        maxHealth = tower.GetMaxHealth();
        healthBarLength = Screen.width / 6;
    }
    void Update()
    {
        curHealth = tower.GetCurrentHealth();

    }

    // Update is called once per frame
    void OnGUI()
    {

        Vector2 targetPos;
        targetPos = Camera.main.WorldToScreenPoint(transform.position);

        GUI.Box(new Rect(targetPos.x, Screen.height - targetPos.y, 60, 20), curHealth + "/" + maxHealth);

    }
}
