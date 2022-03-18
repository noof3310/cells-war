using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    // Start is called before the first frame update
    public Slider slider;

    void Start(){
        // if(Objective.hp.GetMaxHealth() != null){
        //     SetMaxHealthBar(Objective.hp.GetMaxHealth());
        // }
        // if(Objective.hp.GetHealth() != null){
        //     SetHealthBar(Objective.hp.GetHealth());
        // }
    }
    public void SetMaxHealthBar(int health){
        slider.maxValue = health;
    }

    public void SetHealthBar(int health){
        slider.value = health;
    }
}
