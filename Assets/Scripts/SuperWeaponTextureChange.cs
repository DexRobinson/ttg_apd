using UnityEngine;
using System.Collections;

public class SuperWeaponTextureChange : MonoBehaviour {
	public Texture2D[] superWeapon;

	private Renderer rend;
	private Transform t;

	void Awake()
	{
		t = transform;
		rend = renderer;
	}

	// Use this for initialization
	void Start () {
		if(DontDestoryValues.instance.planetNumber == 0)
		{
			t.localPosition += new Vector3(0, 0.140625f, -5f);
		}
		else if(DontDestoryValues.instance.planetNumber == 2)
		{
			t.localPosition = new Vector3(0, 0.94f, -4f);
		}
		
		rend.material.mainTexture = superWeapon[DontDestoryValues.instance.planetNumber];
	}
}
