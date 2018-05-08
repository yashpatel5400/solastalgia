using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBoat : MonoBehaviour {
    public float amplitude = 0.125f;
    public float frequency = 1f;

    // Position Storage Variables
    Vector3 tempPos = new Vector3();

    // vector added to "get out" of the vehicle somewhat naturally
    public float minAngle = 0.0f;
    public float maxAngle = 0.0f;

    private Vector3 GETTING_OUT = new Vector3(0.0f, 0.0f, 0.75f);
    private Vector3 CAMERA_OFFSET = new Vector3(0.0f, 1.75f, 0.0f);

    private const float MAX_SPEED = 30.0f;
    public float dragConstant = .75f;
    
    public Camera firstPersonCamera;
    public GameObject player;
    public GameObject driverSeat;
    public GameObject pickupObjective;
    public FaceObjective pointer;

    private bool canEnterBoat;
    private bool insideBoat;

    private float linearSpeed;
    private float rotationSpeed;
    public float linearAcceleration;
    public float rotationAcceleration;

    // Use this for initialization
    void Start () {
        canEnterBoat = false;
        insideBoat = false;
        
        linearSpeed = 0.0f;
        rotationSpeed = 0.0f;
    }

    // Update is called once per frame
    void Update () {
        tempPos = transform.position;
        tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;

        // transform.position = tempPos;

        linearSpeed = Mathf.Min(Mathf.Abs(linearSpeed), MAX_SPEED) * Mathf.Sign(linearSpeed);
        linearSpeed -= dragConstant * linearSpeed * Time.deltaTime;
        transform.Translate(new Vector3(0, 0, linearSpeed) * Time.deltaTime);

        rotationSpeed = Mathf.Min(Mathf.Abs(rotationSpeed), MAX_SPEED) * Mathf.Sign(rotationSpeed);
        transform.Rotate(new Vector3(0, rotationSpeed, 0) * Time.deltaTime);
        rotationSpeed -= dragConstant * rotationSpeed * Time.deltaTime;

        if (insideBoat) {
            player.transform.position = driverSeat.transform.position;
            firstPersonCamera.transform.position = driverSeat.transform.position + CAMERA_OFFSET;
            
			if (Input.GetKey(KeyCode.D))
            {
                rotationSpeed += rotationAcceleration;
            }
            if (Input.GetKey(KeyCode.A))
            {
                rotationSpeed -= rotationAcceleration;
            }
            if (Input.GetKey(KeyCode.W))
            {
                linearSpeed += linearAcceleration;
            }
            if (Input.GetKey(KeyCode.S))
            {
                linearSpeed -= linearAcceleration;
            }
            if (Input.GetKey(KeyCode.Q))
            {
                player.transform.position += GETTING_OUT;
                insideBoat = false;
            }
        }

        if (canEnterBoat && !insideBoat)
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                insideBoat = true;
                pointer.currentObjective = pickupObjective;
            }
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
