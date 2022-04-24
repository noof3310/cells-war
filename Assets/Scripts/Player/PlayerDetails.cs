using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetails : MonoBehaviour
{
    public static float whiteBloodCellNumber = 100;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public static void BuyTower(float cost)
    {
        whiteBloodCellNumber -= cost;
    }
    public static void SellTower(float cost)
    {
        whiteBloodCellNumber += cost / 2;
    }

    public static void ResetAll()
    {
        whiteBloodCellNumber = 0;
    }
}
