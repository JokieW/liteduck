using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class PinRenderer : MonoBehaviour 
{
    public Mesh pin;
    public RenderTexture image;
    public Color chromaKey = new Color(0.0f, 1.0f, 0.0f);
    public GameObject ParentObject;
    public float xspace = 0.17f;
    public float yspace = 0.15f;
    
    private Texture2D _internalTexture;
    private MeshRenderer[] _matArray;

    private static Dictionary<Color, Material> _MatLibrary = new Dictionary<Color, Material>();
    private static Material GetMaterial(Color color)
    {
        Material m;
        if (!_MatLibrary.TryGetValue(color, out m))
        {
            m = new Material(Resources.Load<Material>("Sprites/Materials/test"));
            m.color = color;
            _MatLibrary.Add(color, m);
        }
        return m;
    }

    void Start()
    {
        int total = image.width * image.height;
        _internalTexture = new Texture2D(image.width, image.height, TextureFormat.RGB24, false);
        _matArray = new MeshRenderer[total];
        for (int i = 0; i != total; i++)
        {
            GameObject go = new GameObject();
            go.transform.parent = ParentObject.transform;
            go.layer = 8;

            go.AddComponent<MeshFilter>().sharedMesh = pin;
            _matArray[i] = go.AddComponent<MeshRenderer>();
        }
    }

    void OnPostRender()
    {
        _internalTexture.ReadPixels(new Rect(0, 0, image.width, image.height), 0, 0);
        Color[] pixels = _internalTexture.GetPixels(0, 0, image.width, image.height);

        for (int i = 0; i != pixels.Length; i++)
        {
            Vector2 temploc = new Vector2(i % _internalTexture.width, i / _internalTexture.width);
            _matArray[i].transform.localPosition = new Vector3((temploc.y % 2 == 0 ? 0.085f : 0.0f) + (temploc.x * xspace), 0.0f, temploc.y * yspace);
            if(pixels[i] == chromaKey)
            {
                _matArray[i].enabled = false;
            }
            else
            {
                _matArray[i].enabled = true;
                _matArray[i].sharedMaterial = GetMaterial(pixels[i]);
            }
            
        }
    }

}
