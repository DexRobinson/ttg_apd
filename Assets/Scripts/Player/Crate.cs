using UnityEngine;
using System.Collections;

public class Crate : MonoBehaviour 
{
	public Renderer crateRenderer;
    private Variables.CrateType crateType;
	public AudioClip warpIn;
    public float life = 2.0f;
    private float timeToDie;
	private bool wasTutorial;
	//private float orginalSize = 1.0f;
	private bool isDead;
	private bool init;
	public AudioSource audioS;
	private Transform t;
	private GameObject go;

    private ExplosionManager explosionManager;
    private Variables variables;

	void Awake()
	{
		//audioS = audio;
		t = transform;
		go = gameObject;
        variables = GameObject.FindGameObjectWithTag("Variables").GetComponent<Variables>();
        explosionManager = GameObject.FindGameObjectWithTag("Variables").GetComponent<ExplosionManager>();
		//ExplosionManager.ActivateShockwaveExplosion(new Vector3(transform.position.x, transform.position.y, transform.position.z - 5));
	}
	
    void OnEnable()
    {
		//ExplosionManager.ActivateShockwaveExplosion(transform.position);
		PlaySFX();
		isDead = false;
        //variables.numberOfItems++;
		if(variables.gameState == Variables.GameState.Tutorial)
		{
			crateType = Variables.CrateType.Money;
            crateRenderer.material.mainTexture = Resources.Load("Crate_" + crateType.ToString()) as Texture2D;
		}
		else
		{
        	crateType = variables.ReturnRandomCrate();
			crateRenderer.material.mainTexture = Resources.Load("Crate_" + crateType.ToString()) as Texture2D;
		}
		
		SpawnInEffects();
    }
	
	void PlaySFX()
	{
		if(DontDestoryValues.instance.effectVolume > 0)
		{
			audioS.clip = warpIn;
			audioS.volume = DontDestoryValues.instance.effectVolume;
			audioS.PlayOneShot(warpIn);
		}
	}
	
    void FixedUpdate()
    {
		if(variables.gameState == Variables.GameState.Game)
		{
        	t.Rotate(new Vector3(0.1f, 0.2f, 0.05f));
        	CountDown();
		}
		else if(variables.gameState == Variables.GameState.Tutorial)
		{
            t.Rotate(new Vector3(0.1f, 0.2f, 0.05f));
			if(wasTutorial)
			{
				wasTutorial = false;
				StartCoroutine(Tutorial.instance.UpdateIndex(11));
			}
		}
    }

    void OnTriggerEnter(Collider coll)
    {
		if(variables.gameState == Variables.GameState.Game || variables.gameState == Variables.GameState.Countdown || variables.gameState == Variables.GameState.Tutorial)
		{
	        if (coll.tag == "Bullet" || coll.tag == "Laser" || coll.tag == "Seeker" || coll.tag == "BulletFlak" || coll.tag == "Bomb")
	        {
				explosionManager.ActivateSmoke(transform.position);
	            PickUp();
	            Deactivate();
				GameManager.ActivateCrateSound();
				
				if(coll.tag == "Seeker")
					coll.gameObject.SetActive(false);
	        }
	        if (coll.tag == "CrateSeeker")
	        {
				//GameManager.ActivateCrateSoundMort();
				explosionManager.ActivateSmoke(transform.position);
	            Deactivate();
				coll.gameObject.SetActive(false);
	        }
		}
    }

