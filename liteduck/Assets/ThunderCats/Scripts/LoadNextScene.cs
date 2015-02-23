using UnityEngine;
using System.Collections;

public class LoadNextScene : MonoBehaviour {

	void OnGUI()
	{
		Screen.showCursor = false;	
	}
	
	
	private IEnumerator Timer()
	{
		float timer = 0.0f;
		while (timer<7.3f)
		{
			timer+=Time.deltaTime;
			yield return null;
		}
		Invoke ("LoadMainMenu", 1.0f);
	}

	private void LoadMainMenu(){
		Application.LoadLevel(2);
	}

	// Use this for initialization
	void Start () {
		StartCoroutine(Timer());
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
