using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnorePeople : MonoBehaviour {

    void Start()
    {
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision collision)
    {
        if (true) // collision.gameObject.tag == "Player")
        {
            Physics.IgnoreCollision(collision.gameObject.GetComponent<Collider>(), GetComponent<Collider>());
        }
    }
    }
