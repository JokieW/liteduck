using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class PinRenderer : MonoBehaviour 
{
    public Mesh pin;
    public Texture2D image;
    public float xspace = 0.17f;
    public float yspace = 0.15f;

    private static Dictionary<Color, Material> _MatLibrary = new Dictionary<Color, Material>();
    private static Material GetMaterial(Color color)
    {
        Material m;
        if(!_MatLibrary.TryGetValue(color, out m))
        {
            m = new Material(Resources.Load<Material>("Objects/Pin/Materials/basePinMat"));
            m.color = color;
            _MatLibrary.Add(color, m);
        }
        return m;
    }

	// Use this for initialization
	void Start () 
    {
        Render();
    }

    void Render()
    {
        //Create pixel DB for rendering
        Color[] pixels = image.GetPixels(0,0,image.width, image.height);
        Dictionary<Color, List<Vector2>> pixelDB = new Dictionary<Color, List<Vector2>>();

        for (int i = 0; i != pixels.Length; i++)
        {
            List<Vector2> list;
            if (!pixelDB.TryGetValue(pixels[i], out list))
            {
                list = new List<Vector2>();
                pixelDB.Add(pixels[i], list);
            }
            list.Add(new Vector2(i % image.width, i / image.width));
        }

        //Delete existing combined gameobjects
        foreach (Transform child in transform)
        {
            if (child != transform)
            {
                GameObject.Destroy(child.gameObject);
            }
        }

        //Create combined gameobjects
        int vertCount = pin.vertexCount;
        int maxPinPerObject = 65000 / vertCount;

        foreach (KeyValuePair<Color, List<Vector2>> kvp in pixelDB)
        {
            GameObject go = null;
            MeshFilter mf = null;
            MeshCombineUtility.MeshInstance[] mcu = null;
            int count = 0;
            int k = 0;
            foreach (Vector2 v2 in kvp.Value)
            {
                if (go == null || k == maxPinPerObject)
                {
                    if (go != null)
                    {
                        mf.sharedMesh = MeshCombineUtility.Combine(mcu, false);
                        mf.sharedMesh.RecalculateBounds();
                    }
                    go = new GameObject("Render #" + (count / maxPinPerObject) + " of " + kvp.Key);
                    go.transform.parent = transform;
                    mf = go.AddComponent<MeshFilter>();
                    mcu = new MeshCombineUtility.MeshInstance[maxPinPerObject];
                    k = 0;
                    MeshRenderer mr = go.AddComponent<MeshRenderer>();
                    mr.sharedMaterial = GetMaterial(kvp.Key);

                }
                mcu[k].mesh = pin;
                mcu[k].subMeshIndex = 0;
                mcu[k].transform = Matrix4x4.TRS(new Vector3((v2.y % 2 == 0 ? 0.085f : 0.0f) + (v2.x * xspace), 0.0f, v2.y * yspace), Quaternion.identity, Vector3.one) * transform.localToWorldMatrix;
                k++;
                count++;
            }
            mf.sharedMesh = MeshCombineUtility.Combine(mcu, false);
            mf.sharedMesh.RecalculateBounds();
        }        
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
