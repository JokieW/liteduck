using UnityEngine;
using System.Collections;

public class DuckerControls : MonoBehaviour 
{
    bool grounded;
    float ySpeed = 0.0f;
	
	void Update () 
    {
        TestGround();

        if (!grounded)
        {
            if (ySpeed > -2.0f)
            {
                ySpeed -= 0.35f;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            ySpeed = 8.0f;
        }
        else
        {
            ySpeed = 0.0f;
        }

        transform.Translate(new Vector3(1.0f, ySpeed, 0.0f) * Time.deltaTime * 21.0f);
        
	}
    void TestGround()
    {
        RaycastHit rayhit;
        BoxCollider col = GetComponent<BoxCollider>();
        int layers = 1 << 10 | 1 << 11 | 1 << 12 | 1 << 13;
        layers &= ~(1 << gameObject.layer);
        Physics.Raycast(transform.position + col.center, Vector3.down, out rayhit, collider.bounds.extents.y + 1.0f, layers);
        if (rayhit.collider == null || rayhit.collider == collider)
        {
            grounded = false;
        }
        else
        {
            grounded = true;
        }
    }
}
