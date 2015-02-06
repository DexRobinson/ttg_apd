using UnityEngine;
using System.Collections;

public class UnlockPlanet : MonoBehaviour 
{
	public int planetId;
	
	void Update()
	{
		if(MainMenuManager.planetSelectionMode)
		{
			if(planetId == 1)
			{
				if(DontDestoryValues.instance.isPlanetTwoUnlocked == 1)
					gameObject.renderer.enabled = false;
				else
					gameObject.renderer.enabled = true;
			}
			else if(planetId == 2)
			{
				if(DontDestoryValues.instance.isPlanetThreeUnlocked == 1)
					gameObject.renderer.enabled = false;
				else
					gameObject.renderer.enabled = true;
			}
		}
		else
		{
			gameObject.renderer.enabled = false;
		}
	}
}