    void PickUp ()
	{
		//renderer.enabled = false;
		//collider.enabled = false;
		
        //if(DontDestoryValues.instance.effectVolume > 0)
        //    audio.PlayOneShot(clip);

        //GameManager.ActivateCrateSound();

		variables.numberOfHitsRound += 1;
		variables.numberOfHitsTotal += 1;
		
		/*if(variables.gameState == Variables.GameState.Tutorial)
		{
			StartCoroutine(Tutorial.instance.UpdateIndex(11));
		}*/
		/*if(crateType == Variables.CrateType.Money)
		{
			GameManager.ActivateComboText("$" + (variables.itemMoneyPickupAmount + (variables.upgradeLevel[11] * 5)));
			variables.playerMoney += variables.itemMoneyPickupAmount + (variables.upgradeLevel[11] * 5);
            variables.playerMoneyRound += variables.itemMoneyPickupAmount + (variables.upgradeLevel[11] * 5);
            variables.playerMoneyTotal += variables.itemMoneyPickupAmount + (variables.upgradeLevel[11] * 5);
		}
		else
		{
			if(WeaponSlot.instance.slot1Full && WeaponSlot.instance.slot2Full)
			{
				switch (crateType) 
		        {
				    case Variables.CrateType.Bomb:
					    variables.playerBombAmount += 3;
					    break;
				    case Variables.CrateType.Energy:
					if(!variables.isPowerMode)
					{
						float maxEnergy = variables.playerMaxEnergy;
						maxEnergy *= (variables.upgradeLevel[8] * 0.2f);
					    variables.ChangePlayerEnergyAmount (maxEnergy);
					}
					    break;
				    case Variables.CrateType.Flak:
						if(!variables.isPowerMode)
					    	variables.ChangeWeapon (Variables.Weapon.Flak);
					    break;
				    case Variables.CrateType.Health:
						float maxHealth = variables.playerMaxHealth;
						maxHealth *= (variables.upgradeLevel[7] * 0.1f);
					    Player.instance.AdjustPlanetHealth (maxHealth);
					    break;
				    case Variables.CrateType.Laser:
						if(!variables.isPowerMode)
					    	variables.ChangeWeapon (Variables.Weapon.Laser);
					    break;
				    case Variables.CrateType.Mines:
					    variables.ItemDropMines();
					    break;
				    case Variables.CrateType.Money:
						//QualitySettings.SetQualityLevel(2);
						GameManager.ActivateComboText("$" + (variables.itemMoneyPickupAmount + (variables.upgradeLevel[11] * 5)));
					    variables.playerMoney += variables.itemMoneyPickupAmount + (variables.upgradeLevel[11] * 5);
		                variables.playerMoneyRound += variables.itemMoneyPickupAmount + (variables.upgradeLevel[11] * 5);
		                variables.playerMoneyTotal += variables.itemMoneyPickupAmount + (variables.upgradeLevel[11] * 5);
					    break;
				    case Variables.CrateType.Multi:
						if(!variables.isPowerMode)
					    	variables.ChangeWeapon (Variables.Weapon.Multi);
					    break;
				    case Variables.CrateType.Rapid:
						if(!variables.isPowerMode)
					    	variables.ChangeWeapon (Variables.Weapon.Rapid);
					    break;
				    case Variables.CrateType.Satalite:
		                variables.ItemDropSatalite();
		                break;
		            case Variables.CrateType.Seeker:
						if(!variables.isPowerMode)
		                	variables.ChangeWeapon(Variables.Weapon.Seeker);
		                break;
		            case Variables.CrateType.Shield:
		                variables.ChangePlayerShields(variables.itemShieldMaxAmount);
		                break;
		            case Variables.CrateType.Slow:
		                variables.ItemSlowOn();
		                break;
		        }
			}
			else
			{
				WeaponSlot.instance.FillInSlots(crateType);
			}
		}*/
		
		switch (crateType) 
        {
		    case Variables.CrateType.Bomb:
			    variables.playerBombAmount += 3;
			    break;
		    case Variables.CrateType.Energy:
			if(!variables.isPowerMode)
			{
				GameManager.ActivateComboText("Energy");
				float maxEnergy = variables.playerMaxEnergy;
				maxEnergy *= (variables.upgradeLevel[8] * 0.2f);
			    variables.ChangePlayerEnergyAmount (maxEnergy);
			}
			    break;
		    case Variables.CrateType.Flak:
			variables.ChangePlayerEnergyAmount (300);
			GameManager.ActivateComboText("Flak");
				if(!variables.isPowerMode)
			    	variables.ChangeWeapon (Variables.Weapon.Flak);
			    break;
		    case Variables.CrateType.Health:
				GameManager.ActivateComboText("Health");
				float maxHealth = variables.playerMaxHealth;
				maxHealth *= (variables.upgradeLevel[7] * 0.1f);
			    Player.instance.AdjustPlanetHealth (maxHealth);
			    break;
		    case Variables.CrateType.Laser:
				
				if(!variables.isPowerMode){
				variables.ChangePlayerEnergyAmount (300);
					GameManager.ActivateComboText("Laser");
			    	variables.ChangeWeapon (Variables.Weapon.Laser);
				}
			    break;
		    case Variables.CrateType.Mines:
				GameManager.ActivateComboText("Mines");
			    variables.ItemDropMines();
			    break;
		    case Variables.CrateType.Money:
				//QualitySettings.SetQualityLevel(2);
				GameManager.ActivateComboText("$" + (variables.itemMoneyPickupAmount + (variables.upgradeLevel[11] * 5)));
                variables.IncreaseMoney(variables.itemMoneyPickupAmount + (variables.upgradeLevel[11] * 5));
                
                //variables.playerMoney += variables.itemMoneyPickupAmount + (variables.upgradeLevel[11] * 5);
                //variables.playerMoneyRound += variables.itemMoneyPickupAmount + (variables.upgradeLevel[11] * 5);
                //variables.playerMoneyTotal += variables.itemMoneyPickupAmount + (variables.upgradeLevel[11] * 5);
			    break;
		    case Variables.CrateType.Multi:
			variables.ChangePlayerEnergyAmount (300);
				if(!variables.isPowerMode)
				{
					GameManager.ActivateComboText("Multi Shot");
			    	variables.ChangeWeapon (Variables.Weapon.Multi);
				}
			    break;
		    case Variables.CrateType.Rapid:
			variables.ChangePlayerEnergyAmount (300);
				if(!variables.isPowerMode){
					GameManager.ActivateComboText("Rapid");
			    	variables.ChangeWeapon (Variables.Weapon.Rapid);
				}
			    break;
		    case Variables.CrateType.Satalite:
				GameManager.ActivateComboText("Satalite");
                variables.ItemDropSatalite();
                break;
            case Variables.CrateType.Seeker:
			variables.ChangePlayerEnergyAmount (300);
				if(!variables.isPowerMode){
					GameManager.ActivateComboText("Seeker");
                	variables.ChangeWeapon(Variables.Weapon.Seeker);
				}
                break;
            case Variables.CrateType.Shield:
				GameManager.ActivateComboText("Shield");
                variables.ChangePlayerShields(variables.itemShieldMaxAmount);
                break;
            case Variables.CrateType.Slow:
				GameManager.ActivateComboText("Slow");
                variables.ItemSlowOn();
                break;
        }
    }
	
