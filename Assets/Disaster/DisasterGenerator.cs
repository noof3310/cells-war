using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisasterGenerator : MonoBehaviour
{
    public Text textAlert;
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
    public float percentOfTowerDestroyOnFire = 0.2f;
    private bool generateSuccess;
    private Text displayText;
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
            if (chanceForDisaster > rand)
            {
                DisasterGenerate(disaster);
            }

        }
        else if (GameManager.State != GameState.RushState && generateSuccess)
        {
            generateSuccess = false;
            if (selectedDisaster == Disaster.Rain)
            {
                Destroy(GameObject.Find("Disaster"));
            }
            else if (selectedDisaster == Disaster.Fire)
            {

                foreach (GameObject fire in fireList)
                {
                    Destroy(fire);
                }
                fireList.Clear();
            }
            DisasterGenerate(Disaster.Unknown);
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
                textAlert.text = "Decrease Player's speed !!!";
                selectedDisasterObject = Instantiate(rainPreFab, position, Quaternion.identity);
                displayText = (Text)Instantiate(textAlert, FindObjectOfType<Canvas>().transform);
                selectedDisasterObject.name = "Disaster";
                break;
            case Disaster.Fire:
                textAlert.text = "Tower was destroyed !!!";
                displayText = (Text)Instantiate(textAlert, FindObjectOfType<Canvas>().transform);

                for (int i = 0; i < fireAmount; i++)
                {
                    Vector3 randomPos = Random.insideUnitCircle * Radius;
                    fireList.Add(Instantiate(firePreFab, randomPos, Quaternion.Euler(new Vector3(-90, 0, 0))));
                }

                GameObject[] tower;
                tower = GameObject.FindGameObjectsWithTag("Tower");
                int amount = (int)Mathf.Floor(percentOfTowerDestroyOnFire * tower.Length);
                for (int i = 0; i < amount; i++)
                {
                    GameObject to = tower[Random.Range(0, tower.Length)];
                    to.GetComponent<TowerBehavior>().Died();
                }
                break;
            // case Disaster.Typhoon:
            //     selectedDisasterObject = Instantiate(typhoonPreFab, position, Quaternion.identity);
            //     selectedDisasterObject.name = "Disaster";
            //     break;
            default:
                if (displayText != null)
                    Destroy(displayText);
                break;
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
