using UnityEngine;
using System.Collections;

public class RotateY : MonoBehaviour {

	public float speed;
	private Transform t;

	void Awake()
	{
		t = transform;
	}

	// Update is called once per frame
	void FixedUpdate () {
		t.Rotate(new Vector3(0, speed, 0));
	}

	public void Die()
	{
		Destroy(gameObject);
	}
}
