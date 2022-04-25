using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject prefab;
    private GameObject uiUse;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (uiUse == null && GameManager.State == GameState.RestState)
        {
            uiUse = Instantiate(prefab, FindObjectOfType<Canvas>().transform);

        }
        else if (uiUse != null && GameManager.State != GameState.RestState)
        {
            Destroy(uiUse);
        }
    }
}
