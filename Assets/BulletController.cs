using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{   
    public static BulletController current;
    public int bulletPoolAmount;
    public bool isGrow;

    List<GameObject> bulletList;

    void Awake()
    {
        current = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        bulletList = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject GetBullet(GameObject ItemPrefab, Vector3 pos,GameObject target,float damage)
    {
        Debug.Log(ItemPrefab);
        Debug.Log(pos);
        Debug.Log(damage);
        Debug.Log(target);
        for(int i = 0; i < bulletList.Count; i++)
        {
            if(!bulletList[i].activeInHierarchy)
            {
                // Debug.Log("Use Old one");
                bulletList[i].transform.position = pos;
                bulletList[i].GetComponent<BulletBehavior>().Reuse();
                bulletList[i].GetComponent<BulletBehavior>().SetTarget(target);
                bulletList[i].GetComponent<BulletBehavior>().SetDamage(damage);
                bulletList[i].SetActive(true);
                return bulletList[i];
            }
        }
        if (isGrow || bulletPoolAmount > bulletList.Count)
        {
            // Debug.Log("Use New");
            GameObject obj = Instantiate(ItemPrefab, pos, Quaternion.identity);
            obj.GetComponent<BulletBehavior>().SetTarget(target);
            obj.GetComponent<BulletBehavior>().SetDamage(damage);
            bulletList.Add(obj);
            return obj;
        }
        // Debug.Log("else");
        return null;
    }
}
