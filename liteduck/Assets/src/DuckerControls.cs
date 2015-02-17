﻿using UnityEngine;
using System.Collections;

public class DuckerControls : MonoBehaviour
{
    private bool _grounded = true, _stoppedGoingUp = true;
    private float ySpeed = 0.0f, yFallRate = 1f;
    private float maxY = 4.0f, minY = -4.0f;
    private Timer _jumpTime, _hoverTime;
    private bool _ducking = false;
    public bool ducking
    {
        get
        {
            return _ducking;
        }
        set
        {
            BoxCollider bc = GetComponent<BoxCollider>();
            if (value)
            {
                bc.center = new Vector3(11.0f, 3.5f, 0.0f);
                bc.size = new Vector3(8.0f, 7.0f, 1.0f);
            }
            else
            {
                bc.center = new Vector3(11.0f, 6.5f, 0.0f);
                bc.size = new Vector3(8.0f, 13.0f, 1.0f);
            }
            _ducking = value;
        }
    }
    public bool Ascending
    {
        get
        {
            return !_grounded && !_stoppedGoingUp;
        }
    }
    public bool Descending
    {
        get
        {
            return !_grounded && _stoppedGoingUp;
        }
    }

    void Awake()
    {
        _jumpTime = new Timer(0.0f);
        _hoverTime = new Timer(0.0f);
    }

    void Update()
    {
        if (_grounded)
        {
            ySpeed = 0.0f;
        }
        else if (Descending)
        {
            if (ySpeed > minY)
            {
                ySpeed -= yFallRate;
            }
        }

        if (Input.GetKey(KeyCode.X))
        {
            ducking = true;
        }
        if (Input.GetKeyUp(KeyCode.X))
        {
            ducking = false;
        }

        if (Input.GetKey(KeyCode.Z) && !ducking)
        {
            if (_grounded)
            {
                _grounded = false;
                ySpeed = maxY;
                _jumpTime.Reset();
                _jumpTime.Start(0.33f);
                _stoppedGoingUp = false;
            }
            if (_jumpTime.Check())
            {
                _stoppedGoingUp = true;
            }
        }
        else if (Input.GetKeyUp(KeyCode.Z) || ducking)
        {
            _stoppedGoingUp = true;
        }


        transform.Translate(new Vector3(2.0f, ySpeed, 0.0f) * Time.deltaTime * 21.0f);

    }

    void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            if (Mathf.Round(contact.normal.normalized.y) == 1.0f)
            {
                _grounded = true;
                ySpeed = 0.0f;

                transform.position = new Vector3(transform.position.x,
                    collision.collider.bounds.center.y + 2.0f,
                    transform.position.z);

                break;
            }
        }
    }

    void OnCollisionExit(Collision collision)
    {
        _grounded = false;
    }
}
