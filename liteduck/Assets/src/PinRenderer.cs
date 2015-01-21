using UnityEngine;
using System.Collections;

public class PinRenderer : MonoBehaviour 
{
    public Mesh pin;
    public Material mat;
    public Texture2D image;
    public float xspace = 0.17f;
    public float yspace = 0.15f;

	// Use this for initialization
	void Start () 
    {
        for (int j = 0; j != 72; j++)
        {
            //MeshCombineUtility.MeshInstance[] mcu = new MeshCombineUtility.MeshInstance[114];
            //int k = 0;
            for (int i = 0; i != 114; i++)
            {
                Color c = image.GetPixel(i, j);
                if (c != Color.black)
                {
                    GameObject go = new GameObject(i+"_" + j);
                    go.transform.position = new Vector3((j % 2 == 0 ? 0.085f : 0.0f) + (i * xspace), 0.0f, j * yspace);
                    MeshFilter mf = go.AddComponent<MeshFilter>();
                    mf.mesh = pin;
                    MeshRenderer mr = go.AddComponent<MeshRenderer>();
                    mr.material = new Material(mat);
                    mr.material.color = c;
                    mr.enabled = true;
                    //mcu[k].mesh = pin;
                    //mcu[k].subMeshIndex = 0;
                    //mcu[k].transform = Matrix4x4.TRS(new Vector3((j % 2 == 0 ? 0.085f : 0.0f) + (i * xspace), 0.0f, j * yspace), Quaternion.identity, Vector3.one);
                    //k++;
                }
            }
            /*GameObject go = new GameObject("Line_"+j);
            go.transform.parent = gameObject.transform;
            MeshFilter mf = go.AddComponent<MeshFilter>();
            MeshRenderer mr = go.AddComponent<MeshRenderer>();
            mf.mesh = MeshCombineUtility.Combine(mcu, false);
            mf.mesh.RecalculateBounds();
            mr.sharedMaterial = mat;
            mr.enabled = true;*/
        }
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
