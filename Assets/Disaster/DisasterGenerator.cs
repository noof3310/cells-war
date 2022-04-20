using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisasterGenerator : MonoBehaviour
{
    public GameObject rainPreFab;
    public GameObject firePreFab;
    public int fireAmount = 50;
    public int Radius = 50;
    public List<GameObject> fireList = new List<GameObject>();
    public GameObject typhoonPreFab;
    public Vector3 position = new Vector3(-1.5f, 39f, -2f);
    public float chanceForDisaster = 0.3f;
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
        if (GameManager.State == GameState.RushState && !generateSuccess)
        {
            generateSuccess = true;
            Disaster disaster = (Disaster)Random.Range(0, System.Enum.GetValues(typeof(Disaster)).Length);
            float rand = Random.Range(0f, 1f);
            Debug.Log("Disaster random: " + rand);
            if (chanceForDisaster > rand)
            {
                DisasterGenerate(disaster);
            }
        }
        else if (GameManager.State != GameState.RushState && GameManager.State != GameState.RestState && generateSuccess)
        {
            generateSuccess = false;
            if (selectedDisaster == Disaster.Rain)
            {
                Destroy(GameObject.Find("Disaster"));
            }
            else if (selectedDisaster == Disaster.Fire)
            {
                foreach (GameObject obj in fireList)
                {
                    Destroy(obj);
                }
                fireList.Clear();
            }
        }
        else if (GameManager.State != GameState.RushState && GameManager.State != GameState.RestState)
        {
            generateSuccess = false;

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
            case Disaster.Fire:

                for (int i = 0; i < fireAmount; i++)
                {
                    Vector3 randomPos = Random.insideUnitCircle * Radius;
                    fireList.Add(Instantiate(firePreFab, randomPos, Quaternion.Euler(new Vector3(-90, 0, 0))));
                }
                break;
                // case Disaster.Typhoon:
                //     selectedDisasterObject = Instantiate(typhoonPreFab, position, Quaternion.identity);
                //     selectedDisasterObject.name = "Disaster";
                //     break;
        }

    }
}

public enum Disaster
{
    Unknown,
    Rain,
    Fire,
    Typhoon
}
