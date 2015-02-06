using UnityEngine;
using System.Collections;

public class MainMenuManager : MonoBehaviour 
{
	public static MainMenuManager instance;
	
    public GameObject mainMenuGO;
    public GameObject optionsGO;
    public GameObject highScoreGO;
    public GameObject achievementsGO;
    public GameObject instructionsGO;
    public GameObject planetSelection;
    public GameObject askForInstructions;
    public GameObject modSelection;
    public GameObject buyGO;
    public GameObject mainCamera;
	public GameObject instructionsOptions;
	public GameObject levelSelectMenu;
	public GameObject storeMenuOption;
	public GameObject iapStore;
	//public GameObject iapLock;
	
    public TextMesh killTotal;
    //public TextMesh killBest;
    public TextMesh moneyTotal;
    //public TextMesh moneyBest;
    public TextMesh accuracyTotal;
    //public TextMesh accuracyBest;
    public TextMesh invasionTotal;
    //public TextMesh invasionBest;
	public TextMesh currentLevel;
	public TextMesh currentExperience;
	public TextMesh currentExperiencePoints;
	
	public TextMesh killTotalB;
    public TextMesh moneyTotalB;
    public TextMesh accuracyTotalB;
    public TextMesh invasionTotalB;
	public TextMesh currentLevelB;
	public TextMesh currentExperienceB;
	public TextMesh currentExperiencePointsB;
	
    public Texture2D[] colorTextures;
    public Texture2D[] greyTextures;
    public GameObject achievementImage;

	public GameObject effectGameObject;
	public Texture2D[] effectsVolumeGraphics;
	public GameObject musicGameObject;
	public Texture2D[] musicVolumeGraphics;

    public GameObject planetTagOne;
    public GameObject planetTagTwo;
    public GUITexture fadeCube;
	
	public AudioClip buttonPress;
	
	public GUITexture storyInstrucions;
	public GUITexture evacInstructions;
	
	public Texture2D[] expBarTextures;
	public GameObject[] expBars;
	//public Camera storeBlockCamera;
	
	public Texture[] tutorialPages;
	private int currentTutorialPage;
	
    public static bool planetSelectionMode;
	private Ray ray;
    private RaycastHit hit;
    private int planetToBuy = 0;

    private Vector3 outPosition = new Vector3(0, 10, 0);
    private Vector3 inPosition = new Vector3(0, 0, 0);
	
	private float timerForButtons = 0.0f;
	public static bool timerUpdating;
	private bool viewingTutorial;
	//private bool updateFromIAP;
	public static bool eightbit;
	
	public bool firstTouch;
	
	public int storeTouchIndex = -1;

	public StatsBar[] statsBars;
	public StatsBarManager statsBarManager;

	public GameObject[] animatedCrates;
	public Transform spawnCratePoint;
	public GameObject currentCrate;
    public int currentCrateSpawn;

	public bool isInStore;

    public GameObject storeMenuUI;

	void Awake()
	{
		instance = this;
	}
	
    void Start()
    {
		timerUpdating = false;
		gameObject.AddComponent<AudioSource>();
        DontDestoryValues.instance.LoadAllStats();
        DontDestoryValues.instance.LoadAchievements();
        Variables.instance.LoadInformation();
		DontDestoryValues.instance.MenuMusicSwitch();
        WriteHighScores();
        DrawAchievement();
		SwitchEffectTextures();
		SwitchMusicTexture();
		DrawCurrentExperiencePoints();
		UpdateExpBars();
		
		StartCoroutine(FadeOnScreen(fadeCube));
		
		//if(Time.time > 20){
		//	MoveMenuPositions(optionsGO, mainMenuGO);
		//	StartCoroutine(TurnOnStoreCam());
		//}

		DestoryCurrentCrate();

        //SpawnFirstCurrentCrate();
    }

