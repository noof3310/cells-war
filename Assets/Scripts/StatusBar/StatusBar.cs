using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusBar : MonoBehaviour
{
    private bool isShown = false;
    // Start is called before the first frame update
    private GameObject towerObject = null;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isShown)
        {
            Tower tower = towerObject.GetComponent<Tower>();
            GameObject atkAmount = gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject;
            GameObject healthAmount = gameObject.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.transform.GetChild(1).gameObject;

            atkAmount.GetComponent<UnityEngine.UI.Text>().text = tower.GetDamage().ToString();
            healthAmount.GetComponent<UnityEngine.UI.Text>().text = string.Format("{0}/{1}", tower.GetCurrentHealth(), tower.GetMaxHealth());

            GameObject levelUpCost = gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject;
            GameObject sellCost = gameObject.transform.GetChild(1).gameObject.transform.GetChild(1).gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject;

            levelUpCost.GetComponent<UnityEngine.UI.Text>().text = string.Format("+{0}", tower.GetLevelUpCost());
            sellCost.GetComponent<UnityEngine.UI.Text>().text = string.Format("-{0}", tower.GetSellCost());
        }
    }

    public void Show(GameObject towerObject)
    {
        if (towerObject != null)
        {
            SetTowerObject(towerObject);
            isShown = true;
            gameObject.SetActive(isShown);
        }
    }

    public void Hide()
    {
        SetTowerObject(null);
        isShown = false;
        gameObject.SetActive(isShown);
    }

    void SetTowerObject(GameObject towerObject)
    {
        this.towerObject = towerObject;
    }

    GameObject GetTowerObject() { return gameObject; }


}
