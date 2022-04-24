using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBuffUIManager : MonoBehaviour
{
    public List<Image> prefabUI;
    public List<Image> uiUse = new List<Image>();
    private bool isDraw = false;
    // Start is called before the first frame update
    void Start()
    {

        // for (int i = 0; i < gameObject.GetComponent<Enemy>().enemyBuffs.Count; i++)
        // {
        //     switch (GetComponent<Enemy>().enemyBuffs[i])
        //     {
        //         case EnemyBuff.Attack:
        //             uiUse.Add(Instantiate(prefabUI[0], FindObjectOfType<Canvas>().transform).GetComponent<Image>());
        //             break;
        //         case EnemyBuff.Speed:
        //             uiUse.Add(Instantiate(prefabUI[1], FindObjectOfType<Canvas>().transform).GetComponent<Image>());
        //             break;
        //         case EnemyBuff.Hp:
        //             uiUse.Add(Instantiate(prefabUI[2], FindObjectOfType<Canvas>().transform).GetComponent<Image>());
        //             break;

        //     }

        // }
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.GetComponent<Enemy>().enemyBuffs != null && !isDraw)
        {
            isDraw = true;
            for (int i = 0; i < gameObject.GetComponent<Enemy>().enemyBuffs.Count; i++)
            {
                switch (gameObject.GetComponent<Enemy>().enemyBuffs[i])
                {
                    case EnemyBuff.Attack:
                        uiUse.Add((Image)Instantiate(prefabUI[0], FindObjectOfType<Canvas>().transform).GetComponent<Image>());
                        break;
                    case EnemyBuff.Speed:
                        uiUse.Add((Image)Instantiate(prefabUI[1], FindObjectOfType<Canvas>().transform).GetComponent<Image>());
                        break;
                    case EnemyBuff.Hp:
                        uiUse.Add((Image)Instantiate(prefabUI[2], FindObjectOfType<Canvas>().transform).GetComponent<Image>());
                        break;

                }

            }
        }
        for (int i = 0; i < uiUse.Count; i++)
        {
            Vector3 offsetX = new Vector3(0.5f * i, 0.5f, 0);
            uiUse[i].transform.position = Camera.main.WorldToScreenPoint(transform.Find("Head").gameObject.transform.position + offsetX);

        }

    }
}
