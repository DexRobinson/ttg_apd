using UnityEngine;
using System.Collections;

public class RotationLocal : MonoBehaviour 
{
    public float speed = 5;

	void Update () 
    {
        transform.Rotate(Vector3.up * speed);
	}
}
