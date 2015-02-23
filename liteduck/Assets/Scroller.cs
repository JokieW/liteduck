using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
 
public class Scroller : MonoBehaviour
{
    public float scrollSpeed = 0.5F;
    void Update()
    {
        float offset = Time.time * scrollSpeed;
        renderer.material.SetTextureOffset("_MainTex", new Vector2(offset, 0));
    }
}