using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject coreTarget;
    public static GameObject currentTarget;
    public static HP hp;
    public string gameObjectName;
    private int maxHealth = 1000;
    // Start is called before the first frame update
    void Start()
    {
        currentTarget = coreTarget;
        gameObject.name = gameObjectName;
        hp = FindObjectOfType(typeof(HP)) as HP;
        hp.SetMaxHealth(maxHealth);
        hp.SetHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            int resultHp = hp.GetHealth() - 100;
            Debug.Log(resultHp);
            hp.SetHealth(resultHp);
        }
        // if(Enemy_behavior.attackMode){
        //     Debug.Log("Attack !!");
        // }
    }

    // For testing taken damage. It can be hidden.
    // void OnTriggerEnter2D(Collider2D collider)
    // {
    //     if (collider.gameObject.tag == "Player")
    //     {
    //         hp.takenDamage(100);
    //     }
    // }

}
