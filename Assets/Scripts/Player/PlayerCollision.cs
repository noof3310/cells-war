using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("colide");
        if (collider.gameObject.name == "whitebloodcell")
        {
            Destroy(collider.gameObject);
            PlayerDetails.whiteBloodCellNumber += 1;
        }
    }
}