    void Update()
    {
		DrawCurrentExperiencePoints();
		//Variables.instance.ReturnCrateDescription();
			
        if (DontDestoryValues.instance.isPlanetTwoUnlocked == 1)
        {
            planetTagOne.renderer.enabled = false;
            planetTagOne.collider.enabled = false;
        }
        else
        {
            planetTagOne.renderer.enabled = true;
            planetTagOne.collider.enabled = true;
        }
        
        if (DontDestoryValues.instance.isPlanetThreeUnlocked == 1)
        {
            planetTagTwo.renderer.enabled = false;
            planetTagTwo.collider.enabled = false;
        }
        else
        {
            planetTagTwo.renderer.enabled = true;
            planetTagTwo.collider.enabled = true;
        }

        if (Input.GetKey(KeyCode.D))
        {
            if (Application.platform == RuntimePlatform.WindowsEditor)
                PlayerPrefs.DeleteAll();
        }
        
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
				if(!timerUpdating)
                	ButtonDownEvents();
            }
        }

        // handles all the input to the main menu objects
        if (Input.touchCount > 0)
        {
            foreach (Touch touch in Input.touches)
            {
                ray = Camera.main.ScreenPointToRay(touch.position);

                if (Physics.Raycast(ray, out hit))
                {
                    if (touch.phase == TouchPhase.Began)
                    {
						if(!timerUpdating)
                        	ButtonDownEvents();
                    }
                }
            }
        }
		
		
		if(timerUpdating){
			timerForButtons += Time.deltaTime;
			
			if(timerForButtons > 0.1f){
				timerUpdating = false;
				timerForButtons = 0.0f;
			}
		}
    }

    public Transform cubeParent;

	public void SpawnFirstCurrentCrate()
	{
		if(currentCrate)
			currentCrate.animation.Play("StoreCrateOut");
		
		GameObject cur = Instantiate(animatedCrates[0], spawnCratePoint.position, Quaternion.identity) as GameObject;
        cur.transform.parent = cubeParent;
        currentCrate = cur;
		cur.animation.Play("StoreCrateIn");
	}

    private IEnumerator _SpawnCrateStore( bool crateDown )
    {
        if (crateDown)
        {
            currentCrateSpawn--;
            if (currentCrateSpawn < 0)
                currentCrateSpawn = animatedCrates.Length - 1;


            if (currentCrate)
                currentCrate.animation.Play("StoreCrateOutDown");

            //Debug.Log(currentCrateSpawn);
            yield return new WaitForEndOfFrame();

            GameObject cur = Instantiate(animatedCrates[currentCrateSpawn], spawnCratePoint.position, Quaternion.identity) as GameObject;
            cur.transform.parent = cubeParent;
            cur.transform.localScale = Vector3.one * 1.1f;
            currentCrate = cur;
            cur.animation.Play("StoreCrateInDown");
        }
        else
        {
            currentCrateSpawn++;
            if (currentCrateSpawn > animatedCrates.Length - 1)
                currentCrateSpawn = 0;

            if (currentCrate)
                currentCrate.animation.Play("StoreCrateOut");

            //Debug.Log(currentCrateSpawn);
            yield return new WaitForEndOfFrame();

            GameObject cur = Instantiate(animatedCrates[currentCrateSpawn], spawnCratePoint.position, Quaternion.identity) as GameObject;
            cur.transform.parent = cubeParent;
            cur.transform.localScale = Vector3.one * 1.1f;
            currentCrate = cur;
            cur.animation.Play("StoreCrateIn");
        }
    }
	public void SpawnCrateStore(bool crateDown)
	{
        StartCoroutine(_SpawnCrateStore(crateDown));

        //if(crateDown)
        //{
        //    currentCrateSpawn--;
        //    if (currentCrateSpawn < 0)
        //        currentCrateSpawn = animatedCrates.Length - 1;
            

        //    if(currentCrate)
        //        currentCrate.animation.Play("StoreCrateOutDown");

        //    Debug.Log(currentCrateSpawn);
        //    GameObject cur = Instantiate(animatedCrates[currentCrateSpawn], spawnCratePoint.position, Quaternion.identity) as GameObject;
        //    cur.transform.parent = cubeParent;
        //    currentCrate = cur;
        //    cur.animation.Play("StoreCrateInDown");
        //}
        //else
        //{
        //    currentCrateSpawn++;
        //    if (currentCrateSpawn > animatedCrates.Length - 1)
        //        currentCrateSpawn = 0;

        //    if(currentCrate)
        //        currentCrate.animation.Play("StoreCrateOut");

        //    Debug.Log(currentCrateSpawn);

        //    GameObject cur = Instantiate(animatedCrates[currentCrateSpawn], spawnCratePoint.position, Quaternion.identity) as GameObject;
        //    cur.transform.parent = cubeParent;
        //    currentCrate = cur;
        //    cur.animation.Play("StoreCrateIn");
        //}

	}
	public void DestoryCurrentCrate()
	{
		Destroy(currentCrate);
	}

	IEnumerator TurnOnStoreCam(){
		yield return new WaitForSeconds(3.0f);
		//storeBlockCamera.camera.enabled = true;
	}
	
	public void UpdateIAP()
	{
		StartCoroutine(WaitToUpdate());
	}
	private IEnumerator WaitToUpdate()
	{
		yield return new WaitForSeconds(1.0f);
		//updateFromIAP = true;
	}
	
	void TurnTutorialPage(){
		
		currentTutorialPage++;
		
		if(currentTutorialPage >= tutorialPages.Length)
		{
			if(DontDestoryValues.instance.gameType == 1)
				StartGame();
			else
			{
				currentTutorialPage = 0;
				instructionsGO.renderer.material.mainTexture = tutorialPages[currentTutorialPage];
				LevelSelect.instance.DrawLevelSelect(DontDestoryValues.instance.planetNumber);
				MoveMenuPositions(levelSelectMenu, instructionsGO);
			}
		}
		else
			instructionsGO.renderer.material.mainTexture = tutorialPages[currentTutorialPage];
	}
	
	void UpdateExpBars()
	{
		expBars[0].renderer.material.mainTexture = expBarTextures[PlayerPrefs.GetInt("playerDurLevel")];
		expBars[1].renderer.material.mainTexture = expBarTextures[PlayerPrefs.GetInt("playerMaxHealthLevel")];
		expBars[2].renderer.material.mainTexture = expBarTextures[PlayerPrefs.GetInt("playerHealthRegainLevel")];
		expBars[3].renderer.material.mainTexture = expBarTextures[PlayerPrefs.GetInt("playerMaxEnergyLevel")];
		expBars[4].renderer.material.mainTexture = expBarTextures[PlayerPrefs.GetInt("playerEnergyRegainLevel")];
		
		DrawCurrentExperiencePoints();
	}
	
    void WriteHighScores()
    {
        killTotal.text = "Kills: " + DontDestoryValues.instance.kills;
        moneyTotal.text = "Money: " + DontDestoryValues.instance.money;
        accuracyTotal.text = "Hits: " + DontDestoryValues.instance.hits;
        invasionTotal.text = "Invasions: " + DontDestoryValues.instance.rounds;

        //killBest.text = "Kills: " + DontDestoryValues.instance.bestKills;
        //moneyBest.text = "Money: " + DontDestoryValues.instance.bestMoney;
        //accuracyBest.text = "Hits: " + DontDestoryValues.instance.bestHits;
        //invasionBest.text = "Invasions: " + DontDestoryValues.instance.bestRounds;
		
		currentLevel.text = "Level: " + Variables.instance.playerCurrentLevel;
		currentExperience.text = "Experience: " + Variables.instance.playerExperience + " / " + Variables.instance.playerMaxExperience;
		
		killTotalB.text = "Kills: " + DontDestoryValues.instance.kills;
        moneyTotalB.text = "Money: " + DontDestoryValues.instance.money;
        accuracyTotalB.text = "Hits: " + DontDestoryValues.instance.hits;
        invasionTotalB.text = "Invasions: " + DontDestoryValues.instance.rounds;

        //killBestB.text = "Kills: " + DontDestoryValues.instance.bestKills;
        //moneyBest.text = "Money: " + DontDestoryValues.instance.bestMoney;
        //accuracyBest.text = "Hits: " + DontDestoryValues.instance.bestHits;
        //invasionBest.text = "Invasions: " + DontDestoryValues.instance.bestRounds;
		
		currentLevelB.text = "Level: " + Variables.instance.playerCurrentLevel;
		currentExperienceB.text = "Experience: " + Variables.instance.playerExperience + " / " + Variables.instance.playerMaxExperience;
    }

    void ButtonDownEvents()
    {
        switch (hit.collider.tag)
        {
			case "LockReturn":
			ButtonDownEffect();
			//MoveMenuPositions(optionsGO, iapLock);
			break;
			case "LockYes":
				ButtonDownEffect();
				//MoveMenuPositions(iapStore, iapLock);
				MoveMenuPositions(iapStore, optionsGO);
				//storeBlockCamera.camera.enabled = false;
			break;
			case "LockIcon":
				ButtonDownEffect();
				//MoveMenuPositions(iapLock, mainMenuGO);
			break;
			case "Respawn":
				ButtonDownEffect();
				PlayerPrefs.DeleteAll();
			break;
			case "TutorialPage":
				ButtonDownEffect();
				TurnTutorialPage();
			break;
			case "PlanetDurUpgrade":
				ButtonDownEffect();
				Variables.instance.BuyPlayerBuffs(0);
				UpdateExpBars();
				break;
			case "PlanetMaxHeaalthUpgrade":
				ButtonDownEffect();
				Variables.instance.BuyPlayerBuffs(1);
				UpdateExpBars();
				break;
			case "PlanetHealthReganUpgrade":
				ButtonDownEffect();
				Variables.instance.BuyPlayerBuffs(2);
				UpdateExpBars();
				break;
			case "PlanetMaxEnergyUpgrade":
				ButtonDownEffect();
				Variables.instance.BuyPlayerBuffs(3);
				UpdateExpBars();
				break;
			case "PlanetEnergyRegainUpgrade":
				ButtonDownEffect();
				Variables.instance.BuyPlayerBuffs(4);
				UpdateExpBars();
				break;
			 case "BuyUpgrade":
                // can you afford it?
				//if(PlayerPrefs.GetInt("FullVersion") != 180069)
				//{
				//	ButtonDownEffect();
				//	MoveMenuPositions(iapLock, mainMenuGO);
				//}
				//else
				//{
					ButtonDownEffect();
	                Variables.instance.BuyUpgrade(Variables.instance.upgradeCurrentCrate);
					
				//}
                break;
            case "RightUpgrade":
                // adjust the upgrades up 1
				ButtonDownEffect();
                Variables.instance.MoveRight();
                break;
            case "LeftUpgrade":
                // adjust the upgrade down 1
				ButtonDownEffect();
                Variables.instance.MoveLeft();
                break;
			case "StoryInstructions":
			if(!storyInstrucions.enabled)
				storyInstrucions.enabled = true;
			else
				storyInstrucions.enabled = false;
			break;
			case "EvacInstructions":
			if(!evacInstructions.enabled)
				evacInstructions.enabled = true;
			else
				evacInstructions.enabled = false;
			break;
			case "InstructionOptions":
				MoveMenuPositions(instructionsOptions, mainMenuGO);
			break;
			case "InstructionOptionsReturn":
				MoveMenuPositions(mainMenuGO, instructionsOptions);
			break;
            case "YesBuyPlanet":
				ButtonDownEffect();
                BuyPlanet(planetToBuy);
				planetSelectionMode = true;
                buyGO.transform.position = outPosition;
                break;
            case "NoBuyPlanet":
				ButtonDownEffect();
                planetToBuy = 0;
				planetSelectionMode = true;
                buyGO.transform.position = outPosition;
                break;
            case "BuyPlanet1":
				if(planetSelectionMode)
				{
	                planetToBuy = 1;
					planetSelectionMode = false;
	                buyGO.transform.position = inPosition;
				}
                break;
            case "BuyPlanet2":
				if(planetSelectionMode)
				{
	                planetToBuy = 2;
					planetSelectionMode = false;
	                buyGO.transform.position = inPosition;
				}
                break;
            case "Planet1":
                if (planetSelectionMode)
                {
                    DontDestoryValues.instance.ChangePlanet(0);
                    MoveMenuPositions(modSelection, planetSelection);
                    planetSelectionMode = false;
                }
                break;
            case "Planet2":
                if (planetSelectionMode)
                {
                    if (DontDestoryValues.instance.isPlanetTwoUnlocked == 1)
                    {
                        DontDestoryValues.instance.ChangePlanet(1);
                        MoveMenuPositions(modSelection, planetSelection);

                        planetSelectionMode = false;
                    }
                    else
                    {
                        // put something here to buy it
                    }
                }
                break;
            case "Planet3":
                if (planetSelectionMode)
                {
                    if (DontDestoryValues.instance.isPlanetThreeUnlocked == 1)
                    {
                        DontDestoryValues.instance.ChangePlanet(2);
                        MoveMenuPositions(modSelection, planetSelection);

                        planetSelectionMode = false;
                    }
                    else
                    {
                        // put something here to buy it
                    }
                }
                break;
            case "YesInstruction":
				ButtonDownEffect();
				viewingTutorial = true;
				StartGame();
			
                //MoveMenuPositions(instructionsGO, askForInstructions );
                break;
            case "GoToGame":
				ButtonDownEffect();
                StartGame();
                break;
            case "Sound":
				ButtonDownEffect();
                DontDestoryValues.instance.ChangeMusic();
                SwitchMusicTexture();
                break;
            case "Effects":
				ButtonDownEffect();
                DontDestoryValues.instance.ChangeEffect();
				SwitchEffectTextures();
                break;
            case "Facebook":
				Variables.instance.IncreaseMoney(100000);
				Variables.instance.playerExperiencePoints+=10;
				Variables.instance.SaveInformationNow();
                //Application.OpenURL("http://facebook.com/TapToonGames");
                break;
            case "Rate":
				PlayerPrefs.DeleteAll();
                //Application.OpenURL("https://itunes.apple.com/us/app/acme-planetary-defense/id521268115?ls=1&mt=8");
                break;
            case "PlanetSelection":
                MoveMenuPositions(planetSelection, mainMenuGO);
                planetSelectionMode = true;
                break;
            case "StartStory":
                DontDestoryValues.instance.gameType = 0;
				//if(PlayerPrefs.GetInt("PlayInstructions") == 0)
            	MoveMenuPositions(askForInstructions, modSelection);
				/*else
				{
					Resources.UnloadUnusedAssets();
					mainMenuGO.SetActive(false);
					instructionsGO.SetActive(false);
					modSelection.SetActive(false);
	                StartCoroutine(CameraZoom(DontDestoryValues.instance.planetNumber));
				}*/
					
                break;
            case "StartEvac":
                DontDestoryValues.instance.gameType = 1;
                MoveMenuPositions(askForInstructions, modSelection);
                break;
			case "IAPStoreBack":
				MoveMenuPositions(mainMenuGO, iapStore);
				break;
			case "UpgradeStoreBack":

				//MoveMenuPositions(mainMenuGO, loc);
				MoveMenuPositions(mainMenuGO, storeMenuOption);
				//storeBlockCamera.camera.enabled = false;
				break;
			case "UpgradeStore":
				isInStore = true;
				//SpawnFirstCurrentCrate();
				MoveMenuPositions(optionsGO, storeMenuOption);
                SpawnFirstCurrentCrate();
				//storeBlockCamera.camera.enabled = true;
				break;
			case "IAPStore":
				MoveMenuPositions(iapStore, storeMenuOption);
				break;
            case "Options":
				//if(Variables.instance.playerMoney != PlayerPrefs.GetInt("Money") || Variables.instance.playerExperiencePoints != PlayerPrefs.GetInt("expPoints"))
				//{
					//Variables.instance.playerMoney = PlayerPrefs.GetInt("Money");
					//Variables.instance.playerExperiencePoints = PlayerPrefs.GetInt("expPoints");
					//updateFromIAP = false;
				//}
				Variables.instance.LoadInformation();
				Variables.instance.ChangeCrate(0);
                MoveMenuPositions(storeMenuOption, mainMenuGO);
                break;
            case "HighScores":
                MoveMenuPositions(highScoreGO, mainMenuGO);
                break;
            case "Achievements":
                MoveMenuPositions(achievementsGO, mainMenuGO);
                break;
            case "Instructions":
                MoveMenuPositions(instructionsGO, mainMenuGO);
                break;
            case "ReturnOptions":
				isInStore = false;
				DestoryCurrentCrate();
                MoveMenuPositions(mainMenuGO, optionsGO);
				//MoveMenuPositions(mainMenuGO, iapLock);
				//storeBlockCamera.camera.enabled = false;
                break;
            case "ReturnHighScores":
                MoveMenuPositions(mainMenuGO, highScoreGO);
                break;
            case "ReturnAchievements":
                MoveMenuPositions(mainMenuGO, achievementsGO);
                break;
            case "ReturnInstructions":
                MoveMenuPositions(mainMenuGO, instructionsGO);

                break;
            case "ReturnMod":
				planetSelectionMode = false;
                MoveMenuPositions(mainMenuGO, modSelection);
                break;
            case "ReturnPlanet":
				buyGO.transform.position = new Vector3(0, 10, 0);
                MoveMenuPositions(mainMenuGO, planetSelection);
                planetSelectionMode = false;
                break;
            case "RightAchievement":
                DontDestoryValues.instance.currentAchievementIndex++;
				ButtonDownEffect();
                if (DontDestoryValues.instance.currentAchievementIndex > DontDestoryValues.instance.achievementValues.Count - 1)
                {
                    DontDestoryValues.instance.currentAchievementIndex = 0;
                }

                DrawAchievement();
                break;
            case "LeftAchievement":
				ButtonDownEffect();
                DontDestoryValues.instance.currentAchievementIndex--;

                if (DontDestoryValues.instance.currentAchievementIndex < 0)
                {
                    DontDestoryValues.instance.currentAchievementIndex = DontDestoryValues.instance.achievementValues.Count - 1;
                }

                DrawAchievement();
                break;
			case "LevelSelect":
				if(DontDestoryValues.instance.gameType == 0)
				{
					LevelSelect.instance.DrawLevelSelect(DontDestoryValues.instance.planetNumber);
				
					//levelSelectMenu.SetActive(true);
					MoveMenuPositions(levelSelectMenu, askForInstructions);
				
					//instructionsGO.SetActive(false);
					//askForInstructions.SetActive(false);
				}
				else
				{
					ButtonDownEffect();
					StartGame();
				}
				break;
			case "LevelBack":
				//levelSelectMenu.SetActive(false);
				//mainMenuGO.SetActive(true);
				MoveMenuPositions(mainMenuGO, levelSelectMenu);
				break;
			case "ModBack":
				//levelSelectMenu.SetActive(false);
				//mainMenuGO.SetActive(true);
				MoveMenuPositions(mainMenuGO, askForInstructions);
				break;
			case "StoreArrowRight":
				ButtonDownEffect();
				StoreManager.instance.UpdateItemsRight();
				break;
			case "StoreArrowLeft":
				ButtonDownEffect();
				StoreManager.instance.UpdateItemsLeft();
				break;
        }
    }
	
	public void ButtonDownEffect()
	{
		audio.clip = buttonPress;
		audio.volume = DontDestoryValues.instance.effectVolume;
		audio.Play();
		timerUpdating = true;
	}
	
    void DrawAchievement()
    {
        if (DontDestoryValues.instance.achievementValues[DontDestoryValues.instance.currentAchievementIndex] == 1)
        {
            achievementImage.renderer.material.mainTexture = colorTextures[DontDestoryValues.instance.currentAchievementIndex];
        }
        else
        {
            achievementImage.renderer.material.mainTexture = greyTextures[DontDestoryValues.instance.currentAchievementIndex];
        }
    }
	
	void SwitchEffectTextures()
	{
		if(DontDestoryValues.instance.effectVolume == 0)
		{
			effectGameObject.renderer.material.mainTexture = effectsVolumeGraphics[0];
		}
		else if(DontDestoryValues.instance.effectVolume == 1)
		{
			effectGameObject.renderer.material.mainTexture = effectsVolumeGraphics[1];
		}
    }
    void SwitchMusicTexture()
    {
		if(DontDestoryValues.instance.musicVolume == 0)
		{
			musicGameObject.renderer.material.mainTexture = musicVolumeGraphics[0];
		}
		else if(DontDestoryValues.instance.musicVolume == 1)
		{
			musicGameObject.renderer.material.mainTexture = musicVolumeGraphics[1];
		}
	}

    void BuyPlanet(int planetId)
    {
        if (planetId == 1)
        {
            if (Variables.instance.playerMoney >= 5000)
            {
                DontDestoryValues.instance.isPlanetTwoUnlocked = 1;
                Variables.instance.playerMoney -= 5000;
                Variables.instance.SaveInformationNow();
            }
        }

        else if (planetId == 2)
        {
            if (DontDestoryValues.instance.isPlanetTwoUnlocked == 1)
            {
                if (Variables.instance.playerMoney >= 5000)
                {
                    DontDestoryValues.instance.isPlanetThreeUnlocked = 1;
                    Variables.instance.playerMoney -= 5000;
                    Variables.instance.SaveInformationNow();
                }
            }
        }
    }

    void MoveMenuPositions(GameObject inMenu, GameObject outMenu)
    {
        if (inMenu == optionsGO)
            inMenu.SetActive(true);

        if (outMenu == optionsGO)
            outMenu.SetActive(false);

		ButtonDownEffect();
        inMenu.transform.position = inPosition;
        outMenu.transform.position = outPosition;
    }

    IEnumerator CameraZoom(int planetID)
    {
        switch (planetID)
        {
            case 0:
                mainCamera.animation.Play("Planet1ZoomIn");
                break;
            case 1:
                mainCamera.animation.Play("Planet2Zoom");
                break;
            case 2:
                mainCamera.animation.Play("Planet3Zoom");
                break;
        }

        StartCoroutine(FadeScreen(fadeCube, mainCamera.animation.clip.length));
        //Debug.Log(mainCamera.animation.clip.length);
        yield return new WaitForSeconds(mainCamera.animation.clip.length);
		DontDestoryValues.instance.GameMusicSwitch();
		if(viewingTutorial)
		{
			Application.LoadLevel("Tutorial");
		}
		else
        	Application.LoadLevel("PreLoader");
    }

    IEnumerator FadeScreen(GUITexture fadeObj, float timetoFade)
    {
        float loops = timetoFade / 22.0f;

        for (int i = 0; i < 30; i++)
        {
            fadeObj.color = new Color(0, 0, 0, (loops * 0.5f) * i );
            //Debug.Log(loops * i);
            yield return new WaitForSeconds(loops);
        }

        fadeObj.renderer.material.color = new Color(0, 0, 0, 1.0f);
    }
	
	IEnumerator FadeOnScreen(GUITexture fadeObj)
    {
        for (int i = 0; i < 30; i++)
        {
            fadeObj.color = new Color(0, 0, 0, 1 - (0.0333f * i));
            //Debug.Log(loops * i);
            yield return new WaitForSeconds(0.1f);
        }

        //fadeObj.renderer.material.color = new Color(0, 0, 0, 0.0f);
    }
	
	public void DrawCurrentExperiencePoints()
	{
		currentExperiencePoints.text = Variables.instance.playerExperiencePoints.ToString();
		currentExperiencePointsB.text = Variables.instance.playerExperiencePoints.ToString();
	}
	
	public void BuyExperienceBouns(int index)
	{
		// 0 - dur
		// 1 - max health
		// 2 - health rechar
		// 3 - max energy
		// 4 - energy recharge
		
		if(Variables.instance.playerExperiencePoints > 0)
		{
			switch(index)
			{
				case 0:
					Variables.instance.playerExperiencePoints -= 1;
					Variables.instance.playerDurLevel += 1;
					//int oldDur = PlayerPrefs.GetInt("playerDurLevel");
					//oldDur -= 0.1f;
					//PlayerPrefs.SetFloat("playerDur", oldDur);
					Variables.instance.SaveInformation();
					break;
				case 1:
					break;
				case 2:
					break;
				case 3:
					break;
				case 4:
					break;
			}
		}
	}
	
	public void StartGame()
	{
		Resources.UnloadUnusedAssets();
		mainMenuGO.SetActive(false);
		instructionsGO.SetActive(false);
		levelSelectMenu.SetActive(false);
		modSelection.SetActive(false);
		askForInstructions.SetActive(false);
        StartCoroutine(CameraZoom(DontDestoryValues.instance.planetNumber));
		PlayerPrefs.SetInt("PlayInstructions", 1);
	}

    public void BackFromStore()
    {
        isInStore = false;
        DestoryCurrentCrate();
        MoveMenuPositions(mainMenuGO, optionsGO);
    }
}
