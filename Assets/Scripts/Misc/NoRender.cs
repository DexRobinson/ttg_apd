using UnityEngine;
using System.Collections;

public class NoRender : MonoBehaviour {
	public bool guitexture;
	
	void Start () 
	{
		if(Application.platform == RuntimePlatform.WindowsWebPlayer ||
			Application.platform == RuntimePlatform.OSXWebPlayer || 
			Application.platform == RuntimePlatform.OSXPlayer ||
			Application.platform == RuntimePlatform.WindowsPlayer ||
			Application.platform == RuntimePlatform.WindowsEditor)
		{
			if(!guitexture)
				gameObject.renderer.enabled = false;
			else
				gameObject.SetActive(false);
		}
	}
}
