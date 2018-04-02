using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour {

    public float speed = 150.0f;
    public float sensitivity = 10.0f;

    // Use this for initialization
    void Start () {

    }

    // Update is called once per frame
    void Update()
    {
        var c = Camera.main.transform;
        c.Rotate(0, Input.GetAxis("Mouse X") * sensitivity, 0);
        c.Rotate(-Input.GetAxis("Mouse Y") * sensitivity, 0, 0);
        if (Input.GetMouseButtonDown(0))
            Cursor.lockState = CursorLockMode.Locked;

        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(new Vector3(-speed * Time.deltaTime, 0, 0));
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(new Vector3(0, -speed * Time.deltaTime, 0));
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(new Vector3(0, speed * Time.deltaTime, 0));
        }
        if (Input.GetKey(KeyCode.Q))
        {
            transform.Translate(new Vector3(0, 0, -speed * Time.deltaTime));
        }
        if (Input.GetKey(KeyCode.E))
        {
            transform.Translate(new Vector3(0, 0, speed * Time.deltaTime));
        }
    }
}
