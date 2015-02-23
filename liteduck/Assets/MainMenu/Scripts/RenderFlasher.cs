using UnityEngine;
using System.Collections;

public class RenderFlasher : MonoBehaviour {

	public float flickerRate = 0.5f;
	private Renderer myRenderer = null;
	private float timer = 0.0f;

	void OnEnable(){
		myRenderer = renderer;
		timer = 0.0f;
		}


	void OnDisable(){
		if (myRenderer != null) {
			myRenderer.enabled = true;
				}
		}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if (timer > flickerRate) {
			timer=0.0f;
			if(renderer!=null){
				myRenderer.enabled = !myRenderer.enabled;
			}
		}
	}
}
