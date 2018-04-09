using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBoat : MonoBehaviour {

    private const float MAX_SPEED = 15.0f;
    public float dragConstant = .75f;

    public Camera boatCamera;
    public Camera firstPersonCamera;

    private bool canEnterBoat;
    private bool insideBoat;

    private float linearSpeed;
    private float rotationSpeed;

    // Use this for initialization
    void Start () {
        canEnterBoat = true;
        insideBoat = false;

        linearSpeed = 0.0f;
        rotationSpeed = 0.0f;
    }
	
	// Update is called once per frame
	void Update () {
        linearSpeed = Mathf.Min(Mathf.Abs(linearSpeed), MAX_SPEED) * Mathf.Sign(linearSpeed);
        linearSpeed -= dragConstant * linearSpeed * Time.deltaTime;
        transform.Translate(new Vector3(0, 0, linearSpeed) * Time.deltaTime);

        rotationSpeed = Mathf.Min(Mathf.Abs(rotationSpeed), MAX_SPEED) * Mathf.Sign(rotationSpeed);
        transform.Rotate(new Vector3(0, rotationSpeed, 0) * Time.deltaTime);
        rotationSpeed -= dragConstant * rotationSpeed * Time.deltaTime;

        if (insideBoat) { 
            firstPersonCamera.enabled = false;
            boatCamera.enabled = true;

            if (Input.GetKey(KeyCode.D))
            {
                rotationSpeed += 1.0f;
            }
            if (Input.GetKey(KeyCode.A))
            {
                rotationSpeed -= 1.0f;
            }
            if (Input.GetKey(KeyCode.W))
            {
                linearSpeed += 1.0f;
            }
            if (Input.GetKey(KeyCode.S))
            {
                linearSpeed -= 1.0f;
            }
            if (Input.GetKey(KeyCode.Q))
            {
                insideBoat = false;
            }
        }

        else
        {
            firstPersonCamera.enabled = true;
            boatCamera.enabled = false;
        }

        if (canEnterBoat && !insideBoat && Input.GetKey(KeyCode.E))
        {
            insideBoat = true;
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canEnterBoat = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canEnterBoat = false;
        }
    }
}
