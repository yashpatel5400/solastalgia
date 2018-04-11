using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour {

    private Vector3 PASSENGER_OFFSET = new Vector3(-1.0f, 2.75f, 0.0f);

    private bool canPickUp;
    private bool pickedUp;
    private GameObject npc;

    // Use this for initialization
    void Start () {
        canPickUp = false;
        npc = null;
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
            npc.transform.position = transform.position + PASSENGER_OFFSET;
        }
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NPC"))
        {
            canPickUp = true;
            npc = other.gameObject;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("NPC"))
        {
            canPickUp = false;
            npc = null;
        }
    }
}
