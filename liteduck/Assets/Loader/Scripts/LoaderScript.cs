using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LoaderScript : MonoBehaviour {

	protected float ScreenFade = 1.0f; 
	private Material FadeMaterial = null;

	//no pesky cursor when not needed, soo cool!! -SebF
	void OnGUI()
	{
		Screen.showCursor = false;	
	}

	//post-processing on camera render, only works when attached/linked to camera. -SebF
	void OnPostRender(){
		{
			if(!FadeMaterial) 
			{
				FadeMaterial = new Material( "Shader \"Hidden/CameraFade\" {" +
				                            "Properties { _Color (\"Main Color\", Color) = (1,1,1,0) }" +
				                            "SubShader {" +
				                            "    Pass {" +
				                            "        ZTest Always Cull Off ZWrite Off "+
				                            "		 Blend SrcAlpha OneMinusSrcAlpha "+
				                            "        Color [_Color]" +
				                            "    }" +
				                            "}" +
				                            "}"
				                            );
			}

			FadeMaterial.SetColor("_Color", new Color(0.0f,0.0f,0.0f, ScreenFade) );
			GL.PushMatrix ();
			GL.LoadOrtho ();

			for (var i = 0; i < FadeMaterial.passCount; ++i) 
			{
				FadeMaterial.SetPass (i);
				GL.Begin( GL.QUADS );
				GL.Vertex3( 0, 0, 0.1f );
				GL.Vertex3( 1, 0, 0.1f );
				GL.Vertex3( 1, 1, 0.1f );
				GL.Vertex3( 0, 1, 0.1f );
				GL.End();
			}
			GL.PopMatrix (); 
		}
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
			ScreenFade = timer/3.0f;
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
			ScreenFade = timer/fadeTime;
			yield return null;
		}
		Invoke ("LoadMainMenu", 1.0f);
	}



	void Awake(){
		ScreenFade = 1.0f;
	}

	void Start () {
		StartCoroutine(FadeIN());
	}

	private void LoadMainMenu(){
		Application.LoadLevel("MainMenu");
	}

	public float GetScreenFade(){
		return ScreenFade;
	}
	public void SetScreenFade(float ScreenFader){
		ScreenFade = ScreenFader;
	}
}
