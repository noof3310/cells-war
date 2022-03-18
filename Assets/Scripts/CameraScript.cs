using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform lookAt;
    public Camera camera;
    public float bound_x = 2.5f;
    public float bound_y = 1f;
    public float min_fov = 5f;
    public float max_fov = 8f;
    public float sensitivity = 5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 delta = Vector3.zero;

        float delta_x = lookAt.position.x - transform.position.x;
        if (delta_x > bound_x || delta_x < -bound_x) {
            if (transform.position.x < lookAt.position.x) {
                delta.x = delta_x - bound_x;
            } else {
                delta.x = delta_x + bound_x;
            }
        }

        float delta_y = lookAt.position.y - transform.position.y;
        if (delta_y > bound_y || delta_y < -bound_y) {
            if (transform.position.y < lookAt.position.y) {
                delta.y = delta_y - bound_y;
            } else {
                delta.y = delta_y + bound_y;
            }
        }

        float fov = camera.orthographicSize - Input.GetAxis("Mouse ScrollWheel") * sensitivity;
        camera.orthographicSize = Mathf.Clamp(fov, min_fov, max_fov);

        transform.position += delta;
    }
}
