using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(Tiler))]
public class TilerEditor : Editor 
{
    private Material _baseMat;

    
    public override void OnInspectorGUI()
    {
        Tiler tiler = (Tiler)target;

        _baseMat = (Material)EditorGUILayout.ObjectField("Base Material", _baseMat, typeof(Material));
        tiler.collider = (BoxCollider)EditorGUILayout.ObjectField("Collider", tiler.collider, typeof(BoxCollider));

        EditorGUI.BeginChangeCheck();
        tiler.tile = (Texture2D)EditorGUILayout.ObjectField("Tile", tiler.tile, typeof(Texture2D));
        if (EditorGUI.EndChangeCheck())
        {
            Material mat  = new Material(_baseMat);
            mat.SetTexture(0, tiler.tile);
            tiler.transform.localScale = new Vector3(tiler.tile.width, tiler.tile.height, 1.0f);
            tiler.GetComponent<MeshRenderer>().material = mat;
            tiler.collider.size = new Vector3(tiler.tile.width, tiler.tile.height, 10.0f);
        }
    }
}
