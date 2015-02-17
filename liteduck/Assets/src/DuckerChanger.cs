using UnityEngine;
using System.Collections;

public class DuckerChanger : MonoBehaviour 
{
    public Texture2D DuckSprite, CapeSprite;
    public MeshRenderer DuckRender, CapeRender;
    public int colorOffset = 2, capeColorOffset = 2, frameOffset = -1;
    public DuckerControls controls;
    private Timer _frame;
    Texture2D _duckerSprite, _capeSprite;

    private Vector3 _standingCenter = new Vector3(11.0f, 6.5f, 0.0f);

    void Start()
    {
        _frame = new Timer(0.10f);
        _duckerSprite = new Texture2D(20, 15);
        _duckerSprite.filterMode = FilterMode.Point;
        DuckRender.sharedMaterial.mainTexture = _duckerSprite;
        _capeSprite = new Texture2D(20, 15);
        _capeSprite.filterMode = FilterMode.Point;
        CapeRender.sharedMaterial.mainTexture = _capeSprite;
    }

    void Update()
    {
        int initialFrame = frameOffset;
        if (controls.ducking)
        {
            frameOffset = 2;
        }
        else if (controls.Ascending)
        {
            frameOffset = 3;
        }
        else if (controls.Descending)
        {
            frameOffset = 4;
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
            
            Color[] pixels = DuckSprite.GetPixels(frameOffset * 20, colorOffset * 15, 20, 15);
            _duckerSprite.SetPixels(0, 0, 20, 15, pixels);
            _duckerSprite.Apply();

            pixels = CapeSprite.GetPixels(frameOffset * 20, capeColorOffset * 15, 20, 15);
            _capeSprite.SetPixels(0, 0, 20, 15, pixels);
            _capeSprite.Apply();
        }
    }

    void SetAs(GameColor color)
    {
        if (color == GameColor.Blue)
        {
            gameObject.layer = 12;
            colorOffset = 0;
        }
        else if (color == GameColor.Red)
        {
            gameObject.layer = 11;
            colorOffset = 1;
        }
        else if (color == GameColor.Yellow)
        {
            gameObject.layer = 13;
            colorOffset = 2;
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