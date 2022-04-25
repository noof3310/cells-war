using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using Pathfinding;

public class BuildManager : MonoBehaviour
{
    public AstarPath astarPath;

    public List<GameObject> towers;
    public List<float> towerCost;

    public Tile[] towerIcons;
    public List<GameObject> UITowers;

    public int selectedTower = 0;

    public Transform tileGridUI;

    public PlayerMovement player;

    public bool isSelected = false;

    public int handItem = 0;

    public bool inHand = false;

    public Transform handGridUI;

    private GameObject UIHand;

    // Start is called before the first frame update
    void Start()
    {
        UIHand = new GameObject("UI Tower");
        UIHand.transform.parent = handGridUI;
        UIHand.transform.localScale = new Vector3(1f, 1f, 1f);

        Image UIImage = UIHand.AddComponent<Image>();
        UIImage.sprite = towerIcons[0].sprite;

        Color tileColor = UIImage.color;
        tileColor.a = 0f;

        UIImage.color = tileColor;

        int i = 0;
        foreach (Tile towerIcon in towerIcons)
        {
            GameObject UITower = new GameObject("UI Tower");
            UITower.transform.parent = tileGridUI;
            UITower.transform.localScale = new Vector3(1f, 1f, 1f);

            UIImage = UITower.AddComponent<Image>();
            UIImage.sprite = towerIcon.sprite;

            tileColor = UIImage.color;
            tileColor.a = 0.5f;

            if (i == selectedTower && isSelected) tileColor.a = 1f;

            UIImage.color = tileColor;

            GameObject UITowerCost = new GameObject("UI Tower Cost");
            UITowerCost.transform.SetParent(UITower.transform, false);
            UITowerCost.transform.localScale = new Vector3(1f, 1f, 1f);

            Text UIText = UITowerCost.AddComponent<Text>();
            UIText.text = towerCost[i].ToString();
            UIText.alignment = TextAnchor.LowerRight;
            UIText.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
            UIText.fontSize = 48;

            UITowers.Add(UITower);

            i++;
        }
    }

    // Update is called once per frame
    async void Update()
    {
        Vector3 playerPos = player.getPosition();
        Vector3Int centerTowerPos = Vector3Int.FloorToInt(playerPos);

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
        else if (Input.GetKeyDown(KeyCode.Space))
        {            
            if (isTileAvailable(centerTowerPos) && isSelected && PlayerDetails.whiteBloodCellNumber - towerCost[selectedTower] >= 0)
            {
                Instantiate(towers[selectedTower], centerTowerPos, Quaternion.identity);
                PlayerDetails.BuyTower(towerCost[selectedTower]);
                updatePath(centerTowerPos);
                SoundManager.playSound("place");
            }
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(player.getPosition(), 0.01f);
            foreach (var collider in colliders)
            {
                var go = collider.gameObject;
                if (go.tag == "Tower" && !collider.isTrigger)
                {
                    go.GetComponent<Tower>().SetDied(true);
                    PlayerDetails.SellTower(towerCost[int.Parse(go.name.Substring(6, 1))]);
                    break;
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.Z))
        {
            if (isTileAvailable(centerTowerPos))
            {
                if (inHand)
                {
                    inHand = false;
                    RenderUIHand();
                    Instantiate(towers[handItem], centerTowerPos, Quaternion.identity);
                    updatePath(centerTowerPos);
                }
            }
            else
            {
                if (!inHand)
                {
                    Collider2D[] colliders = Physics2D.OverlapCircleAll((Vector3)centerTowerPos, 0.01f);
                    foreach (var collider in colliders)
                    {
                        var go = collider.gameObject;
                        if (go.tag == "Tower" && !collider.isTrigger)
                        {
                            go.GetComponent<Tower>().SetDied(true);
                            handItem = int.Parse(go.name.Substring(6, 1));
                            break;
                        }
                    }
                    inHand = true;
                    isSelected = false;
                    RenderUITowers(0);
                    RenderUIHand();
                }
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
        Image UIImage = UIHand.GetComponent<Image>();
        Color tileColor = UIImage.color;

        if (inHand) {
            tileColor.a = 1f;
            UIImage.sprite = towerIcons[handItem].sprite;
        }
        else {
            tileColor.a = 0f;
        }

        UIImage.color = tileColor;
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
    bool isTileAvailable(Vector3Int centerTowerPos)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll((Vector3)centerTowerPos, 0.01f);
        foreach (var collider in colliders)
        {
            var go = collider.gameObject;
            if (go.tag == "Tower" && !collider.isTrigger)
            {
                return false;
            }
        }
        return true;
    }
    public void updatePath(Vector3Int centerTowerPos)
    {
        Bounds bounds = new Bounds(centerTowerPos, new Vector3(5f, 5f, 5f));
        var guo = new GraphUpdateObject(bounds);
        guo.updatePhysics = true;
        astarPath.UpdateGraphs(guo);
    }
}
