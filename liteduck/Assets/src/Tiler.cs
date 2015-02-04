using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class Tiler : MonoBehaviour 
{
    public Texture2D tile;


	// Use this for initialization
	void Start () 
    {
	
	}


	
	// Update is called once per frame
	void OnGUI () 
    {
        int divider = 1;
        if (Event.current.shift)
        {
            divider = 2;
        }
        Vector3 pos = transform.position * divider;

        transform.position = new Vector3(Mathf.Round(pos.x) / divider, Mathf.Round(pos.y) / divider, Mathf.Round(pos.z) / divider);
	}
}
