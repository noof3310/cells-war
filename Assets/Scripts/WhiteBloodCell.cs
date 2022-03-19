using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WhiteBloodCell : MonoBehaviour
{
    // Start is called before the first frame update
    public int numberOfWhiteBloodCell;
    public Text countText;

    // public Text whiteBloodCell = "test";
    void Start()
    {
        numberOfWhiteBloodCell = PlayerDetails.whiteBloodCellNumber;
        setCountText(numberOfWhiteBloodCell);
    }

    // Update is called once per frame
    void Update()
    {
        numberOfWhiteBloodCell = PlayerDetails.whiteBloodCellNumber;
        setCountText(numberOfWhiteBloodCell);

    }
    void setCountText(int count)
    {
        countText.text = count.ToString();
    }
}
