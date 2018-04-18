using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour {

    public GameObject passengerSeat;
    public GameObject objective;

    private Vector3 PASSENGER_OFFSET = new Vector3(-1.0f, 2.0f, 0.0f);
    private Vector3 DROPOFF_OFFSET = new Vector3(0.0f, -0.5f, 0.0f);

    private bool canPickUp;
    private bool canDropOff;
    private bool pickedUp;

    private GameObject npc;
    private GameObject dropOff;

    // Use this for initialization
    void Start () {
        canPickUp = false;
        npc = null;
        dropOff = null;
    }
	
	// Update is called once per frame
	void Update () {
		if (canPickUp && npc != null)
        {
            if (Input.GetKey(KeyCode.F))
            {
                pickedUp = true;
            }
        }

        if (pickedUp)
        {
            objective.GetComponent<Renderer>().enabled = true;
            npc.transform.rotation = transform.rotation;
            npc.transform.position = passengerSeat.transform.position;
            if (canDropOff && dropOff != null && Input.GetKey(KeyCode.F))
            {
                npc.transform.position = transform.position + DROPOFF_OFFSET;
                npc = null;
                pickedUp = false;
            }
        }
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NPC"))
        {
            canPickUp = true;
            npc = other.gameObject;
        }
        if (other.CompareTag("Objective"))
        {
            canDropOff = true;
            dropOff = other.gameObject;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("NPC"))
        {
            canPickUp = false;
            npc = null;
        }
        if (other.CompareTag("Objective"))
        {
            canDropOff = false;
            dropOff = null;
        }
    }
}
