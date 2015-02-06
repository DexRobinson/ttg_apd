using UnityEngine;
using System.Collections;

public class PowerHUD : MonoBehaviour {
	public static PowerHUD instance;
	
	public GameObject joystickButton;
	public Texture joystickPowerTexture;
	private Texture orgJoystickButton;
	
	public GameObject leftButton;
	public Texture leftButtonPowerTexture;
	private Texture orgLeftButton;
	
	public GameObject rightButton;
	public Texture rightButtonPowerTexture;
	private Texture orgRightButton;
	
	public GameObject fireMeterBottom;
	public Texture fireMeterPowerTextureBottom;
	private Texture orgFireMeterBottom;
	
	public GameObject fireMeterTop;
	public Texture fireMeterPowerTextureTop;
	private Texture orgFireMeterTop;
	
	public GameObject fireButton;
	public Texture fireButtonPowerTexture;
	private Texture orgFireButton;
	
	//public GameObject bombButton;
	//public Texture bombButtonPowerTexture;
	//private Texture orgBombButton;
	
	public GameObject planetOverlay;
	
	//public GameObject shield;
	//public Texture shieldPowerTexture;
	//private Texture orgShieldButton;
	
	public SpriteRenderer powerButton;
	public SpriteRenderer powerButtonSS;
	public GameObject powerButtonSSLighting;
	
	public GameObject powerPlanetAura;
	public GameObject powerWeaponAura;
	
	public GameObject auraPlanetTwo;
	public GameObject auraPlanetThree;
	
	public Sprite[] powerButtonTextures;
	
	public float powerTime = 0.0f;
	private bool changeHud;
	private float powerTimer = 10.0f;

    private Variables variables;

	void Awake()
	{
		instance = this;
        variables = GameObject.FindGameObjectWithTag("Variables").GetComponent<Variables>();
	}
	
	// Use this for initialization
	void Start () {
		powerTimer *= Modifiers.supwerWeaponMod;
		planetOverlay.renderer.enabled = false;
		powerButtonSSLighting.renderer.enabled = false;
		powerPlanetAura.renderer.enabled = false;
		powerWeaponAura.renderer.enabled = false;
				
		orgJoystickButton = joystickButton.guiTexture.texture;
		orgLeftButton = leftButton.renderer.material.mainTexture;
		orgRightButton = rightButton.renderer.material.mainTexture;
		orgFireMeterBottom = fireMeterBottom.renderer.material.mainTexture;
		orgFireMeterTop = fireMeterTop.renderer.material.mainTexture;
		orgFireButton = fireButton.renderer.material.mainTexture;
		//orgShieldButton = shield.renderer.material.mainTexture;
		//orgBombButton = bombButton.renderer.material.mainTexture;
	}
	
	// Update is called once per frame
	void Update () {

        if (variables.numberOfPowerCrystals < 5)
        {
			powerButtonSS.renderer.enabled = false;
			powerButton.renderer.enabled = true;
            powerButton.sprite = powerButtonTextures[variables.numberOfPowerCrystals];
		}
		else {
			powerButtonSS.renderer.enabled = true;
			powerButton.renderer.enabled = false;
		}

        if (variables.isPowerMode)
        {
			powerTime += Time.deltaTime;
			
			if(!changeHud)
				ChangeHud();

            if (powerTime > powerTimer || variables.playerCurrentHealth <= 0)
            {
				powerTime = 0.0f;
                variables.isPowerMode = false;
                if (variables.itemShieldCurrentAmount > 0)
				{
                    variables.TurnShieldsOn();
				}
				changeHud = false;
				ResetTexture();
			}
		}
	}
	
	void ChangeHud(){
        variables.ChangeWeapon(Variables.Weapon.Power);
		
		changeHud = true;
		joystickButton.guiTexture.texture = joystickPowerTexture;
		leftButton.renderer.material.mainTexture = leftButtonPowerTexture;
		rightButton.renderer.material.mainTexture = rightButtonPowerTexture;
		fireMeterBottom.renderer.material.mainTexture = fireMeterPowerTextureBottom;
		fireMeterTop.renderer.material.mainTexture = fireMeterPowerTextureTop;
		fireButton.renderer.material.mainTexture = fireButtonPowerTexture;
		//shield.renderer.material.mainTexture = shieldPowerTexture;
		//bombButton.renderer.material.mainTexture = bombButtonPowerTexture;
		
		powerButtonSSLighting.renderer.enabled = true;
		
		if(DontDestoryValues.instance.planetNumber == 2)
		{
			planetOverlay.renderer.enabled = true;
			powerPlanetAura.renderer.enabled = true;
		}
		else if(DontDestoryValues.instance.planetNumber == 0)
		{
			auraPlanetTwo.renderer.enabled = true;
		}
		else if(DontDestoryValues.instance.planetNumber == 1)
		{
			auraPlanetThree.renderer.enabled = true;
		}
		//powerWeaponAura.renderer.enabled = true;
	}
	
	public void ResetTexture () {
		joystickButton.guiTexture.texture = orgJoystickButton;
	 	leftButton.renderer.material.mainTexture = orgLeftButton;
		rightButton.renderer.material.mainTexture = orgRightButton;
		fireMeterBottom.renderer.material.mainTexture = orgFireMeterBottom;
		fireMeterTop.renderer.material.mainTexture = orgFireMeterTop;
		fireButton.renderer.material.mainTexture = orgFireButton;
		//shield.renderer.material.mainTexture = orgShieldButton;
		//bombButton.renderer.material.mainTexture = orgBombButton;
		planetOverlay.renderer.enabled = false;
		powerButtonSSLighting.renderer.enabled = false;
		powerPlanetAura.renderer.enabled = false;
		auraPlanetTwo.renderer.enabled = false;
		auraPlanetThree.renderer.enabled = false;
		//powerWeaponAura.renderer.enabled = false;

        variables.ChangeWeapon(Variables.Weapon.Single);
	}
}
