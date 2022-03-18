using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class BuildManager : MonoBehaviour
{
    public Tilemap tilemap;
    public Tilemap targetTilemap;
    public Tile[] towers;
    public Tile[] towerIcons;
    public List<GameObject> UITowers;

    public int selectedTower = 0;

    public Transform tileGridUI;

    public PlayerMovement player;

    public Tile target;
    Vector3 targetPos;

    // Start is called before the first frame update
    void Start()
    {
        int i = 0;
        foreach (Tile towerIcon in towerIcons)
        {
            GameObject UITower = new GameObject("UI Tower");
            UITower.transform.parent = tileGridUI;
            UITower.transform.localScale = new Vector3(1f, 1f, 1f);

            Image UIImage = UITower.AddComponent<Image>();
            UIImage.sprite = towerIcon.sprite;

            Color tileColor = UIImage.color;
            tileColor.a = 0.5f;

            if (i == selectedTower) tileColor.a = 1f;

            UIImage.color = tileColor;
            UITowers.Add(UITower);

            i++;
        }

        targetPos = GetTowerPos();
    }

    // Update is called once per frame
    void Update()
    {
        targetTilemap.SetTile(targetTilemap.WorldToCell(targetPos), null);
        Vector3 towerPos = GetTowerPos();
        targetTilemap.SetTile(targetTilemap.WorldToCell(towerPos), target);
        targetPos = towerPos;

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedTower = 0;
            RenderUITowers();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            selectedTower = 1;
            RenderUITowers();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            selectedTower = 2;
            RenderUITowers();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            selectedTower = 3;
            RenderUITowers();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            selectedTower = 4;
            RenderUITowers();
        }
        else if (Input.GetKeyDown(KeyCode.Space) && isTileAvailable(towerPos))
        {
            tilemap.SetTile(tilemap.WorldToCell(towerPos), towers[selectedTower]);
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            tilemap.SetTile(tilemap.WorldToCell(towerPos), null);
        }

        Debug.Log(tilemap.GetTile(Vector3Int.FloorToInt(towerPos)));
    }

    void RenderUITowers()
    {
        int i = 0;
        foreach (GameObject tower in UITowers)
        {
            Image UIImage = tower.GetComponent<Image>();
            Color tileColor = UIImage.color;
            tileColor.a = 0.5f;

            if (i == selectedTower) tileColor.a = 1f;

            UIImage.color = tileColor;

            i++;
        }
    }

    Vector3 GetTowerPos()
    {
        Vector3 playerPos = player.getPosition();
        string playerDir = player.getDirection();
        Vector3 towerPos = Vector3.zero;

        switch (playerDir)
        {
            case "up":
                towerPos = playerPos + 1.25f * Vector3.up;
                break;
            case "left":
                towerPos = playerPos + 1.25f * Vector3.left;
                break;
            case "down":
                towerPos = playerPos + 1.25f * Vector3.down;
                break;
            case "right":
                towerPos = playerPos + 1.25f * Vector3.right;
                break;
        }

        return towerPos;
    }

    bool isTileAvailable(Vector3 towerPos)
    {
        if (tilemap.GetTile(Vector3Int.FloorToInt(towerPos)) == null) return true;
        return false;
    }
}
