using UnityEngine;
using System.Collections;

public class MiniMapCamera : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if(PhoneDetection.device == PhoneDetection.Device._16by9){
			camera.rect = new Rect(0.01f, 0.76f, 0.15f, 0.22f);
		}
		else if(PhoneDetection.device == PhoneDetection.Device._3by2){
			camera.rect = new Rect(0.01f, 0.79f, 0.16f, 0.2f);
		}
		else if(PhoneDetection.device == PhoneDetection.Device._4by3){
			camera.rect = new Rect(0.01f, 0.79f, 0.16f, 0.18f);
		}
	}
}
