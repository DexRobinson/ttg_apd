using UnityEngine;
using System.Collections;

public class SatRotateItem : MonoBehaviour {
	private Renderer rend;

	void Awake()
	{
		rend = renderer;
	}

	void Update()
	{
		if(Variables.instance.itemSataliteOn)
		{
			rend.enabled = true;
			//transform.RotateAround(new Vector3(0, -5, 0), -Vector3.forward, 30.0f * Time.deltaTime);
		}
		else
		{
			rend.enabled = false;
		}
	}
}
