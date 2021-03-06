﻿using UnityEngine;
using System.Collections;

public class DuckerChanger : MonoBehaviour 
{
    public Texture2D DuckSprite, CapeSprite;
    public MeshRenderer DuckRender, CapeRender;
    public int colorOffset = 2, capeColorOffset = 3, frameOffset = -1;
    public DuckerControls controls;
    private Timer _frame;
    Texture2D _duckerSprite, _capeSprite;

    private Vector3 _standingCenter = new Vector3(11.0f, 6.5f, 0.0f);

    void Start()
    {
        

        _frame = new Timer(0.10f);
        _duckerSprite = new Texture2D(20, 15);
        _duckerSprite.filterMode = FilterMode.Point;
        DuckRender.sharedMaterial = new Material(DuckRender.sharedMaterial);
        DuckRender.sharedMaterial.mainTexture = _duckerSprite;
        _capeSprite = new Texture2D(20, 15);
        _capeSprite.filterMode = FilterMode.Point;
        CapeRender.sharedMaterial = new Material(CapeRender.sharedMaterial);
        CapeRender.sharedMaterial.mainTexture = _capeSprite;
        if (colorOffset == 0)
        {
            SetAs(GameColor.Blue);
        }
        else if (colorOffset == 1)
        {
            SetAs(GameColor.Red);
        }
        else if (colorOffset == 2)
        {
            SetAs(GameColor.Yellow);
        }

        if (capeColorOffset == 2)
        {
            SetCapeAs(GameColor.Blue);
        }
        else if (capeColorOffset == 1)
        {
            SetCapeAs(GameColor.Red);
        }
        else if (capeColorOffset == 3)
        {
            SetCapeAs(GameColor.Yellow);
        }
        else if (capeColorOffset == 0)
        {
            SetCapeAs(GameColor.Neutral);
        }
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Z) && capeColorOffset != 0)
        {
            SoundEngine.PlayClip("Color_Change");
            if (capeColorOffset == 3)
            {
                SetAs(GameColor.Yellow);
            }
            else if (capeColorOffset == 2)
            {
                SetAs(GameColor.Blue);
            }
            else if (capeColorOffset == 1)
            {
                SetAs(GameColor.Red);
            }
            SetCapeAs(GameColor.Neutral);
        }

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
            ApplySprite();
        }
    }

    void ApplySprite()
    {
        Color[] pixels = DuckSprite.GetPixels(frameOffset * 20, colorOffset * 15, 20, 15);
        _duckerSprite.SetPixels(0, 0, 20, 15, pixels);
        _duckerSprite.Apply();

        pixels = CapeSprite.GetPixels(frameOffset * 20, capeColorOffset * 15, 20, 15);
        _capeSprite.SetPixels(0, 0, 20, 15, pixels);
        _capeSprite.Apply();
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
        ApplySprite();
    }

    void SetCapeAs(GameColor color)
    {
        if (color == GameColor.Blue)
        {
            capeColorOffset = 2;
        }
        else if (color == GameColor.Red)
        {
            capeColorOffset = 1;
        }
        else if (color == GameColor.Yellow)
        {
            capeColorOffset = 3;
        }
        else
        {
            capeColorOffset = 0;
        }
        ApplySprite();
    }
    

    void OnTriggerEnter(Collider collider)
    {
        Door door = collider.gameObject.GetComponent<Door>();
        if(door != null)
        {
            SetAs(door.GameColor);
			SoundEngine.PlayClip("Color_Change");
        }
        else
        {
            Spike spike = collider.gameObject.GetComponent<Spike>();
            if (spike != null)
            {
                SoundEngine.PlayClip("Dies");
                Application.LoadLevel(2);
            }
            else
            {
                Powerup powerup = collider.gameObject.GetComponent<Powerup>();
                if (powerup != null)
                {
                    SetCapeAs(powerup.Color);
                    Destroy(collider.gameObject);
                }
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