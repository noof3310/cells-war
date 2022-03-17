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
        numberOfWhiteBloodCell = 0;
        countText = GetComponent<Text> ();
        setCountText(numberOfWhiteBloodCell);
        // whiteBloodCell.Text = "0";
    }

    // Update is called once per frame
    void Update()
    {
        setCountText(numberOfWhiteBloodCell);

    }
     void setCountText(int count){
         countText.GetComponent<Text> ().text = count.ToString();
    }
}
