using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
 
namespace Brix
{
	public class MovieAutoplay : MonoBehaviour
	{
		void Start()
		{
            ((MovieTexture)renderer.material.mainTexture).Play();
		}
        void Update()
        {
            if (!((MovieTexture)renderer.material.mainTexture).isPlaying)
            {
                Application.LoadLevel(1);
            }
        }
	}
} 