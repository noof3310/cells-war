using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteBloodCellBounce : MonoBehaviour
{
    private float movementSpeed = 1f;
    float yCenter;

    int randomRatio;

    // public float distroyTime = 3.0f;

    // void OnEnable()
    // {
    //     Invoke("Distroy", distroyTime);
    // }
    void Distroy()
    {
        gameObject.SetActive(false);
    }
    // void OnDisable()
    // {
    //     CancelInvoke();
    // }

    // Update is called once per frame
    void Start()
    {
        gameObject.name = "whitebloodcell";
        yCenter = transform.position.y;
    }
    void Update()
    {
        //get the Input from Vertical axis
        // float y = Mathf.PingPong(Time.time * movementSpeed, 1) * 1 - 1;

        // transform.position = new Vector3(transform.position.x, yCenter + y, 0);

        //update the position
        // transform.position = transform.position + new Vector3(0, verticalInput * movementSpeed * Time.deltaTime, 0);
    }
}
