using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossUIManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Image prefabUI;
    public Image uiUse;
    public Vector3 offsetY = new Vector3(0f, 0f, 0f);
    void Start()
    {
        uiUse = (Image)Instantiate(prefabUI, FindObjectOfType<Canvas>().transform).GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        uiUse.transform.position = Camera.main.WorldToScreenPoint(transform.Find("Head").gameObject.transform.position + offsetY);
    }
}
