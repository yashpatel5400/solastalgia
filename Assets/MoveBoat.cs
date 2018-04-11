﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBoat : MonoBehaviour {

    // vector added to "get out" of the vehicle somewhat naturally
    private Vector3 GETTING_OUT = new Vector3(0.0f, 0.0f, 0.75f);
    private Vector3 CAMERA_OFFSET = new Vector3(0.0f, 2.75f, 0.0f);

    private const float MAX_SPEED = 30.0f;
    public float dragConstant = .75f;
    
    public Camera firstPersonCamera;
    public GameObject player;

    private bool canEnterBoat;
    private bool insideBoat;

    private float linearSpeed;
    private float rotationSpeed;
    private float acceleration;

	public float baseAcell = 1f;
    
    // Use this for initialization
    void Start () {
        canEnterBoat = false;
        insideBoat = false;
        
        linearSpeed = 0.0f;
        rotationSpeed = 0.0f;
		acceleration = baseAcell;
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
            player.transform.position = transform.position + CAMERA_OFFSET;
            firstPersonCamera.transform.position = transform.position + CAMERA_OFFSET;
            
			acceleration = baseAcell;
            if (Input.GetKey(KeyCode.LeftShift))
            {
				acceleration = baseAcell;
            }

            if (Input.GetKey(KeyCode.D))
            {
                rotationSpeed += acceleration;
            }
            if (Input.GetKey(KeyCode.A))
            {
                rotationSpeed -= acceleration;
            }
            if (Input.GetKey(KeyCode.W))
            {
                linearSpeed += acceleration;
            }
            if (Input.GetKey(KeyCode.S))
            {
                linearSpeed -= acceleration;
            }
            if (Input.GetKey(KeyCode.Q))
            {
                player.transform.position += GETTING_OUT;
                insideBoat = false;
            }
        }

        if (canEnterBoat && !insideBoat)
        {
            if (Input.GetKey(KeyCode.E))
            {
                insideBoat = true;
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
