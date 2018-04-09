using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBoat : MonoBehaviour {

    // vector added to "get out" of the vehicle somewhat naturally
    private Vector3 GETTING_OUT = new Vector3(0.0f, 0.25f,0.0f);
    private const float MAX_SPEED = 30.0f;
    public float dragConstant = .75f;

    public Camera boatCamera;
    public Camera firstPersonCamera;
    public GameObject player;

    private bool canEnterBoat;
    private bool insideBoat;

    private float linearSpeed;
    private float rotationSpeed;
    private float acceleration;

    private bool ShowEnterGUI;
    private bool ShowExitGUI;
    
    // Use this for initialization
    void Start () {
        canEnterBoat = true;
        insideBoat = false;

        ShowEnterGUI = false;
        ShowExitGUI = false;

        linearSpeed = 0.0f;
        rotationSpeed = 0.0f;
        acceleration = 1.0f;
    }

    void OnGUI()
    {
        var centeredStyle = GUI.skin.GetStyle("Label");
        centeredStyle.alignment = TextAnchor.UpperCenter;

        if (ShowEnterGUI)
        {
            GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 25, 100, 50),
                "Press E to Enter", centeredStyle);
        }

        else if (ShowExitGUI)
        {
            GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 25, 100, 50),
                "Press Q to Exit", centeredStyle);
        }

        else
        {
            GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 25, 100, 50),
                "", centeredStyle);
        }

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
            ShowEnterGUI = false;
            ShowExitGUI  = true;

            firstPersonCamera.enabled = false;
            boatCamera.enabled = true;

            acceleration = 1.0f;
            if (Input.GetKey(KeyCode.LeftShift))
            {
                acceleration = 2.0f;
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
                player.transform.position = transform.position + GETTING_OUT;
                insideBoat = false;
            }
        }

        else
        {
            firstPersonCamera.enabled = true;
            boatCamera.enabled = false;
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
        ShowEnterGUI = true;
        ShowExitGUI = false;
        if (other.CompareTag("Player"))
        {
            canEnterBoat = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ShowEnterGUI = false;
            ShowExitGUI = false;
            canEnterBoat = false;
        }
    }
}
