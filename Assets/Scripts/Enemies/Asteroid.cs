using UnityEngine;
using System.Collections;

public class Asteroid : MonoBehaviour {
	public Sprite[] asteroidTextures;

	// Use this for initialization
	void Start () {
		gameObject.GetComponent<SpriteRenderer>().sprite = asteroidTextures[DontDestoryValues.instance.planetNumber];
		//renderer.material.mainTexture = asteroidTextures[DontDestoryValues.instance.planetNumber];
	}
}
