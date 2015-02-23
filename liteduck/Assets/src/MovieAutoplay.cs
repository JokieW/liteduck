using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
 
namespace Brix
{
	public class MovieAutoplay : MonoBehaviour
	{
        public AudioSource source;
		void Start()
		{
            ((MovieTexture)renderer.material.mainTexture).Play();
            source.Play();
		}
        void Update()
        {
            if (!((MovieTexture)renderer.material.mainTexture).isPlaying)
            {
                Application.LoadLevel(2);
            }
        }
	}
} 