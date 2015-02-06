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

        tiler.collider = (BoxCollider)EditorGUILayout.ObjectField("Collider", tiler.collider, typeof(BoxCollider));

        EditorGUI.BeginChangeCheck();
        tiler.renderer.sharedMaterial = (Material)EditorGUILayout.ObjectField("Tile", tiler.renderer.sharedMaterial, typeof(Material));
        if (EditorGUI.EndChangeCheck())
        {
            tiler.transform.localScale = new Vector3(tiler.renderer.sharedMaterial.GetTexture(0).width, tiler.renderer.sharedMaterial.GetTexture(0).height, 1.0f);
            tiler.collider.size = new Vector3(tiler.renderer.sharedMaterial.GetTexture(0).width, tiler.renderer.sharedMaterial.GetTexture(0).height, 10.0f);
        }
    }
}
