using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossAlert : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject bossAlert;
    private int levelBossSpawn;
    private GameObject textObject;

    void Start()
    {
        levelBossSpawn = GameManager.levelBossSpawn;
    }

    // Update is called once per frame
    void Update()
    {

        if (textObject == null && (GameManager.level) % levelBossSpawn == 0)
        {
            textObject = (GameObject)Instantiate(bossAlert, FindObjectOfType<Canvas>().transform);
        }
        else if (textObject != null && (GameManager.level) % levelBossSpawn != 0)
        {
            Destroy(textObject);
        }
    }
}
