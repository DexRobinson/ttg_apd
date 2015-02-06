using UnityEngine;
using System.Collections;

public class PlanetTextureSwitch : MonoBehaviour 
{
	public int planetID;
	public Texture2D colorImage;
	
	private bool unlocked;
	private Renderer rend;

	// Use this for initialization
	void Start () 
	{
		rend = renderer;
		if(planetID == 1)
		{
			if(DontDestoryValues.instance.isPlanetTwoUnlocked == 1)
			{
				unlocked = true;
				rend.material.mainTexture = colorImage;
			}
		}
		else if(planetID == 2)
		{
			if(DontDestoryValues.instance.isPlanetThreeUnlocked == 1)
			{
				unlocked = true;
				rend.material.mainTexture = colorImage;
			}
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(!unlocked)
		{
			if(planetID == 1)
			{
				if(DontDestoryValues.instance.isPlanetTwoUnlocked == 1)
				{
					unlocked = true;
					rend.material.mainTexture = colorImage;
				}
			}
			else if(planetID == 2)
			{
				if(DontDestoryValues.instance.isPlanetThreeUnlocked == 1)
				{
					unlocked = true;
					rend.material.mainTexture = colorImage;
				}
			}
		}
	}
}
