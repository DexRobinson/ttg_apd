using UnityEngine;
using System.Collections;

public class RotateAndScale : MonoBehaviour 
{
    public Vector3 rotAxisSpeed;
    public Vector3 scaleAxisSpeed;
    public bool pingPong;

    private Transform t;
    private Vector3 orgScale;

    void Start()
    {
        t = transform;
        orgScale = t.localScale;
    }

	void FixedUpdate () 
    {
	    
	}
}
