using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class Tiler : MonoBehaviour 
{
    public BoxCollider collider;
    public int xTiling = 1;
    public int yTiling = 1;
    public ColliderType colliderType = ColliderType.Platform;

	// Use this for initialization
	void Start ()
    {
        Texture tex = renderer.sharedMaterial.GetTexture(0);
        xTiling = Mathf.CeilToInt(transform.localScale.x / tex.width);
        yTiling = Mathf.CeilToInt(transform.localScale.y / tex.height);
        renderer.sharedMaterial = new Material(renderer.sharedMaterial);
        renderer.sharedMaterial.mainTextureScale = new Vector2(xTiling, yTiling);
    }
	
	void OnGUI () 
    {
        if (Application.isEditor)
        {
            int divider = 1;
            if (Event.current.shift)
            {
                divider = 2;
            }
            Vector3 pos = transform.parent.position * divider;

            transform.parent.position = new Vector3(Mathf.Round(pos.x) / divider, Mathf.Round(pos.y) / divider, Mathf.Round(pos.z) / divider);

            Texture tex = renderer.sharedMaterial.GetTexture(0);
            xTiling = Mathf.CeilToInt(transform.localScale.x / tex.width);
            yTiling = Mathf.CeilToInt(transform.localScale.y / tex.height);
            transform.localScale = new Vector3(xTiling * tex.width, yTiling * tex.height, 1.0f);
            if (colliderType == ColliderType.Platform)
            {
                collider.size = new Vector3(xTiling * tex.width, yTiling * (tex.height / 2), 10.0f);
                collider.center = new Vector3(0.0f, -(tex.height / 4), 0.0f);
            }
            else if (colliderType == ColliderType.Spike)
            {
                collider.size = new Vector3(xTiling * (tex.width / 2), yTiling * (tex.height / 2), 10.0f);
                collider.center = new Vector3(-(tex.width / 4), -(tex.height / 4), 0.0f);
            }
            else
            {
                collider.size = new Vector3(xTiling * tex.width, yTiling * tex.height, 10.0f);
                collider.center = Vector3.zero;
            }
            renderer.sharedMaterial.mainTextureScale = new Vector2(xTiling, yTiling);
        }
	}


}
