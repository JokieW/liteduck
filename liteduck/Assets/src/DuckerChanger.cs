using UnityEngine;
using System.Collections;

public class DuckerChanger : MonoBehaviour 
{
    public Texture2D BlueTex;
    public Texture2D RedTex;
    public Texture2D YellowTex;
    public MeshRenderer Renderer;
    void OnTriggerEnter(Collider collider)
    {
        Door door = collider.gameObject.GetComponent<Door>();
        if(door != null)
        {
            if (door.GameColor == GameColor.Blue)
            {
                gameObject.layer = 12;
                Renderer.material.SetTexture(0, BlueTex);
            }
            else if (door.GameColor == GameColor.Red)
            {
                gameObject.layer = 11;
                Renderer.material.SetTexture(0, RedTex);
            }
            else if (door.GameColor == GameColor.Yellow)
            {
                gameObject.layer = 13;
                Renderer.material.SetTexture(0, YellowTex);
            }
        }
        else
        {
            Spike spike = collider.gameObject.GetComponent<Spike>();
            if (spike != null)
            {
                Application.LoadLevel(0);
            }
        }
    }
}

public enum GameColor
{
    Neutral,
    Red,
    Blue,
    Yellow
}