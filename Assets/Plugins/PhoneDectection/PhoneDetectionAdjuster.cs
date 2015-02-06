using UnityEngine;
using System.Collections;

public class PhoneDetectionAdjuster : MonoBehaviour {
    [HideInInspector]
    public Vector3 _3by2Scale;
    [HideInInspector]
    public Vector3 _3by2Position;
    [HideInInspector]
    public Vector3 _16by9Scale;
    [HideInInspector]
    public Vector3 _16by9Position;
    [HideInInspector]
    public Vector3 _4by3Scale;
    [HideInInspector]
    public Vector3 _4by3Position;
    [HideInInspector]
    public Vector3 _16by10Scale;
    [HideInInspector]
    public Vector3 _16by10Position;

	// Use this for initialization
	void Start () {
        if (PhoneDetection.CurrentDevice() == PhoneDetection.Device._3by2){
                transform.localScale = _3by2Scale;
                transform.localPosition = _3by2Position;
        }
        else if (PhoneDetection.CurrentDevice() == PhoneDetection.Device._16by9){
                transform.localPosition = _16by9Position;
                transform.localScale = _16by9Scale;
        }
        if (PhoneDetection.CurrentDevice() == PhoneDetection.Device._4by3){
                transform.localPosition = _4by3Position;
                transform.localScale = _4by3Scale;
        }
        if (PhoneDetection.CurrentDevice() == PhoneDetection.Device._16by10)
        {
                transform.localPosition = _16by10Position;
                transform.localScale = _16by10Scale;
        }
	}
}
