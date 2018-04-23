using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    public float stepSize = .00025f;

    public string locations = "";
    private ArrayList destinationVectors;
    private int currentDestination;
    private int destinationCount;

    public float rotateSpeed = 0.5f;

    private const float ROT_THRESHOLD  = 3.0f; // threshold of distance at which to start rotating to face dest
    private const float DEST_THRESHOLD = 1.0f; // threshold of distance to be considered "at" the destination

    // Use this for initialization
    void Start () {
        destinationVectors = new ArrayList();
        currentDestination = 0;
        string[] destinations = locations.Split(';');

        foreach (var destination in destinations)
        {
            string[] positions = destination.Split(',');
            Vector3 destinationVector = new Vector3(
                float.Parse(positions[0]),
                float.Parse(positions[1]),
                float.Parse(positions[2]));
            destinationVectors.Add(destinationVector);
        }

        destinationCount = destinationVectors.Capacity;
    }
	
	// Update is called once per frame
	void Update () {
        if (destinationCount == 0) return;

        Vector3 destinationVector = (Vector3) destinationVectors[currentDestination % destinationCount];
        transform.LookAt(destinationVector);
        Vector3 direction = destinationVector - transform.position;
        
        transform.position += stepSize * direction / direction.magnitude;

        // Move our position a step closer to the target.
        /* if (direction.magnitude < ROT_THRESHOLD)
        {
            float step = rotateSpeed * Time.deltaTime;
            Vector3 nextDestinationVector = (Vector3)destinationVectors[(currentDestination + 1) % destinationCount];
            Vector3 targetDir = nextDestinationVector - transform.position;
            Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDir);
        } */

        if (direction.magnitude < DEST_THRESHOLD)
        {
            currentDestination += 1;
        }
	}
}
