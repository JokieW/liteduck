using UnityEngine;
using System.Collections;

public class DuckerChanger : MonoBehaviour 
{
    public Texture2D DuckSprite, CapeSprite;
    public MeshRenderer DuckRender, CapeRender;
    public int colorOffset = 0, frameOffset = -1;
    public DuckerControls controls;
    private Timer _frame;

    private Vector3 _standingCenter = new Vector3(11.0f, 6.5f, 0.0f);

    void Start()
    {
        _frame = new Timer(0.10f);
        
        
    }

    void Update()
    {
        int initialFrame = frameOffset;
        if (controls.ducking)
        {
            frameOffset = 2;
        }
        else
        {
            if (_frame.Check())
            {
                if (frameOffset == 0)
                {
                    frameOffset = 1;
                }
                else
                {
                    frameOffset = 0;
                }
                _frame.Reset();
            }
        }
        if (frameOffset != initialFrame)
        {
            Texture2D t = new Texture2D(20, 15);
            t.filterMode = FilterMode.Point;
            Color[] pixels = DuckSprite.GetPixels(frameOffset * 20, colorOffset * 15, 20, 15);
            t.SetPixels(0, 0, 20, 15, pixels);
            t.Apply();
            DuckRender.sharedMaterial.mainTexture = t;
            
        }
    }

    void SetAs(GameColor color)
    {
        if (color == GameColor.Blue)
        {
            gameObject.layer = 12;
            colorOffset = 2;
        }
        else if (color == GameColor.Red)
        {
            gameObject.layer = 11;
            colorOffset = 1;
        }
        else if (color == GameColor.Yellow)
        {
            gameObject.layer = 13;
            colorOffset = 0;
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        Door door = collider.gameObject.GetComponent<Door>();
        if(door != null)
        {
            SetAs(door.GameColor);
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