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
        this.statusBar.SetActive(currentTowerActive != null);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null && hit.collider is BoxCollider2D)
            {
                SetCurrentTowerActive(hit.collider.gameObject);
            }
            else
            {
                SetCurrentTowerActive(null);
            }
            this.statusBar.SetActive(currentTowerActive != null);
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
