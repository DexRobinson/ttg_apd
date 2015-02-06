using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DontDestoryValues : MonoBehaviour 
{
    public static DontDestoryValues instance;

    // sound values
    public float effectVolume = 1.0f;
    public float musicVolume = 1.0f;

    // stats values
    public int hits;
    public int shots;
    public int kills;
    public int rounds;
    public int money;
    public float accuracy;

    public int bestHits;
    public int bestShots;
    public int bestKills;
    public int bestRounds;
    public int bestMoney;
    public int bestAccuracy;

    // achievements
    public List<int> achievementValues = new List<int>(10);
    public int currentAchievementIndex;

    // holds the game information, to play the Campagin, Evac, Survival modes
    public int gameType;
    public int planetNumber;

    // stored information
    public int isPlanetTwoUnlocked;
    public int isPlanetThreeUnlocked;
	
	public AudioClip menuMusic;
	public AudioClip gameMusic;
	
	public int startingLevel = 1;
	
    void Awake()
    {
        instance = this;
        useGUILayout = false;
        DontDestroyOnLoad(gameObject);


		PlayerPrefs.DeleteAll();


		//MenuMusicSwitch();
		Application.targetFrameRate = 30;
    }

    public void LoadAllStats()
    {
        // load in all the stats that the user has gotten
        hits = PlayerPrefs.GetInt("hits");
        shots = PlayerPrefs.GetInt("shots");
        kills = PlayerPrefs.GetInt("kills");
        rounds = PlayerPrefs.GetInt("invasion");
        money = PlayerPrefs.GetInt("TotalMoney");

        // check to make sure were not dividing by zero
        if (hits == 0 && shots == 0)
            accuracy = 0;
        else
            accuracy = hits / shots;

        bestHits = PlayerPrefs.GetInt("besthits");
        bestShots = PlayerPrefs.GetInt("bestshots");
        bestKills = PlayerPrefs.GetInt("bestkills");
        bestRounds = PlayerPrefs.GetInt("bestinvasion");
        bestMoney = PlayerPrefs.GetInt("bestmoney");

        if (bestHits == 0 || bestShots == 0)
            bestAccuracy = 0;
        else
            bestAccuracy = bestHits / bestShots;

        isPlanetTwoUnlocked = PlayerPrefs.GetInt("Planet2");
        isPlanetThreeUnlocked = PlayerPrefs.GetInt("Planet3");
    }

    public void SaveNewScore(int number, string name)
    {
        // grab the value to check
        int tempNumber = PlayerPrefs.GetInt(name);

        // check the new number with the old value, if the new value is greater than the old one save over it
        if (number > tempNumber)
        {
            PlayerPrefs.SetInt(name, number);
        }
    }

    public void ChangePlanet(int planetId)
    {
        // change the planet, and planet number
        planetNumber = planetId;
    }

    public void ChangeMusic()
    {
        // change the music volume amount
        if (musicVolume == 0)
		{
            musicVolume = 1;
			audio.volume = 1;
		}
        else
		{
			audio.volume = 0;
            musicVolume = 0;
		}
    }

    public void ChangeEffect()
    {
        // change the effects volume
        if (effectVolume == 0)
            effectVolume = 1;
        else
            effectVolume = 0;
    }
	
	public void GameMusicSwitch()
	{
		audio.clip = gameMusic;
		audio.Play();
	}
	public void MenuMusicSwitch()
	{
		audio.clip = menuMusic;
		audio.Play();
	}
    public void LoadAchievements()
    {
        achievementValues[0] = PlayerPrefs.GetInt("RoundAcc");
        achievementValues[1] = PlayerPrefs.GetInt("TotalAcc");
        achievementValues[2] = PlayerPrefs.GetInt("RoundKill");
        achievementValues[3] = PlayerPrefs.GetInt("TotalKill");
        achievementValues[4] = PlayerPrefs.GetInt("UnlockEverything");
        achievementValues[5] = PlayerPrefs.GetInt("UpgradeEverything");
        achievementValues[6] = PlayerPrefs.GetInt("WavesComplete");
        achievementValues[7] = PlayerPrefs.GetInt("MoneyRound");
        achievementValues[8] = PlayerPrefs.GetInt("MoneyTotal");
        achievementValues[9] = PlayerPrefs.GetInt("Mines");
        //achievementValues[10] = PlayerPrefs.GetInt("All");
    }

    public void AchievementCheck()
    {
        if (Variables.instance.gameState == Variables.GameState.Game)
        {
            if (achievementValues[9] == 0 && Variables.instance.numberOfMinesOnScreen > 10)
            {
                achievementValues[9] = 1;
                PlayerPrefs.SetInt("Mines", 1);
                GameManager.achievementUnlocked = true;
                //StartCoroutine(Variables.instance.DisplayAchievementUnlocked());
            }
        }
        else if (Variables.instance.gameState == Variables.GameState.RoundOver)
        {
            if (achievementValues[0] == 0 && Variables.instance.ReturnRoundAccuracy() > 90)
            {
                achievementValues[0] = 1;
                PlayerPrefs.SetInt("RoundAcc", 1);
                //StartCoroutine(Variables.instance.DisplayAchievementUnlocked());
                GameManager.achievementUnlocked = true;
            }

            if (achievementValues[2] == 0 && Variables.instance.numberOfEnemiesKilledRound > 100)
            {
                achievementValues[2] = 1;
                PlayerPrefs.SetInt("RoundKill", 1);
                GameManager.achievementUnlocked = true;
                //StartCoroutine(Variables.instance.DisplayAchievementUnlocked());
            }

            if (achievementValues[4] == 0 && Variables.instance.ReturnUpgradeNumbers())
            {
                achievementValues[4] = 1;
                PlayerPrefs.SetInt("UnlockEverything", 1);
                GameManager.achievementUnlocked = true;
                //StartCoroutine(Variables.instance.DisplayAchievementUnlocked());
            }

            if (achievementValues[5] == 0 && Variables.instance.ReturnUpgradeNumbersAll())
            {
                achievementValues[5] = 1;
                PlayerPrefs.SetInt("UpgradeEverything", 1);
                GameManager.achievementUnlocked = true;
                //StartCoroutine(Variables.instance.DisplayAchievementUnlocked());
            }

            if (achievementValues[7] == 0 && Variables.instance.playerMoneyRound > 500)
            {
                achievementValues[7] = 1;
                PlayerPrefs.SetInt("MoneyRound", 1);
                GameManager.achievementUnlocked = true;
                //StartCoroutine(Variables.instance.DisplayAchievementUnlocked());
            }
        }
        else if (Variables.instance.gameState == Variables.GameState.GameOver)
        {
            if (achievementValues[1] == 0 && Variables.instance.ReturnTotalAccuracy() > 80)
            {
                achievementValues[1] = 1;
                PlayerPrefs.SetInt("TotalAcc", 1);
                GameManager.achievementUnlocked = true;
                //StartCoroutine(Variables.instance.DisplayAchievementUnlocked());
            }

            if (achievementValues[3] == 0 && Variables.instance.numberOfEnemiesKilledTotal > 1000)
            {
                achievementValues[3] = 1;
                PlayerPrefs.SetInt("TotalKill", 1);
                GameManager.achievementUnlocked = true;
                //StartCoroutine(Variables.instance.DisplayAchievementUnlocked());
            }

            if (achievementValues[6] == 0 && Variables.instance.currentInvasion > 9)
            {
                achievementValues[6] = 1;
                PlayerPrefs.SetInt("WavesComplete", 1);
                GameManager.achievementUnlocked = true;
                //StartCoroutine(Variables.instance.DisplayAchievementUnlocked());
            }

            if (achievementValues[8] == 0 && Variables.instance.playerMoneyTotal > 5000)
            {
                achievementValues[8] = 1;
                PlayerPrefs.SetInt("MoneyTotal", 1);
                GameManager.achievementUnlocked = true;
                //StartCoroutine(Variables.instance.DisplayAchievementUnlocked());
            }

            /*if (CheckAll())
            {
                achievementValues[11] = 1;
                PlayerPrefs.SetInt("All", 1);
                GameManager.achievementUnlocked = true;
                //StartCoroutine(Variables.instance.DisplayAchievementUnlocked());
            }*/
        }
    }
	
	public void AchievementCheckMenu()
    {
        if (achievementValues[4] == 0 && Variables.instance.ReturnUpgradeNumbers())
        {
            achievementValues[4] = 1;
            PlayerPrefs.SetInt("UnlockEverything", 1);
            //GameManager.achievementUnlocked = true;
            //StartCoroutine(Variables.instance.DisplayAchievementUnlocked());
        }

        if (achievementValues[5] == 0 && Variables.instance.ReturnUpgradeNumbersAll())
        {
            achievementValues[5] = 1;
            PlayerPrefs.SetInt("UpgradeEverything", 1);
            //GameManager.achievementUnlocked = true;
            //StartCoroutine(Variables.instance.DisplayAchievementUnlocked());
        }
		
		if (achievementValues[9] == 0 && Variables.instance.numberOfMinesOnScreen > 10)
            {
                achievementValues[9] = 1;
                PlayerPrefs.SetInt("Mines", 1);
                GameManager.achievementUnlocked = true;
                //StartCoroutine(Variables.instance.DisplayAchievementUnlocked());
            }


        if (CheckAll())
        {
            achievementValues[11] = 1;
            PlayerPrefs.SetInt("All", 1);
            //GameManager.achievementUnlocked = true;
            //StartCoroutine(Variables.instance.DisplayAchievementUnlocked());
        }
    }
	
    bool CheckAll()
    {
        for (int i = 0; i < 10; i++)
        {
            if (achievementValues[i] == 0)
            {
                return false;
            }
        }

        return true;
    }

	public void SwitchAudio(AudioClip clip)
	{
		audio.Stop();
		audio.clip = clip;
		audio.Play();
	}

    public void TurnOnAudio()
    {
        if (musicVolume > 0)
        {
            if (!audio.isPlaying)
            {
                audio.Play();
            }
        }
    }
	public void ToggleAudio()
	{
		if(musicVolume > 0)
		{
			if(audio.isPlaying)
			{
				audio.Pause();
			}
			else
			{
				audio.Play();
			}
		}
	}
}
