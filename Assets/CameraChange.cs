using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChange : MonoBehaviour {
    public int animationTime;

    public Camera animationCamera;
    public Camera fpsCamera;
    public Camera minimapCamera;
    public Camera pointerCamera;
    public Canvas minimap;

    public GameObject ignored;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(animator());
    }

    IEnumerator animator()
    {
        GameObject[] minimapObjects = GameObject.FindGameObjectsWithTag("Minimap");

        foreach (GameObject minimapObject in minimapObjects)
        {
            minimapObject.SetActive(false);
        }
        animationCamera.enabled = true;
        fpsCamera.enabled = false;
        fpsCamera.GetComponent<AudioListener>().enabled = false;
        minimapCamera.enabled = false;
        pointerCamera.enabled = false;
        minimap.enabled = false;

        ignored.SetActive(false);

        yield return new WaitForSeconds(animationTime);

        animationCamera.enabled = false;
        fpsCamera.GetComponent<AudioListener>().enabled = true;
        fpsCamera.enabled = true;
        minimapCamera.enabled = true;
        pointerCamera.enabled = true;
        minimap.enabled = true;

        foreach (GameObject minimapObject in minimapObjects)
        {
            minimapObject.SetActive(true);
        }

        ignored.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        
    }
}
