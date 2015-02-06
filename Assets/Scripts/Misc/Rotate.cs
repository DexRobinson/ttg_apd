using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour 
{
    public float speed;
	private Transform t;
	void Awake()
	{
		t = transform;
	}

	// Update is called once per frame
	void FixedUpdate () 
    {
        t.Rotate(new Vector3(0, 0, speed));
	}
}
