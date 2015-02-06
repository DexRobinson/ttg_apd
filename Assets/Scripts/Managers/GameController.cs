using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour 
{
	public static GameController instance;
	public GameObject sfxCube;
	public GameObject musicCube;
	
    private Ray ray;
    private RaycastHit hit;
    private Player p;
    private Variables v;
    public bool isMovingLeft;
    public bool isMovingRight;
    public bool isFiring;
	public GameObject lButton;
	public GameObject rButton;
	public GameObject pauseButton;
	private float newspaperLagTime = 0.0f;
	private bool newpaperActive;
	public Camera hudCamera;
	
	//  button, tilt, joystick
	public GameObject[] controlButtons;
	
	public GameObject[] specialEnemiesSpriteSheets;
	
	// 0 - buttons
	// 1 - tilt
	// 2 - joystick
	public static int currentControlType = 2;
	
	private float fireDelayTimer;
	private bool notFiring;
	private bool notLeft;
	private bool notRight;
	private float leftDelayTimer;
	private float rightDelayTime;


	public UnlockedCratesMenu unlockedCrateManager;

	void Awake()
	{
		instance = this;
	}
	
    void Start()
    {
        p = Player.instance;
        v = GameObject.FindGameObjectWithTag("Variables").GetComponent<Variables>();
		
		ChangeSFXTexture();
		ChangeMusicTexture();
		
		currentControlType = PlayerPrefs.GetInt("currentControlType");
		RenderBoxAroundButtonType();
    }
	
    void Update()
    {
		if(Input.GetKeyUp(KeyCode.B))
		{
			v.UnlockUpgrades(5);
		}

		if(currentControlType != 0)
		{
			lButton.gameObject.SetActive(false);
			rButton.gameObject.SetActive(false);
		}
		else
		{
			if(!lButton.activeSelf)
			{
				lButton.gameObject.SetActive(true);
				rButton.gameObject.SetActive(true);
			}
		}
		
		if(v.gameState == Variables.GameState.Tutorial || v.gameState == Variables.GameState.Game || v.gameState == Variables.GameState.Countdown)
		{
			if(notLeft)
			{
				leftDelayTimer += Time.deltaTime;
				if(leftDelayTimer > 0.2f)
				{
					leftDelayTimer = 0;
					isMovingLeft = false;
					notLeft = false;
				}
			}
			if(notRight)
			{
				rightDelayTime += Time.deltaTime;
				if(rightDelayTime > 0.2f)
				{
					isMovingRight = false;
					rightDelayTime = 0;
					notRight = false;
				}
			}
			if(notFiring)
			{
				fireDelayTimer += Time.deltaTime;
				if(fireDelayTimer > 0.2f)
				{
					fireDelayTimer = 0;
					isFiring = false;
					notFiring = false;
				}
			}
			if(Application.platform != RuntimePlatform.OSXWebPlayer && Application.platform != RuntimePlatform.WindowsWebPlayer && Application.platform != RuntimePlatform.WindowsPlayer && Application.platform != RuntimePlatform.OSXPlayer && Application.platform != RuntimePlatform.WindowsEditor && Application.platform != RuntimePlatform.OSXEditor)
			{
		        if (isMovingLeft)
		        {
		            p.MoveLeftEase();
		        }
		        if (isMovingRight)
		        {
		            p.MoveRightEase();
		        }
				
				if(!isMovingLeft && !isMovingRight)
				{
					p.EaseNoButtons();
				}
				
		        if (isFiring)
		        {
		            p.Fire();
		        }
			}
		}
		else
		{
			isFiring = false;
			isMovingRight = false;
			isMovingLeft = false;
		}
		
        if (Input.GetMouseButtonUp(0))
        {
            if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
				//Debug.Log(hit.transform.gameObject.name);

                ButtonDownEvents();
            }
        }
		 
        

        // handles all the controlls used inside of the game
        foreach (Touch touch in Input.touches)
        {
			//Touch touch = Input.GetTouch(0);
            ray = Camera.main.ScreenPointToRay(touch.position);
			
            if(Physics.Raycast(ray, out hit))
            {
                // handles all the single button press objects
                if (touch.phase == TouchPhase.Ended)
                {
                    ButtonDownEvents();
                }
				
				if(touch.phase == TouchPhase.Stationary)
				{
					if(hit.collider.tag == "Left")
					{
						isMovingLeft = true;
					}
					
					if(hit.collider.tag == "Right")
					{
						isMovingRight = true;
					}
					
					if (hit.collider.tag == "Fire")
                    {
						isFiring = true;
						if(v.weapon == Variables.Weapon.Power)
						{
							if(v.playerCurrentHealth > 0)
							{
								p.superWeaponCube.renderer.enabled = true;
								p.superWeaponCube.collider.enabled = true;
							
								if(DontDestoryValues.instance.planetNumber == 2)
									p.superWeaponAura.renderer.enabled = true;
							}
						}
						else
						{
							p.superWeaponCube.renderer.enabled = false;
							p.superWeaponCube.collider.enabled = false;
						
							if(DontDestoryValues.instance.planetNumber == 2)
								p.superWeaponAura.renderer.enabled = false;
						}
					}
				}
				else
				{
					if (hit.collider.tag == "Fire")
                    {
						if(isFiring)
						{
							notFiring = true;
							fireDelayTimer = 0;
	                        //v.playerCurrentTurnRate = 0;
	                        //isFiring = false;
							if(v.weapon == Variables.Weapon.Power)
							{
								p.superWeaponCube.renderer.enabled = false;
								p.superWeaponCube.collider.enabled = false;
								p.superWeaponAura.renderer.enabled = false;
								p.superWeaponAura.collider.enabled = false;
							}
						}
                    }

					if(hit.collider.tag == "Left")
					{
						if(isMovingLeft)
						{
							notLeft = true;
							leftDelayTimer = 0;
							//isMovingLeft = false;
						}
					}
					if(hit.collider.tag == "Right")
					{
						if(isMovingRight)
						{
							rightDelayTime = 0;
							notRight = true;
							//isMovingRight = false;
						}
					}

				}
				
                if (touch.phase == TouchPhase.Ended)
                {
					if (hit.collider.tag == "Effects")
                    {
                        DontDestoryValues.instance.ChangeEffect();
						ChangeSFXTexture();
                    }
					
					if (hit.collider.tag == "Sound")
                    {
                        DontDestoryValues.instance.ChangeMusic();
						ChangeMusicTexture();
                    }
					
                    if (hit.collider.tag == "Left")
                    {
                        if(isMovingLeft)
                        	isMovingLeft = false;
                    }

                    if (hit.collider.tag == "Right")
                    {
                        if(isMovingRight)
                        	isMovingRight = false;
                    }

                    if (hit.collider.tag == "Fire")
                    {
						if(isFiring)
						{
	                        //v.playerCurrentTurnRate = 0;
	                        isFiring = false;
							if(v.weapon == Variables.Weapon.Power)
							{
								p.superWeaponCube.renderer.enabled = false;
								p.superWeaponCube.collider.enabled = false;
								p.superWeaponAura.renderer.enabled = false;
								p.superWeaponAura.collider.enabled = false;
							}
						}
                    }
                }
            }
        }
		
		if(newpaperActive)
		{
			if(newspaperLagTime < 1.0f)
				newspaperLagTime += Time.deltaTime;
		}
		
		if (v.gameState == Variables.GameState.Menu)
        {
            isMovingRight = false;
            isMovingLeft = false;
            isFiring = false;
            v.upgradeCrates[1].transform.Rotate(new Vector3(0, 0.03f, 0));
        }
    }

	public void FireDone()
	{
		if(v.weapon == Variables.Weapon.Power)
		{
			p.superWeaponCube.renderer.enabled = false;
			p.superWeaponCube.collider.enabled = false;
			p.superWeaponAura.renderer.enabled = false;
			p.superWeaponAura.collider.enabled = false;
		}
	}
	public void FireInput()
	{
		p.Fire();

		if(v.weapon == Variables.Weapon.Power)
		{
			if(v.playerCurrentHealth > 0)
			{
				p.superWeaponCube.renderer.enabled = true;
				p.superWeaponCube.collider.enabled = true;
				
				if(DontDestoryValues.instance.planetNumber == 2)
					p.superWeaponAura.renderer.enabled = true;
			}
		}
		else
		{
			p.superWeaponCube.renderer.enabled = false;
			p.superWeaponCube.collider.enabled = false;
			
			if(DontDestoryValues.instance.planetNumber == 2)
				p.superWeaponAura.renderer.enabled = false;
		}
	}

	void SS(int index)
	{
		switch(index)
		{
		case 0:
			specialEnemiesSpriteSheets[0].renderer.enabled = true;
			
			specialEnemiesSpriteSheets[1].renderer.enabled = false;
			specialEnemiesSpriteSheets[2].renderer.enabled = false;
			specialEnemiesSpriteSheets[3].renderer.enabled = false;
			
			break;
		case 1:
			specialEnemiesSpriteSheets[1].renderer.enabled = true;
			
			specialEnemiesSpriteSheets[0].renderer.enabled = false;
			specialEnemiesSpriteSheets[2].renderer.enabled = false;
			specialEnemiesSpriteSheets[3].renderer.enabled = false;
			break;
		case 2:
			specialEnemiesSpriteSheets[2].renderer.enabled = true;
			
			specialEnemiesSpriteSheets[1].renderer.enabled = false;
			specialEnemiesSpriteSheets[0].renderer.enabled = false;
			specialEnemiesSpriteSheets[3].renderer.enabled = false;
			break;
		case 3:
			specialEnemiesSpriteSheets[3].renderer.enabled = true;
			
			specialEnemiesSpriteSheets[1].renderer.enabled = false;
			specialEnemiesSpriteSheets[2].renderer.enabled = false;
			specialEnemiesSpriteSheets[0].renderer.enabled = false;
			break;
		}
	}
	
	public void FireBomb()
	{
		p.FireBomb();
	}
	public void FireEscapePod()
	{
		if(v.gameState == Variables.GameState.Tutorial)
		{
			if(Tutorial.instance.currentIndex == 17){
				//p.FireEscapePod();
				Tutorial.instance.currentIndex++;
			}
		}
        // save people
        p.FireEscapePod();
	}
	
	public void FireSuperWeapon()
	{
		if(v.numberOfPowerCrystals == 5){
			v.laserSS.renderer.enabled = false;
			v.rapidSS.renderer.enabled = false;
			
			if(v.itemShieldCurrentAmount > 0)
				p.shield.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
			v.numberOfPowerCrystals = 0;
			v.isPowerMode = true;
		}
	}
	
    void ButtonDownEvents()
    {
        switch (hit.collider.tag)
        {
		case "Effects":
			if(Application.platform == RuntimePlatform.WindowsWebPlayer ||
			Application.platform == RuntimePlatform.OSXWebPlayer || 
			Application.platform == RuntimePlatform.OSXPlayer ||
			Application.platform == RuntimePlatform.WindowsPlayer ||
			Application.platform == RuntimePlatform.WindowsEditor)
			{
				DontDestoryValues.instance.ChangeEffect();
				ChangeSFXTexture();
			}
			break;
		case "Sound":
			if(Application.platform == RuntimePlatform.WindowsWebPlayer ||
			Application.platform == RuntimePlatform.OSXWebPlayer || 
			Application.platform == RuntimePlatform.OSXPlayer ||
			Application.platform == RuntimePlatform.WindowsPlayer ||
			Application.platform == RuntimePlatform.WindowsEditor)
			{
				DontDestoryValues.instance.ChangeMusic();
				ChangeMusicTexture();
			}
			break;
		case "Slot1":
			WeaponSlot.instance.ActivateSlot1();
			break;
		case "Slot2":
			WeaponSlot.instance.ActivateSlot2();
			break;
		case "Slot":
			WeaponSlot.instance.ChangeSlot();
			break;
		case "PowerMode":
			if(v.numberOfPowerCrystals == 5){
				v.laserSS.renderer.enabled = false;
				v.rapidSS.renderer.enabled = false;
				
				if(v.itemShieldCurrentAmount > 0)
					p.shield.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
				v.numberOfPowerCrystals = 0;
				v.isPowerMode = true;
			}
			break;
		case "Buttons":
			ChangeControlType(0);
			RenderBoxAroundButtonType();
			break;
		case "Tilt":
			ChangeControlType(1);
			RenderBoxAroundButtonType();
			break;
		case "Joystick":
			ChangeControlType(2);
			RenderBoxAroundButtonType();
			break;
            case "Left":
                isMovingLeft = true;
                break;
            case "Right":
                isMovingRight = true;
                break;
            case "Fire":
                isFiring = true;
				if(v.weapon == Variables.Weapon.Power)
				{
					if(v.playerCurrentHealth > 0)
					{
						p.superWeaponCube.renderer.enabled = true;
						p.superWeaponCube.collider.enabled = true;
						
						if(DontDestoryValues.instance.planetNumber == 2)
							p.superWeaponAura.renderer.enabled = true;
					}
				}
				else
				{
					p.superWeaponCube.renderer.enabled = false;
					p.superWeaponCube.collider.enabled = false;
				
					p.superWeaponAura.renderer.enabled = false;
				}
                break;
            case "Pause":
                // pause the game and collect your thoughts
                if (v.gameState == Variables.GameState.Game)
                {
					pauseButton.renderer.enabled = false;
                    Time.timeScale = 0;
                    v.ChangeGameState(Variables.GameState.Pause);
                    //v.cameraMiniMap.enabled = false;
					//Camera.main.enabled = false;
					hudCamera.enabled = false;
                    v.cameraPause.enabled = true;
                }
                else if (v.gameState == Variables.GameState.Pause)
                {
					pauseButton.renderer.enabled = true;
                    Time.timeScale = 1;
                    v.ChangeGameState(Variables.GameState.Game);
                    v.cameraPause.enabled = false;
					hudCamera.enabled = true;
					//Camera.main.enabled = true;
                    //v.cameraMiniMap.enabled = true;
                }
                break;
            case "ExitGame":
                // bye bye
                Time.timeScale = 1;
				//Debug.Log("ExitGame");
                //v.ChangeGameState(Variables.GameState.Game);
				v.ChangeGameState(Variables.GameState.GameOver);
                v.cameraPause.enabled = false;
				if(v.cameraRate.enabled)
					v.cameraRate.enabled = false;
			
				hudCamera.enabled = false;
				
                //v.cameraMiniMap.enabled = false;
			
				int oldHighest  = PlayerPrefs.GetInt("planet" + DontDestoryValues.instance.planetNumber + "highestLevel", 1);
				if(v.currentInvasion > oldHighest)
					PlayerPrefs.SetInt("planet" + DontDestoryValues.instance.planetNumber + "highestLevel", v.currentInvasion);
				
				GameManager.instance.SaveStats();
                v.playerCurrentHealth = 0;
				v.SaveInformationNow();
				Fade.instance.FadeToBlack("MainMenu");
                break;
            case "Upgrade":
                // checks to see the players health is greater than 0, if so go to upgrades, else return to main menu
                if (v.playerCurrentHealth > 0)
                {
                    if (v.currentInvasion == 10)
                    {
                        Application.LoadLevel("Victory");
                    }
                    else
                    {
						if(v.currentInvasion == 3)
						{
							// mort newspaper
							v.cameraUpgrade.enabled = true;
						}
						else if(v.currentInvasion == 4)
						{
							// stealer newspaper
						}
						else if(v.currentInvasion == 5)
						{
							// attacker newspaper
						}
						else if(v.currentInvasion == 6)
						{
							// mothership newspaper
						}
						else
						{
	                        Time.timeScale = 0;
	                        v.ApplyCrateTextures();
	                        v.cameraRoundOver.enabled = false;
	                        v.numberOfEnemiesSpawned = 0;
	                        v.numberOfEnemiesAlive = 0;
	                        v.cameraUpgrade.enabled = true;
	                        v.ChangeGameState(Variables.GameState.Menu);
						}
                    }
                }
                else
                {
					//Debug.Log("Upgrade");
                    Application.LoadLevel("MainMenu");
                }
                break;
            case "ReturnToGame":
				if (v.playerCurrentHealth > 0 && v.currentInvasion < 10)
                {
					DontDestoryValues.instance.TurnOnAudio();

					//Debug.Log("ReturnToGame");
					if(v.currentInvasion == 2 && DontDestoryValues.instance.planetNumber == 0)
					{
						newpaperActive = true;
						// mort newspaper
						v.cameraRoundOver.enabled = false;
                        v.cameraUpgrade.enabled = true;
						SS(0);
						v.newpaperInGame.renderer.material.mainTexture = v.levelNewspaper[0];
					}
					else if(v.currentInvasion == 3 && DontDestoryValues.instance.planetNumber == 0)
					{
						newpaperActive = true;
						// stealer newspaper
						v.cameraRoundOver.enabled = false;
                        v.cameraUpgrade.enabled = true;
						SS(1);
						v.newpaperInGame.renderer.material.mainTexture = v.levelNewspaper[1];
					}
					else if(v.currentInvasion == 5 && DontDestoryValues.instance.planetNumber == 0)
					{
						/*
						*        
						*	THIS IS TO UNLOCK THE GAME IT SHOULD BE != HERE!!!!
						*
						*
						*
						*
						*
						*
						*/
						if(PlayerPrefs.GetInt("FullVersion") != 180069)
						{
							// buy game
							v.cameraRoundOver.enabled = false;
							v.SaveInformationNow();
							hudCamera.enabled = false;
							//v.cameraMiniMap.enabled = false;
							Fade.instance.FadeToBlack("BuyGame");
						}
						else
						{
							newpaperActive = true;
							// attacker newspaper
							v.cameraRoundOver.enabled = false;
	                        v.cameraUpgrade.enabled = true;
							SS(2);
							v.newpaperInGame.renderer.material.mainTexture = v.levelNewspaper[2];
						}
					}
					else if(v.currentInvasion == 6 && DontDestoryValues.instance.planetNumber == 0)
					{
						newpaperActive = true;
						// mothership newspaper
						v.cameraRoundOver.enabled = false;
                        v.cameraUpgrade.enabled = true;
						SS(3);
						v.newpaperInGame.renderer.material.mainTexture = v.levelNewspaper[3];
					}
					else
					{
		                // get back in the action
		                // reactivate the current weapon and give it any new upgrades the user may of bought
						DontDestoryValues.instance.TurnOnAudio();
		                v.ChangeWeapon(v.weapon);
		                Time.timeScale = 1;
						v.cameraRoundOver.enabled = false;
		                v.cameraUpgrade.enabled = false;
						
						if(v.currentInvasion == 1 && DontDestoryValues.instance.planetNumber == 0 && v.upgradeLevel[9] == 0)
						{
							unlockedCrateManager.AnimateIn(9, Variables.CrateType.Shield);
						}
					else if(v.currentInvasion == 4 && DontDestoryValues.instance.planetNumber == 0 && v.upgradeLevel[4] == 0)
						{
							unlockedCrateManager.AnimateIn(4, Variables.CrateType.Rapid);
						}
					else if(v.currentInvasion == 9 && DontDestoryValues.instance.planetNumber == 0 && v.upgradeLevel[8] == 0)
						{
							unlockedCrateManager.AnimateIn(8, Variables.CrateType.Energy);
						}
					else if(v.currentInvasion == 5 && DontDestoryValues.instance.planetNumber == 1 && v.upgradeLevel[2] == 0)
						{
							unlockedCrateManager.AnimateIn(2, Variables.CrateType.Laser);
						}
					else if(v.currentInvasion == 9 && DontDestoryValues.instance.planetNumber == 1 && v.upgradeLevel[7] == 0)
						{
							unlockedCrateManager.AnimateIn(7, Variables.CrateType.Health);
						}
					else if(v.currentInvasion == 5 && DontDestoryValues.instance.planetNumber == 2 && v.upgradeLevel[5] == 0)
						{
							unlockedCrateManager.AnimateIn(5, Variables.CrateType.Flak);
						}
					else if(v.currentInvasion == 9 && DontDestoryValues.instance.planetNumber == 2 && v.upgradeLevel[6] == 0)
						{
							unlockedCrateManager.AnimateIn(6, Variables.CrateType.Mines);
						}
						else
						{
			                v.cameraInvasionBegin.enabled = true;
			                v.ChangeGameState(Variables.GameState.Countdown);
			                v.ChangeInvasion();
			                EnemySpawner.instance.SpawnAllEnemies();
						}
					}
				}
				else
				{
					v.SaveInformationNow();
					//HealthBar.instance.RemoveHealth(100000);
					v.cameraRoundOver.enabled = false;
	                v.cameraUpgrade.enabled = false;
					//v.cameraMiniMap.enabled = false;
					hudCamera.enabled = false;
					if (v.currentInvasion >= 10)
                    {
						//if(GameManager.bossDead)
						if(v.playerCurrentHealth > 0)
                        	Fade.instance.FadeToBlack("Victory");
						else
							Fade.instance.FadeToBlack("MainMenu");
                    }
					else
					{
						Fade.instance.FadeToBlack("MainMenu");
							//Application.LoadLevel("MainMenu");
					}
				}
                break;
            case "BuyUpgrade":
                // can you afford it?
                v.BuyUpgrade(v.upgradeCurrentCrate);
                break;
            case "RightUpgrade":
                // adjust the upgrades up 1
                v.MoveRight();
                break;
            case "LeftUpgrade":
                // adjust the upgrade down 1
                v.MoveLeft();
                break;
            case "Bomb":
                // KILL EVERYTHING!!!
                p.FireBomb();
                break;
            case "EscapePod":
				if(v.gameState == Variables.GameState.Tutorial)
				{
					if(Tutorial.instance.currentIndex == 17){
						//p.FireEscapePod();
						Tutorial.instance.currentIndex++;
					}
				}
                // save people
                p.FireEscapePod();
                break;
            case "Crystal":
				if(v.gameState == Variables.GameState.Tutorial)
				{
					if(Tutorial.instance.currentIndex == 15)
						Tutorial.instance.currentIndex++;
				}
                // gives crystals for energy to save people
                v.playerPlasmaCrystals++;
                hit.collider.gameObject.SetActive(false);
                break;
			case "NewspaperContButton":
				if(newspaperLagTime > 1.0f)
				{
					newspaperLagTime = 0.0f;
					newpaperActive = false;
					v.ChangeWeapon(v.weapon);
	                Time.timeScale = 1;
					v.cameraRoundOver.enabled = false;
	                v.cameraUpgrade.enabled = false;
					
                    if(DontDestoryValues.instance.audio.isPlaying)
					DontDestoryValues.instance.TurnOnAudio();

					if(v.currentInvasion == 4 && DontDestoryValues.instance.planetNumber == 0 && v.upgradeLevel[4] == 0)
					{
						unlockedCrateManager.AnimateIn(4, Variables.CrateType.Rapid);
					}

					else
					{
		                v.cameraInvasionBegin.enabled = true;
		                v.ChangeGameState(Variables.GameState.Countdown);
		                v.ChangeInvasion();
		                EnemySpawner.instance.SpawnAllEnemies();
					}
				}
				break;

        }
    }

	public void UpdateInvasionAfterUnlock()
	{
		v.cameraInvasionBegin.enabled = true;
		v.ChangeGameState(Variables.GameState.Countdown);
		v.ChangeInvasion();
		EnemySpawner.instance.SpawnAllEnemies();
	}

	public void PauseGame()
	{
		if (v.gameState == Variables.GameState.Game)
        {
			pauseButton.renderer.enabled = false;
            Time.timeScale = 0;
            v.ChangeGameState(Variables.GameState.Pause);
            //v.cameraMiniMap.enabled = false;
			//Camera.main.enabled = false;
			hudCamera.enabled = false;
            v.cameraPause.enabled = true;
        }
        else if (v.gameState == Variables.GameState.Pause)
        {
			pauseButton.renderer.enabled = true;
            Time.timeScale = 1;
            v.ChangeGameState(Variables.GameState.Game);
            v.cameraPause.enabled = false;
			hudCamera.enabled = true;
			//Camera.main.enabled = true;
            //v.cameraMiniMap.enabled = true;
        }
	}
	
	void ChangeMusicTexture()
	{
		if(DontDestoryValues.instance.musicVolume == 0)
		{
			musicCube.renderer.material.mainTexture = Resources.Load("MusicOff") as Texture2D;
		}
		else
		{
			musicCube.renderer.material.mainTexture = Resources.Load("MusicOn") as Texture2D;
		}
	}
	void ChangeSFXTexture()
	{
		if(DontDestoryValues.instance.effectVolume == 0)
		{
			sfxCube.renderer.material.mainTexture = Resources.Load("SFXOff") as Texture2D;
		}
		else
		{
			sfxCube.renderer.material.mainTexture = Resources.Load("SFXOn") as Texture2D;
		}
	}
	public static void ChangeControlType(int newControlType)
	{
		currentControlType = newControlType;
		PlayerPrefs.SetInt("currentControlType", currentControlType);
	}
	
	public void RenderBoxAroundButtonType()
	{
		int ct = PlayerPrefs.GetInt("currentControlType");
		
		for(int i = 0; i < controlButtons.Length; i++)
		{
			if(i == ct)
				controlButtons[i].renderer.enabled = true;
			else
				controlButtons[i].renderer.enabled = false;
		}
	}
}
