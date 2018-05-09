using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetScript : MonoBehaviour {

	public static ResetScript instance;

	public bool showCursor = true;

	// Use this for initialization
	void Start () {
		if(instance == null){
			instance = this;
			DontDestroyOnLoad(gameObject);
		} else {
			Destroy(gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log("Cursor.visible: " + Cursor.visible);

		if(Input.GetKeyDown(KeyCode.R)){
			SceneManager.LoadScene(0);	

			Cursor.visible = showCursor;

			Debug.Log("Cursor.visible: " + Cursor.visible);
		}
	}
}
