using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusBar : MonoBehaviour
{
    private bool isShown = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GameObject towerManager = GameObject.Find("TowerManager");
        if (towerManager != null)
        {
            GameObject towerObject = towerManager.GetComponent<TowerManager>().GetCurrentTowerActive();
            if (towerObject != null)
            {
                isShown = true;
            }
            else
            {
                isShown = false;
            }
        }
        if (isShown)
        {
            Tower tower = towerManager.GetComponent<TowerManager>().GetCurrentTowerActive().GetComponent<Tower>();
            GameObject atkAmount = gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject;
            GameObject healthAmount = gameObject.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.transform.GetChild(1).gameObject;

            atkAmount.GetComponent<UnityEngine.UI.Text>().text = tower.GetDamage().ToString();
            healthAmount.GetComponent<UnityEngine.UI.Text>().text = string.Format("{0}/{1}", tower.GetCurrentHealth(), tower.GetMaxHealth());
        }
    }
}
