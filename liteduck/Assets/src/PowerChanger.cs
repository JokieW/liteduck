using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
 
public class PowerChanger : MonoBehaviour
{
    public Texture2D CandySprite;
    public MeshRenderer CandyRender;
    public int colorOffset = 0, frameOffset = 0;
    private Timer _frame;
    Texture2D _candySprite;
	void Start()
	{

        SetAs(GetComponentInParent<Powerup>().Color);
        _frame = new Timer(0.05f);
        _candySprite = new Texture2D(5, 5);
        _candySprite.filterMode = FilterMode.Point;
        CandyRender.sharedMaterial = new Material(CandyRender.sharedMaterial);
        CandyRender.sharedMaterial.mainTexture = _candySprite;

	}
	void Update()
	{
        int initialFrame = frameOffset;
        if (_frame.Check())
        {
            if (frameOffset >= 7)
            {
                frameOffset = 0;
            }
            else
            {
                frameOffset++;
            }
            _frame.Reset();
        }
        if (frameOffset != initialFrame)
        {

            ApplySprite();
        }
	}

    void ApplySprite()
    {
        Color[] pixels = CandySprite.GetPixels(frameOffset * 5, colorOffset * 5, 5, 5);
        _candySprite.SetPixels(0, 0, 5, 5, pixels);
        _candySprite.Apply();
    }

    void SetAs(GameColor color)
    {
        if (color == GameColor.Blue)
        {
            colorOffset = 2;
        }
        else if (color == GameColor.Red)
        {
            colorOffset = 0;
        }
        else if (color == GameColor.Yellow)
        {
            colorOffset = 1;
        }
    }
}
