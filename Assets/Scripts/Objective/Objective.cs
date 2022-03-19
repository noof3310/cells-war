using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective : MonoBehaviour
{
    // Start is called before the first frame update
    public static HP hp;
    public static HealthBar healthBar;
    public int maxHealth = 1000;
    void Start()
    {

        hp = FindObjectOfType(typeof(HP)) as HP;
        healthBar = FindObjectOfType(typeof(HealthBar)) as HealthBar;
        hp.SetMaxHealth(maxHealth);
        hp.SetHealth(maxHealth);
        healthBar.SetMaxHealthBar(maxHealth);
        healthBar.SetHealthBar(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)){
            int resultHp = hp.GetHealth()-100;
            hp.SetHealth(resultHp);
            healthBar.SetHealthBar(resultHp);
        }
        // hp.SetHealth(currentHealth);
        // if(hp.GetHealth() < 900){
        //     Debug.Log("HP Less than 900");
        // }
    }
}
