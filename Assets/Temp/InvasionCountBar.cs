using UnityEngine;
using System.Collections;

public class InvasionCountBar : MonoBehaviour {
	public static InvasionCountBar instance;
	public GUITexture bar;
	private float current;
	private float max;
	private bool inUse;
	private Rect orgainlRect;
	// Use this for initialization
	void Start () {
		/*if(iPhone.generation == iPhoneGeneration.iPad3Gen){
			guiTexture.pixelInset = new Rect(guiTexture.pixelInset.x * 2, guiTexture.pixelInset.y * 2, guiTexture.pixelInset.width * 2, guiTexture.pixelInset.height * 2);
			bar.guiTexture.pixelInset = new Rect(bar.guiTexture.pixelInset.x * 2, bar.guiTexture.pixelInset.y * 2, bar.guiTexture.pixelInset.width * 2, bar.guiTexture.pixelInset.height * 2);
			orgainlRect = bar.guiTexture.pixelInset;
		}
		else
		{*/
			orgainlRect = bar.guiTexture.pixelInset;
		//}
		
		guiTexture.enabled = false;
		bar.guiTexture.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(Variables.instance.currentInvasion == 5 || Variables.instance.currentInvasion == 10 || Variables.instance.playerCurrentHealth <= 0)
		{
			guiTexture.enabled = false;
			bar.guiTexture.enabled = false;
		}
		else
		{
			if(!guiTexture.enabled)
			{
				guiTexture.enabled = true;
				bar.guiTexture.enabled = true;
			}
			
			float amount = (Variables.instance.numberOfEnemiesSpawned / Variables.instance.numberOfEnemiesPerInvasion);
			if(amount > 1.0f)
				amount = 1.0f;
				
			bar.guiTexture.pixelInset = new Rect(orgainlRect.x, orgainlRect.y, amount * orgainlRect.width, orgainlRect.height);
		
		}
	}
}
