using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
	public static Player instance;							// get/set values in Player.cs

    public Variables variables;
    public GameController gameController;

	public GameObject player; 								// the planet
	public GameObject atmosphere;							// the atmosphere
	public GameObject radar;								// the radar
	public Transform[] bulletSpawn;							// firing points
	public Transform[] singleBulletSpawn;
	public Rigidbody[] bullets;								// all bullet projectiles
	public GameObject bomb;									// bomb explosion
	public GameObject bombExplosion;                        // particles
	public Texture2D[] planetTextures;// = new Texture2D[5];	// holds all the planet textures stages
	public Texture2D blankPlanetTexture;					// blank transparent texture
	public GameObject backgroundObject;						// galexy background
	public GameObject playerHealthBar;						// health bar object
	public GameObject playerEnergyBar;						// energy bar object

	public SpriteRenderer fireButton;
	public Sprite[] fireButtonTextures;					// buttons to activate
    
	public AudioClip audioSingleShot;						// audio for all the bullet sound effects
	public AudioClip audioFlakShot;
	public AudioClip audioRapidShot;
	public AudioClip audioLaserShot;
	public AudioClip audioBomb;
	public AudioClip seeker;
	public AudioClip[] superWeapon;
	public GameObject planetExplosion;
	public GameObject bombButton;
	public TextMesh numberOfBombsText;
	public GameObject[] planetPieces;
	//public TextMesh crystalAmmo;
	public GameObject shield;
	public GameObject ringTop;
	public Texture2D orgRingTopTexture;
	public Texture2D shieldRingTopTexture;
	public GameObject energyRing;
	public GameObject energyRingTicks;
	public GameObject planetHealthRing;
	public GameObject shieldRing;
	public GameObject bombFlash;
	
	//private Variables v;									// pulls information from Variables.cs
	private Transform t;									// transform of this GO	
	private Vector3 turnRate;								// how fast the planet will spin
	private float playerFireTimer;							// current fire time
	private float playerBombFireTimer;						// bomb fire rate
	
	private float multiTimer;
	private float seekerTimer;
	private float rapidTimer;
	private float laserTimer;
	private float flakTimer;
	public PeopleRunningManager peopleOnPlanetManager;
	GameObject[] projectiles = null;
	public int numberOfProjectiles = 7;
	GameObject[] rapidFireProjectiles = null;
	public int numberOfRapidFireProjectiles = 10;
	GameObject[] laserProjectiles = null;
	public int numberOflaserProjectiles = 3;
	GameObject[] multiProjectiles = null;
	public int numberOfMultiProjectiles = 20;
	public static GameObject[] seekerProjectiles = null;
	public static int numberOfSeekerProjectiles = 20;
	GameObject[] bombExplosionProjectile = null;
	GameObject[] bombProjectile = null;
	public int numberOfBombs = 1;
	GameObject[] flakProjectile = null;
	public int numberOfFlakProjectiles = 4;
	GameObject[] flakProjectilePower = null;
	public int numberOfFlakProjectilesPower = 4;
	public JoystickScript joystick;
	
	//private int planetDur = 1;
	private int planetMaxHealthMod = 1;
	private int planetMaxEnergyMod = 1;
	private int planetEnergyRegainMod = 1;
	private int planetHealthRegainMod = 1;
	public float turnrate = 0.0f;
	public Texture[] launchTextures;
	public GameObject launchButton;
	public GameObject launchButtonSS;
	public GameObject superWeaponCube;
	public GameObject superWeaponAura;
	public Renderer peopleOnPlanet;

	public Animator peopleAnimator;

	public Texture2D[] planet1Textures;
	public Texture2D[] planet2Textures;
	public Texture2D[] planet3Textures;

	public bool isFiring;
	private bool movingRight;
	public bool joystickBeingPressed;
	public float joystickPositionX;
	private float powerTimer = 17;

	public Transform planetTrans;
	public Transform backgroundTrans;

	public ProFlare proFlare;

    private ExplosionManager explosionManager;

	void Awake ()
	{
		instance = this;

		//v = variables;
		t = transform;
		powerTimer *= Modifiers.powerWeaponMod;
		LoadPlanetBuffs ();
		
		//variables.playerMaxHealth * planetMaxHealthMod;
		//variables.playerMaxEnergy * planetEnergyRegainMod;
		
		// set the inital size of the health/ energy bar

		// put the planet textures onto the planet
        
		projectiles = new GameObject[numberOfProjectiles];
		rapidFireProjectiles = new GameObject[numberOfRapidFireProjectiles];
		laserProjectiles = new GameObject[numberOflaserProjectiles];
		multiProjectiles = new GameObject[numberOfMultiProjectiles];
		seekerProjectiles = new GameObject[numberOfSeekerProjectiles];
		bombExplosionProjectile = new GameObject[numberOfBombs];
		bombProjectile = new GameObject[numberOfBombs];
		flakProjectile = new GameObject[numberOfFlakProjectiles];
		flakProjectilePower = new GameObject[numberOfFlakProjectilesPower];
		
		if (DontDestoryValues.instance.gameType == 0) {
			launchButton.SetActive (false);
			launchButtonSS.SetActive (false);
		} else {
			launchButtonSS.renderer.enabled = false;
		}
		
		StartCoroutine (Init ());

		InvokeRepeating ("RegainHealth", 0.0f, 2.0f);
		InvokeRepeating ("RegainEnergy", 0.0f, 0.1f);
        explosionManager = GameObject.FindGameObjectWithTag("Variables").GetComponent<ExplosionManager>();
	}

	void Start ()
	{
		StartCoroutine (AssignPlanetTextures ());
		ChangePlanetTextures ();
		ChangeFireButton (0);
	}

	void FixedUpdate ()
	{
		RotatingObjects ();
		numberOfBombsText.text = variables.playerBombAmount.ToString ();
		
		// checks the fire rate and shoots
		if (variables.gameState == Variables.GameState.Game || variables.gameState == Variables.GameState.Tutorial) {
			if (DontDestoryValues.instance.gameType == 1) {
				ChangeLaunchButton ();
			}
				
			if (playerFireTimer <= variables.playerCurrentFireRate) {
				playerFireTimer += Time.deltaTime;
			}
			if (playerBombFireTimer <= variables.playerBombFireRate) {
				playerBombFireTimer += Time.deltaTime;
			}
			if (variables.weapon != Variables.Weapon.Single) {
				if (variables.weapon != Variables.Weapon.Power) {
					RunPowerWeapon ();
				}
			}
		}
		
		//if(variables.gameState != Variables.GameState.GameOver || variables.gameState != Variables.GameState.Pause || variables.gameState != Variables.GameState.RoundOver)
		if (variables.gameState != Variables.GameState.Pause) {
			PlayerController ();
		}

		if (variables.playerCurrentHealth <= 0f) {
			atmosphere.renderer.material.color = new Color (1, 1, 1, 0.0f);
			atmosphere.renderer.enabled = false;
			//shield.renderer.enabled = false;
			player.renderer.material.mainTexture = blankPlanetTexture;
			peopleAnimator.SetInteger("state", 4);
		}
	}
	
	private void LoadPlanetBuffs ()
	{
		//planetDur = PlayerPrefs.GetInt("playerDurLevel", 0);
		planetMaxHealthMod = PlayerPrefs.GetInt ("playerMaxHealthLevel", 0);
		planetMaxEnergyMod = PlayerPrefs.GetInt ("playerMaxEnergyLevel", 0);
		planetEnergyRegainMod = PlayerPrefs.GetInt ("playerEnergyRegainLevel", 0);
		planetHealthRegainMod = PlayerPrefs.GetInt ("playerHealthRegainLevel", 0);
		
		float maxHealth = 12000 * (planetMaxHealthMod * 0.1f);
		float maxEnergy = 5000 * (planetMaxEnergyMod * 0.1f);
		float energyRegain = (planetEnergyRegainMod);
		float healthRegain = 10 * (planetHealthRegainMod);

        variables.playerMaxHealth += maxHealth;
        variables.playerMaxEnergy += maxEnergy;
        variables.playerEnergyRegainAmount += energyRegain;
        variables.playerHealthRegainAmount += healthRegain;

        variables.playerCurrentHealth = variables.playerMaxHealth;
        variables.playerCurrentEnegy = variables.playerMaxEnergy;
	}
	
	private void RotatingObjects ()
	{
		player.transform.Rotate (Vector3.forward * -1.1f * Time.deltaTime);
		//energyRingTicks.transform.Rotate(Vector3.forward * -40.4f * Time.deltaTime);
		//radar.transform.Rotate(Vector3.forward * -6f);
		atmosphere.transform.Rotate (Vector3.forward * 1.44f * Time.deltaTime);
		shield.transform.Rotate (Vector3.forward * -0.5f * Time.deltaTime);
		
		energyRing.transform.localRotation = Quaternion.Euler (0, 0, 225 - (variables.playerCurrentEnegy / variables.playerMaxEnergy) * 90);
		
		if (variables.itemShieldCurrentAmount == 0) {
			planetHealthRing.transform.localRotation = Quaternion.Euler (0, 0, 45 + (variables.playerCurrentHealth / variables.playerMaxHealth) * 90);
		} else {
			shieldRing.transform.localRotation = Quaternion.Euler (0, 0, 45 + (variables.itemShieldCurrentAmount / variables.itemShieldMaxAmount) * 90);
		}
		
		
		/*playerHealthBar.transform.localScale = 
				new Vector3 ((variables.playerCurrentHealth / variables.playerMaxHealth) * playerHealthBarSize,
				playerHealthBar.transform.localScale.y,
                playerHealthBar.transform.localScale.z);
			
			playerEnergyBar.transform.localScale = new Vector3 (
				playerEnergyBar.transform.localScale.x,
				(variables.playerCurrentEnegy / variables.playerMaxEnergy) * playerEnergyBarSize,
                playerEnergyBar.transform.localScale.z);*/
	}
	
	public void ChangeRingTopTexture ()
	{
		if (variables.itemShieldCurrentAmount == 0) {
			ringTop.renderer.material.mainTexture = orgRingTopTexture;
		} else {
			ringTop.renderer.material.mainTexture = shieldRingTopTexture;
		}
	}
	
	private IEnumerator Init ()
	{
		for (int i = 0; i < numberOfProjectiles; i++) {
			projectiles [i] = Instantiate (Resources.Load ("BulletSingle")) as GameObject;
			projectiles [i].SetActive (false);
		}

		yield return new WaitForSeconds (3f);


		for (int ix = 0; ix < numberOfRapidFireProjectiles; ix++) {
			rapidFireProjectiles [ix] = Instantiate (Resources.Load ("BulletRapid")) as GameObject;
			rapidFireProjectiles [ix].SetActive (false);
		}

		for (int xx = 0; xx < numberOflaserProjectiles; xx++) {
			laserProjectiles [xx] = Instantiate (Resources.Load ("BulletLaser")) as GameObject;
			laserProjectiles [xx].SetActive (false);
		}
		for (int ii = 0; ii < numberOfMultiProjectiles; ii++) {
			multiProjectiles [ii] = Instantiate (Resources.Load ("BulletMulti")) as GameObject;
			multiProjectiles [ii].SetActive (false);
		}

		for (int iz = 0; iz < numberOfSeekerProjectiles; iz++) {
			seekerProjectiles [iz] = Instantiate (Resources.Load ("BulletSeeker")) as GameObject;
			seekerProjectiles [iz].SetActive (false);
		}
		for (int ia = 0; ia < numberOfBombs; ia++) {
			bombExplosionProjectile [ia] = Instantiate (Resources.Load ("ExplosionBomb")) as GameObject;
			bombExplosionProjectile [ia].SetActive (false);

			bombProjectile [ia] = Instantiate (Resources.Load ("Bomb")) as GameObject;
			bombProjectile [ia].SetActive (false);
		}
		for (int iw = 0; iw < numberOfFlakProjectiles; iw++) {
			flakProjectile [iw] = Instantiate (Resources.Load ("Bullet_Flak")) as GameObject;
			flakProjectile [iw].SetActive (false);
		}
		
		for (int iw = 0; iw < numberOfFlakProjectilesPower; iw++) {
			flakProjectilePower [iw] = Instantiate (Resources.Load ("BulletFlakPower")) as GameObject;
			flakProjectilePower [iw].SetActive (false);
		}

	}

	#region Activate
	public void ActivateProjectile ()
	{
		for (int i = 0; i < numberOfProjectiles; i++) {
			if (projectiles [i].activeSelf == false) {
				//projectiles[i].SetActive(true);
				projectiles [i].GetComponent<Bullet> ().Activate ();

				return;
			}
		}
	}

	public void ActivateRapidProjectile ()
	{
		for (int i = 0; i < numberOfRapidFireProjectiles; i++) {
			if (rapidFireProjectiles [i].activeSelf == false) {
				//rapidFireProjectiles[i].SetActive(true);
				rapidFireProjectiles [i].GetComponent<Bullet> ().Activate ();

				return;
			}
		}
	}

	public void ActivateLaserProjectile ()
	{
		for (int i = 0; i < numberOflaserProjectiles; i++) {
			if (laserProjectiles [i].activeSelf == false) {
				//laserProjectiles[i].SetActive(true);
				laserProjectiles [i].GetComponent<Bullet> ().Activate ();

				return;
			}
		}
	}

	public void ActivateMultiProjectile ()
	{
		for (int i = 0; i < numberOfMultiProjectiles; i++) {
			if (multiProjectiles [i].activeSelf == false) {
				//multiProjectiles[i].SetActive(true);
				multiProjectiles [i].GetComponent<Bullet> ().Activate ();

				return;
			}
		}
	}

	public void ActivateSeekerProjectile ()
	{
		for (int i = 0; i < numberOfSeekerProjectiles; i++) {
			if (seekerProjectiles [i].activeSelf == false) {
				//seekerProjectiles[i].SetActive(true);
				seekerProjectiles [i].GetComponent<BoidFlocking> ().Activate ();

				return;
			}
		}
	}

	public static void ActivateSeekerProjectile (Vector3 position)
	{
		for (int i = 0; i < numberOfSeekerProjectiles; i++) {
			if (seekerProjectiles [i].activeSelf == false) {
				seekerProjectiles [i].SetActive (true);
				seekerProjectiles [i].transform.position = position;
				seekerProjectiles [i].GetComponent<BoidFlocking> ().Activate ();
				//seekerProjectiles [i].GetComponent<Bullet> ().ActivateSataliteSeeker ();

				return;
			}
		}
	}

	public void ActivateBombProjectile ()
	{
		for (int i = 0; i < numberOfBombs; i++) {
			if (bombExplosionProjectile [i].activeSelf == false) {
				bombExplosionProjectile [i].SetActive (true);
				//bombExplosionProjectile[i].GetComponent<Explosion>().Activate();

				bombProjectile [i].SetActive (true);
				//bombProjectile[i].GetComponent<Explosion>().Activate();

				return;
			}
		}
	}

	public void ActivateFlakProjectile ()
	{
		for (int i = 0; i < numberOfFlakProjectiles; i++) {
			if (flakProjectile [i].activeSelf == false) {
				Debug.Log ("LLL");
				//flakProjectile[i].SetActive(true);
				flakProjectile [i].GetComponent<Flak> ().Activate ();

				return;
			}
		}
	}
	
	public void ActivatePowerFlakProjectile ()
	{
		for (int i = 0; i < numberOfFlakProjectilesPower; i++) {
			if (flakProjectilePower [i].activeSelf == false) {
				//flakProjectile[i].SetActive(true);
                
				Debug.Log ("Commented this");
				//flakProjectilePower[i].GetComponent<Bullet>().Activate();

				return;
			}
		}
	}
	#endregion
	
	public void MoveLeftEase ()
	{
		if (!movingRight || turnrate == 0f)
			PlayerMoveLeft ();
		else {
			if (turnrate > 0) {
				if (movingRight) {
					PlayerMoveRight ();
				} else {
					PlayerMoveLeft ();
				}	
				
				turnrate -= 0.65f;
			} else {
				turnrate = 0;
			}
		}
	}

	public void MoveRightEase ()
	{
		if (movingRight || turnrate == 0f)
			PlayerMoveRight ();
		else {
			if (turnrate > 0) {
				if (movingRight) {
					PlayerMoveRight ();
				} else {
					PlayerMoveLeft ();
				}	
				
				turnrate -= 0.65f;
			} else {
				turnrate = 0;
			}
		}
	}

	public void EaseNoButtons ()
	{
		if (turnrate > 0) {
			if (movingRight) {
				PlayerMoveRight ();
			} else {
				PlayerMoveLeft ();
			}	
			
			turnrate -= 0.6f;
		} else {
			turnrate = 0;
		}
	}
	
	void PlayerController ()
	{
		if (Application.platform == RuntimePlatform.OSXWebPlayer || Application.platform == RuntimePlatform.WindowsWebPlayer || Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.OSXPlayer) {
			if (Input.GetKey (KeyCode.A)) {
				MoveLeftEase ();
			} else if (Input.GetKey (KeyCode.D)) {
				MoveRightEase ();
			} else {
				EaseNoButtons ();
			}
		
			if (Input.GetKey (KeyCode.Space)) {
				Fire ();
				if (variables.weapon == Variables.Weapon.Power) {
					if (variables.playerCurrentHealth > 0) {
						superWeaponCube.renderer.enabled = true;
						superWeaponCube.collider.enabled = true;
						if (DontDestoryValues.instance.planetNumber == 0)
							superWeaponAura.renderer.enabled = true;
					}
				} else {
					superWeaponCube.renderer.enabled = false;
					superWeaponCube.collider.enabled = false;
					if (DontDestoryValues.instance.planetNumber == 0)
						superWeaponAura.renderer.enabled = false;
				}
				//isFiring = true;
			} else if (Input.GetKeyUp (KeyCode.Space)) {
				if (superWeaponCube.renderer.enabled) {
					superWeaponCube.renderer.enabled = false;
					superWeaponCube.collider.enabled = false;
					if (DontDestoryValues.instance.planetNumber == 0)
						superWeaponAura.renderer.enabled = false;
				}
				//isFiring = false;
			}
			
			if (Input.GetKeyUp (KeyCode.LeftAlt) || Input.GetKeyUp (KeyCode.RightAlt)) {
				gameController.FireBomb ();
			}
			if (Input.GetKeyUp (KeyCode.LeftShift) || Input.GetKeyUp (KeyCode.RightShift)) {
				gameController.FireSuperWeapon ();
			}
			if (Input.GetKeyUp (KeyCode.Escape)) {
				gameController.PauseGame ();
			}
			if (Input.GetKeyUp (KeyCode.LeftControl) || Input.GetKeyUp (KeyCode.RightControl)) {
				gameController.FireEscapePod ();
			}
		}
		
		if (GameController.currentControlType == 2) 
		{
			if (joystickBeingPressed) 
			{
				t.Rotate (0, 0, -joystick.position.x * 1.8f);
				turnRate = new Vector3 (0, 0, -joystick.position.x * 1.8f);

				atmosphere.transform.eulerAngles += turnRate * 0.6f;
				planetTrans.eulerAngles += turnRate * 0.6f;
				backgroundTrans.eulerAngles += turnRate * 0.95f;
			} 
			else 
			{
				if (joystickPositionX > 0) 
				{
					joystickPositionX -= Time.deltaTime * 1.2f;
					if (joystickPositionX <= 0)
						joystickPositionX = 0;
				} 
				else if (joystickPositionX < 0) 
				{
					joystickPositionX += Time.deltaTime * 1.2f;
					if (joystickPositionX >= 0)
						joystickPositionX = 0;
				}

				turnRate = new Vector3 (0, 0, joystickPositionX * 1.2f);
				t.Rotate (0, 0, -joystickPositionX * 1.2f);

				atmosphere.transform.eulerAngles += turnRate * 0.6f;
				planetTrans.eulerAngles += turnRate * 0.6f;
				backgroundTrans.eulerAngles += turnRate * 0.95f;
			}
		} 
		else if (GameController.currentControlType == 1) 
		{
			Vector3 dir = Vector3.zero;
			dir.x = -Input.acceleration.x * 1.5f;

			if (dir.x > 0.25f || dir.x < -0.05f)
				t.Rotate (0, 0, -dir.x);

			turnRate = new Vector3 (0, 0, dir.x * 1.5f);

			atmosphere.transform.eulerAngles += turnRate * 0.6f;
			planetTrans.eulerAngles += turnRate * 0.25f;
			backgroundTrans.eulerAngles += turnRate * 0.85f;
		}
	}
	
	public void PlayerMoveLeft ()
	{
		if (movingRight) {
			if (turnrate == 0.0f) {
				turnrate = 0;
				movingRight = false;
			}
		}
		
		if (variables.gameState == Variables.GameState.Tutorial) {
			StartCoroutine (Tutorial.instance.UpdateIndex (3));
		}
		
		// move the player left, increase the turning rate for a gradual change in spin
		
		if (turnrate < 4.0f)
			turnrate += 0.3f;
		else
			turnrate = 4.0f;


		turnRate = new Vector3 (0, 0, turnrate* 0.5f);
		t.eulerAngles += turnRate;

		atmosphere.transform.eulerAngles += turnRate * 0.6f;
		planetTrans.eulerAngles += turnRate * 0.25f;
		backgroundTrans.eulerAngles += turnRate * 0.85f;

	}

	public void PlayerMoveRight ()
	{
		if (!movingRight) {
			if (turnrate == 0.0f) {
				turnrate = 0;
				movingRight = true;
			}
		}
		if (variables.gameState == Variables.GameState.Tutorial) {
			StartCoroutine (Tutorial.instance.UpdateIndex (3));
		}
		
		// move the player right, increase the turning rate for a gradual change in spin
		if (turnrate < 4.0f)
			turnrate += 0.3f;
		else
			turnrate = 4.0f;
		
		turnRate = new Vector3 (0, 0, turnrate * 0.5f);
		t.eulerAngles -= turnRate;

		atmosphere.transform.eulerAngles -= turnRate * 0.6f;
		planetTrans.eulerAngles -= turnRate * 0.25f;
		backgroundTrans.eulerAngles -= turnRate * 0.85f;
	}

	void RegainHealth ()
	{
		// used to auto regain health every couple of seconds
		if (variables.gameState == Variables.GameState.Game) {
			if (variables.playerCurrentHealth > 0)
				AdjustPlanetHealth (variables.playerHealthRegainAmount);
		}
	}

	public void Fire ()
	{
		if (variables.gameState == Variables.GameState.Tutorial) {
			StartCoroutine (Tutorial.instance.UpdateIndex (5));
		}
		
		// shots missiles, checks the player fire rate and checks energy levels
		if (variables.playerCurrentEnegy < variables.playerCurrentEnergyRate) {
			isFiring = false;
		}
		
		if (playerFireTimer >= variables.playerCurrentFireRate && variables.playerCurrentEnegy > variables.playerCurrentEnergyRate) {
			// increase the numbers of bullets shot, stats purpose, decrease energy levels
			variables.numberOfShotsRound += 1;
			variables.numberOfShotsTotal += 1;
			variables.playerCurrentEnegy -= variables.playerCurrentEnergyRate;
			isFiring = true;
			
			// 0 - Single
			// 1 - Seeker
			// 2 - Rapid
			// 3 - Multi
			// 4 - Laser
			// 5 - Flak
			switch (variables.weapon) {
			case Variables.Weapon.Flak:
				PlaySFX (audioFlakShot);
				ActivateFlakProjectile ();
				break;
			case Variables.Weapon.Laser:
				PlaySFX (audioLaserShot);
				ActivateLaserProjectile ();
				break;
			case Variables.Weapon.Multi:
				StartCoroutine (MultiShot (1 + variables.upgradeLevel [1]));
				break;
			case Variables.Weapon.Rapid:
				PlaySFX (audioRapidShot);
				ActivateRapidProjectile ();
				break;
			case Variables.Weapon.Seeker:
				PlaySFX (seeker);
				ActivateSeekerProjectile ();
				break;
			case Variables.Weapon.Single:
				if (superWeaponCube.renderer.enabled) {
					superWeaponCube.renderer.enabled = false;
					superWeaponCube.collider.enabled = false;
					superWeaponAura.renderer.enabled = false;
				}
				PlaySFX (audioSingleShot);
				ActivateProjectile ();
				break;
			case Variables.Weapon.Power:
				PlaySFX (superWeapon [DontDestoryValues.instance.planetNumber]);
					//superWeaponCube.renderer.enabled = true;
					//superWeaponCube.collider.enabled = true;
					//StartCoroutine(MultiShot(4));
				break;
			}

			playerFireTimer = 0;
		}
	}

	public void FireBomb ()
	{
		if (variables.gameState == Variables.GameState.Tutorial) {
			StartCoroutine (Tutorial.instance.UpdateIndex (7));
		}
		
		if (variables.playerBombAmount > 0) {
			if (playerBombFireTimer >= variables.playerBombFireRate) {
				// play bomb explosion
				PayBombFlash ();
				ActivateBombProjectile ();
				playerBombFireTimer = 0;
				variables.playerBombAmount--;
				variables.numberOfShotsRound += 1;
				variables.numberOfShotsTotal += 1;
			}
		}
	}

	public void FireEscapePod ()
	{
		if (variables.playerPlasmaCrystals >= 5) {
			// fire escape pod
			//Rigidbody escapePod = Instantiate(bullets[6], bulletSpawn[0].position, t.rotation) as Rigidbody;
			//escapePod.AddRelativeForce(Vector3.up * variables.singleBulletSpeed * 3 * Time.deltaTime);
			GameManager.ActivateEvacShip (bulletSpawn [0].position);
			launchButtonSS.renderer.enabled = false;
			
			variables.playerPlasmaCrystals -= 5;
			launchButton.renderer.enabled = true;
		}
	}

	void RunPowerWeapon ()
	{
		// runs down your power timer when you get a power up
		if (variables.playerPowerTimer < powerTimer) {
			variables.playerPowerTimer += Time.deltaTime;
		} else {
			superWeaponCube.renderer.enabled = false;
			superWeaponCube.collider.enabled = false;
			superWeaponAura.renderer.enabled = false;
			variables.playerPowerTimer = 0;
			variables.ChangeWeapon (Variables.Weapon.Single);
		}
	}
	
	public void PickNewWeapon (Variables.Weapon newWeapon)
	{
		switch (newWeapon) {
		case Variables.Weapon.Flak:
			flakTimer = 1;
			break;
		case Variables.Weapon.Laser:
			laserTimer = 1;
			break;
		case Variables.Weapon.Multi:
			multiTimer = 1;
			break;
		case Variables.Weapon.Rapid:
			rapidTimer = 1;
			break;
		case Variables.Weapon.Seeker:
			seekerTimer = 1;
			break;
		}
	}
	
	IEnumerator MultiShot (int missiles)
	{
		// used for the multi shot gun to fire multi missles
		for (int i = 0; i < missiles; i++) {
			if (variables.weapon == Variables.Weapon.Multi) {
				PlaySFX (audioSingleShot);
				ActivateMultiProjectile ();
			}
			// waits a random amount of time before firing off the next bullet
			yield return new WaitForSeconds (Random.Range (0.1f, 0.3f));
		}
	}

	IEnumerator AssignPlanetTextures ()
	{
		if (DontDestoryValues.instance.planetNumber == 0) {
			planetTextures [0] = planet1Textures [0];
            if(proFlare)
			    proFlare.GlobalTintColor = new Color(160, 178, 97);
		} else if (DontDestoryValues.instance.planetNumber == 1) {
			planetTextures [0] = planet2Textures [0];
            if (proFlare)
			    proFlare.GlobalTintColor = new Color(109, 116, 176);
		} else if (DontDestoryValues.instance.planetNumber == 2) {
			planetTextures [0] = planet3Textures [0];
            if (proFlare)
			    proFlare.GlobalTintColor = new Color(100, 110, 158);
		}

		atmosphere.renderer.material.mainTexture = Resources.Load ("Atmosphere" + DontDestoryValues.instance.planetNumber) as Texture2D;
		backgroundObject.renderer.material.mainTexture = Resources.Load ("Background_" + DontDestoryValues.instance.planetNumber) as Texture2D;

		yield return new WaitForSeconds (5.0f);

		if (DontDestoryValues.instance.planetNumber == 0) {
			planetTextures = planet1Textures;
		} else if (DontDestoryValues.instance.planetNumber == 1) {
			planetTextures = planet2Textures;
		} else if (DontDestoryValues.instance.planetNumber == 2) {
			planetTextures = planet3Textures;
		}

		// runs this at start to place the right planets textures onto the planet
		/*for (int i = 0; i < 5; i++)
        {
            planetTextures[i] = Resources.Load("Planet" + DontDestoryValues.instance.planetNumber + "_" + i) as Texture2D;
        }
		
		for (int i = 0; i < 3; i++)
        {
            peopleOnPlanetTextures[i] = Resources.Load("PeopleOnPlanet0_" + i) as Texture2D;
        }*/
		
	}

	public void TurnAtmosphereOff ()
	{
		atmosphere.renderer.enabled = false;
	}

	public void TurnAtmosphereOn ()
	{
		atmosphere.renderer.enabled = true;
	}
	
	void ChangePlanetTextures ()
	{
		float planetHealthPercent = variables.playerCurrentHealth / variables.playerMaxHealth;

		// these changes the textures of the planet when you lost/gain health
		if (planetHealthPercent >= 0.8f) {
			atmosphere.renderer.material.color = new Color (1, 1, 1, 1);
			player.renderer.material.mainTexture = planetTextures [0];
            
            if(peopleOnPlanetManager)
			    peopleAnimator.SetInteger("state", 0);
		} else if (planetHealthPercent < 0.8f && planetHealthPercent >= 0.6f) {
			atmosphere.renderer.material.color = new Color (1, 1, 1, 0.8f);
			player.renderer.material.mainTexture = planetTextures [1];
		} else if (planetHealthPercent < 0.6f && planetHealthPercent >= 0.4f) {
			atmosphere.renderer.material.color = new Color (1, 1, 1, 0.6f);
			player.renderer.material.mainTexture = planetTextures [2];
            if (peopleOnPlanetManager)
			    peopleAnimator.SetInteger("state", 1);
		} else if (planetHealthPercent < 0.4f && planetHealthPercent >= 0.2f) {
			atmosphere.renderer.material.color = new Color (1, 1, 1, 0.4f);
			peopleAnimator.SetInteger("state", 2);
			player.renderer.material.mainTexture = planetTextures [3];
		} else if (planetHealthPercent < 0.2f && planetHealthPercent > 0.0f) {
			atmosphere.renderer.material.color = new Color (1, 1, 1, 0.2f);
			player.renderer.material.mainTexture = planetTextures [4];
            if (peopleOnPlanetManager)
			    peopleAnimator.SetInteger("state", 3);
		} else if (variables.playerCurrentHealth <= 0f) {
			atmosphere.renderer.material.color = new Color (1, 1, 1, 0.0f);
			atmosphere.renderer.enabled = false;
			shield.renderer.enabled = false;
			player.renderer.material.mainTexture = blankPlanetTexture;
            if (peopleOnPlanetManager)
			    peopleAnimator.SetInteger("state", 4);
		}
	}

	public void AdjustPlanetHealth (float amount)
	{
		variables.playerCurrentHealth += amount;
		// lose/gain health with this
        
		if (variables.playerCurrentHealth >= variables.playerMaxHealth)
			variables.playerCurrentHealth = variables.playerMaxHealth;

		ChangePlanetTextures ();
	}
	
	public void RemovePlanetHealth (float amount)
	{
		variables.playerCurrentHealth -= (amount);
		peopleOnPlanetManager.SpawnPeople ();
		
		if (variables.playerCurrentHealth >= variables.playerMaxHealth)
			variables.playerCurrentHealth = variables.playerMaxHealth;

		ChangePlanetTextures ();
	}
	
	void RegainEnergy ()
	{
		if (variables.gameState == Variables.GameState.Game) {
			// increase the energy slowy
			variables.playerCurrentEnegy += variables.playerEnergyRegainAmount;
	
			if (variables.playerCurrentEnegy > variables.playerMaxEnergy)
				variables.playerCurrentEnegy = variables.playerMaxEnergy;
		}
	}
	
	public void BlowUpPlanet ()
	{
		variables.laserSS.renderer.enabled = false;
		variables.rapidSS.renderer.enabled = false;
		
		PayBombFlash ();
		ActivateBombProjectile ();
		playerBombFireTimer = 0;
		
		//ChangePlanetTextures();
		//PlayPlanetExplosion();
		////StartCoroutine(_Explode());
		//Instantiate(planetExplosion, new Vector3(transform.position.x, transform.position.y, transform.position.z - 4), Quaternion.identity);
	}
	
	private IEnumerator _Explode ()
	{
		for (int i = 0; i < 15; i++) {
			explosionManager.ActivateRandomExplosion (new Vector3 (transform.position.x + (Random.Range (-2.5f, 2.5f)), 
				transform.position.y + (Random.Range (-2.5f, 2.5f)), 
				transform.position.z - 4));
			
			yield return new WaitForSeconds (0.01f);
		}
	}
	
	public void ChangeFireButton (int index)
	{
		fireButton.sprite = fireButtonTextures [index];
	}
	
	public void ChangeLaunchButton ()
	{
		
		if (variables.playerPlasmaCrystals < 5) {
			launchButtonSS.renderer.enabled = false;
			launchButton.renderer.material.mainTexture = launchTextures [variables.playerPlasmaCrystals];
		} else {
			launchButton.renderer.enabled = false;
			launchButtonSS.renderer.enabled = true;
		}
	}
	
	void PlaySFX (AudioClip clip)
	{
		audio.volume = DontDestoryValues.instance.effectVolume;
		audio.PlayOneShot (clip);
	}

	void PlayPlanetExplosion ()
	{
		for (int i = 0; i < planetPieces.Length; i++) {
			planetPieces [i].renderer.enabled = true;
			planetPieces [i].animation.Play ();
		}
	}
	
	public void PayBombFlash ()
	{
		StartCoroutine (_PlayBombFlash ());
	}
	
	IEnumerator _PlayBombFlash ()
	{
		bombFlash.renderer.material.color = new Color (1, 1, 1, 0.75f);
		
		for (int i = 0; i < 10; i++) {
			bombFlash.renderer.material.color = new Color (1, 1, 1, 0.75f - (i * 0.075f));
			//Debug.Log(1.0f - (i * 0.14f));
			yield return new WaitForSeconds (0.1f);
		}
		
		bombFlash.renderer.material.color = new Color (1, 1, 1, 0);
	}
}
