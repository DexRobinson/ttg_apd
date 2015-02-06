using UnityEngine;
using System.Collections;

public class LocalRot : MonoBehaviour {
	private Transform t;

	void Awake()
	{
		t = transform;
	}
	void FixedUpdate()
	{
		t.RotateAround(new Vector3(0, -5, 0), Vector3.forward, 5 * Time.deltaTime);
	}
}
