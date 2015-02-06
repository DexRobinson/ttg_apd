using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour {
	public static HealthBar instance;
	public GUITexture healthBar;
	private float currentHealth;
	private float maxHealth;
	private bool inUse;
	private Rect orgainlRect;
	
	void Awake()
	{
		instance = this;
	}
	
	void Start()
	{
		//if(iPhone.generation == iPhoneGeneration.iPad3Gen){
		//	guiTexture.pixelInset = new Rect(guiTexture.pixelInset.x * 2, guiTexture.pixelInset.y * 2, guiTexture.pixelInset.width * 2, guiTexture.pixelInset.height * 2);
		//	healthBar.guiTexture.pixelInset = new Rect(healthBar.guiTexture.pixelInset.x * 2, healthBar.guiTexture.pixelInset.y * 2, healthBar.guiTexture.pixelInset.width * 2, healthBar.guiTexture.pixelInset.height * 2);
		//	orgainlRect = healthBar.guiTexture.pixelInset;
		//}
		//else
		//{
			orgainlRect = healthBar.guiTexture.pixelInset;
		//}
		
		guiTexture.enabled = false;
		healthBar.guiTexture.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(inUse)
		{
			healthBar.guiTexture.pixelInset = new Rect(orgainlRect.x, orgainlRect.y, (currentHealth / maxHealth) * orgainlRect.width, orgainlRect.height);
		}
		
	}
	
	public void AddToCurrentHealth(float amount)
	{
		inUse = true;
		maxHealth += amount;
		currentHealth += amount;
		guiTexture.enabled = true;
		healthBar.guiTexture.enabled = true;
	}
	
	public void RemoveHealth(float amount)
	{
		currentHealth -= amount;
		if(currentHealth <= 0)
		{
			inUse = false;
			currentHealth = 0;
			maxHealth = 0;
			guiTexture.enabled = false;
			healthBar.guiTexture.enabled = false;
		}
	}
}
