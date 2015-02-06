using UnityEngine;
using System.Collections;

public class NoCollider : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		if(Application.platform == RuntimePlatform.WindowsWebPlayer ||
			Application.platform == RuntimePlatform.OSXWebPlayer || 
			Application.platform == RuntimePlatform.OSXPlayer ||
			Application.platform == RuntimePlatform.WindowsPlayer ||
			Application.platform == RuntimePlatform.WindowsEditor)
		{
			if(gameObject.collider2D)
				gameObject.collider2D.enabled = false;
			else if(gameObject.collider)
				gameObject.collider.enabled = false;
		}
	}
}
