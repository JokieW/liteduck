using UnityEngine;
using System.Collections;

public class DuckerControls : MonoBehaviour 
{
    bool _grounded;
    float ySpeed = 0.0f, yFallRate = 0.35f, yJumpRate = 0.35f;
    float maxY = 8.0f, midY = 2.0f, minY = -2.0f;
    Timer _jumpTime, _hoverTime;

    void Awake()
    {
        _jumpTime = new Timer(0.0f);
        _hoverTime = new Timer(0.0f);
    }

	void Update () 
    {
        if (!_grounded)
        {
            if (ySpeed > minY)
            {
                if (_hoverTime.Check())
                {
                    ySpeed -= yFallRate / 2;
                }
                else
                {
                    ySpeed -= yFallRate;
                }
            }
        }
        if (Input.GetKey(KeyCode.Space))
        {
            if (_grounded)
            {
                _grounded = false;
                ySpeed = midY;
                _jumpTime.Reset();
                _jumpTime.Start(1.0f);
            }
            if (!_jumpTime.Check())
            {
                ySpeed += yJumpRate;
            }
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            _hoverTime.Reset();
            _hoverTime.Start(1.0f);
        }
        else
        {
            ySpeed = 0.0f;
        }

        transform.Translate(new Vector3(1.0f, ySpeed, 0.0f) * Time.deltaTime * 21.0f);
        
	}

    void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            if (Mathf.Round(contact.normal.normalized.y) == 1.0f)
            {
                _grounded = true;
                ySpeed = 0.0f;
                break;
            }
        }
    }
    

}
