using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP : MonoBehaviour
{
    // Start is called before the first frame update
    public int maxHealth;
    public int currentHealth;

    public void SetMaxHealth(int health){
        maxHealth = health;
    }

    public void SetHealth(int health){
        currentHealth = health;
    }

    public int GetHealth(){
        return currentHealth;
    }

    public int GetMaxHealth(){
        return maxHealth;
    }

    // Update is called once per frame
}
