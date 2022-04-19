using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelText : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Text levelText;
    [SerializeField] int currentLevel;
    void Start()
    {
        currentLevel = GameManager.level;
    }

    // Update is called once per frame
    void Update()
    {
        currentLevel = GameManager.level;
        levelText.text = "Level: " + currentLevel.ToString("0");
    }
}
