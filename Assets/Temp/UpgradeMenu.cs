using UnityEngine;
using System.Collections;

public class UpgradeMenu : MonoBehaviour 
{
	public GameObject[] upgradeCrates;
	public Texture2D[] crateTextures;
    public Texture2D[] levelTextures;
	public GameObject levelLabel;
	public int currentCrate;
	
	void Start()
	{
		ApplyCrateTextures();
	}
	
	void MoveLeft()
	{
		currentCrate -= 1;
		
		if(currentCrate < 0)
		{
			currentCrate = crateTextures.Length - 1;
		}
		
		ApplyCrateTextures();
	}
	
	void MoveRight()
	{
		currentCrate += 1;
		
		if(currentCrate >= crateTextures.Length)
		{
			currentCrate = 0;
		}
		
		ApplyCrateTextures();
	}
	
	void ApplyCrateTextures()
	{
		for(int i = 0; i < 3; i++)
		{
			int tempI = i + currentCrate;
			
			if(tempI > crateTextures.Length - 1)
				tempI -= crateTextures.Length;

			upgradeCrates[i].renderer.material.mainTexture = crateTextures[tempI];
		}
		
		
		
		int tempCrateIndex = currentCrate + 1;
		
		if(tempCrateIndex > crateTextures.Length - 1)
		{
			tempCrateIndex -= crateTextures.Length;
		}

        levelLabel.renderer.material.mainTexture = levelTextures[Variables.instance.upgradeLevel[tempCrateIndex]];
	}
}
