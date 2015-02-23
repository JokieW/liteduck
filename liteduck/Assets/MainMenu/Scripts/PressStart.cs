using UnityEngine;
using System.Collections;

public class PressStart : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Z)){
			Application.LoadLevel(3);
		}
	}
}
