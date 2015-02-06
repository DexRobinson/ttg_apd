using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour 
{
	public static GameManager instance;
	
    public static List<GameObject> enemyDestoryList = new List<GameObject>();

	public ParticleSystem winningFireWorks;

    public TextMesh countDownTextMesh;
    public GameObject countDownObject;

    public GameObject endOfRoundObject;
    public TextMesh[] endOfRoundStats;
    public GameObject[] itemSpawns;
    public int numberOfItems = 0;
	
	public GameObject targetRecticle;
	public GameObject bossBar;
	
    private Variables v;
    private float countDownTimer = 3.0f;
	private float itemCrateTime = 0.0f;
    private bool saved;
    public static bool achievementUnlocked = false;
    private float achTimer = 0.0f;
	public static int numberOfSpawnersLeft = 4;
	public static bool boss1Spawned;
	public static bool boss1Alive;
	
    GameObject[] items = null;
    static GameObject cratePickUpSound = null;
    static GameObject cratePickUpMort = null;
	
    static GameObject[] mortBullets = null;
    static int numberOfMortBullets = 3;

    static GameObject[] smallBlobs = null;
    static int numberOfSmallBlobs = 4;

    static GameObject[] gorgMinions = null;
    static int numberOfGorgMinions = 4;

    static GameObject[] motherShipSpawns = null;
    static int numberOfMotherShipSpawns = 5;

    static GameObject[] playerSeekerBullet = null;
    static int numberOfPlayerSeekerBullet = 5;
	static GameObject[] playerSeekerBullet2x = null;
    static int numberOfPlayerSeekerBullet2x = 5;
	
    static GameObject[] plasmaCrystals = null;
    static int numberOfPlasmaCrystals = 5;

    static GameObject[] evacShip = null;
    static int numberOfEvacShips = 3;
	
	static GameObject[] centipedeEnemies = null;
	static int numberOfCentipedeEnemies = 5;
   
	static GameObject[] mortMissileEnemies = null;
	static int numberOfMortMissileEnemies = 6;
	
	static GameObject[] kamakizeMorts = null;
	static int numberOfKamzkizeMorts = 1;
	
	static GameObject[] sataliteBullet = null;
	static int numberOfSatBullets = 5;
	
	static GameObject[] comboText = null;
	static int numberOfcomboText = 3;
	static GameObject[] comboText2 = null;
	static int numberOfcomboText2 = 3;
	
	static GameObject[] miniBossSpawns = null;
	
	public static bool boss2Dead;
	
	public static bool bossDead;
	
	public static int miniBossDead = 0;
	
	public static GameObject[] audioFleetShot;
	public static GameObject audioHarvestor;
	public static GameObject audioMotherShipSpawn;
	public static bool mortAlive = false;
	private bool isComplete;
	//public static bool miniBoss2Dead;
	//public static bool miniBoss3Dead;

	public AudioSource winningAudio;
	public AudioClip bossAudio;
	public AudioClip orginalAudioClip;
	private bool beatBoss;
    private ExplosionManager explosionManager;

    private IEnumerator Init()
    {
		orginalAudioClip = DontDestoryValues.instance.audio.clip;
        explosionManager = GameObject.FindGameObjectWithTag("Variables").GetComponent<ExplosionManager>();


		mortAlive = false;
		miniBossDead = 0;
		bossDead = false;
        for (int i = 0; i < numberOfItems; i++)
        {
            items[i] = Instantiate(Resources.Load("Crate")) as GameObject;
            items[i].SetActive(false);
        }
		SpawnComboText();
		SpawnComboText2();

		yield return new WaitForSeconds(4f);
		//SpawnMortMissileEnemies();
		SpawnSmallBlobs();
		SpawnCentipedeEnemies();
		//SpawnGorgMinions();
        cratePickUpSound = Instantiate(Resources.Load("CrateSound")) as GameObject;
        cratePickUpSound.SetActive(false);
		yield return new WaitForSeconds(0.1f);
		cratePickUpMort = Instantiate(Resources.Load("CrateSoundMort")) as GameObject;
        cratePickUpMort.SetActive(false);
		yield return new WaitForSeconds(0.1f);
		audioHarvestor = Instantiate(Resources.Load("AudioHavester")) as GameObject;
        audioHarvestor.SetActive(false);
		audioMotherShipSpawn = Instantiate(Resources.Load("AudioMotherShipSpawn")) as GameObject;
        audioMotherShipSpawn.SetActive(false);
			
    }
	
	#region Spawns
	public static void SpawnFleetAudioShot()
	{
		for(int i = 0; i < 3; i++)
		{
			audioFleetShot[i] = Instantiate(Resources.Load("AudioFleetShot")) as GameObject;
            audioFleetShot[i].SetActive(false);
		}
	}
	
	public static void SpawnComboText2()
    {
        for (int i = 0; i < numberOfcomboText2; i++)
        {
            comboText2[i] = Instantiate(Resources.Load("ComboDropText")) as GameObject;
            comboText2[i].SetActive(false);
        }
	}
	
	public static void SpawnComboText()
    {
        for (int i = 0; i < numberOfcomboText; i++)
        {
            comboText[i] = Instantiate(Resources.Load("ComboText")) as GameObject;
            comboText[i].SetActive(false);
        }
	}
	
	public static void SpawnKamzkizeMorts()
    {
        for (int i = 0; i < numberOfKamzkizeMorts; i++)
        {
            kamakizeMorts[i] = Instantiate(Resources.Load("KamkizeMorts")) as GameObject;
            kamakizeMorts[i].SetActive(false);
        }
	}
    
	public static void SpawnMortMissileEnemies()
    {
        for (int i = 0; i < numberOfMortMissileEnemies; i++)
        {
            mortMissileEnemies[i] = Instantiate(Resources.Load("MortMissile")) as GameObject;
            mortMissileEnemies[i].SetActive(false);
        }
    }
	
	public static void SpawnCentipedeEnemies()
    {
        for (int i = 0; i < numberOfCentipedeEnemies; i++)
        {
            centipedeEnemies[i] = Instantiate(Resources.Load("EnemyGorgMinion")) as GameObject;
            centipedeEnemies[i].SetActive(false);
        }
    }
	
    public static void SpawnEvacShips()
    {
        for (int i = 0; i < numberOfEvacShips; i++)
        {
            evacShip[i] = Instantiate(Resources.Load("EscapePod")) as GameObject;
            evacShip[i].SetActive(false);
        }
    }

    public static void SpawnPlasmaCrystals()
    {
        for (int i = 0; i < numberOfPlasmaCrystals; i++)
        {
            plasmaCrystals[i] = Instantiate(Resources.Load("PlasmaCrystal")) as GameObject;
            plasmaCrystals[i].SetActive(false);
        }
    }

    public static void SpawnPlayerSeekerBullets()
    {
        for (int i = 0; i < numberOfPlayerSeekerBullet; i++)
        {
            playerSeekerBullet[i] = Instantiate(Resources.Load("PlayerSeeker")) as GameObject;
            playerSeekerBullet[i].SetActive(false);
        }
    }
	public static void SpawnPlayerSeekerBullets2x()
    {
        for (int i = 0; i < numberOfPlayerSeekerBullet2x; i++)
        {
            playerSeekerBullet2x[i] = Instantiate(Resources.Load("PlayerSeeker2x")) as GameObject;
            playerSeekerBullet2x[i].SetActive(false);
        }
    }

	public static void SpawnSmallBlobs()
    {
        for (int i = 0; i < numberOfSmallBlobs; i++)
        {
            smallBlobs[i] = Instantiate(Resources.Load("EnemyR7BlobSmall")) as GameObject;
            smallBlobs[i].SetActive(false);
        }
    }

    public static void SpawnMortBullets()
    {
        for (int i = 0; i < numberOfMortBullets; i++)
        {
            mortBullets[i] = Instantiate(Resources.Load("MortBullet")) as GameObject;
            mortBullets[i].SetActive(false);
        }
    }

    public static void SpawnGorgMinions()
    {
        for (int i = 0; i < numberOfGorgMinions; i++)
        {
            gorgMinions[i] = Instantiate(Resources.Load("EnemyGorgMinion")) as GameObject;
            gorgMinions[i].SetActive(false);
        }
    }

    public static void SpawnMotherShipSpawns()
    {
        for (int i = 0; i < numberOfMotherShipSpawns; i++)
        {
            motherShipSpawns[i] = Instantiate(Resources.Load("MotherShipSpawn")) as GameObject;
            motherShipSpawns[i].SetActive(false);
        }
    }
	
	public static void SpawnSataliteBullet()
    {
        for (int i = 0; i < numberOfSatBullets; i++)
        {
            sataliteBullet[i] = Instantiate(Resources.Load("BulletSatalite")) as GameObject;
            sataliteBullet[i].SetActive(false);
        }
    }
	
	public static void SpawnMiniBossBullets()
    {
        for (int i = 0; i < 5; i++)
        {
            miniBossSpawns[i] = Instantiate(Resources.Load("MiniBoss1Bullet")) as GameObject;
            miniBossSpawns[i].SetActive(false);
        }
    }
	#endregion
	#region Activates
    public void ActivateItem()
    {
        for (int i = 0; i < numberOfItems; i++)
        {
            if (items[i].activeSelf == false)
            {
                int randomSpawn = Random.Range(0, itemSpawns.Length - 1);
                items[i].SetActive(true);
                items[i].GetComponent<Crate>().Activate();
				
                items[i].transform.position = new Vector3(itemSpawns[randomSpawn].transform.position.x,itemSpawns[randomSpawn].transform.position.y,itemSpawns[randomSpawn].transform.position.z - 0.5f);
				explosionManager.ActivateShockwaveExplosion(new Vector3(items[i].transform.position.x, items[i].transform.position.y, items[i].transform.position.z - 1.5f));
                return;
            }
        }
    }
	public void ActivateItemStart(Vector3 pos, Variables.CrateType ct)
    {
        for (int i = 0; i < numberOfItems; i++)
        {
            if (items[i].activeSelf == false)
            {
                //int randomSpawn = Random.Range(0, itemSpawns.Length - 1);
                items[i].SetActive(true);
                items[i].GetComponent<Crate>().Activate();
				items[i].GetComponent<Crate>().SetCrateType(ct);
                items[i].transform.position = pos;
                return;
            }
        }
    }
	
	public static void ActivateSataliteBullet(Vector3 position)
    {
        for (int i = 0; i < numberOfSatBullets; i++)
        {
            if (evacShip[i].activeSelf == false)
            {
				sataliteBullet[i].SetActive(true);
                sataliteBullet[i].GetComponent<Bullet>().ActivateSataliteSeeker();
                sataliteBullet[i].transform.position = position;
                return;
            }
        }
    }
	
	public static void ActivateComboTDropText(string text)
    {
        for (int i = 0; i < numberOfcomboText2; i++)
        {
            if (comboText2[i].activeSelf == false)
            {
				comboText2[i].transform.position = new Vector3(0.5f, Random.Range(0.75f, 0.9f), 5);
				comboText2[i].GetComponent<PlayKillText>().text = text;
                comboText2[i].SetActive(true);
                return;
            }
        }
    }
	
	public static void ActivateComboText(string text)
    {
        for (int i = 0; i < numberOfcomboText; i++)
        {
            if (comboText[i].activeSelf == false)
            {
				comboText[i].transform.position = new Vector3(0.5f, Random.Range(0.35f, 0.7f), 5);
				comboText[i].GetComponent<PlayKillText>().text = text;
                comboText[i].SetActive(true);
                return;
            }
        }
    }
	
	public static void ActivateMiniBossSpawn(Vector3 position)
    {
        for (int i = 0; i < 5; i++)
        {
            if (miniBossSpawns[i].activeSelf == false)
            {
                miniBossSpawns[i].SetActive(true);
                miniBossSpawns[i].transform.position = position;
                return;
            }
        }
    }
	
	public static void ActivateKamzkieMort(Vector3 position)
    {
        for (int i = 0; i < numberOfKamzkizeMorts; i++)
        {
            if (kamakizeMorts[i].activeSelf == false)
            {
                kamakizeMorts[i].SetActive(true);
                kamakizeMorts[i].transform.position = position;
                return;
            }
        }
    }
	
	public static void ActivateMortMissiles(Vector3 position)
    {
        for (int i = 0; i < numberOfMortMissileEnemies; i++)
        {
            if (mortMissileEnemies[i].activeSelf == false)
            {
                mortMissileEnemies[i].SetActive(true);
                mortMissileEnemies[i].transform.position = position;
                return;
            }
        }
    }
	
    public static void ActivateEvacShip(Vector3 position)
    {
        for (int i = 0; i < numberOfEvacShips; i++)
        {
            if (evacShip[i].activeSelf == false)
            {
                //evacShip[i].SetActive(true);
                evacShip[i].GetComponent<Bullet>().Activate();
                evacShip[i].transform.position = position;
                return;
            }
        }
    }

    public static void ActivatePlasmaCrystal(Vector3 position)
    {
        for (int i = 0; i < numberOfPlasmaCrystals; i++)
        {
            if (plasmaCrystals[i].activeSelf == false)
            {
                plasmaCrystals[i].SetActive(true);
                plasmaCrystals[i].transform.position = position;
                return;
            }
        }
    }

    public static void ActivatePlayerSeekerBullet(Vector3 position)
    {
        for (int i = 0; i < numberOfPlayerSeekerBullet; i++)
        {
            if (playerSeekerBullet[i].activeSelf == false)
            {
                playerSeekerBullet[i].SetActive(true);
                playerSeekerBullet[i].transform.position = position;
                return;
            }
        }
    }
	public static void ActivatePlayerSeekerBullet2x(Vector3 position)
    {
        for (int i = 0; i < numberOfPlayerSeekerBullet2x; i++)
        {
            if (playerSeekerBullet2x[i].activeSelf == false)
            {
                playerSeekerBullet2x[i].SetActive(true);
                playerSeekerBullet2x[i].transform.position = position;
                return;
            }
        }
    }
	
    public static void ActivateMortBullet(Vector3 position)
    {
        for(int i = 0; i < numberOfMortBullets; i++)
        {
            if (mortBullets[i].activeSelf == false)
            {
                mortBullets[i].SetActive(true);
                mortBullets[i].transform.position = position;
                return;
            }
        }
    }

    public static void ActivateCrateSound()
    {
        cratePickUpSound.SetActive(true);
    }
	public static void ActivateCrateSoundMort()
    {
        cratePickUpMort.SetActive(true);
    }
	public static void ActivateMotherShipSpawn()
    {
        audioMotherShipSpawn.SetActive(true);
    }
	public static void ActivateHarvesterAudio()
    {
        audioHarvestor.SetActive(true);
    }
	public static void ActivateAudioFleetShot()
    {
        for (int i = 0; i < 5; i++)
        {
            if (audioFleetShot[i].activeSelf == false)
            {
                audioFleetShot[i].SetActive(true);
                return;
            }
        }
    }
	
    public static void ActivateBlob(Vector3 position)
    {
        for (int i = 0; i < numberOfSmallBlobs; i++)
        {
            if (smallBlobs[i].activeSelf == false)
            {
                smallBlobs[i].SetActive(true);
                smallBlobs[i].transform.position = position;
                return;
            }
        }
    }
	
	public static void ActivateCentipedeEnemy(Vector3 position)
    {
        for (int i = 0; i < numberOfCentipedeEnemies; i++)
        {
            if (centipedeEnemies[i].activeSelf == false)
            {
                centipedeEnemies[i].SetActive(true);
                centipedeEnemies[i].transform.position = position;
                return;
            }
        }
    }
	
    public static void ActivateGorgMinion(Vector3 position)
    {
        for (int i = 0; i < numberOfGorgMinions; i++)
        {
            if (gorgMinions[i].activeSelf == false)
            {
                gorgMinions[i].SetActive(true);
                gorgMinions[i].transform.position = position;
                return;
            }
        }
    }

    public static void ActivateMotherShipSpawns(Vector3 position)
    {
        for (int i = 0; i < numberOfMotherShipSpawns; i++)
        {
            if (motherShipSpawns[i].activeSelf == false)
            {
				Variables.instance.numberOfEnemiesSpawned++;
                Variables.instance.numberOfEnemiesAlive++;
				
                motherShipSpawns[i].SetActive(true);
                motherShipSpawns[i].transform.position = position;
				motherShipSpawns[i].transform.rotation = Quaternion.identity;
                return;
            }
        }
    }
	public static void ActivateMotherShipSpawns(Vector3 position, Quaternion rot)
    {
        for (int i = 0; i < numberOfMotherShipSpawns; i++)
        {
            if (motherShipSpawns[i].activeSelf == false)
            {
				Variables.instance.numberOfEnemiesSpawned++;
                Variables.instance.numberOfEnemiesAlive++;
				
                motherShipSpawns[i].SetActive(true);
                motherShipSpawns[i].transform.position = position;
				motherShipSpawns[i].transform.rotation = rot;
                return;
            }
        }
    }
	
	#endregion
	
	void Awake()
	{
		instance = this;
	}
	
    void Start()
    {
        v = Variables.instance;
        items = new GameObject[numberOfItems];
        mortBullets = new GameObject[numberOfMortBullets];
        smallBlobs = new GameObject[numberOfSmallBlobs];
        gorgMinions = new GameObject[numberOfGorgMinions];
        motherShipSpawns = new GameObject[numberOfMotherShipSpawns];
        playerSeekerBullet = new GameObject[numberOfPlayerSeekerBullet];
        plasmaCrystals = new GameObject[numberOfPlasmaCrystals];
        evacShip = new GameObject[numberOfEvacShips];
		centipedeEnemies = new GameObject[numberOfCentipedeEnemies];
        sataliteBullet = new GameObject[numberOfSatBullets];
		playerSeekerBullet2x = new GameObject[numberOfPlayerSeekerBullet2x];
		
		itemSpawns = GameObject.FindGameObjectsWithTag("ItemSpawner");
		mortMissileEnemies = new GameObject[numberOfMortMissileEnemies];
		kamakizeMorts = new GameObject[numberOfKamzkizeMorts];
		comboText = new GameObject[numberOfcomboText];
		comboText2 = new GameObject[numberOfcomboText2];
		miniBossSpawns = new GameObject[5];
		
		StartCoroutine(Init());
		
		StartCoroutine(SlowSpawn());
		
        //SpawnEvacShips();
        //SpawnPlasmaCrystals();
        //SpawnMotherShipSpawns();
        //SpawnMortBullets();
        //SpawnSmallBlobs();
        //SpawnPlayerSeekerBullets();
		//SpawnPlayerSeekerBullets2x();
		////SpawnFleetAudioShot();
        //SpawnSataliteBullet();
		
		if(DontDestoryValues.instance.planetNumber == 0)
		{
			SpawnMiniBossBullets();
			SpawnMortBullets();
			SpawnKamzkizeMorts();
			SpawnMortMissileEnemies();
		}
		else if(DontDestoryValues.instance.planetNumber == 1)
		{
			SpawnGorgMinions();
		}
		else if(DontDestoryValues.instance.planetNumber == 2)
		{
			SpawnGorgMinions();
		}
    }
	
	IEnumerator SlowSpawn()
	{
		//SpawnFleetAudioShot();
		SpawnSataliteBullet();

		yield return new WaitForSeconds(10f);
		SpawnEvacShips();
		SpawnPlasmaCrystals();
		SpawnMotherShipSpawns();
		SpawnMortBullets();
		SpawnSmallBlobs();
		SpawnPlayerSeekerBullets();
		SpawnPlayerSeekerBullets2x();

	}
	
	void Update () 
    {
        if(Variables.instance.currentInvasion > 2)
            DontDestoryValues.instance.AchievementCheck();

        if (achievementUnlocked)
        {
            achTimer += Time.deltaTime;

            Variables.instance.DisplayAchievementUnlocked();

            if (achTimer > 3.0f)
            {
                Variables.instance.HideAchievementBoard();
                achievementUnlocked = false;
            }
        }

        // handles all the game state transitions
        switch (v.gameState)
        {
            case Variables.GameState.Countdown:
                CountDown();
                break;
            case Variables.GameState.Game:
			
				if(v.startCombo)
				{
					if(v.comboTimer > 4.0f)
					{
						v.comboTimer = 0.0f;
						v.startCombo = false;
						v.comboBouns = 1;
					}
					else
					{
						v.comboTimer += Time.deltaTime;
					}
				}
			
				if(Variables.instance.hitByEnemy)
				{
					StartCoroutine(Variables.instance.PlayHurtScreen());
				}
			
                if (v.currentInvasion == 10)
                {
                    if (DontDestoryValues.instance.planetNumber == 0)
                    {
                        if (boss1Spawned && !boss1Alive)
                        {
                            if (v.numberOfEnemiesAlive <= 0)
                                StartCoroutine(GameComplete());
                        }
                    }
                    else if (DontDestoryValues.instance.planetNumber == 1)
                    {
						if(numberOfSpawnersLeft <= 0)
						{
							boss2Dead = true;
							numberOfSpawnersLeft = 4;
							StartCoroutine(GameComplete());
						}
                    }
                    else if (DontDestoryValues.instance.planetNumber == 2)
                    {
                        if (v.boss3BothDead <= 1)
                        {
                            if (v.numberOfEnemiesAlive <= 0)
                                StartCoroutine(GameComplete());
                        }
                    }
                }
				else if(v.currentInvasion == 5)
				{
					if (DontDestoryValues.instance.planetNumber == 0)
                    {
                        if(miniBossDead > 0)
						{
							if(AllEnemiesDead())
							{
								StartCoroutine(RoundOver());
							}
						}
                    }
                    else if (DontDestoryValues.instance.planetNumber == 1)
                    {
						if(miniBossDead > 1)
						{
							if(AllEnemiesDead())
							{
								//miniBossDead = 0;
								StartCoroutine(RoundOver());
							}
						}
                    }
                    else if (DontDestoryValues.instance.planetNumber == 2)
                    {
                        if(miniBossDead > 3)
						{
							if(AllEnemiesDead())
							{
								StartCoroutine(RoundOver());
							}
						}
                    }
					
				}
                else
                {
                    if (v.numberOfEnemiesSpawned >= v.numberOfEnemiesPerInvasion)
                    {
                        // normal game
                        if (DontDestoryValues.instance.gameType == 0)
                        {
							//Debug.Log("Mort Alive: " + mortAlive);
                            if (AllEnemiesDead() && !mortAlive)// && Variables.instance.numberOfEnemiesAlive <= 0)
                            {
                                if (v.playerCurrentHealth > 0)
                                    StartCoroutine(RoundOver());
                                else
                                    StartCoroutine(GameOver());
                            }
                        }

                        // evac game
                        else if (DontDestoryValues.instance.gameType == 1)
                        {
                                v.enemySpawnTimer *= 0.95f;
                                v.numberOfEnemiesPerInvasion += 16;
                        }
                    }
                }

                // take this out later, and only check the players health when something hits it
                if (v.playerCurrentHealth <= 0)
                {
                    StartCoroutine(GameOver());
                }
			
				// handles all the item updates
                ItemUpdates();

                break;
            case Variables.GameState.Menu:
                v.playerMoneyLabel.text = v.playerMoney.ToString();
                v.upgradePrice.text = v.upgradeCost[v.upgradeCurrentCrate].ToString();
                break;
        }
	}
	
	public bool AllEnemiesDead()
	{
		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
		
		if(enemies.Length != 0)
			return false;
		
		return true;
	}
	
	public void SaveStats()
	{
		targetRecticle.SetActive(false);
		bossBar.SetActive(false);
		
		// save all the stats
        int tempHits = PlayerPrefs.GetInt("hits");
        int tempShots = PlayerPrefs.GetInt("shots");
        int tempMoney = PlayerPrefs.GetInt("TotalMoney");
        int tempKills = PlayerPrefs.GetInt("kills");
        int tempInvasion = PlayerPrefs.GetInt("invasion");

        // add up all the stats
        tempHits += v.numberOfHitsTotal;
        tempShots += v.numberOfShotsTotal;
        tempMoney += (v.playerMoneyTotal * (int)Modifiers.creditsMod);
        tempKills += v.numberOfEnemiesKilledTotal;
        tempInvasion += v.currentInvasion;

        // save all the total stats
        PlayerPrefs.SetInt("hits", tempHits);
        PlayerPrefs.SetInt("shots", tempShots);
        PlayerPrefs.SetInt("TotalMoney", tempMoney);
        PlayerPrefs.SetInt("kills", tempKills);
        PlayerPrefs.SetInt("invasion", tempInvasion);

        // check and save the best stats
        DontDestoryValues.instance.SaveNewScore(v.currentInvasion, "bestinvasion");
        DontDestoryValues.instance.SaveNewScore(v.numberOfEnemiesKilledTotal, "bestkills");
        DontDestoryValues.instance.SaveNewScore(v.playerMoneyTotal, "bestmoney");
        DontDestoryValues.instance.SaveNewScore(v.numberOfHitsTotal, "besthits");
	}
	
    IEnumerator RoundOver()
    {
		if(!isComplete){
			DontDestoryValues.instance.ToggleAudio();

			if(DontDestoryValues.instance.musicVolume > 0)
			{

				winningAudio.Play();
			}

			winningFireWorks.Play();
			GameManager.ActivateComboText("Round Complete $" + Variables.instance.currentInvasion * (10 * (DontDestoryValues.instance.planetNumber + 1)));
			Variables.instance.IncreaseMoney(Variables.instance.currentInvasion * 10);
			isComplete = true;
			
            //SaveStats();
		}
		
        Resources.UnloadUnusedAssets();
        v.ChangeGameState(Variables.GameState.RoundOver);
        GameBoarder.CleanUp();
        CleanUp();
		
		if(PowerHUD.instance.powerTime > 0)
			PowerHUD.instance.powerTime = 15.0f;

        yield return new WaitForSeconds(5.0f);
		
		isComplete = false;
		miniBossDead = 0;
        countDownTimer = 3.0f;
		
		if(Variables.instance.currentInvasion == 5)
		{
			if(PlayerPrefs.GetInt("FullVersion") != 1800699)
			{
				DisplayFreeGameStats();
			}
			else
			{
				DisplayRoundOverStats();
			}
		}
		else
		{
			DisplayRoundOverStats();
		}
		
    }

    IEnumerator GameOver()
    {
        //EnemySpawner.instance.CleanUp();
		targetRecticle.SetActive(false);
		bossBar.SetActive(false);
        GameBoarder.CleanUp();
        CleanUp();

		Player.instance.BlowUpPlanet();
        
		v.ChangeGameState(Variables.GameState.GameOver);
		
        yield return new WaitForSeconds(3.0f);

        DisplayGameOverStats();
        v.SaveInformation();
        //v.ChangeGameState(Variables.GameState.GameOver);
    }

    IEnumerator GameComplete()
    {
        //EnemySpawner.instance.CleanUp();
		winningFireWorks.Play();

		targetRecticle.SetActive(false);
		bossBar.SetActive(false);
		
        GameBoarder.CleanUp();
        CleanUp();
		
		if(DontDestoryValues.instance.planetNumber == 0)
		{
			DontDestoryValues.instance.isPlanetTwoUnlocked = 1;
		}
		else if(DontDestoryValues.instance.planetNumber == 1)
		{
			DontDestoryValues.instance.isPlanetThreeUnlocked = 1;
		}
        //Player.instance.BlowUpPlanet();
		bossDead = true;
        v.ChangeGameState(Variables.GameState.GameOver);
        yield return new WaitForSeconds(3.0f);
		
		if(DontDestoryValues.instance.planetNumber == 2)
		{
			DisplayTotallyCompletedStats();
		}
		else
        	DisplayCompletedStats();
		
        v.SaveInformation();
    }
	
	void LockNLoad()
	{
		StartCoroutine(LNL());

		if(Variables.instance.currentInvasion == 5 || Variables.instance.currentInvasion == 10)
		{
			beatBoss = true;
			DontDestoryValues.instance.SwitchAudio(bossAudio);
		}
		else if (Variables.instance.currentInvasion == 6)
		{
			if(beatBoss)
			{
				DontDestoryValues.instance.SwitchAudio(orginalAudioClip);
			}
		}
	}
	
	IEnumerator LNL()
	{
		for(int i = 0; i < Variables.instance.itemCrateTypeSpawnList.Count; i++)
		{
			//ExplosionManager.ActivateShockwaveExplosion(itemSpawns[i].transform.position);
			explosionManager.ActivateShockwaveExplosion(new Vector3(itemSpawns[i].transform.position.x, itemSpawns[i].transform.position.y, itemSpawns[i].transform.position.z - 1.5f));
			ActivateItemStart(itemSpawns[i].transform.position, Variables.instance.itemCrateTypeSpawnList[i]);
			
			yield return new WaitForSeconds(0.2f);
		}
	}
	
    void ItemUpdates()
    {
		// run an update to spawn crates around the planet
		// get a random spawn, and a random crate
        itemCrateTime += Time.deltaTime;

        if (itemCrateTime > 15)
        {
            ActivateItem();
            itemCrateTime = 0.0f;
        }
		
        SlowEnemies();
        Satalite();
    }

    void SlowEnemies()
    {
        // this will run a countdown to slow enemies down for a certain amount of time
        if (v.itemSlowEnemies)
        {
            v.itemSlowTime += Time.deltaTime;

            if (v.itemSlowTime > v.itemSlowTimer + (Variables.instance.upgradeLevel[10] * 10))
            {
                v.itemSlowEnemies = false;
            }
        }
    }

    void Satalite ()
	{
		if (v.itemSataliteOn) {
			v.itemSataliteTime += Time.deltaTime;

			if (v.itemSataliteTime > v.itemSataliteTimer + (Variables.instance.upgradeLevel[13] * 10)) {
				Variables.instance.satalite.renderer.enabled = false;
                v.itemSataliteOn = false;
            }
        }
    }

    void CountDown ()
	{
		countDownTimer -= Time.deltaTime;

		if (countDownTimer < 0.5f) {
			countDownTextMesh.text = "Defend!";
			Variables.instance.numberOfEnemiesAlive = 0;
			itemCrateTime = 0f;
		} else
		{
			if(DontDestoryValues.instance.gameType == 0)
			{
				countDownTextMesh.text = "Invasion " + v.currentInvasion + " Begins In " + countDownTimer.ToString ("f0");
			}
			else
			{
				countDownTextMesh.text = "Evacuation begins in " + countDownTimer.ToString("f0");
			}
		}

		if (countDownTimer < -0.5f) {
			mortAlive = false;
			LockNLoad();
			v.playerMoneyRound = 0;
			v.numberOfEnemiesKilledRound = 0;
			v.numberOfHitsRound = 0;
			v.numberOfShotsRound = 0;
			v.roundAccuracy = 0;

            v.cameraInvasionBegin.enabled = false;
            //v.cameraMiniMap.enabled = true;
            v.ChangeGameState(Variables.GameState.Game);
        }
    }

    void DisplayRoundOverStats()
    {
        v.cameraRoundOver.enabled = true;

        endOfRoundStats[0].text = "Invasion " + v.currentInvasion + " completed!";
        endOfRoundStats[1].text = "Kills: " + v.numberOfEnemiesKilledRound;
        endOfRoundStats[2].text = "Money: " + v.playerMoneyRound;
        endOfRoundStats[3].text = "Accuracy: " + v.ReturnRoundAccuracy().ToString("f1") + "%";
    }
	
	void DisplayFreeGameStats()
    {
        v.cameraRoundOver.enabled = true;
		//PlayerPrefs.SetInt(Variables.instance.currentInvasion, "planet" + DontDestoryValues.instance.planetNumber + "highestLevel");
        // display the stats to the player for the round
        if (DontDestoryValues.instance.gameType == 0)
        {
			int oldHighest  = PlayerPrefs.GetInt("planet" + DontDestoryValues.instance.planetNumber + "highestLevel", 1);
			if(Variables.instance.currentInvasion > oldHighest)
			{
				if(Variables.instance.currentInvasion > 5)
					Variables.instance.currentInvasion = 5;
				
				PlayerPrefs.SetInt("planet" + DontDestoryValues.instance.planetNumber + "highestLevel", Variables.instance.currentInvasion);
			}
			
            endOfRoundStats[0].text = "Mini Mort Defeated!";
            endOfRoundStats[1].text = "Kills: " + v.numberOfEnemiesKilledTotal;
            endOfRoundStats[2].text = "Money: " + v.playerMoneyTotal;
            endOfRoundStats[3].text = "Accuracy: " + v.ReturnTotalAccuracy().ToString("f1") + "% \n Make sure to visit the store!";
        }
        else if (DontDestoryValues.instance.gameType == 1)
        {
            endOfRoundStats[0].text = "Escape Pods Launched: " + v.playerEscapePodsRecused;
            endOfRoundStats[1].text = "Kills: " + v.numberOfEnemiesKilledTotal;
            endOfRoundStats[2].text = "Money: " + v.playerMoneyTotal;
            endOfRoundStats[3].text = "Accuracy: " + v.ReturnTotalAccuracy().ToString("f1") + "% \n Make sure to visit the store!";
        }

        if (!saved)
        {
            saved = true;

            // save all the stats
            int tempHits = PlayerPrefs.GetInt("hits");
            int tempShots = PlayerPrefs.GetInt("shots");
            int tempMoney = PlayerPrefs.GetInt("TotalMoney");
            int tempKills = PlayerPrefs.GetInt("kills");
            int tempInvasion = PlayerPrefs.GetInt("invasion");

            // add up all the stats
            tempHits += v.numberOfHitsTotal;
            tempShots += v.numberOfShotsTotal;
            tempMoney += (v.playerMoneyTotal * (int)Modifiers.creditsMod);
            tempKills += v.numberOfEnemiesKilledTotal;
            tempInvasion += v.currentInvasion;

            // save all the total stats
            PlayerPrefs.SetInt("hits", tempHits);
            PlayerPrefs.SetInt("shots", tempShots);
            PlayerPrefs.SetInt("TotalMoney", tempMoney);
            PlayerPrefs.SetInt("kills", tempKills);
            PlayerPrefs.SetInt("invasion", tempInvasion);

            // check and save the best stats
            DontDestoryValues.instance.SaveNewScore(v.currentInvasion, "bestinvasion");
            DontDestoryValues.instance.SaveNewScore(v.numberOfEnemiesKilledTotal, "bestkills");
            DontDestoryValues.instance.SaveNewScore(v.playerMoneyTotal, "bestmoney");
            DontDestoryValues.instance.SaveNewScore(v.numberOfHitsTotal, "besthits");
        }
    }
	
    void DisplayGameOverStats()
    {
        v.cameraRoundOver.enabled = true;
		//PlayerPrefs.SetInt(Variables.instance.currentInvasion, "planet" + DontDestoryValues.instance.planetNumber + "highestLevel");
        // display the stats to the player for the round
        if (DontDestoryValues.instance.gameType == 0)
        {
			int oldHighest  = PlayerPrefs.GetInt("planet" + DontDestoryValues.instance.planetNumber + "highestLevel", 1);
			if(Variables.instance.currentInvasion > oldHighest)
				PlayerPrefs.SetInt("planet" + DontDestoryValues.instance.planetNumber + "highestLevel", Variables.instance.currentInvasion);
			
            endOfRoundStats[0].text = "Planet Destroyed!";
            endOfRoundStats[1].text = "Kills: " + v.numberOfEnemiesKilledTotal;
            endOfRoundStats[2].text = "Money: " + v.playerMoneyTotal;
            endOfRoundStats[3].text = "Accuracy: " + v.ReturnTotalAccuracy().ToString("f1") + "% \n Make sure to visit the store!";
        }
        else if (DontDestoryValues.instance.gameType == 1)
        {
            endOfRoundStats[0].text = "Escape Pods Launched: " + v.playerEscapePodsRecused;
            endOfRoundStats[1].text = "Kills: " + v.numberOfEnemiesKilledTotal;
            endOfRoundStats[2].text = "Money: " + v.playerMoneyTotal;
            endOfRoundStats[3].text = "Accuracy: " + v.ReturnTotalAccuracy().ToString("f1") + "% \n Make sure to visit the store!";
        }

        if (!saved)
        {
            saved = true;

            // save all the stats
            int tempHits = PlayerPrefs.GetInt("hits");
            int tempShots = PlayerPrefs.GetInt("shots");
            int tempMoney = PlayerPrefs.GetInt("TotalMoney");
            int tempKills = PlayerPrefs.GetInt("kills");
            int tempInvasion = PlayerPrefs.GetInt("invasion");

            // add up all the stats
            tempHits += v.numberOfHitsTotal;
            tempShots += v.numberOfShotsTotal;
            tempMoney += (v.playerMoneyTotal * (int)Modifiers.creditsMod);
            tempKills += v.numberOfEnemiesKilledTotal;
            tempInvasion += v.currentInvasion;

            // save all the total stats
            PlayerPrefs.SetInt("hits", tempHits);
            PlayerPrefs.SetInt("shots", tempShots);
            PlayerPrefs.SetInt("TotalMoney", tempMoney);
            PlayerPrefs.SetInt("kills", tempKills);
            PlayerPrefs.SetInt("invasion", tempInvasion);

            // check and save the best stats
            DontDestoryValues.instance.SaveNewScore(v.currentInvasion, "bestinvasion");
            DontDestoryValues.instance.SaveNewScore(v.numberOfEnemiesKilledTotal, "bestkills");
            DontDestoryValues.instance.SaveNewScore(v.playerMoneyTotal, "bestmoney");
            DontDestoryValues.instance.SaveNewScore(v.numberOfHitsTotal, "besthits");
        }
    }

    void DisplayCompletedStats()
    {
        v.cameraRoundOver.enabled = true;
		//PlayerPrefs.SetInt(Variables.instance.currentInvasion, "planet" + DontDestoryValues.instance.planetNumber + "highestLevel");
        // display the stats to the player for the round
        if (DontDestoryValues.instance.gameType == 0)
        {
			int oldHighest  = PlayerPrefs.GetInt("planet" + DontDestoryValues.instance.planetNumber + "highestLevel", 1);
			if(Variables.instance.currentInvasion > oldHighest)
				PlayerPrefs.SetInt("planet" + DontDestoryValues.instance.planetNumber + "highestLevel", Variables.instance.currentInvasion);
			
			endOfRoundStats[0].text = "Victory";
            endOfRoundStats[1].text = "\nTotal Kills: " + v.numberOfEnemiesKilledTotal;
			
			if(Modifiers.creditsMod == 1)
            	endOfRoundStats[2].text = "\nTotal Money: " + v.playerMoneyTotal;
			else
				endOfRoundStats[2].text = "\nTotal Money: " + (v.playerMoneyTotal * (int)Modifiers.creditsMod) + "x2";
			
            endOfRoundStats[3].text = "\nOverall Accuracy: " + v.ReturnTotalAccuracy().ToString("f1") + "% ";
        }
        else if (DontDestoryValues.instance.gameType == 1)
        {
            endOfRoundStats[0].text = "Escape Pods Launched: " + v.playerEscapePodsRecused;
            endOfRoundStats[1].text = "Kills: " + v.numberOfEnemiesKilledTotal;
            
			if(Modifiers.creditsMod == 1)
				endOfRoundStats[2].text = "Money: " + v.playerMoneyTotal;
			else
				endOfRoundStats[2].text = "Total Money: " + (v.playerMoneyTotal * (int)Modifiers.creditsMod) + "x2";
			
            endOfRoundStats[3].text = "Accuracy: " + v.ReturnTotalAccuracy().ToString("f1") + "% ";
        }

        if (!saved)
        {
            saved = true;

            // save all the stats
            int tempHits = PlayerPrefs.GetInt("hits");
            int tempShots = PlayerPrefs.GetInt("shots");
            int tempMoney = PlayerPrefs.GetInt("Money");
            int tempKills = PlayerPrefs.GetInt("kills");
            int tempInvasion = PlayerPrefs.GetInt("invasion");

            // add up all the stats
            tempHits += v.numberOfHitsTotal;
            tempShots += v.numberOfShotsTotal;
            tempMoney += (v.playerMoneyTotal * (int)Modifiers.creditsMod);
            tempKills += v.numberOfEnemiesKilledTotal;
            tempInvasion += v.currentInvasion;

            // save all the total stats
            PlayerPrefs.SetInt("hits", tempHits);
            PlayerPrefs.SetInt("shots", tempShots);
            PlayerPrefs.SetInt("Money", tempMoney);
            PlayerPrefs.SetInt("kills", tempKills);
            PlayerPrefs.SetInt("invasion", tempInvasion);

            // check and save the best stats
            DontDestoryValues.instance.SaveNewScore(v.currentInvasion, "bestinvasion");
            DontDestoryValues.instance.SaveNewScore(v.numberOfEnemiesKilledTotal, "bestkills");
            DontDestoryValues.instance.SaveNewScore(v.playerMoneyTotal, "bestmoney");
            DontDestoryValues.instance.SaveNewScore(v.numberOfHitsTotal, "besthits");
        }
    }
	
	void DisplayTotallyCompletedStats()
    {
        v.cameraRoundOver.enabled = true;
		
        
		// display the stats to the player for the round
        if (DontDestoryValues.instance.gameType == 0)
        {
			//PlayerPrefs.SetInt(Variables.instance.currentInvasion, "planet" + DontDestoryValues.instance.planetNumber + "highestLevel");
            int oldHighest  = PlayerPrefs.GetInt("planet" + DontDestoryValues.instance.planetNumber + "highestLevel", 1);
			if(Variables.instance.currentInvasion > oldHighest)
				PlayerPrefs.SetInt("planet" + DontDestoryValues.instance.planetNumber + "highestLevel", Variables.instance.currentInvasion);
			
			endOfRoundStats[0].text = "You defended the galexy!";
            endOfRoundStats[1].text = "Total Kills: " + v.numberOfEnemiesKilledTotal;
            endOfRoundStats[2].text = "Total Money: " + v.playerMoneyTotal;
            endOfRoundStats[3].text = "Overall Accuracy: " + v.ReturnTotalAccuracy().ToString("f1") + "%";
        }
        else if (DontDestoryValues.instance.gameType == 1)
        {
            endOfRoundStats[0].text = "You saved: " + v.playerEscapePodsRecused + " people!";
            endOfRoundStats[1].text = "Kills: " + v.numberOfEnemiesKilledTotal;
            endOfRoundStats[2].text = "Money: " + v.playerMoneyTotal;
            endOfRoundStats[3].text = "Accuracy: " + v.ReturnTotalAccuracy().ToString("f1") + "%";
        }

        if (!saved)
        {
            saved = true;

            // save all the stats
            int tempHits = PlayerPrefs.GetInt("hits");
            int tempShots = PlayerPrefs.GetInt("shots");
            int tempMoney = PlayerPrefs.GetInt("Money");
            int tempKills = PlayerPrefs.GetInt("kills");
            int tempInvasion = PlayerPrefs.GetInt("invasion");

            // add up all the stats
            tempHits += v.numberOfHitsTotal;
            tempShots += v.numberOfShotsTotal;
            tempMoney += v.playerMoneyTotal;
            tempKills += v.numberOfEnemiesKilledTotal;
            tempInvasion += v.currentInvasion;

            // save all the total stats
            PlayerPrefs.SetInt("hits", tempHits);
            PlayerPrefs.SetInt("shots", tempShots);
            PlayerPrefs.SetInt("Money", tempMoney);
            PlayerPrefs.SetInt("kills", tempKills);
            PlayerPrefs.SetInt("invasion", tempInvasion);

            // check and save the best stats
            DontDestoryValues.instance.SaveNewScore(v.currentInvasion, "bestinvasion");
            DontDestoryValues.instance.SaveNewScore(v.numberOfEnemiesKilledTotal, "bestkills");
            DontDestoryValues.instance.SaveNewScore(v.playerMoneyTotal, "bestmoney");
            DontDestoryValues.instance.SaveNewScore(v.numberOfHitsTotal, "besthits");
        }
    }
	
    void CleanUp()
    {
        for (int ii = 0; ii < enemyDestoryList.Count; ii++)
        {
            Destroy(enemyDestoryList[ii].gameObject);
        }

        enemyDestoryList.Clear();
    }
}
