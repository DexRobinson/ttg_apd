using UnityEngine;
using System.Collections;

public class MortLaserRender : MonoBehaviour {
	private float onTimer;
	private Renderer rend;

	void Awake()
	{
		rend = renderer;
	}
	void Update () {
		if(onTimer < 2.0f){
			onTimer += Time.deltaTime;
			if(onTimer >= 2.0f){
				rend.enabled = true;
			}
		}
	}
}
