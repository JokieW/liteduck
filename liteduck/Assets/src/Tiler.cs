using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class Tiler : MonoBehaviour 
{
    public Texture2D tile;
    public BoxCollider collider;
    public int xTiling = 1;
    public int yTiling = 1;

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

        xTiling = Mathf.CeilToInt(transform.localScale.x / tile.width) ;
        yTiling = Mathf.CeilToInt(transform.localScale.y / tile.height) ;
        transform.localScale = new Vector3(xTiling * tile.width, yTiling * tile.height, 1.0f);
        collider.size = new Vector3(xTiling * tile.width, yTiling * tile.height, 10.0f);
        renderer.sharedMaterial.mainTextureScale = new Vector2(xTiling, yTiling);
	}


}
