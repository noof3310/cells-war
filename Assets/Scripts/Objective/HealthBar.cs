using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    // Start is called before the first frame update
    public Slider slider;

    public void SetMaxHealthBar(float health)
    {
        slider.maxValue = health;
    }

    public void SetHealthBar(float health)
    {
        slider.value = health;
    }
}