    public void Activate()
    {
        timeToDie = Time.time + life;
		if(!init)
		{
			init = true;
			//ExplosionManager.ActivateShockwaveExplosion(new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z - 5));
		}
        //crateType = variables.ReturnRandomCrate();
        //renderer.material.mainTexture = Resources.Load("Crate_" + crateType.ToString()) as Texture2D;
    }

    private void Deactivate()
    {
		if(variables.gameState == Variables.GameState.Tutorial)
		{
			go.GetComponentInChildren<Collider>().enabled = false;
            go.GetComponentInChildren<Renderer>().enabled = false;
			wasTutorial = true;
            init = false;
            //go.SetActive(false);
		}
		else
		{
			init = false;
        	go.SetActive(false);
		}
    }

    private void CountDown()
    {
        if (timeToDie < Time.time){
			if(!isDead)
			{
				isDead = true;
            	//Deactivate();
				SpawnOutEffect();
			}
        }
    }
	
	public void SetCrateType(Variables.CrateType ct)
	{
		crateType = ct;
		crateRenderer.material.mainTexture = Resources.Load("Crate_" + crateType.ToString()) as Texture2D;
	}
	
	void SpawnInEffects()
	{
		StartCoroutine(SpawnInCrateEffect());
	}
	void SpawnOutEffect()
	{
		StartCoroutine(SpawnOutCrateEffect());
	}
	
	IEnumerator SpawnInCrateEffect()
	{
		t.localScale = (Vector3.one * 0.1f);
		
		for(int i = 0; i < 10; i++){
			//transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
			t.localScale += (Vector3.one * 0.1f);
			yield return new WaitForSeconds(0.005f);
		}
		
		for(int i = 0; i < 10; i++){
			//transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
			t.localScale -= (Vector3.one * 0.01f);
			yield return new WaitForSeconds(0.005f);
		}
	}
	IEnumerator SpawnOutCrateEffect()
	{
		for(int i = 0; i < 10; i++){
			//transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
			t.localScale += (Vector3.one * 0.01f);
			yield return new WaitForSeconds(0.005f);
		}
		
		for(int i = 0; i < 10; i++){
			//transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
			t.localScale -= (Vector3.one * 0.1f);
			yield return new WaitForSeconds(0.005f);
		}
		
		if(variables.gameState == Variables.GameState.Tutorial)
		{
			//go.collider.enabled = false;
			//go.renderer.enabled = false;
            
			wasTutorial = true;

            go.SetActive(false);
		}
		else
		{
        	go.SetActive(false);
		}
	}
}
