using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    [SerializeField] private LayerMask towerLayer;
    [SerializeField] private GameObject statusBar;

    private GameObject currentTowerActive;
    // Start is called before the first frame update
    void Start()
    {
        currentTowerActive = null;
        // this.statusBar.SetActive(currentTowerActive != null);
        this.statusBar.GetComponent<StatusBar>().Hide();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D[] allHit = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            RaycastHit2D hit = new RaycastHit2D();
            foreach (RaycastHit2D e in allHit)
            {
                if (e.collider is BoxCollider2D)
                {
                    hit = e;
                    break;
                }
            }

            if (hit.collider != null && hit.collider is BoxCollider2D)
            {
                if (currentTowerActive != null)
                {
                    currentTowerActive.GetComponent<SpriteRenderer>().material.color = new Color(1, 1, 1, 1);
                }
                hit.collider.gameObject.GetComponent<SpriteRenderer>().material.color = Color.blue;
                SetCurrentTowerActive(hit.collider.gameObject);
            }
            else
            {
                if (currentTowerActive != null)
                {
                    currentTowerActive.GetComponent<SpriteRenderer>().material.color = new Color(1, 1, 1, 1);
                }
                SetCurrentTowerActive(null);
            }

            if (currentTowerActive != null)
            {

                this.statusBar.GetComponent<StatusBar>().Show(currentTowerActive);
            }
            else
            {
                this.statusBar.GetComponent<StatusBar>().Hide();
            }
        }

    }

    void SetCurrentTowerActive(GameObject tower)
    {
        if (currentTowerActive != null)
        {
            currentTowerActive.GetComponent<Tower>().SetIsSelected(false);
        }
        if (tower != null)
        {
            tower.GetComponent<Tower>().SetIsSelected(true);
        }
        currentTowerActive = tower;
    }

    public GameObject GetCurrentTowerActive()
    {
        return currentTowerActive;
    }
}
