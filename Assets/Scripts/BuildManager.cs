using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using Pathfinding;

public class BuildManager : MonoBehaviour
{
    public AstarPath astarPath;

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

    public bool isSelected = false;

    public int handItem = 0;

    public bool inHand = false;

    public Transform handGridUI;

    public List<GameObject> UIHands;

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

            if (i == selectedTower && isSelected) tileColor.a = 1f;

            UIImage.color = tileColor;
            UITowers.Add(UITower);

            i++;
        }
        foreach (Tile towerIcon in towerIcons)
        {
            GameObject UIHand = new GameObject("UI Tower");
            UIHand.transform.parent = handGridUI;
            UIHand.transform.localScale = new Vector3(1f, 1f, 1f);

            Image UIImage = UIHand.AddComponent<Image>();
            UIImage.sprite = towerIcon.sprite;

            Color tileColor = UIImage.color;
            tileColor.a = 0f;

            UIImage.color = tileColor;
            UIHands.Add(UIHand);
            if(i==5) break;
        }
    }

    // Update is called once per frame
    async void Update()
    {
        targetTilemap.SetTile(targetTilemap.WorldToCell(targetPos), null);

        Vector3 towerPos = GetTowerPos();
        Vector3Int centerTowerPosInt = Vector3Int.FloorToInt(towerPos);
        Vector3 centerTowerPos = tilemap.GetCellCenterLocal(centerTowerPosInt);
        Bounds bounds = new Bounds(centerTowerPos, new Vector3(3f, 3f, 3f));

        if (isSelected || inHand){
            targetTilemap.SetTile(targetTilemap.WorldToCell(towerPos), target);
        }
        targetPos = towerPos;

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            RenderUITowers(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            RenderUITowers(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            RenderUITowers(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            RenderUITowers(3);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            RenderUITowers(4);
        }
        else if (Input.GetKeyDown(KeyCode.Space) && isTileAvailable(towerPos))
        {
            if (isSelected)
            {
                tilemap.SetTile(tilemap.WorldToCell(towerPos), towers[selectedTower]);
                updatePath(bounds);
            }
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            tilemap.SetTile(tilemap.WorldToCell(towerPos), null);
            updatePath(bounds);
        }
        else if (Input.GetKeyDown(KeyCode.Z))
        {
            if(!isTileAvailable(towerPos) && isSelected) {
                for (int i = 0; i < 5; i++) {
                    if (tilemap.GetTile(centerTowerPosInt) == towers[i])
                    {
                        handItem = i;
                    }
                }
                inHand = true;
                isSelected = false;
                RenderUITowers(0);
                RenderUIHand();
                tilemap.SetTile(tilemap.WorldToCell(towerPos), null);
                updatePath(bounds);
            }
            else if(inHand) {
                inHand = false;
                RenderUIHand();
                tilemap.SetTile(tilemap.WorldToCell(towerPos), towers[handItem]);
                updatePath(bounds);
            }
        }

    }

    void RenderUITowers(int selecter)
    {
        if((selectedTower == selecter && isSelected) || inHand) {
            this.isSelected = false;
        }
        else {
            this.selectedTower = selecter;
            this.isSelected = true;
        }
        int i = 0;
        foreach (GameObject tower in UITowers)
        {
            Image UIImage = tower.GetComponent<Image>();
            Color tileColor = UIImage.color;
            tileColor.a = 0.5f;

            if (i == selectedTower && isSelected) tileColor.a = 1f;

            UIImage.color = tileColor;

            i++;
        }
    }

    void RenderUIHand()
    {
        if (inHand) {
            int i = 0;
            foreach (GameObject UIhand in UIHands)
            {
                Image UIImage = UIhand.GetComponent<Image>();
                Color tileColor = UIImage.color;
                tileColor.a = 1f;
                UIImage.sprite = towerIcons[handItem].sprite;
                UIImage.color = tileColor;
                i++;
            }
        }
        else {
            foreach (GameObject UIhand in UIHands)
            {
                Image UIImage = UIhand.GetComponent<Image>();
                Color tileColor = UIImage.color;
                tileColor.a = 0f;
                UIImage.color = tileColor;
            }
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

    void updatePath(Bounds bounds)
    {
        var guo = new GraphUpdateObject(bounds);
        guo.updatePhysics = true;
        astarPath.UpdateGraphs(guo);
    }
}
