using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerBuffManager : MonoBehaviour
{
    // Start is called before the first frame update
    public List<Image> prefabUI;
    public List<Image> uiUse = new List<Image>();
    private bool isDraw = false;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.GetComponent<Tower>().towerBuffs != null && !isDraw)
        {
            isDraw = true;
            for (int i = 0; i < gameObject.GetComponent<Tower>().towerBuffs.Count; i++)
            {
                switch (gameObject.GetComponent<Tower>().towerBuffs[i])
                {
                    case TowerBuff.Attack:
                        uiUse.Add((Image)Instantiate(prefabUI[0], FindObjectOfType<Canvas>().transform).GetComponent<Image>());
                        break;
                    case TowerBuff.Speed:
                        uiUse.Add((Image)Instantiate(prefabUI[1], FindObjectOfType<Canvas>().transform).GetComponent<Image>());
                        break;
                    case TowerBuff.Hp:
                        uiUse.Add((Image)Instantiate(prefabUI[2], FindObjectOfType<Canvas>().transform).GetComponent<Image>());
                        break;

                }

            }
        }
        for (int i = 0; i < uiUse.Count; i++)
        {
            Vector3 offsetX = new Vector3(0.5f * i, 0.5f, 0);
            uiUse[i].transform.position = Camera.main.WorldToScreenPoint(gameObject.transform.position + offsetX);

        }
    }
}
