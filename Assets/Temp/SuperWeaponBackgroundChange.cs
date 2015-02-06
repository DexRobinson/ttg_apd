using UnityEngine;
using System.Collections;

public class SuperWeaponBackgroundChange : MonoBehaviour {
	public Texture2D[] background;
	
	// Use this for initialization
	void Start () {
		renderer.material.mainTexture = background[DontDestoryValues.instance.planetNumber];
	}
}
