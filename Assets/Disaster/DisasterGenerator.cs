using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisasterGenerator : MonoBehaviour
{
    public GameObject rainPreFab;
    public GameObject thunderPreFab;
    public GameObject typhoonPreFab;
    public Vector3 position = new Vector3(-1.5f, 39f, -2f);
    public static Disaster selectedDisaster;
    public static GameObject selectedDisasterObject;
    private bool generateSuccess;
    // Start is called before the first frame update
    void Start()
    {
        generateSuccess = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.State == GameState.RushState && selectedDisasterObject == null && !generateSuccess)
        {
            generateSuccess = true;
            Disaster disaster = (Disaster)Random.Range(0, System.Enum.GetValues(typeof(Disaster)).Length);
            DisasterGenerate(disaster);
        }
        else if (GameManager.State != GameState.RushState && selectedDisasterObject != null && generateSuccess)
        {
            generateSuccess = false;
            Destroy(GameObject.Find("Disaster"));
        }
    }

    public void DisasterGenerate(Disaster disaster)
    {
        selectedDisaster = disaster;
        Debug.Log("Disaster: " + disaster);
        switch (disaster)
        {
            case Disaster.Rain:
                selectedDisasterObject = Instantiate(rainPreFab, position, Quaternion.identity);
                selectedDisasterObject.name = "Disaster";
                break;
            case Disaster.Thunder:
                selectedDisasterObject = Instantiate(thunderPreFab, position, Quaternion.identity);
                selectedDisasterObject.name = "Disaster";
                break;
            case Disaster.Typhoon:
                selectedDisasterObject = Instantiate(typhoonPreFab, position, Quaternion.identity);
                selectedDisasterObject.name = "Disaster";
                break;
        }

    }
}

public enum Disaster
{
    Rain,
    Thunder,
    Typhoon
}
