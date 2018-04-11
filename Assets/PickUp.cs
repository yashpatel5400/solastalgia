using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour {

    private bool canPickUp;
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
                npc.transform.position = transform.position;
            }
        }
	}

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
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
