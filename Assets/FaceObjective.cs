using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceObjective : MonoBehaviour {

    public GameObject currentObjective;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        var lookPos = currentObjective.transform.position - transform.position;
        lookPos.y = 0;
        transform.rotation = Quaternion.LookRotation(lookPos);
    }
}
