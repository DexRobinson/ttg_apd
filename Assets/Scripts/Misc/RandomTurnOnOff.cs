using UnityEngine;
using System.Collections;

public class RandomTurnOnOff : MonoBehaviour {

	private float f_StartWaitTime = 0.0f;
	private float f_RandomWaitTime = 0.0f;
	private float f_RandomTimeToSpawn = 0.0f;

	private Renderer rend;

	void Start(){
		f_RandomTimeToSpawn = Random.Range(1.0f, 2.0f);
		rend = renderer;
	}
	
	void Update () {
		if(f_StartWaitTime < 2.0f){
			f_StartWaitTime += Time.deltaTime;
		}
		
		if(f_StartWaitTime > 2.0f){
			f_RandomWaitTime += Time.deltaTime;
			if(f_RandomWaitTime > f_RandomTimeToSpawn){
				f_RandomWaitTime = 0.0f;
				rend.enabled = !renderer.enabled;
				f_RandomTimeToSpawn = Random.Range(1.0f, 2.0f);
			}
		}
	}
}
