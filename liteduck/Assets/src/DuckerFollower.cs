using UnityEngine;
using System.Collections;

public class DuckerFollower : MonoBehaviour 
{
    public GameObject Ducker;

	void Update () 
    {
        transform.position = new Vector3(Ducker.transform.position.x+30.0f, transform.position.y, transform.position.z);
	}
}
