using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LoaderScript : MonoBehaviour {
	//Obsolete version
	//protected float ScreenFade = 1.0f; 
	//public GameObject ScreenBlack = null;
	//public Color ScreenFade = new Color(0.0f,0.0f,0.0f,1.0f);
	//public float ScreenFade;
	public Color CurrentFade = new Color(1,1,1,1);

	void OnGUI()
	{
		Screen.showCursor = false;	
	}


	private IEnumerator FadeIN()
	{
		float timer1 = 0.0f;
		while (timer1<1.0f)
		{
			timer1+=Time.deltaTime;
			yield return null;
		}
		float timer= 3.0f;
		while (timer > 0.0f) {
			timer-=Time.deltaTime;
			//ScreenFade = timer/3.0f;
			Color alphaColor = gameObject.renderer.material.color;
			alphaColor.a = timer/3.0f;
			gameObject.renderer.material.color = alphaColor;
			yield return null;
		}
		timer = 0.0f;
		while (timer<2.0f)
		{
			timer+=Time.deltaTime;
			yield return null;
		}
		StartCoroutine (FadeOUT ());
	}

	private IEnumerator FadeOUT(){
		float fadeTime = 3.0f;
		float timer = 0.0f;
		while (fadeTime > timer) {
			timer+=Time.deltaTime;
			//ScreenFade = timer/fadeTime;
			Color alphaColor = gameObject.renderer.material.color;
			alphaColor.a = timer/fadeTime;
			gameObject.renderer.material.color = alphaColor;
			yield return null;
		}
		Invoke ("LoadMainMenu", 1.0f);
	}
	

	void Start () {
		StartCoroutine(FadeIN());
		//CurrentFade = gameObject.renderer.material.color.a;
	}

	private void LoadMainMenu(){
		Application.LoadLevel(1);
	}

	void udpade(){
		gameObject.renderer.material.color = CurrentFade;
	}
}
