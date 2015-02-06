
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Variables : MonoBehaviour
{
    #region Variables

    public static Variables instance;
    public static GameObject playerGameObject;

    // enums
    public enum GameState
    {
        Game, 
        Menu,
        Pause,
        Countdown,
        RoundOver,
        GameOver,
        Evcauation,
        Attack,
		Tutorial
    }
    public enum Weapon
    {
        Single, 
        Multi,
        Laser,
        Flak,
        Rapid,
        Seeker,
		Power
    }
    public enum EnemyType
    {
        Normal,
        Split,
        Grow,
        Shrink,
        Mort,
        Shoot,
        MotherShip,
		Steal,
		PlanetTwoBall,
		Shoot2x
    }
    public enum CrateType
    {
        Money,
        Health,
        Energy,
        Shield,
        Satalite,
        Mines,
        Flak,
        Rapid,
        Multi,
        Laser,
        Seeker,
        Slow,
        Bomb,
		None
    }
    public enum SeekerType
    {
        Crate,
        Enemy,
        Player,
        Bomb,
        Normal,
        FlakShell,
        FlakBullet
    }
    public enum HUD
    {
        Right,
        Left,
        Fire
    }

    public GameState gameState; // current state of the game
    public Weapon weapon;       // current weapon
	public Weapon previousWeapon = Weapon.Single;
	
    // player stats
	public int comboBouns = 1;
	public float comboTimer = 0.0f;
	public bool startCombo;
	
	public int 		playerExperience = 0;
	public int 		playerMaxExperience = 750;
	public int 		playerCurrentLevel = 1;
	public int 		playerExperiencePoints = 0;
	
    public float    playerMaxHealth = 12000.0f;
    public float    playerCurrentHealth = 12000.0f;
    public float    playerHealthRegainAmount = 0.2f;
    public float    playerTurnRate = 0.2f;
    public float    playerMaxTurnSpeed = 2.0f;
    public float    playerCurrentTurnRate = 0;
    public float    playerCurrentFireRate = 0.6f;
    public float    playerPowerTimer = 0;
    public float    playerCurrentEnergyRate = 0.6f;
    public float    playerBulletDamage = 3.0f;
    public float    playerBulletSpeed = 300;
    public int      playerMoney = 0;
    public int      playerMoneyRound = 0;
    public int      playerMoneyTotal = 0;
    public int      playerBombAmount = 3;
    public float    playerCurrentEnegy = 100.0f;
    public float    playerMaxEnergy = 100.0f;
    public float    playerEnergyRegainAmount = 0.05f;
    public float    playerBombFireRate = 3.0f;  
    public int      playerPlasmaCrystals = 0;           // number of crystals the player has
    public int      playerEscapePodsRecused = 0;        // number of people saved in escape pods

    // game stats
    public int      currentInvasion = 1;                
    public float    numberOfEnemiesPerInvasion = 16;    // number of enemies to spawn per invasion
    public float    enemyIncreasedAmount = 1.3f;        // increase the number of enemies each round by this amount
    public float    numberOfEnemiesSpawned = 0;         // number of enemies spawned
    public float    numberOfEnemiesAlive = 0;           // total enemies alive
    public float    enemySpawnTimer = 3.0f;             // how fast to spawn enemies
    public float    rareEnemySpawnTimer = 15.0f;        // how fast to spawn rare enemies
    public int      enemiesToSpawnFromList = 4;         // the first x amount of enemies in this list to spawn from
    public int      rareToSpawnFromList = 0;
    public float    enemySpawnTimerAdjuster = 0.85f;    // reduced the enemy spawn timer by this %
    public float    minEnemySpawnTimeAmount = 0.8f;
    public int      numberOfItems = 0;                  // number of items on the screen
    public int      numberOfEnemiesKilledRound = 0;     // number of round kills
    public int      numberOfEnemiesKilledTotal = 0;     // number of total kills
    public int      numberOfShotsRound = 0;
    public int      numberOfShotsTotal = 0;
    public int      numberOfHitsRound = 0;
    public int      numberOfHitsTotal = 0;
    public float    roundAccuracy = 0;
    public float    totalAccuracy = 0;
    public int      boss3SpawnersLeft = 4;              // how many boss 3 arms left
    public bool     isBoss1Dead = false;                // is boss 1 dead?
    public int      numberOfMinesOnScreen = 0;          // how many mines are current;y orbiting
    
    // weapon stats
    public float    singleFireRate = 0.6f;
    public float    singleEnergyRate = 0.08f;
    public float    singleBulletSpeed = 2.0f;
    public float    singleBulletDamage = 3.0f;
    public float    multiFireRate = 0.7f;
    public float    multiEnergyRate = 0.09f;
    public float    multiPowerTimer = 30.0f;
    public float    multiBulletSpeed = 2.0f;
    public float    multiBulletDamage = 2.0f;
    public int      multiBulletAmount = 2;      // holds how many of the multi shot bullets get spawned
    public float    laserFireRate = 1.0f;
    public float    laserEnergyRate = 0.13f;
    public float    laserPowerTimer = 30.0f;
    public float    laserBulletSpeed = 12.0f;
    public float    laserBulletDamage = 7.0f;
	public float    laserBulletWidth = 1.0f;
    public float    flakFireRate = 1.4f;
    public float    flakEnergyRate = 0.15f;
    public float    flakPowerTimer = 30.0f;
    public float    flakBulletSpeed = 1.0f;
    public float    flakBulletDamage = 8.0f;
    public float    seekerFireRate = 0.6f;
    public float    seekerEnergyRate = 0.09f;
    public float    seekerPowerTimer = 30.0f;
    public float    seekerBulletSpeed = 2.0f;
    public float    seekerBulletDamage = 5.0f;
    public float    seekerFuelTime = 2.5f;
    public float    seekerSpeed = 4.0f;
    public float    rapidFireRate = 0.2f;
    public float    rapidEnergyRate = 0.03f;
    public float    rapidPowerTimer = 30.0f;
    public float    rapidBulletSpeed = 2.0f;
    public float    rapidBulletDamage = 1.5f;

    // item stats
    //public GameObject       itemCrate;                                          // crate prefab
    public List<CrateType>  itemCrateTypeSpawnList = new List<CrateType>();     // all the crate types that can be spawned
    public float            itemKillTimer = 15.0f;                              // remove the crate after this amount of time
    public float            itemHealthIncreaseAmount = 2.0f;
    public float            itemShieldMaxAmount = 4.0f;
    public float            itemShieldCurrentAmount = 0;
    public float            itemEnergyIncreaseAmount = 25;
    public int              itemMoneyPickupAmount = 30;
    public bool             itemSlowEnemies;
    public float            itemSlowTimer = 10.0f;
    public float            itemSlowTime = 0.0f;
    public int              itemNumberOfMinesDropped = 4;
    public bool             itemSataliteOn;
    public float            itemSataliteTimer = 20.0f;
    public float            itemSataliteTime = 0.0f;
	public float            itemSpawnTimer = 10.0f;

    // enemies
    //public GameObject[] enemyBlobSplitClones;       // holds the smaller blobs when the bigger blob splits
    //public Rigidbody    enemyMortBullet;            // bullet to apply shields, and steal crates
    //public Rigidbody    enemyPlayerSeekerBullet;    // seeks the players position
    //public GameObject   enemyFleetShip;             // little ships that get spawned from the mother ship
    public bool         enemyMotherShipOut;         // check to see if the mother ship is out, is so don't spawn another one
    public int          boss3BothDead = 2;

    // enemy attack times
    public float motherShipSpawnTime = 2.5f;
    public float mrMortAttackTime = 1.5f;
    public float fleetShootAttackTime = 2.0f;

    // player objects
    public GameObject shield;
    //public GameObject mines;
    public GameObject satalite;
    public GameObject plasmaCrystal;
    //public Rigidbody  flakShell;
    //public GameObject flakShellExplosion;
	//public Rigidbody  sataliteBullet;
    public int        numberOfMinesAlive = 0;

    // menu/HUD and camera objects
    public Camera       cameraPause;
    public Camera       cameraUpgrade;
    public Camera       cameraRoundOver;
    public Camera       cameraInvasionBegin;
    //public Camera       cameraMiniMap;
	public Camera		cameraRate;
	
    public GameObject   evacButton;
    public GameObject   achievementBoard;
	public GameObject   hurtScreen;
	public bool 		hitByEnemy;

    //upgrade options
    /// <summary>
    /// 0 - Single - 500
    /// 1 - Multi  - 750
    /// 2 - Laser  - 1250
    /// 3 - Seeker - 1250
    /// 4 - Rapid  - 1000
    /// 5 - Flak   - 1500
    /// 6 - Mines  - 800
    /// 7 - Health - 750
    /// 8 - Energy - 250
    /// 9 - Shield - 1000
    /// 10 - Slow  - 1000
    /// 11 - Money - 2000
    /// 12 - Bomb  - 1000
    /// 13 - Satalite - 1200
    /// 14 - Player Max Health - 2000
    /// 15 - Player Health Regen - 2500
    /// 16 - Player Energy Regen - 2500
    /// </summary>
    /// <param name="index"></param>
    public List<int>    upgradeCost;            // price of all the upgrades
    public List<int>    upgradeLevel;           // current level of the upgrades
    public GameObject[] upgradeCrates;          // the three crates that display the previous, current, next upgrade
    public Texture2D[]  upgradeCrateTextures;   // holds 12 textures to display the upgrade
    public Texture2D[]  upgradeLevelTextures;   // holds 4 textures for each level
    //public GameObject   upgradeInfoObject;      // displays the current upgrade option
    public int          upgradeCurrentCrate = 0;    // the current upgrade index
    public TextMesh     upgradePrice;           // the current price of currentCrate upgrade
	public TextMesh     upgradePriceB;  
    public TextMesh     playerMoneyLabel;       // the players current money
	public TextMesh     playerMoneyLabelB;
	public GameObject 	updgradeLevelObject;	// where the level textures get put
	public string[]		upgradeCrateDescription;
	public string[]		upgradeCrateDescription2;
	public TextMesh 	upgradeCrateDescriptionText;
	public TextMesh 	upgradeCrateDescriptionTextB;
	public TextMesh 	upgradeCrateDescriptionText2;
	public TextMesh 	upgradeCrateDescriptionTextB2;
	public TextMesh 	storeName;
	public TextMesh 	storeNameB;
	
	public int playerDurLevel = 0;
	public int playerMaxHealthLevel = 0;
	public int playerHealthRegainLevel = 0;
	public int playerMaxEnergyLevel = 0;
	public int playerEnergyRegainLevel = 0;
	public string[] storeNames;
	
    //save flag
    private bool isSaving;
	
	public static int highestLevelCompleted;
	public int numberOfPowerCrystals = 0;
	public bool isPowerMode;
	
	public Texture2D[] levelNewspaper;
	public GameObject newpaperInGame;
	public Camera planetCamera;
	private bool isBeingHurt;
	
	public GameObject laserSS;
	public GameObject rapidSS;
    #endregion

    private ExplosionManager explosionManager;

    void Awake()
    {
        instance = this;
		
        if (DontDestoryValues.instance.gameType != 1)
		{
			currentInvasion = DontDestoryValues.instance.startingLevel;
            Destroy(evacButton);
		}
    }

    void Start()
    {
		//if(PlayerPrefs.GetInt("") == 1)
			LoadInformation();
		
	 	if(Application.loadedLevelName == "Game" || Application.loadedLevelName == "Tutorial")
		{
			playerGameObject = GameObject.Find("Player");
            ChangeWeapon(Weapon.Single);
			LoadInCrates();
            explosionManager = GameObject.FindGameObjectWithTag("Variables").GetComponent<ExplosionManager>();

			//ReturnCrateDescription();
			//storeNames = new string[19];
			//ReturnStorename();
			//ApplyCrateTextures();
		}
		else
		{
			ApplyCrateTextures();
			ReturnCrateDescription();
			ReturnCrateDescription2();
			//storeNames = new string[19];
			ReturnStorename();
			ApplyCrateTextures();
		}
		
		playerBombAmount = 3 + upgradeLevel[12];
		//playerMoney = 10000;
		//PlayerPrefs.DeleteAll();
    }


    #region Global Functions
	public void SavePlayerBuffs()
	{
		PlayerPrefs.SetInt("playerDurLevel", playerDurLevel);
		PlayerPrefs.SetInt("playerMaxHealthLevel", playerMaxHealthLevel);
		PlayerPrefs.SetInt("playerHealthRegainLevel", playerHealthRegainLevel);
		PlayerPrefs.SetInt("playerMaxEnergyLevel", playerMaxEnergyLevel);
		PlayerPrefs.SetInt("playerEnergyRegainLevel", playerEnergyRegainLevel);
	}
	public void LoadPlayerBuffs()
	{
		playerDurLevel = PlayerPrefs.GetInt("playerDurLevel", 0);
		playerMaxHealthLevel = PlayerPrefs.GetInt("playerMaxHealthLevel", 0);
		playerHealthRegainLevel = PlayerPrefs.GetInt("playerHealthRegainLevel", 0);
		playerMaxEnergyLevel = PlayerPrefs.GetInt("playerMaxEnergyLevel", 0);
		playerEnergyRegainLevel = PlayerPrefs.GetInt("playerEnergyRegainLevel", 0);
	}
	public void BuyPlayerBuffs(int index)
	{
		if(playerExperiencePoints > 0)
		{
			switch(index)
			{
				case 0:
				if(playerDurLevel < 4)
				{
					playerDurLevel++;
					playerExperiencePoints--;
				}
				break;
				case 1:
				if(playerMaxHealthLevel < 4)
				{
					playerMaxHealthLevel++;
					playerExperiencePoints--;
				}
				break;
				case 2:
				if(playerHealthRegainLevel < 4)
				{
					playerHealthRegainLevel++;
					playerExperiencePoints--;
				}
				break;
				case 3:
				if(playerMaxEnergyLevel < 4)
				{
					playerMaxEnergyLevel++;
					playerExperiencePoints--;
				}
				break;
				case 4:
				if(playerEnergyRegainLevel < 4)
				{
					playerEnergyRegainLevel++;
					playerExperiencePoints--;
				}
				break;
			}
			
			SaveInformationNow();
			ReturnCrateDescription();
		}
	}
	
	public void AddExperience(int amount)
	{
		playerExperience += amount;
		
		if(playerExperience >= playerMaxExperience)
		{
			playerExperience -= playerMaxExperience;
			playerMaxExperience += 750 * playerCurrentLevel;
			playerCurrentLevel++;
			playerExperiencePoints++;
			GameManager.ActivateComboText("Level Up!");
		}
	}
	
	public IEnumerator PlayHurtScreen()
	{
        hitByEnemy = false;
		if(!isBeingHurt)
		{
			isBeingHurt = true;
			hurtScreen.renderer.material.color = new Color(1, 1, 1, 1);
			//Camera.main.animation.Play();
			planetCamera.animation.Play();
			
			for(int i = 0; i < 10; i++)
			{
	            hurtScreen.renderer.material.color = new Color(1, 1, 1, 1.0f - (i * 0.14f));
				//Debug.Log(1.0f - (i * 0.14f));
				yield return new WaitForSeconds(0.1f);
			}
			isBeingHurt = false;
		}
	}
	public void SaveInformationNow()
    {
        string tempUpgradeLevels = ConvertIntsToString(upgradeLevel);
        PlayerPrefs.SetString("UpgradeValues", tempUpgradeLevels);

        string tempUpgradeCost = ConvertIntsToString(upgradeCost);
        PlayerPrefs.SetString("UpgradePrice", tempUpgradeCost);
		
		string tempAchievements = ConvertIntsToString(DontDestoryValues.instance.achievementValues);
		PlayerPrefs.SetString("AchievementValues", tempAchievements);
		
        PlayerPrefs.SetFloat("PlayerHealth", playerMaxHealth);
        PlayerPrefs.SetFloat("PlayerEnergyRegen", playerEnergyRegainAmount);
        PlayerPrefs.SetFloat("PlayerHealthRegen", playerHealthRegainAmount);

       // int oldMoney = PlayerPrefs.GetInt("Money");
        //int newMoney = oldMoney + playerMoney;
        PlayerPrefs.SetInt("Money", playerMoney);

		//PlayerPrefs.SetInt("TotalMoney", PlayerMoneyTotalAmount(playerMoneyTotal));
        PlayerPrefs.SetInt("Planet2", DontDestoryValues.instance.isPlanetTwoUnlocked);
        PlayerPrefs.SetInt("Planet3", DontDestoryValues.instance.isPlanetThreeUnlocked);
		
		PlayerPrefs.SetInt("exp", playerExperience);
		PlayerPrefs.SetInt("expMax", playerMaxExperience);
		PlayerPrefs.SetInt("expLvl", playerCurrentLevel);
		PlayerPrefs.SetInt("expPoints", playerExperiencePoints);
		PlayerPrefs.SetInt("Save", 1);
		
		SavePlayerBuffs();
    }
	
    public void SaveInformation()
    {
        if (!isSaving)
        {
            isSaving = true;

            //string tempUpgradeLevels = ConvertIntsToString(upgradeLevel);
            //PlayerPrefs.SetString("UpgradeValues", tempUpgradeLevels);

            //string tempUpgradeCost = ConvertIntsToString(upgradeCost);
            //PlayerPrefs.SetString("UpgradePrice", tempUpgradeCost);
			
			string tempAchievements = ConvertIntsToString(DontDestoryValues.instance.achievementValues);
			PlayerPrefs.SetString("AchievementValues", tempAchievements);
			
            PlayerPrefs.SetFloat("PlayerHealth", playerMaxHealth);
            PlayerPrefs.SetFloat("PlayerEnergyRegen", playerEnergyRegainAmount);
            PlayerPrefs.SetFloat("PlayerHealthRegen", playerHealthRegainAmount);

           // int oldMoney = PlayerPrefs.GetInt("Money");
            //int newMoney = oldMoney + playerMoney;
            PlayerPrefs.SetInt("Money", playerMoney);
			//PlayerPrefs.SetInt("TotalMoney", PlayerMoneyTotalAmount(playerMoneyTotal));
            PlayerPrefs.SetInt("Planet2", DontDestoryValues.instance.isPlanetTwoUnlocked);
            PlayerPrefs.SetInt("Planet3", DontDestoryValues.instance.isPlanetThreeUnlocked);
			
			PlayerPrefs.SetInt("exp", playerExperience);
			PlayerPrefs.SetInt("expMax", playerMaxExperience);
			PlayerPrefs.SetInt("expLvl", playerCurrentLevel);
			PlayerPrefs.SetInt("expPoints", playerExperiencePoints);
			
			SavePlayerBuffs();
			
			PlayerPrefs.SetInt("Save", 1);
        }
    }
    public void LoadInformation()
    {
		if(PlayerPrefs.GetInt("Save") == 1)
		{
	        upgradeCost = SeperateIntsNums("UpgradePrice");
	        upgradeLevel = SeperateInts("UpgradeValues");
			
			DontDestoryValues.instance.achievementValues = SeperateInts("AchievementValues");
			DontDestoryValues.instance.LoadAchievements();
	        //playerMaxHealth = PlayerPrefs.GetFloat("PlayerHealth");
	        //playerEnergyRegainAmount = PlayerPrefs.GetFloat("PlayerEnergyRegen");
	        //playerHealthRegainAmount = PlayerPrefs.GetFloat("PlayerHealthRegen");
	        playerMoney = PlayerPrefs.GetInt("Money");
	        DontDestoryValues.instance.isPlanetTwoUnlocked = PlayerPrefs.GetInt("Planet2");
	        DontDestoryValues.instance.isPlanetThreeUnlocked = PlayerPrefs.GetInt("Planet3");
			
			playerExperience = PlayerPrefs.GetInt("exp");
			playerMaxExperience = PlayerPrefs.GetInt("expMax");
			playerCurrentLevel = PlayerPrefs.GetInt("expLvl");
			playerExperiencePoints = PlayerPrefs.GetInt("expPoints");
			
			LoadPlayerBuffs();
		}
		else
			SaveInformationNow();
    }
	public int PlayerMoneyTotalAmount(int newTotal)
	{
		int oldTotal = PlayerPrefs.GetInt("TotalMoney");
		oldTotal += newTotal;
		return oldTotal;
	}
    public void LoadInCrates()
    {
		if(PlayerPrefs.GetInt("Save") == 1)
		{
	        if (upgradeLevel[1] > 0)
	            itemCrateTypeSpawnList.Add(CrateType.Multi);
	        if (upgradeLevel[2] > 0)
	            itemCrateTypeSpawnList.Add(CrateType.Laser);
	        if (upgradeLevel[3] > 0)
	            itemCrateTypeSpawnList.Add(CrateType.Seeker);
	        if (upgradeLevel[4] > 0)
	            itemCrateTypeSpawnList.Add(CrateType.Rapid);
	        if (upgradeLevel[5] > 0)
	            itemCrateTypeSpawnList.Add(CrateType.Flak);
	        if (upgradeLevel[6] > 0)
	            itemCrateTypeSpawnList.Add(CrateType.Mines);
	        if (upgradeLevel[7] > 0)
	            itemCrateTypeSpawnList.Add(CrateType.Health);
	        if (upgradeLevel[8] > 0)
	            itemCrateTypeSpawnList.Add(CrateType.Energy);
	        if (upgradeLevel[9] > 0)
	            itemCrateTypeSpawnList.Add(CrateType.Shield);
	        if (upgradeLevel[10] > 0)
	            itemCrateTypeSpawnList.Add(CrateType.Slow);
	        if (upgradeLevel[11] > 0)
	            itemCrateTypeSpawnList.Add(CrateType.Money);
	        if (upgradeLevel[13] > 0)
	            itemCrateTypeSpawnList.Add(CrateType.Satalite);
		}
    }
	void ReturnStorename()
	{
		storeNames[0] = "single";
		storeNames[1] = "multi";
		storeNames[2] = "laser";
		storeNames[3] = "seeker";
		storeNames[4] = "rapid";
		storeNames[5] = "flak";
		storeNames[6] = "mines";
		storeNames[7] = "health";
		storeNames[8] = "energy";
		storeNames[9] = "shield";
		storeNames[10] = "slow";
		storeNames[11] = "money";
		storeNames[12] = "bombs";
		storeNames[13] = "satalite";
		storeNames[14] = "planet \ndurability";
		storeNames[15] = "max \nhealth";
		storeNames[16] = "health \nrecharge";
		storeNames[17] = "max \nenergy";
		storeNames[18] = "energy \nrecharge";
		
	}
	void ReturnCrateDescription2()
	{
		upgradeCrateDescription2[0] = "-your basic fire and forget missle";
		upgradeCrateDescription2[1] = "-a multi shot version of the single shot";
		upgradeCrateDescription2[2] = "-an energy beam of super destruction";
		upgradeCrateDescription2[3] = "-an enemy seeking missile";
		upgradeCrateDescription2[4] = "-a rapid fire turbo laser";
		upgradeCrateDescription2[5] = "-an energy bomb that makes a bang";
		upgradeCrateDescription2[6] = "-floating spheres of explosive goodness";
		upgradeCrateDescription2[7] = "-its like a gigantic bandage";
		upgradeCrateDescription2[8] = "-it refills your fire recharge rate";
		upgradeCrateDescription2[9] = "-an energy shield that protects your planet";
		upgradeCrateDescription2[10] = "-it slows down time";
		upgradeCrateDescription2[11] = "-more cash money in your crates";
		upgradeCrateDescription2[12] = "-start with more bombs";
		upgradeCrateDescription2[13] = "-a orbiting defense satalite that helps kill the enemy";
		upgradeCrateDescription2[14] = "-take less damage";
		upgradeCrateDescription2[15] = "-have more health";
		upgradeCrateDescription2[16] = "-faster health recharge  ";
		upgradeCrateDescription2[17] = "-more energy";
		upgradeCrateDescription2[18] = "-recharge enegy faster  ";
	}

    public StoreManagerNewUI storeManagerUI;
	public void ReturnCrateDescription()
	{
		float _singleDamage = 3 + (upgradeLevel[0] * 1);
		float _singleFireRate = singleFireRate - (upgradeLevel[0] * 0.075f);
		float _singleEnergyRate = 40 - (40 * (upgradeLevel[0] * 0.1f));
		float _sdn = (3 + ((upgradeLevel[0] + 1) * 1));
		float _sfrn = singleFireRate - ((1 + upgradeLevel[0]) * 0.075f);
		float _sdern = 40 - (40 * ((1 + upgradeLevel[0]) * 0.1f));

		if(upgradeLevel[0] != 4)
		{
            //storeManagerUI.SetStats(0, _singleDamage, 50, _sdn, "Damage");
            //storeManagerUI.SetStats(1, _singleFireRate, 2, _sfrn, "Fire Rate");
            //storeManagerUI.SetStats(2, _singleEnergyRate, 120, _sdern, "Energy Rate");
		}
		else
		{
            //storeManagerUI.SetStats(0, _singleDamage, 50, 0, "Damage");
            //storeManagerUI.SetStats(1, _singleFireRate, 2, 0, "Fire Rate");
            //storeManagerUI.SetStats(2, _singleEnergyRate, 120, 0, "Energy Rate");
		}

		float _multiDamage = 8 + (upgradeLevel[1] * 3);
		float _mulitiFR = multiFireRate - (upgradeLevel[1] * 0.01f);
		float _multiE = 100 - (100 * (upgradeLevel[1] * 0.1f));
		float _mdn = (8 + ((upgradeLevel[1] + 1) * 3));
		float _mfrn = multiFireRate - ((1 + upgradeLevel[1]) * 0.01f);
		float _men = 100 - (100 * ((1 + upgradeLevel[1]) * 0.1f));



		if(upgradeLevel[1] != 4)
		{
            //storeManagerUI.SetStats(0, _multiDamage, 50, _mdn, "Damage");
            //storeManagerUI.SetStats(1, _mulitiFR, 2, _mulitiFR, "Fire Rate");
            //storeManagerUI.SetStats(2, _multiE, 120, _multiE, "Energy Rate");

            //MainMenuManager.instance.statsBarManager.topBarStats[1] = new StatsInfo(50, _multiDamage, 50, _mdn);
            //MainMenuManager.instance.statsBarManager.middleBarStats[1] = new StatsInfo(2, _mfrn, 2, _mulitiFR);
            //MainMenuManager.instance.statsBarManager.bottomBarStats[1] = new StatsInfo(120, _men, 120, _multiE);

            //MainMenuManager.instance.statsBars[0].SetStatsBar(30, 30, _multiDamage, _mdn);
            //upgradeCrateDescription[1] = "damage: " + _multiDamage + " -> " + _mdn + "\n" +
            //                                "fire rate: " + _mulitiFR + " -> " + _mfrn + "\n" +
            //                                "energy used: " + _multiE + " -> " + _men;
		}
		else
		{
            //storeManagerUI.SetStats(0, _multiDamage, 50, 0, "Damage");
            //storeManagerUI.SetStats(1, _mulitiFR, 2, 0, "Fire Rate");
            //storeManagerUI.SetStats(2, _multiE, 120, 0, "Energy Rate");

            //MainMenuManager.instance.statsBarManager.topBarStats[1] = new StatsInfo(50, _multiDamage, 50, _multiDamage);
            //MainMenuManager.instance.statsBarManager.middleBarStats[1] = new StatsInfo(2, _mulitiFR, 2, _mulitiFR);
            //MainMenuManager.instance.statsBarManager.bottomBarStats[1] = new StatsInfo(120, _multiE, 120, _multiE);

            ////MainMenuManager.instance.statsBars[0].SetStatsBar(30, 30, _multiDamage, _multiDamage);
            //upgradeCrateDescription[1] = 	"damage: " + _multiDamage + "\n" +
            //                                "fire rate: " + _mulitiFR + "\n" +
            //                                "energy used: " + _multiE ;
		}
		
		float _ld = 17 + (upgradeLevel[2] * 5);
		float _lfr = laserFireRate - (laserFireRate * (upgradeLevel[2] * 0.1f));
		float _le = 120 - (120 * (upgradeLevel[2] * 0.1f));
		float _ldn = (17 + ((upgradeLevel[2] + 1) * 5));
		float _lfrn = laserFireRate - ((1 + upgradeLevel[2]) * 0.1f);
		float _len = 120 - (120 * ((upgradeLevel[2] + 1) * 0.1f));



		if(upgradeLevel[2] != 4)
		{
			MainMenuManager.instance.statsBarManager.topBarStats[2] = new StatsInfo(50, _ld, 50, _ldn);
			MainMenuManager.instance.statsBarManager.middleBarStats[2] = new StatsInfo(2, _lfrn, 2, _lfr);
			MainMenuManager.instance.statsBarManager.bottomBarStats[2] = new StatsInfo(120, _len, 120, _le);

			upgradeCrateDescription[2] = 	"damage: " + _ld + " -> " + _ldn + "\n" +
											"fire rate: " + _lfr + " -> " + _lfrn + "\n" +
											"energy used: " + _le + " -> " + _len;
		}
		else
		{
			MainMenuManager.instance.statsBarManager.topBarStats[2] = new StatsInfo(50, _ld, 50, _ld);
			MainMenuManager.instance.statsBarManager.middleBarStats[2] = new StatsInfo(2, _lfr, 2, _lfr);
			MainMenuManager.instance.statsBarManager.bottomBarStats[2] = new StatsInfo(120, _le, 120, _le);

			upgradeCrateDescription[2] = 	"damage: " + _ld + "\n" +
											"fire rate: " + _lfr + "\n" +
											"energy used: " + _le ;
		}
		
		float _ssd = 0.5f + (upgradeLevel[3] * 1);
		float _ssfr = seekerFireRate - (upgradeLevel[3] * 0.09f);
		float _sse = 50 - (50 * (upgradeLevel[3] * 0.1f));
		float _ssdn = 0.5f + ((1 + upgradeLevel[3]) * 1);
		float _ssfrn = seekerFireRate - ((1 + upgradeLevel[3]) * 0.09f);
		float _ssen = 50 - (50 * ((1 + upgradeLevel[3]) * 0.1f));



		if(upgradeLevel[3] != 4)
		{
			MainMenuManager.instance.statsBarManager.topBarStats[3] = new StatsInfo(50, _ssd, 50, _ssdn);
			MainMenuManager.instance.statsBarManager.middleBarStats[3] = new StatsInfo(2, _ssfrn, 2, _ssfr);
			MainMenuManager.instance.statsBarManager.bottomBarStats[3] = new StatsInfo(120, _ssen, 120, _sse);

			upgradeCrateDescription[3] = 	"damage: " + _ssd + " -> " + _ssdn + "\n" +
											"fire rate: " + _ssfr + " -> " + _ssfrn + "\n" +
											"energy used: " + _sse + " -> " + _ssen;
		}
		else
		{
			MainMenuManager.instance.statsBarManager.topBarStats[3] = new StatsInfo(50, _ssd, 50, _ssd);
			MainMenuManager.instance.statsBarManager.middleBarStats[3] = new StatsInfo(2, _ssfr, 2, _ssfr);
			MainMenuManager.instance.statsBarManager.bottomBarStats[3] = new StatsInfo(120, _sse, 120, _sse);

			upgradeCrateDescription[3] = 	"damage: " + _ssd + "\n" +
											"fire rate: " + _ssfr + "\n" +
											"energy used: " + _sse ;
		}
		
		
		float _rd = 3 + (upgradeLevel[4] * 1);
		float _rfr = rapidFireRate;
		float _re = 25 - (25 * (upgradeLevel[4] * 0.1f));
		float _rdn = (3 + ((upgradeLevel[4] + 1) * 1));
		float _rfrn = rapidFireRate;
		float _ren = 25 - (25 * ((1 + upgradeLevel[4]) * 0.1f));



		if(upgradeLevel[4] != 4)
		{
			MainMenuManager.instance.statsBarManager.topBarStats[4] = new StatsInfo(50, _rd, 50, _rdn);
			MainMenuManager.instance.statsBarManager.middleBarStats[4] = new StatsInfo(2, _rfrn, 2, _rfr);
			MainMenuManager.instance.statsBarManager.bottomBarStats[4] = new StatsInfo(120, _ren, 120, _re);

			upgradeCrateDescription[4] = 	"damage: " + _rd + " -> " + _rdn + "\n" +
											"fire rate: " + _rfr + " -> " + _rfrn + "\n" +
											"energy used: " + _re + " -> " + _ren;
		}
		else
		{
			MainMenuManager.instance.statsBarManager.topBarStats[4] = new StatsInfo(50, _rd, 50, _rd);
			MainMenuManager.instance.statsBarManager.middleBarStats[4] = new StatsInfo(2, _rfr, 2, _rfr);
			MainMenuManager.instance.statsBarManager.bottomBarStats[4] = new StatsInfo(120, _re, 120, _re);

			upgradeCrateDescription[4] = 	"damage: " + _rd + "\n" +
											"fire rate: " + _rfr + "\n" +
											"energy used: " + _re ;
		}
		
		float _fd = 20 + (upgradeLevel[5] * 3);
		float _ffr = 2 - (upgradeLevel[5] * 0.075f);
		float _fe = 120 - (120 * (upgradeLevel[5] * 0.1f));
		float _fdn = 20 + ((1 + upgradeLevel[5]) * 3);
		float _ffrn = 2 - ((1 + upgradeLevel[5]) * 0.075f);
		float _fen = 120 - (120 * ((1 + upgradeLevel[5]) * 0.1f));



		if(upgradeLevel[5] != 4)
		{
			MainMenuManager.instance.statsBarManager.topBarStats[5] = new StatsInfo(50, _fd, 50, _fdn);
			MainMenuManager.instance.statsBarManager.middleBarStats[5] = new StatsInfo(2, _ffrn, 2, _ffr);
			MainMenuManager.instance.statsBarManager.bottomBarStats[5] = new StatsInfo(120, _fen, 120, _fe);

			upgradeCrateDescription[5] = 	"damage: " + _fd + " -> " + _fdn + "\n" +
											"fire rate: " + _ffr + " -> " + _ffrn + "\n" +
											"energy used: " + _fe + " -> " + _fen;
		}
		else
		{
			MainMenuManager.instance.statsBarManager.topBarStats[5] = new StatsInfo(50, _fd, 50, _fd);
			MainMenuManager.instance.statsBarManager.middleBarStats[5] = new StatsInfo(2, _ffr, 2, _ffr);
			MainMenuManager.instance.statsBarManager.bottomBarStats[5] = new StatsInfo(120, _fe, 120, _fe);

			upgradeCrateDescription[5] = 	"damage: " + _fd + "\n" +
											"fire rate: " + _ffr + "\n" +
											"energy used: " + _fe ;
		}



		if(upgradeLevel[6] != 4)
		{
			MainMenuManager.instance.statsBarManager.topBarStats[6] = new StatsInfo(16, upgradeLevel[6] * 4, 16, ((1 + upgradeLevel[6]) * 4));
			MainMenuManager.instance.statsBarManager.middleBarStats[6] = new StatsInfo(0, 0, 0, 0);
			MainMenuManager.instance.statsBarManager.bottomBarStats[6] = new StatsInfo(0, 0, 0, 0);
			upgradeCrateDescription[6] = 	"mines: " + (upgradeLevel[6] * 4) + " -> " + ((1 + upgradeLevel[6]) * 4);
		}
		else
		{
			MainMenuManager.instance.statsBarManager.topBarStats[6] = new StatsInfo(16, upgradeLevel[6] * 4, 16, upgradeLevel[6] * 4);
			MainMenuManager.instance.statsBarManager.middleBarStats[6] = new StatsInfo(0, 0, 0, 0);
			MainMenuManager.instance.statsBarManager.bottomBarStats[6] = new StatsInfo(0, 0, 0, 0);
			upgradeCrateDescription[6] = 	"mines: " + (upgradeLevel[6] * 4);
		}
		
		if(upgradeLevel[7] != 4)
		{
			MainMenuManager.instance.statsBarManager.topBarStats[7] = new StatsInfo(100, upgradeLevel[7] * 10, 100, ((1 + upgradeLevel[7]) * 10));
			MainMenuManager.instance.statsBarManager.middleBarStats[7] = new StatsInfo(0, 0, 0, 0);
			MainMenuManager.instance.statsBarManager.bottomBarStats[7] = new StatsInfo(0, 0, 0, 0);

			upgradeCrateDescription[7] = 	"heal: " + (upgradeLevel[7] * 10) + "% -> " + ((1 + upgradeLevel[7]) * 10) + "%";
		}
		else
		{
			MainMenuManager.instance.statsBarManager.topBarStats[7] = new StatsInfo(100, upgradeLevel[7] * 10, 100, upgradeLevel[7] * 10);
			MainMenuManager.instance.statsBarManager.middleBarStats[7] = new StatsInfo(0, 0, 0, 0);
			MainMenuManager.instance.statsBarManager.bottomBarStats[7] = new StatsInfo(0, 0, 0, 0);

			upgradeCrateDescription[7] = 	"heal: " + (upgradeLevel[7] * 10) + "%";
		}
		
		if(upgradeLevel[8] != 4)
		{
			MainMenuManager.instance.statsBarManager.topBarStats[8] = new StatsInfo(100, upgradeLevel[8] * 20, 100, (1+ upgradeLevel[8]) * 20);
			MainMenuManager.instance.statsBarManager.middleBarStats[8] = new StatsInfo(0, 0, 0, 0);
			MainMenuManager.instance.statsBarManager.bottomBarStats[8] = new StatsInfo(0, 0, 0, 0);

			upgradeCrateDescription[8] = 	"recharge: " + (upgradeLevel[8] * 20) + "% -> " + ((1 + upgradeLevel[8]) * 20) + "%";
		}
		else
		{
			MainMenuManager.instance.statsBarManager.topBarStats[8] = new StatsInfo(100, upgradeLevel[8] * 20, 100, upgradeLevel[8] * 20);
			MainMenuManager.instance.statsBarManager.middleBarStats[8] = new StatsInfo(0, 0, 0, 0);
			MainMenuManager.instance.statsBarManager.bottomBarStats[8] = new StatsInfo(0, 0, 0, 0);

			upgradeCrateDescription[8] = 	"recharge: " + (upgradeLevel[8] * 20) + "%";
		}
		
		if(upgradeLevel[9] != 4)
		{
			MainMenuManager.instance.statsBarManager.topBarStats[9] = new StatsInfo(8, itemShieldMaxAmount + (upgradeLevel[9] * 1), 8, itemShieldMaxAmount + ((1+ upgradeLevel[9]) * 1));
			MainMenuManager.instance.statsBarManager.middleBarStats[9] = new StatsInfo(0, 0, 0, 0);
			MainMenuManager.instance.statsBarManager.bottomBarStats[9] = new StatsInfo(0, 0, 0, 0);


			upgradeCrateDescription[9] = 	"shield hp: " + (itemShieldMaxAmount + (upgradeLevel[9] * 1)) + " -> " + (itemShieldMaxAmount + ((1 + upgradeLevel[9]) * 1));
		}
		else
		{
			MainMenuManager.instance.statsBarManager.topBarStats[9] = new StatsInfo(8, itemShieldMaxAmount + (upgradeLevel[9] * 1), 8, itemShieldMaxAmount + (upgradeLevel[9] * 1));
			MainMenuManager.instance.statsBarManager.middleBarStats[9] = new StatsInfo(0, 0, 0, 0);
			MainMenuManager.instance.statsBarManager.bottomBarStats[9] = new StatsInfo(0, 0, 0, 0);

			upgradeCrateDescription[9] = 	"shield hp: " + (itemShieldMaxAmount + (upgradeLevel[9] * 1));
		}


		// START HERE!!!!
		if(upgradeLevel[10] != 4)
		{
			MainMenuManager.instance.statsBarManager.topBarStats[10] = new StatsInfo(50, upgradeLevel[10] * 10, 50, (1 + upgradeLevel[10]) * 10);
			MainMenuManager.instance.statsBarManager.middleBarStats[10] = new StatsInfo(0, 0, 0, 0);
			MainMenuManager.instance.statsBarManager.bottomBarStats[10] = new StatsInfo(0, 0, 0, 0);

			upgradeCrateDescription[10] = 	"slow time: " + (upgradeLevel[10] * 10) + "'s -> " + ((1 + upgradeLevel[10]) * 10) + "'s";
		}
		else
		{
			MainMenuManager.instance.statsBarManager.topBarStats[10] = new StatsInfo(50, upgradeLevel[10] * 10, 50, upgradeLevel[10] * 10);
			MainMenuManager.instance.statsBarManager.middleBarStats[10] = new StatsInfo(0, 0, 0, 0);
			MainMenuManager.instance.statsBarManager.bottomBarStats[10] = new StatsInfo(0, 0, 0, 0);

			upgradeCrateDescription[10] = 	"slow time: " + (upgradeLevel[10] * 10) + "'s";
		}
		
		if(upgradeLevel[11] != 4)
		{
			MainMenuManager.instance.statsBarManager.topBarStats[11] = new StatsInfo(25, upgradeLevel[11] * 5, 25, (1 + upgradeLevel[11]) * 5);
			MainMenuManager.instance.statsBarManager.middleBarStats[11] = new StatsInfo(0, 0, 0, 0);
			MainMenuManager.instance.statsBarManager.bottomBarStats[11] = new StatsInfo(0, 0, 0, 0);

			upgradeCrateDescription[11] = 	"crate money: " + (10 + (upgradeLevel[11] * 5)) + "$ -> " + (10 + ((1 + upgradeLevel[11]) * 5)) + "$";
		}
		else
		{
			MainMenuManager.instance.statsBarManager.topBarStats[11] = new StatsInfo(25, upgradeLevel[11] * 5, 25, upgradeLevel[11] * 5);
			MainMenuManager.instance.statsBarManager.middleBarStats[11] = new StatsInfo(0, 0, 0, 0);
			MainMenuManager.instance.statsBarManager.bottomBarStats[11] = new StatsInfo(0, 0, 0, 0);

			upgradeCrateDescription[11] = 	"crate money: " + (10 + (upgradeLevel[11] * 5)) + "$";
		}
		
		if(upgradeLevel[12] != 4)
		{
			MainMenuManager.instance.statsBarManager.topBarStats[12] = new StatsInfo(8, (3+upgradeLevel[12]) * 1, 8, (3 + ((1 + upgradeLevel[12])) * 1));
			MainMenuManager.instance.statsBarManager.middleBarStats[12] = new StatsInfo(0, 0, 0, 0);
			MainMenuManager.instance.statsBarManager.bottomBarStats[12] = new StatsInfo(0, 0, 0, 0);

			upgradeCrateDescription[12] = 	"starting bombs: " + (3 + (upgradeLevel[12] * 1)) + " -> " + (3 + ((1 + upgradeLevel[12]) * 1));
		}
		else
		{
			MainMenuManager.instance.statsBarManager.topBarStats[12] = new StatsInfo(8, (3 + upgradeLevel[12]) * 1, 8, (3 + upgradeLevel[12]) * 1);
			MainMenuManager.instance.statsBarManager.middleBarStats[12] = new StatsInfo(0, 0, 0, 0);
			MainMenuManager.instance.statsBarManager.bottomBarStats[12] = new StatsInfo(0, 0, 0, 0);

			upgradeCrateDescription[12] = 	"starting bombs: " + (3 + (upgradeLevel[12] * 1));
		}
		
		if(upgradeLevel[13] != 4)
		{
			MainMenuManager.instance.statsBarManager.topBarStats[13] = new StatsInfo(50, upgradeLevel[13] * 10, 50, 10 * (1 + upgradeLevel[13]));
			MainMenuManager.instance.statsBarManager.middleBarStats[13] = new StatsInfo(0, 0, 0, 0);
			MainMenuManager.instance.statsBarManager.bottomBarStats[13] = new StatsInfo(0, 0, 0, 0);

			upgradeCrateDescription[13] = 	"satalite time: " + (10 * upgradeLevel[13]) + "'s -> " + (10 * (1 + upgradeLevel[13])) + "'s";
		}
		else
		{
			MainMenuManager.instance.statsBarManager.topBarStats[13] = new StatsInfo(50, upgradeLevel[13] * 10, 50, upgradeLevel[13] * 10);
			MainMenuManager.instance.statsBarManager.middleBarStats[13] = new StatsInfo(0, 0, 0, 0);
			MainMenuManager.instance.statsBarManager.bottomBarStats[13] = new StatsInfo(0, 0, 0, 0);

			upgradeCrateDescription[13] = 	"satalite time: " + (10 * upgradeLevel[13]) + "'s";
		}
		
		if(playerDurLevel != 4)
		{
			MainMenuManager.instance.statsBarManager.topBarStats[14] = new StatsInfo(500, playerDurLevel * 85, 500, (1+playerDurLevel) * 85);
			MainMenuManager.instance.statsBarManager.middleBarStats[14] = new StatsInfo(0, 0, 0, 0);
			MainMenuManager.instance.statsBarManager.bottomBarStats[14] = new StatsInfo(0, 0, 0, 0);

			upgradeCrateDescription[14] = 	"damage reduction: " + (playerDurLevel * 85) + " -> " + ((1+playerDurLevel) * 85);
		}
		else
		{
			MainMenuManager.instance.statsBarManager.topBarStats[14] = new StatsInfo(500, playerDurLevel * 85, 500, playerDurLevel * 85);
			MainMenuManager.instance.statsBarManager.middleBarStats[14] = new StatsInfo(0, 0, 0, 0);
			MainMenuManager.instance.statsBarManager.bottomBarStats[14] = new StatsInfo(0, 0, 0, 0);

			upgradeCrateDescription[14] = 	"damage reduction: " + (playerDurLevel * 85);
		}
			
		if(playerMaxHealthLevel != 4)
		{
			MainMenuManager.instance.statsBarManager.topBarStats[15] = new StatsInfo(17000, 12000 + playerMaxHealthLevel * 1200, 17000, 12000 + (1+playerMaxHealthLevel) * 1200);
			MainMenuManager.instance.statsBarManager.middleBarStats[15] = new StatsInfo(0, 0, 0, 0);
			MainMenuManager.instance.statsBarManager.bottomBarStats[15] = new StatsInfo(0, 0, 0, 0);

			upgradeCrateDescription[15] = 	"max health: " + (12000 + (playerMaxHealthLevel * 1200)) + " -> " + (12000 + ((1 + playerMaxHealthLevel) * 1200));
		}
		else
		{
			MainMenuManager.instance.statsBarManager.topBarStats[15] = new StatsInfo(17000, 12000 + playerMaxHealthLevel * 1200, 17000, 12000 + playerMaxHealthLevel * 1200);
			MainMenuManager.instance.statsBarManager.middleBarStats[15] = new StatsInfo(0, 0, 0, 0);
			MainMenuManager.instance.statsBarManager.bottomBarStats[15] = new StatsInfo(0, 0, 0, 0);

			upgradeCrateDescription[15] = 	"max health: " + (12000 + (playerMaxHealthLevel * 1200));
		}
		
		if(playerHealthRegainLevel != 4)
		{
			MainMenuManager.instance.statsBarManager.topBarStats[16] = new StatsInfo(100, 50 + (playerHealthRegainLevel * 10), 100, 50 + ((1 + playerHealthRegainLevel) * 10));
			MainMenuManager.instance.statsBarManager.middleBarStats[16] = new StatsInfo(0, 0, 0, 0);
			MainMenuManager.instance.statsBarManager.bottomBarStats[16] = new StatsInfo(0, 0, 0, 0);

			upgradeCrateDescription[16] = 	"health regen: " + (50 + (playerHealthRegainLevel * 10)) + " -> " + (50 + ((1 + playerHealthRegainLevel) * 10));
		}
		else
		{
			MainMenuManager.instance.statsBarManager.topBarStats[16] = new StatsInfo(100, 50 + (playerHealthRegainLevel * 10), 100, 50 + (playerHealthRegainLevel * 10));
			MainMenuManager.instance.statsBarManager.middleBarStats[16] = new StatsInfo(0, 0, 0, 0);
			MainMenuManager.instance.statsBarManager.bottomBarStats[16] = new StatsInfo(0, 0, 0, 0);

			upgradeCrateDescription[16] = 	"health regen: " + (50 + (playerHealthRegainLevel * 10));
		}
		
		if(playerMaxEnergyLevel != 4)
		{
			MainMenuManager.instance.statsBarManager.topBarStats[17] = new StatsInfo(7000, 5000 + (playerMaxEnergyLevel * 500), 7000, 5000 + (1 + playerMaxEnergyLevel) * 500);
			MainMenuManager.instance.statsBarManager.middleBarStats[17] = new StatsInfo(0, 0, 0, 0);
			MainMenuManager.instance.statsBarManager.bottomBarStats[17] = new StatsInfo(0, 0, 0, 0);

			upgradeCrateDescription[17] = 	"max energy: " + (5000 + (playerMaxEnergyLevel * 500)) + " -> " + (5000 + ((1 + playerMaxEnergyLevel) * 500));
		}
		else
		{
			MainMenuManager.instance.statsBarManager.topBarStats[17] = new StatsInfo(7000, 5000 + (playerMaxEnergyLevel * 500), 7000, 5000 + (playerMaxEnergyLevel * 500));
			MainMenuManager.instance.statsBarManager.middleBarStats[17] = new StatsInfo(0, 0, 0, 0);
			MainMenuManager.instance.statsBarManager.bottomBarStats[17] = new StatsInfo(0, 0, 0, 0);

			upgradeCrateDescription[17] = 	"max energy: " + (5000 + (playerMaxEnergyLevel * 500));
		}
		
		if(playerEnergyRegainLevel != 4)
		{
			MainMenuManager.instance.statsBarManager.topBarStats[18] = new StatsInfo(8, 3 + (playerEnergyRegainLevel * 1), 8, 3 + ((1 + playerEnergyRegainLevel) * 1));
			MainMenuManager.instance.statsBarManager.middleBarStats[18] = new StatsInfo(0, 0, 0, 0);
			MainMenuManager.instance.statsBarManager.bottomBarStats[18] = new StatsInfo(0, 0, 0, 0);

			upgradeCrateDescription[18] = 	"energy regen: " + (3 + (playerEnergyRegainLevel * 1)) + " -> " + (3 + ((1 + playerEnergyRegainLevel) * 1));
		}
		else
		{
			MainMenuManager.instance.statsBarManager.topBarStats[18] = new StatsInfo(8, 3 + (playerEnergyRegainLevel * 1), 8, 3 + (playerEnergyRegainLevel * 1));
			MainMenuManager.instance.statsBarManager.middleBarStats[18] = new StatsInfo(0, 0, 0, 0);
			MainMenuManager.instance.statsBarManager.bottomBarStats[18] = new StatsInfo(0, 0, 0, 0);

			upgradeCrateDescription[18] = 	"energy regen: " + (3 + (playerEnergyRegainLevel * 1));
		}
		
	}

    public void ReturnCrateDescriptionNew(int index)
    {
        switch (index)
        {
            case 0:
                float _singleDamage = 3 + (upgradeLevel[0] * 1);
                float _singleFireRate = singleFireRate - (upgradeLevel[0] * 0.075f);
                float _singleEnergyRate = 40 - (40 * (upgradeLevel[0] * 0.1f));
                float _sdn = (3 + ((upgradeLevel[0] + 1) * 1));
                float _sfrn = singleFireRate - ((1 + upgradeLevel[0]) * 0.075f);
                float _sdern = 40 - (40 * ((1 + upgradeLevel[0]) * 0.1f));

                if (upgradeLevel[0] != 4)
                {
                    storeManagerUI.SetStats(0, _singleDamage, 50, _sdn, "Damage");
                    storeManagerUI.SetStats(1, _singleFireRate, 2, _sfrn, "Fire Rate");
                    storeManagerUI.SetStats(2, _singleEnergyRate, 120, _sdern, "Energy Rate");
                }
                else
                {
                    storeManagerUI.SetStats(0, _singleDamage, 50, 0, "Damage");
                    storeManagerUI.SetStats(1, _singleFireRate, 2, 0, "Fire Rate");
                    storeManagerUI.SetStats(2, _singleEnergyRate, 120, 0, "Energy Rate");
                }

                break;
            case 1:
                float _multiDamage = 8 + (upgradeLevel[1] * 3);
                float _mulitiFR = multiFireRate - (upgradeLevel[1] * 0.01f);
                float _multiE = 100 - (100 * (upgradeLevel[1] * 0.1f));
                float _mdn = (8 + ((upgradeLevel[1] + 1) * 3));
                float _mfrn = multiFireRate - ((1 + upgradeLevel[1]) * 0.01f);
                float _men = 100 - (100 * ((1 + upgradeLevel[1]) * 0.1f));

                if (upgradeLevel[1] != 4)
                {
                    storeManagerUI.SetStats(0, _multiDamage, 50, _mdn, "M Damage");
                    storeManagerUI.SetStats(1, _mulitiFR, 2, _mfrn, "Fire Rate");
                    storeManagerUI.SetStats(2, _multiE, 120, _men, "Energy Rate");
                }
                else
                {
                    storeManagerUI.SetStats(0, _multiDamage, 50, 0, "M Damage");
                    storeManagerUI.SetStats(1, _mulitiFR, 2, 0, "Fire Rate");
                    storeManagerUI.SetStats(2, _multiE, 120, 0, "Energy Rate");
                }
                break;
            case 2:
                float _ld = 17 + (upgradeLevel[2] * 5);
                float _lfr = laserFireRate - (laserFireRate * (upgradeLevel[2] * 0.1f));
                float _le = 120 - (120 * (upgradeLevel[2] * 0.1f));
                float _ldn = (17 + ((upgradeLevel[2] + 1) * 5));
                float _lfrn = laserFireRate - ((1 + upgradeLevel[2]) * 0.1f);
                float _len = 120 - (120 * ((upgradeLevel[2] + 1) * 0.1f));

                if (upgradeLevel[2] != 4)
                {
                    storeManagerUI.SetStats(0, _ld, 50, _ldn, "Damage");
                    storeManagerUI.SetStats(1, _lfr, 2, _lfrn, "Fire Rate");
                    storeManagerUI.SetStats(2, _le, 120, _len, "Energy Rate");
                }
                else
                {
                    storeManagerUI.SetStats(0, _ld, 50, 0, "Damage");
                    storeManagerUI.SetStats(1, _lfr, 2, 0, "Fire Rate");
                    storeManagerUI.SetStats(2, _le, 120, 0, "Energy Rate");
                }
                break;
            case 3:
                float _ssd = 0.5f + (upgradeLevel[3] * 1);
                float _ssfr = seekerFireRate - (upgradeLevel[3] * 0.09f);
                float _sse = 50 - (50 * (upgradeLevel[3] * 0.1f));
                float _ssdn = 0.5f + ((1 + upgradeLevel[3]) * 1);
                float _ssfrn = seekerFireRate - ((1 + upgradeLevel[3]) * 0.09f);
                float _ssen = 50 - (50 * ((1 + upgradeLevel[3]) * 0.1f));



                if (upgradeLevel[3] != 4)
                {
                    storeManagerUI.SetStats(0, _ssd, 50, _ssdn, "Damage");
                    storeManagerUI.SetStats(1, _ssfr, 2, _ssfrn, "Fire Rate");
                    storeManagerUI.SetStats(2, _sse, 120, _ssen, "Energy Rate");
                }
                else
                {
                    storeManagerUI.SetStats(0, _ssd, 50, 0, "Damage");
                    storeManagerUI.SetStats(1, _ssfr, 2, 0, "Fire Rate");
                    storeManagerUI.SetStats(2, _sse, 120, 0, "Energy Rate");
                }
                break;
            case 4:
                float _rd = 3 + (upgradeLevel[4] * 1);
                float _rfr = rapidFireRate;
                float _re = 25 - (25 * (upgradeLevel[4] * 0.1f));
                float _rdn = (3 + ((upgradeLevel[4] + 1) * 1));
                float _rfrn = rapidFireRate;
                float _ren = 25 - (25 * ((1 + upgradeLevel[4]) * 0.1f));



                if (upgradeLevel[4] != 4)
                {
                    storeManagerUI.SetStats(0, _rd, 50, _rdn, "Damage");
                    storeManagerUI.SetStats(1, _rfr, 2, _rfrn, "Fire Rate");
                    storeManagerUI.SetStats(2, _re, 120, _ren, "Energy Rate");
                }
                else
                {
                    storeManagerUI.SetStats(0, _rd, 50, 0, "Damage");
                    storeManagerUI.SetStats(1, _rfr, 2, 0, "Fire Rate");
                    storeManagerUI.SetStats(2, _re, 120, 0, "Energy Rate");
                }
                break;
            case 5:
                
                float _fd = 20 + (upgradeLevel[5] * 3);
                float _ffr = 2 - (upgradeLevel[5] * 0.075f);
                float _fe = 120 - (120 * (upgradeLevel[5] * 0.1f));
                float _fdn = 20 + ((1 + upgradeLevel[5]) * 3);
                float _ffrn = 2 - ((1 + upgradeLevel[5]) * 0.075f);
                float _fen = 120 - (120 * ((1 + upgradeLevel[5]) * 0.1f));



                if (upgradeLevel[5] != 4)
                {
                    storeManagerUI.SetStats(0, _fd, 50, _fdn, "Damage");
                    storeManagerUI.SetStats(1, _ffr, 2, _ffrn, "Fire Rate");
                    storeManagerUI.SetStats(2, _fe, 120, _fen, "Energy Rate");
                }
                else
                {
                    storeManagerUI.SetStats(0, _fd, 50, 0, "Damage");
                    storeManagerUI.SetStats(1, _ffr, 2, 0, "Fire Rate");
                    storeManagerUI.SetStats(2, _fe, 120, 0, "Energy Rate");
                }
                break;
            case 6:
                if (upgradeLevel[6] != 4)
                {
                    float current = upgradeLevel[6] * 4f;
                    float next = (1 + upgradeLevel[6]) * 4;

                    storeManagerUI.SetStats(0, current, 16f, next, "Mines(" + current + ")");
                    storeManagerUI.SetStatsOff(1);
                    storeManagerUI.SetStatsOff(2);
                }
                else
                {
                    float current = upgradeLevel[6] * 4f;
                    storeManagerUI.SetStats(0, current, 16f, 0, "Mines(" + current + ")");
                    storeManagerUI.SetStatsOff(1);
                    storeManagerUI.SetStatsOff(2);
                }

                break;
            case 7:
                if (upgradeLevel[7] != 4)
                {
                    float current = upgradeLevel[7] * 10;
                    float next = (1 + upgradeLevel[7]) * 10;

                    storeManagerUI.SetStats(0, current, 100f, next, "Heal(" + current + "%)");
                    storeManagerUI.SetStatsOff(1);
                    storeManagerUI.SetStatsOff(2);
                }
                else
                {
                    float current = upgradeLevel[7] * 10;

                    storeManagerUI.SetStats(0, current, 100f, 0, "Heal(" + current + "%)");
                    storeManagerUI.SetStatsOff(1);
                    storeManagerUI.SetStatsOff(2);
                }
                break;
            case 8:
                if (upgradeLevel[8] != 4)
                {
                    float current = upgradeLevel[8] * 20;
                    float next = (1 + upgradeLevel[8]) * 20;

                    storeManagerUI.SetStats(0, current, 100f, next, "Recharge(" + current + "%)");
                    storeManagerUI.SetStatsOff(1);
                    storeManagerUI.SetStatsOff(2);
                }
                else
                {
                    float current = upgradeLevel[8] * 20;

                    storeManagerUI.SetStats(0, current, 100f, 0, "Recharge(" + current + "%)");
                    storeManagerUI.SetStatsOff(1);
                    storeManagerUI.SetStatsOff(2);
                }
                break;
            case 9:
                if (upgradeLevel[9] != 4)
                {
                    float current = itemShieldMaxAmount + (upgradeLevel[9] * 1);
                    float next = itemShieldMaxAmount + ((1 + upgradeLevel[9]) * 1);

                    storeManagerUI.SetStats(0, current, 8, next, "Shield Health (" + current + " hits)");
                    storeManagerUI.SetStatsOff(1);
                    storeManagerUI.SetStatsOff(2);
                }
                else
                {
                    float current = itemShieldMaxAmount + (upgradeLevel[9] * 1);
                    float next = itemShieldMaxAmount + ((1 + upgradeLevel[9]) * 1);

                    storeManagerUI.SetStats(0, current, 8, 0, "Shield Health (" + current + " hits)");
                    storeManagerUI.SetStatsOff(1);
                    storeManagerUI.SetStatsOff(2);
                }
                break;
            case 10:
                if (upgradeLevel[10] != 4)
                {
                    float current = upgradeLevel[10] * 10;
                    float next = (1 + upgradeLevel[10]) * 10;

                    storeManagerUI.SetStats(0, current, 50, next, "Slow Time(" + current + " seconds)");
                    storeManagerUI.SetStatsOff(1);
                    storeManagerUI.SetStatsOff(2);
                }
                else
                {
                    float current = upgradeLevel[10] * 10;
                    float next = (1 + upgradeLevel[10]) * 10;

                    storeManagerUI.SetStats(0, current, 50, 0, "Slow Time(" + current + " seconds)");
                    storeManagerUI.SetStatsOff(1);
                    storeManagerUI.SetStatsOff(2);
                }
                break;
            case 11:
                if (upgradeLevel[11] != 4)
                {
                    float current = upgradeLevel[11] * 5;
                    float next = (1 + upgradeLevel[11]) * 5;

                    storeManagerUI.SetStats(0, current, 25, next, "Crate Money $" + current);
                    storeManagerUI.SetStatsOff(1);
                    storeManagerUI.SetStatsOff(2);
                }
                else
                {
                    float current = upgradeLevel[11] * 5;
                    float next = (1 + upgradeLevel[11]) * 5;

                    storeManagerUI.SetStats(0, current, 25, 0, "Crate Money $" + current);
                    storeManagerUI.SetStatsOff(1);
                    storeManagerUI.SetStatsOff(2);
                }
                break;
            case 12:
                if (upgradeLevel[11] != 4)
                {
                    float current = (3 + upgradeLevel[12]) * 1;
                    float next = (3 + ((1 + upgradeLevel[12])) * 1);

                    storeManagerUI.SetStats(0, current, 8, next, "Starting Bombs" + current);
                    storeManagerUI.SetStatsOff(1);
                    storeManagerUI.SetStatsOff(2);
                }
                else
                {
                    float current = (3 + upgradeLevel[12]) * 1;
                    float next = (3 + ((1 + upgradeLevel[12])) * 1);

                    storeManagerUI.SetStats(0, current, 8, 0, "Starting Bombs " + current);
                    storeManagerUI.SetStatsOff(1);
                    storeManagerUI.SetStatsOff(2);
                }
                break;
            case 13:
                if (upgradeLevel[13] != 4)
                {
                    float current = upgradeLevel[13] * 10;
                    float next = 10 * (1 + upgradeLevel[13]);

                    storeManagerUI.SetStats(0, current, 50, next, "Satalite Orbiting Time (" + current + " seconds)");
                    storeManagerUI.SetStatsOff(1);
                    storeManagerUI.SetStatsOff(2);
                }
                else
                {
                    float current = upgradeLevel[13] * 10;
                    float next = 10 * (1 + upgradeLevel[13]);

                    storeManagerUI.SetStats(0, current, 50, 0, "Satalite Orbiting Time (" + current + " seconds)");
                    storeManagerUI.SetStatsOff(1);
                    storeManagerUI.SetStatsOff(2);
                }
                break;
            case 14:
                if (playerDurLevel != 4)
                {
                    float current = playerDurLevel * 85;
                    float next = (1 + playerDurLevel) * 85;

                    storeManagerUI.SetStats(0, current, 500, next, "Damage Reduction (" + current + " damage)");
                    storeManagerUI.SetStatsOff(1);
                    storeManagerUI.SetStatsOff(2);
                }
                else
                {
                    float current = playerDurLevel * 85;
                    float next = (1 + playerDurLevel) * 85;

                    storeManagerUI.SetStats(0, current, 500, 0, "Damage Reduction (" + current + " damage)");
                    storeManagerUI.SetStatsOff(1);
                    storeManagerUI.SetStatsOff(2);
                }
                break;
            case 15:

                if (playerMaxHealthLevel != 4)
                {
                    float current = 12000 + playerMaxHealthLevel * 1200;
                    float next = (1 + playerMaxHealthLevel) * 1200;

                    storeManagerUI.SetStats(0, current, 17000, next, "Max Health (" + current + " health)");
                    storeManagerUI.SetStatsOff(1);
                    storeManagerUI.SetStatsOff(2);
                }
                else
                {
                    float current = 12000 + playerMaxHealthLevel * 1200;
                    float next = (1 + playerMaxHealthLevel) * 1200;

                    storeManagerUI.SetStats(0, current, 17000, 0, "Max Health (" + current + " health)");
                    storeManagerUI.SetStatsOff(1);
                    storeManagerUI.SetStatsOff(2);
                }
                break;
            case 16:
                if (playerHealthRegainLevel != 4)
                {
                    float current = 50 + (playerHealthRegainLevel * 10);
                    float next = 50 + ((1 + playerHealthRegainLevel) * 10);

                    storeManagerUI.SetStats(0, current, 100, next, "Health Regen (" + current + " health per second)");
                    storeManagerUI.SetStatsOff(1);
                    storeManagerUI.SetStatsOff(2);

                }
                else
                {
                    float current = 50 + (playerHealthRegainLevel * 10);
                    float next = 50 + ((1 + playerHealthRegainLevel) * 10);

                    storeManagerUI.SetStats(0, current, 100, 0, "Health Regen (" + current + " health per second)");
                    storeManagerUI.SetStatsOff(1);
                    storeManagerUI.SetStatsOff(2);
                }
                break;
            case 17:
                if (playerMaxEnergyLevel != 4)
                {
                    float current = (5000 + (playerMaxEnergyLevel * 500));
                    float next = (5000 + ((1 + playerMaxEnergyLevel) * 500));

                    storeManagerUI.SetStats(0, current, 7000, next, "Max Energy (" + current + " energy)");
                    storeManagerUI.SetStatsOff(1);
                    storeManagerUI.SetStatsOff(2);

                }
                else
                {
                    float current = (5000 + (playerMaxEnergyLevel * 500));
                    float next = (5000 + ((1 + playerMaxEnergyLevel) * 500));

                    storeManagerUI.SetStats(0, current, 7000, 0, "Max Energy (" + current + " energy)");
                    storeManagerUI.SetStatsOff(1);
                    storeManagerUI.SetStatsOff(2);
                }
                break;
            case 18:
                if (playerEnergyRegainLevel != 4)
                {
                    float current = 3 + (playerEnergyRegainLevel * 1);
                    float next = ((1 + playerEnergyRegainLevel) * 1);

                    storeManagerUI.SetStats(0, current, 8, next, "Energy Regen (" + current + " energy per second)");
                    storeManagerUI.SetStatsOff(1);
                    storeManagerUI.SetStatsOff(2);
                }
                else
                {
                    float current = 3 + (playerEnergyRegainLevel * 1);
                    float next = ((1 + playerEnergyRegainLevel) * 1);

                    storeManagerUI.SetStats(0, current, 8, 0, "Energy Regen (" + current + " energy per second)");
                    storeManagerUI.SetStatsOff(1);
                    storeManagerUI.SetStatsOff(2);
                }
                break;
            default:
                break;
        }


        if (playerEnergyRegainLevel != 4)
        {
            MainMenuManager.instance.statsBarManager.topBarStats[18] = new StatsInfo(8, 3 + (playerEnergyRegainLevel * 1), 8, 3 + ((1 + playerEnergyRegainLevel) * 1));
            MainMenuManager.instance.statsBarManager.middleBarStats[18] = new StatsInfo(0, 0, 0, 0);
            MainMenuManager.instance.statsBarManager.bottomBarStats[18] = new StatsInfo(0, 0, 0, 0);

            upgradeCrateDescription[18] = "energy regen: " + (3 + (playerEnergyRegainLevel * 1)) + " -> " + (3 + ((1 + playerEnergyRegainLevel) * 1));
        }
        else
        {
            MainMenuManager.instance.statsBarManager.topBarStats[18] = new StatsInfo(8, 3 + (playerEnergyRegainLevel * 1), 8, 3 + (playerEnergyRegainLevel * 1));
            MainMenuManager.instance.statsBarManager.middleBarStats[18] = new StatsInfo(0, 0, 0, 0);
            MainMenuManager.instance.statsBarManager.bottomBarStats[18] = new StatsInfo(0, 0, 0, 0);

            upgradeCrateDescription[18] = "energy regen: " + (3 + (playerEnergyRegainLevel * 1));
        }

    }
    /// <summary>
    /// displays the achievement board for x amount of seconds, then fades
    /// </summary>
    /// <returns></returns>
    public void DisplayAchievementUnlocked()
    {
        achievementBoard.renderer.enabled = true;
    }
    public void HideAchievementBoard()
    {
        achievementBoard.renderer.enabled = false;
    }

    /// <summary>
    /// returns the upgrade level of given index
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    bool ReturnUpgradeLevel(int index)
    {
        if (upgradeLevel[index] == 1)
            return true;
        else
            return false;
    }

    /// <summary>
    /// checks user money with current index price and current level to insure upgrade can be purchased
    /// </summary>
    /// <param name="index"></param>
    public void BuyUpgrade(int index)
    {
		playerMoney += 10000;
		if(index <= 13)
		{
        	if (playerMoney >= upgradeCost[index] && upgradeLevel[index] < 4)
        	{
	            // remove money, increase price, upgrade level
	            playerMoney -= upgradeCost[index];
	
	            upgradeLevel[index]++;
	            if (upgradeLevel[index] == 4)
	            {
	                upgradeCost[index] = 99999;
	            }
	            else
	            {
	                upgradeCost[index] *= 2;
	            }
				ReturnCrateDescription();
				
				playerMoneyLabel.text = "My Money: " + playerMoney.ToString();
				playerMoneyLabelB.text = "My Money: " + playerMoney.ToString();
				
				upgradePrice.text = "Cost: $" + upgradeCost[upgradeCurrentCrate].ToString();
				upgradePriceB.text = "Cost: $" + upgradeCost[upgradeCurrentCrate].ToString();
				
				//upgradePrice.text = upgradeCost[upgradeCurrentCrate].ToString();
				//upgradePriceB.text = upgradeCost[upgradeCurrentCrate].ToString();
			
            	// switch between index to upgrade the certain weapon or defensive upgrade
	            switch (index)
	            {
	                case 0:
	                    break;
	                case 1:
	                    if (ReturnUpgradeLevel(index))
	                        itemCrateTypeSpawnList.Add(CrateType.Multi);
	
	                    
	                    multiBulletAmount += 1;
	                    break;
	                case 2:
	                    if (ReturnUpgradeLevel(index))
	                        itemCrateTypeSpawnList.Add(CrateType.Laser);
	                    break;
	                case 3:
	                    if (ReturnUpgradeLevel(index))
	                        itemCrateTypeSpawnList.Add(CrateType.Seeker);
	                    break;
	                case 4:
	                    if (ReturnUpgradeLevel(index))
	                        itemCrateTypeSpawnList.Add(CrateType.Rapid);
	                    break;
	                case 5:
	                    if (ReturnUpgradeLevel(index))
	                        itemCrateTypeSpawnList.Add(CrateType.Flak);
	                    break;
	                case 6:
	                    if (ReturnUpgradeLevel(index))
	                        itemCrateTypeSpawnList.Add(CrateType.Mines);
	                    break;
	                case 7:
	                    if (ReturnUpgradeLevel(index))
	                        itemCrateTypeSpawnList.Add(CrateType.Health);
	                    break;
	                case 8:
	                    if (ReturnUpgradeLevel(index))
	                        itemCrateTypeSpawnList.Add(CrateType.Energy);
	                    break;
	                case 9:
	                    if (ReturnUpgradeLevel(index))
	                        itemCrateTypeSpawnList.Add(CrateType.Shield);
	
	                    if (itemShieldCurrentAmount > 0)
	                        itemShieldCurrentAmount += 1;
	                    break;
	                case 10:
	                    if (ReturnUpgradeLevel(index))
	                        itemCrateTypeSpawnList.Add(CrateType.Slow);
	                    break;
	                case 11:
	                    if (ReturnUpgradeLevel(index))
	                        itemCrateTypeSpawnList.Add(CrateType.Money);
	                    break;
	                case 12:
                    	playerBombAmount += 3;
	                    break;
	                case 13:
	                    if (ReturnUpgradeLevel(index))
	                        itemCrateTypeSpawnList.Add(CrateType.Satalite);
	                    break;
	            }
			}
        }
		
		else
		{
			switch (index)
            {
                case 14:
					BuyPlayerBuffs(0);
                    break;
				case 15:
					BuyPlayerBuffs(1);
                    break;
				case 16:
					BuyPlayerBuffs(2);
                    break;
				case 17:
					BuyPlayerBuffs(3);
                    break;
				case 18:
					BuyPlayerBuffs(4);
                    break;
			}
		}
		
		SaveInformationNow();
        ApplyCrateTextures();
		DontDestoryValues.instance.AchievementCheckMenu();
		ReturnCrateDescription();
        ReturnCrateDescriptionNew(index);
    }

	public void UnlockUpgrades(int index)
	{
		//playerMoney += 10000;
		if(index <= 13)
		{
			if (upgradeLevel[index] < 4)
			{
				// remove money, increase price, upgrade level
				//playerMoney -= upgradeCost[index];
				
				upgradeLevel[index]++;
				if (upgradeLevel[index] == 4)
				{
					upgradeCost[index] = 99999;
				}
				else
				{
					upgradeCost[index] *= 2;
				}
				//ReturnCrateDescription();
				
				//playerMoneyLabel.text = "My Money: " + playerMoney.ToString();
				//playerMoneyLabelB.text = "My Money: " + playerMoney.ToString();
				
				//upgradePrice.text = "Cost: $" + upgradeCost[upgradeCurrentCrate].ToString();
				//upgradePriceB.text = "Cost: $" + upgradeCost[upgradeCurrentCrate].ToString();
				
				//upgradePrice.text = upgradeCost[upgradeCurrentCrate].ToString();
				//upgradePriceB.text = upgradeCost[upgradeCurrentCrate].ToString();
				
				// switch between index to upgrade the certain weapon or defensive upgrade
				switch (index)
				{
				case 0:
					break;
				case 1:
					if (ReturnUpgradeLevel(index))
						itemCrateTypeSpawnList.Add(CrateType.Multi);
					
					
					multiBulletAmount += 1;
					break;
				case 2:
					if (ReturnUpgradeLevel(index))
						itemCrateTypeSpawnList.Add(CrateType.Laser);
					break;
				case 3:
					if (ReturnUpgradeLevel(index))
						itemCrateTypeSpawnList.Add(CrateType.Seeker);
					break;
				case 4:
					if (ReturnUpgradeLevel(index))
						itemCrateTypeSpawnList.Add(CrateType.Rapid);
					break;
				case 5:
					if (ReturnUpgradeLevel(index))
						itemCrateTypeSpawnList.Add(CrateType.Flak);
					break;
				case 6:
					if (ReturnUpgradeLevel(index))
						itemCrateTypeSpawnList.Add(CrateType.Mines);
					break;
				case 7:
					if (ReturnUpgradeLevel(index))
						itemCrateTypeSpawnList.Add(CrateType.Health);
					break;
				case 8:
					if (ReturnUpgradeLevel(index))
						itemCrateTypeSpawnList.Add(CrateType.Energy);
					break;
				case 9:
					if (ReturnUpgradeLevel(index))
						itemCrateTypeSpawnList.Add(CrateType.Shield);
					
					if (itemShieldCurrentAmount > 0)
						itemShieldCurrentAmount += 1;
					break;
				case 10:
					if (ReturnUpgradeLevel(index))
						itemCrateTypeSpawnList.Add(CrateType.Slow);
					break;
				case 11:
					if (ReturnUpgradeLevel(index))
						itemCrateTypeSpawnList.Add(CrateType.Money);
					break;
				case 12:
					playerBombAmount += 3;
					break;
				case 13:
					if (ReturnUpgradeLevel(index))
						itemCrateTypeSpawnList.Add(CrateType.Satalite);
					break;
				}
			}
		}
		
		SaveInformationNow();
		//ApplyCrateTextures();
		//DontDestoryValues.instance.AchievementCheckMenu();
		//ReturnCrateDescription();
	}

    /// <summary>
    /// when moving through the store this applys the show crate with the correct texture to show the user
    /// </summary>
    public void ApplyCrateTextures()
    {
        //upgradeCrates[0].renderer.material.mainTexture = upgradeCrateTextures[LeftCrateId()];
        upgradeCrates[1].renderer.material.mainTexture = upgradeCrateTextures[upgradeCurrentCrate];
        //upgradeCrates[2].renderer.material.mainTexture = upgradeCrateTextures[RightCrateid()];


        int tempCrateIndex = upgradeCurrentCrate + 1;

        if (tempCrateIndex > upgradeCrateTextures.Length - 1)
        {
            tempCrateIndex -= upgradeCrateTextures.Length;
        }
		
		if(upgradeCurrentCrate <= 13)
        	updgradeLevelObject.renderer.material.mainTexture = upgradeLevelTextures[upgradeLevel[upgradeCurrentCrate]];
		else
		{
			if(upgradeCurrentCrate == 14)
			{
				updgradeLevelObject.renderer.material.mainTexture = upgradeLevelTextures[PlayerPrefs.GetInt("playerDurLevel")];
			}
			else if(upgradeCurrentCrate == 15)
			{
				updgradeLevelObject.renderer.material.mainTexture = upgradeLevelTextures[PlayerPrefs.GetInt("playerMaxHealthLevel")];
			}
			else if(upgradeCurrentCrate == 16)
			{
				updgradeLevelObject.renderer.material.mainTexture = upgradeLevelTextures[PlayerPrefs.GetInt("playerHealthRegainLevel")];
			}
			else if(upgradeCurrentCrate == 17)
			{
				updgradeLevelObject.renderer.material.mainTexture = upgradeLevelTextures[PlayerPrefs.GetInt("playerMaxEnergyLevel")];
			}
			else if(upgradeCurrentCrate == 18)
			{
				updgradeLevelObject.renderer.material.mainTexture = upgradeLevelTextures[PlayerPrefs.GetInt("playerEnergyRegainLevel")];
			}
		}
		
		upgradeCrateDescriptionText.text = upgradeCrateDescription[upgradeCurrentCrate];
		upgradeCrateDescriptionTextB.text = upgradeCrateDescription[upgradeCurrentCrate];
		
		upgradeCrateDescriptionText2.text = upgradeCrateDescription2[upgradeCurrentCrate];
		upgradeCrateDescriptionTextB2.text = upgradeCrateDescription2[upgradeCurrentCrate];
		
		playerMoneyLabel.text = "Money: " + playerMoney.ToString();
		playerMoneyLabelB.text = "Money: " + playerMoney.ToString();
		
		storeName.text = storeNames[upgradeCurrentCrate];
		storeNameB.text = storeNames[upgradeCurrentCrate];
		
		if(upgradeCost[upgradeCurrentCrate] == 99999)
		{
			upgradePrice.text = "Cost: N/A";
			upgradePriceB.text = "Cost: N/A";
		}
		else if(upgradeCost[upgradeCurrentCrate] == 1)
		{
			upgradePrice.text = "Cost: 1 EXP";
			upgradePriceB.text = "Cost: 1 EXP";
		}
		else
		{
			upgradePrice.text = "Cost: $" + upgradeCost[upgradeCurrentCrate].ToString();
			upgradePriceB.text = "Cost: $" + upgradeCost[upgradeCurrentCrate].ToString();
		}
		
		ReturnCrateDescription();
	}

    int LeftCrateId()
    {
        if (upgradeCurrentCrate == 0)
        {
            return upgradeCrateTextures.Length - 1;
        }
        else
        {
            return upgradeCurrentCrate - 1;
        }
    }

    int RightCrateid()
    {
        if (upgradeCurrentCrate == upgradeCrateTextures.Length - 1)
        {
            return 0;
        }
        else
        {
            return upgradeCurrentCrate + 1;
        }
    }

    /// <summary>
    /// moves the crate textures in the store left
    /// </summary>
    public void MoveLeft()
    {
        upgradeCurrentCrate -= 1;

        if (upgradeCurrentCrate < 0)
        {
            upgradeCurrentCrate = upgradeCrateTextures.Length - 1;
        }

        ApplyCrateTextures();
    }

    /// <summary>
    /// moves the crate textures in the store right
    /// </summary>
    public void MoveRight()
    {
        upgradeCurrentCrate += 1;

        if (upgradeCurrentCrate >= upgradeCrateTextures.Length)
        {
            upgradeCurrentCrate = 0;
        }

        ApplyCrateTextures();
    }
	
	public void ChangeCrate(int index)
    {
        upgradeCurrentCrate = index;

        if (upgradeCurrentCrate >= upgradeCrateTextures.Length)
        {
            upgradeCurrentCrate = 0;
        }

        ReturnCrateDescriptionNew(index);
        ApplyCrateTextures();
    }
	
    /// <summary>
    /// check the players current health for game over
    /// </summary>
    public void CheckPlayerHealth()
    {
        // check the player health to make sure its over zero
        if (playerCurrentHealth <= 0)
        {
            gameState = Variables.GameState.GameOver;
        }
    }

    /// <summary>
    /// change the players values with that of the new weapon
    /// </summary>
    /// <param name="newWeapon"></param>
    public void ChangeWeapon(Weapon newWeapon)
    {
		if(!isPowerMode)
		{
			//previousWeapon = weapon;
			if(Variables.instance.gameState != GameState.Tutorial)
			{
				laserSS.renderer.enabled = false;
				rapidSS.renderer.enabled = false;
			}
	        // adjust the weapon and set the values for the weapon
	        weapon = newWeapon;
			
			Player.instance.PickNewWeapon(newWeapon);
			Player.instance.TurnAtmosphereOn();
			
	        // when a crate is shot a new weapon is given and the values for the players fire rate, energy rate, damage, and bullet speed change and a timer is given for that weapon
	        switch (newWeapon)
	        {
	            case Weapon.Single:
					Player.instance.ChangeFireButton(0);
	                playerCurrentFireRate = singleFireRate - (upgradeLevel[0] * 0.075f); 
	                playerCurrentEnergyRate = 40 - (40 * (upgradeLevel[0] * 0.1f));
	                playerPowerTimer = 0;
	                playerBulletDamage = 3 + (upgradeLevel[0] * 1);
	                playerBulletSpeed = singleBulletSpeed + (upgradeLevel[0] * 100);
	                break;
	            case Weapon.Seeker:
					Player.instance.ChangeFireButton(1);
	                playerCurrentFireRate = seekerFireRate - (upgradeLevel[3] * 0.09f);
	                playerCurrentEnergyRate = 100 - (100 * (upgradeLevel[3] * 0.1f));
	                playerPowerTimer = 0;//seekerPowerTimer + (upgradeLevel[3] * 15);
	                playerBulletDamage = 9 + (upgradeLevel[3] * 3);
	                playerBulletSpeed = seekerBulletSpeed;
	                seekerFuelTime = 2.5f + (upgradeLevel[3] * 1.0f);
	                seekerSpeed = 4.0f + (upgradeLevel[3] * 2.0f);
					
	                break;
	            case Weapon.Rapid:
				rapidSS.renderer.enabled = true;
					Player.instance.ChangeFireButton(2);
	                playerCurrentFireRate = rapidFireRate;// - (upgradeLevel[4] * 0.025f);
	                playerCurrentEnergyRate = 25 - (25 * (upgradeLevel[4] * 0.1f));
	                playerPowerTimer = 0;//rapidPowerTimer + (upgradeLevel[4] * 15);
	                playerBulletDamage = 3 + (upgradeLevel[4] * 1);
	                playerBulletSpeed = rapidBulletSpeed + (upgradeLevel[4] * 150);
	                break;
	            case Weapon.Multi:
					Player.instance.ChangeFireButton(3);
	                playerCurrentFireRate = multiFireRate - (upgradeLevel[1] * 0.01f);
	                playerCurrentEnergyRate = 100 - (100 * (upgradeLevel[1] * 0.1f));
	                playerPowerTimer = 0;//multiPowerTimer + (upgradeLevel[1] * 15);
	                playerBulletDamage = 8 + (upgradeLevel[1] * 3);
	                playerBulletSpeed = multiBulletSpeed + (upgradeLevel[1] * 110);
				
	                break;
	            case Weapon.Laser:
					laserSS.renderer.enabled = true;
					Player.instance.ChangeFireButton(4);
	                playerCurrentFireRate = laserFireRate - (laserFireRate * (upgradeLevel[2] * 0.1f));
	                playerCurrentEnergyRate = 120 - (120 * (upgradeLevel[2] * 0.1f));
	                playerPowerTimer = 0;//laserPowerTimer + (upgradeLevel[2] * 15);
	                playerBulletDamage = 27 + (upgradeLevel[2] * 5);
	                playerBulletSpeed = laserBulletSpeed; //+ (upgradeLevel[2] * 125);
				
				
				
	                break;
	            case Weapon.Flak:
					Player.instance.ChangeFireButton(5);
	                playerCurrentFireRate = 2 - (upgradeLevel[5] * 0.075f);
	                playerCurrentEnergyRate = 120 - (120 * (upgradeLevel[5] * 0.1f));
	                playerPowerTimer = 0;
	                playerBulletDamage = 20 + (upgradeLevel[5] * 3);
	                playerBulletSpeed = 2;
				
	                break;
				
	        }
		}
		else
		{
			if(DontDestoryValues.instance.planetNumber == 0)
				Player.instance.TurnAtmosphereOff();
			
			weapon = newWeapon;
			Player.instance.PickNewWeapon(newWeapon);
			
			//Player.instance.ChangeFireButton(5);
            playerCurrentFireRate = rapidFireRate * 2.5f;
            playerCurrentEnergyRate = 0;//singleEnergyRate / 4;
            playerPowerTimer = 0;
            playerBulletDamage = 3;
            playerBulletSpeed = 500;
		}
    }
	
	public void ChangeWeaponCombo(Weapon newWeapon)
    {
		previousWeapon = weapon;
        // adjust the weapon and set the values for the weapon
        weapon = newWeapon;
		
		Player.instance.PickNewWeapon(newWeapon);
		
        // when a crate is shot a new weapon is given and the values for the players fire rate, energy rate, damage, and bullet speed change and a timer is given for that weapon
        switch (newWeapon)
        {
            case Weapon.Single:
				Player.instance.ChangeFireButton(0);
                playerCurrentFireRate = singleFireRate - (upgradeLevel[0] * 0.1f); 
                playerCurrentEnergyRate = singleEnergyRate - (singleEnergyRate * (upgradeLevel[0] * 0.1f));
                playerPowerTimer = 0;
                playerBulletDamage = singleBulletDamage + (upgradeLevel[0]);
                playerBulletSpeed = singleBulletSpeed;// + (upgradeLevel[0] * 100);
                break;
            case Weapon.Seeker:
				Player.instance.ChangeFireButton(1);
                playerCurrentFireRate = seekerFireRate - (upgradeLevel[3] * 0.09f);
                playerCurrentEnergyRate = seekerEnergyRate - (seekerEnergyRate * (upgradeLevel[3] * 0.05f));
                playerPowerTimer = 0;//seekerPowerTimer + (upgradeLevel[3] * 15);
                playerBulletDamage = seekerBulletDamage + upgradeLevel[3];
                playerBulletSpeed = seekerBulletSpeed;
                seekerFuelTime = 2.5f + (upgradeLevel[3] * 1.0f);
                seekerSpeed = 4.0f + (upgradeLevel[3] * 2.0f);
                break;
            case Weapon.Rapid:
				Player.instance.ChangeFireButton(2);
                playerCurrentFireRate = rapidFireRate;// - (upgradeLevel[4] * 0.025f);
                playerCurrentEnergyRate = rapidEnergyRate - (rapidEnergyRate * (upgradeLevel[4] * 0.06f));
                playerPowerTimer = 0;//rapidPowerTimer + (upgradeLevel[4] * 15);
                playerBulletDamage = rapidBulletDamage + (upgradeLevel[4]);
                playerBulletSpeed = rapidBulletSpeed;// + (upgradeLevel[4] * 150);
                break;
            case Weapon.Multi:
				Player.instance.ChangeFireButton(3);
                playerCurrentFireRate = multiFireRate - (upgradeLevel[1] * 0.01f);
                playerCurrentEnergyRate = multiEnergyRate - (multiEnergyRate * (upgradeLevel[1] * 0.04f));
                playerPowerTimer = 0;//multiPowerTimer + (upgradeLevel[1] * 15);
                playerBulletDamage = multiBulletDamage + (upgradeLevel[1]);
                playerBulletSpeed = multiBulletSpeed + (upgradeLevel[1] * 110);
                break;
            case Weapon.Laser:
				Player.instance.ChangeFireButton(4);
                playerCurrentFireRate = laserFireRate - (laserFireRate * (upgradeLevel[2] * 0.1f));
                playerCurrentEnergyRate = laserEnergyRate - (laserEnergyRate * (upgradeLevel[2] * 0.1f));
                playerPowerTimer = 0;//laserPowerTimer + (upgradeLevel[2] * 15);
                playerBulletDamage = laserBulletDamage + (upgradeLevel[2] * 2);
                playerBulletSpeed = laserBulletSpeed; //+ (upgradeLevel[2] * 125);
                break;
            case Weapon.Flak:
				Player.instance.ChangeFireButton(5);
                playerCurrentFireRate = flakFireRate - (upgradeLevel[5] * 0.2f);
                playerCurrentEnergyRate = flakEnergyRate - (flakEnergyRate * (upgradeLevel[5] * 0.05f));
                playerPowerTimer = 0;//flakPowerTimer + (upgradeLevel[5] * 15);
                playerBulletDamage = flakBulletDamage + (upgradeLevel[5] * 2);
                playerBulletSpeed = 90;
                break;
        }
		
		/*if(previousWeapon != Weapon.Single || newWeapon != Weapon.Single)
		{
			ChangeWeaponValues(previousWeapon);
		}*/
    }
	
	public void ChangeWeaponValues(Weapon previousWeaponId)
	{
		switch(previousWeapon)
		{
			case Weapon.Rapid:
	            playerCurrentFireRate = rapidFireRate;
	            playerCurrentEnergyRate = rapidEnergyRate - (rapidEnergyRate * (upgradeLevel[4] * 0.06f));
				break;
			case Weapon.Flak:
				playerBulletDamage = flakBulletDamage + (upgradeLevel[5] * 3);
	            break;
			case Weapon.Laser:
				playerBulletDamage = laserBulletDamage + (upgradeLevel[2] * 3);
	            break;
		}
	}
    /// <summary>
    /// change the current game state
    /// </summary>
    /// <param name="newGameState"></param>
    public void ChangeGameState(GameState newGameState)
    {
        // change the game states
        gameState = newGameState;
    }

    /// <summary>
    /// when the round is over, run this to update information and reset current round stats
    /// </summary>
    public void ChangeInvasion()
    {
        // increases the invasion number, how many total enemies from the spawn list we can spawn, increases the amount of enemies that will spawn next round, resets number of alive enemies and spawned enemies
        // decreased the spawn time for normal enemies until it reachs a set amount then it remains the same
        SaveInformation();
        currentInvasion++;
        enemiesToSpawnFromList++;
        
		numberOfEnemiesPerInvasion *= enemyIncreasedAmount;
        //numberOfEnemiesPerInvasion = 1;
		
		numberOfEnemiesAlive = 0;
        numberOfEnemiesSpawned = 0;

        if (enemySpawnTimer < minEnemySpawnTimeAmount)
        {
            enemySpawnTimer = minEnemySpawnTimeAmount;
        }
        else
        {
            enemySpawnTimer *= enemySpawnTimerAdjuster;
        }
    }

    /// <summary>
    /// increase the players current enegy when they pick up a crate or get a new weapon
    /// </summary>
    /// <param name="amount"></param>
    public void ChangePlayerEnergyAmount(float amount)
    {
        playerCurrentEnegy += amount;

        if (playerCurrentEnegy > playerMaxEnergy)
            playerCurrentEnegy = playerMaxEnergy;
        else if (playerCurrentEnegy <= 0)
            playerCurrentEnegy = 0;
    }

    /// <summary>
    /// gives the player extra shield, then if the shield amount is less than zero then incease the shield to be visable or else reduce the shield to zero
    /// </summary>
    /// <param name="amount"></param>
 
    public void ChangePlayerShields(float amount)
    {
        itemShieldCurrentAmount += amount + (upgradeLevel[9] * 1);
		Player.instance.ChangeRingTopTexture();
		
        if (itemShieldCurrentAmount > 0)
        {
			if(!Variables.instance.isPowerMode)
			{
	            shield.transform.localScale = new Vector3(9.5f, 9.5f, 9.5f);
                shield.GetComponentInChildren<MeshRenderer>().enabled = true;
			}
        }

        if (itemShieldCurrentAmount > itemShieldMaxAmount)
        {
            itemShieldCurrentAmount = itemShieldMaxAmount;
        }
        else if (itemShieldCurrentAmount <= 0)
        {
			//shield.renderer.enabled = false;
            shield.GetComponentInChildren<MeshRenderer>().enabled = false;
            shield.transform.localScale = new Vector3(0.0f, 0.0f, 0.0f);
            itemShieldCurrentAmount = 0;
        }
    }
	
	public void TurnShieldsOn()
	{
		shield.transform.localScale = new Vector3(9.0f, 9.0f, 9.0f);
		//shield.renderer.enabled = true;
	}
	
	public void RemovePlayerShield(float amount)
    {
        itemShieldCurrentAmount -= amount;

        if (itemShieldCurrentAmount <= 0)
        {
			Player.instance.ChangeRingTopTexture();
			//shield.renderer.enabled = false;
            shield.GetComponentInChildren<MeshRenderer>().enabled = true;
            shield.transform.localScale = new Vector3(0.0f, 0.0f, 0.0f);
            itemShieldCurrentAmount = 0;
        }
    }
	
    /// <summary>
    /// slow all the enemies down
    /// </summary>
    public void ItemSlowOn()
    {
        itemSlowTime = 0;
        itemSlowEnemies = true;
    }

    /// <summary>
    /// drops a certain number of mines in random locations around the planet
    /// </summary>
    public void ItemDropMines()
    {
        itemNumberOfMinesDropped = (upgradeLevel[6] * 4) - numberOfMinesAlive;
		if(itemNumberOfMinesDropped >= 10){
			PlayerPrefs.SetInt("Mines", 1);
		}
        //Debug.Log(itemNumberOfMinesDropped);
		for(int i = 0; i < itemNumberOfMinesDropped; i++)
		{
			int randomPositionX = Random.Range(0, 2);
			int randomPositionY = Random.Range(0, 2);
			float randomSpawnX;
			float randomSpawnY;
			
			if(randomPositionX == 0)
				randomSpawnX = Random.Range(0f, 4.0f);
			else
				randomSpawnX = Random.Range(0f, -4.0f);
			
			if(randomPositionY == 0)
				randomSpawnY = Random.Range(0f, 5.0f);
			else
				randomSpawnY = Random.Range(-10.0f, -15.0f);

            numberOfMinesAlive++;
            explosionManager.ActivateMine(new Vector3(randomSpawnX, randomSpawnY, 0));
			//Instantiate(mines, new Vector3(randomSpawnX, randomSpawnY, 0), Quaternion.identity);
		}
    }
	
    /// <summary>
    /// reset the satalite timer, and make the satalite go into attack mode
    /// </summary>
    public void ItemDropSatalite()
    {
		satalite.renderer.enabled = true;
        itemSataliteTime = 0;
        itemSataliteOn = true;
    }

    /// <summary>
    ///  return the accuracy for the round
    /// </summary>
    /// <returns></returns>
    public float ReturnRoundAccuracy()
    {
        float temp = ((float)numberOfHitsRound / (float)numberOfShotsRound) * 100.0f;
        return temp;
    }

    /// <summary>
    /// return the accuracy for the game
    /// </summary>
    /// <returns></returns>
    public float ReturnTotalAccuracy()
    {
        float temp = (float)numberOfHitsTotal / (float)numberOfShotsTotal;
        temp *= 100.0f;

        return temp;
    }
	
    /// <summary>
    /// add a crate type to the list of crates that will be spawned for the player to shoot
    /// </summary>
    /// <param name="crate"></param>
	public void AddCrateToList(CrateType crate)
	{
        itemCrateTypeSpawnList.Add(crate);
	}
	
    /// <summary>
    /// return a random crate from the list crates the player has unlocked
    /// </summary>
    /// <returns></returns>
	public CrateType ReturnRandomCrate()
	{
        int randomCrate = Random.Range(0, itemCrateTypeSpawnList.Count);

        return itemCrateTypeSpawnList[randomCrate];
	}
	
    /// <summary>
    /// increase the players money
    /// </summary>
    /// <param name="amount"></param>
	public void IncreaseMoney(int amount)
	{
		playerMoney += amount;
		playerMoneyRound += amount;
		playerMoneyTotal += amount;
		
		//Debug.Log(amount);
		
		if(playerMoney < 0)
			playerMoney = 0;
		
		if(playerMoneyTotal < 0)
			playerMoneyTotal = 0;
		
		if(playerMoneyRound < 0)
			playerMoneyRound = 0;
	}

    /// <summary>
    /// returns when all the upgrades have been unlocked
    /// </summary>
    /// <returns></returns>
    public bool ReturnUpgradeNumbers()
    {
        for (int i = 0; i < 13; i++)
        {
            if (upgradeLevel[i] == 0)
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    /// returns true when all the upgrades have been fully upgraded
    /// </summary>
    /// <returns></returns>
    public bool ReturnUpgradeNumbersAll()
    {
        for (int i = 0; i < 13; i++)
        {
            if (upgradeLevel[i] != 4)
            {
                return false;
            }
        }

        return true;
    }

    private List<float> SeperateFloats(string entry)
    {
		string newEntry = PlayerPrefs.GetString(entry);
        string[] seperatedEntries = newEntry.Split('|');
        List<float> arrToList = new List<float>();

        foreach (string s in seperatedEntries)
        {
            float tempNumber;
            if (float.TryParse(s, out tempNumber))
            {
                if (tempNumber != 0)
                {
                    arrToList.Add((float)tempNumber);
                }
            }
        }
        return arrToList;
    }
    private List<int> SeperateIntsNums(string entry)
    {
		string newEntry = PlayerPrefs.GetString(entry);
        string[] seperatedEntries = newEntry.Split('|');
        List<int> arrToList = new List<int>();

        foreach (string s in seperatedEntries)
        {
            int tempNumber;
            if (int.TryParse(s, out tempNumber))
            {
                if (tempNumber != 0)
                {
                    arrToList.Add((int)tempNumber);
                }
            }
        }
        return arrToList;
    }
    private List<int> SeperateInts(string entry)
    {
        //Application.ExternalCall("AlertValues", entry);
		string newEntry = PlayerPrefs.GetString(entry);
        string[] seperatedEntries = newEntry.Split('|');
        List<int> arrToList = new List<int>();

        foreach (string s in seperatedEntries)
        {
            if (s == "0")
                arrToList.Add(0);
            else if (s == "1")
                arrToList.Add(1);
            else if (s == "2")
                arrToList.Add(2);
            else if (s == "3")
                arrToList.Add(3);
            else if (s == "4")
                arrToList.Add(4);
            //arrToList.Add(int.Parse(s));
        }

        return arrToList;
    }
    private List<bool> SeperateBools(string entry)
    {
        //Application.ExternalCall("AlertValues", entry);
		string newEntry = PlayerPrefs.GetString(entry);
        string[] seperatedEntries = newEntry.Split('|');
        List<bool> arrToList = new List<bool>();

        foreach (string s in seperatedEntries)
        {
            if (s == "0")
                arrToList.Add(false);
            else if (s == "1")
                arrToList.Add(true);
        }

        return arrToList;
    }

    private string ConvertBoolsToString(List<bool> list)
    {
        string boolsToString = "";

        for (int i = 0; i < list.Count; i++)
        {
            if (list[i])
            {
                boolsToString += "|1";
            }

            else if (!list[i])
            {
                boolsToString += "|0";
            }
        }

        return boolsToString;
    }
    private string ConvertIntsToString(List<int> list)
    {
        string boolsToString = "";

        for (int i = 0; i < list.Count; i++)
        {
            boolsToString += "|" + list[i].ToString();
        }

        return boolsToString;
    }
    private string ConvertFloatToString(List<float> list)
    {
        string boolsToString = "";

        for (int i = 0; i < list.Count; i++)
        {
            boolsToString += "|" + list[i].ToString();
        }

        Debug.Log(boolsToString);
        return boolsToString;
    }
    #endregion
}
