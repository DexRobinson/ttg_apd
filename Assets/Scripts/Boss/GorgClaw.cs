using UnityEngine;
using System.Collections;

public class GorgClaw : MonoBehaviour
{
    public float                health = 3.0f;  // max enemy health
    public float                speed = 3.0f;   // enemy speed
	//private int                 money = 1;    // how much enemy is worth
    public GameObject           shield;         // shield object
    private Variables   v;
    private Transform   t;
    private bool        shieldEnabled;      
	public bool 		isMiniBoss;
	public GameObject arms;
	public GameObject blackHole;
    private ExplosionManager explosionManager;

	private float bombTimer = 0.0f;
	
    void Awake()
    {
        v = Variables.instance;
        t = transform;
        explosionManager = GameObject.FindGameObjectWithTag("Variables").GetComponent<ExplosionManager>();
    }

    void OnEnable()
    {
        shield.SetActive(false);
		arms.renderer.materials[0].color = new Color(1, 1, 1, 1);
		arms.renderer.materials[1].color = new Color(1, 1, 1, 1);
    }
	
	void Start()
	{
		if(isMiniBoss)
		{
			HealthBar.instance.AddToCurrentHealth(health);
		}
	}
	
	void OnTriggerStay(Collider coll)
	{
		if(coll.tag ==  "Bomb")
		{
			bombTimer += Time.deltaTime;
			
			if(bombTimer > 0.5f)
			{
				bombTimer = 0.0f;
				ChangeHealth(10);
			}
		}
	}
	
    // switch between the things that hit the enemy and adjust their health accordingly
    void OnTriggerEnter (Collider coll)
	{
		switch (coll.tag) 
        {
			case "SuperWeapon":
				ChangeHealth(1000);
				break;
            case "CrateSeeker":
                if (!shieldEnabled)
                {
                    shieldEnabled = true;
					shield.gameObject.SetActive(true);
                    shield.renderer.enabled = true;
                }
                break;
		    case "Mines":
                Variables.instance.numberOfMinesAlive--;
				ChangeHealth (10);
				//Destroy (coll.gameObject);
                coll.gameObject.GetComponent<Explosion>().Deactivate();
				break;
            case "Bullet":
                ChangeHealth(v.playerBulletDamage);
                v.numberOfHitsRound += 1;
                v.numberOfHitsTotal += 1;

                //Destroy(coll.gameObject);
                //coll.gameObject.SetActive(false);
                coll.gameObject.SetActive(false);

                break;

			case "Planet":
				//money = 0;
				v.hitByEnemy = true;
                //v.numberOfEnemiesKilledRound -= 1;
                //v.numberOfEnemiesKilledTotal -= 1;
				ChangeHealth(1000);
				
				Player.instance.RemovePlanetHealth(450 - (Variables.instance.playerDurLevel * 45));
				break;

            case "Shield":
                //money = 0;
                //v.numberOfEnemiesKilledRound -= 1;
                //v.numberOfEnemiesKilledTotal -= 1;
                ChangeHealth(1000);
                v.RemovePlayerShield(1);
                break;

            case "EscapePod":
                //money = 0;
                v.numberOfEnemiesKilledRound -= 1;
                v.numberOfEnemiesKilledTotal -= 1;
				coll.gameObject.SetActive(false);
                ChangeHealth(100);
                break;

            case "Laser":
                ChangeHealth(v.playerBulletDamage * 0.7f);
                v.numberOfHitsRound += 1;
                v.numberOfHitsTotal += 1;
			
				if(Variables.instance.previousWeapon == Variables.Weapon.Flak || (Variables.instance.weapon == Variables.Weapon.Flak && Variables.instance.previousWeapon == Variables.Weapon.Laser))
				{
					explosionManager.ActivateFlakExplosion(t.position);
				}
                break;

            case "BulletFlak":
                ChangeHealth(v.playerBulletDamage);
                v.numberOfHitsRound += 1;
                v.numberOfHitsTotal += 1;
                break;

            case "Seeker":
                ChangeHealth(v.playerBulletDamage);
                v.numberOfHitsRound += 1;
                v.numberOfHitsTotal += 1;
			
				if(Variables.instance.previousWeapon == Variables.Weapon.Flak || (Variables.instance.weapon == Variables.Weapon.Flak && Variables.instance.previousWeapon == Variables.Weapon.Seeker))
				{
					explosionManager.ActivateFlakExplosion(t.position);
				}
				coll.gameObject.SetActive(false);
                break;

            case "Bomb":
				if(isMiniBoss)
				{
					ChangeHealth(10);
				}
				else{
					ChangeHealth(1000);
				}
                
                v.numberOfHitsRound += 1;
                v.numberOfHitsTotal += 1;
                break;

            case "FlakShell":
                ChangeHealth(v.playerBulletDamage * 0.5f);
                v.numberOfHitsRound += 1;
                v.numberOfHitsTotal += 1;
                break;
        }
    }

    void ChangeHealth(float amount)
    {
        // check to make sure that the damge recieved isn't to great, if so completely destory the enemy
        if (amount < 50)
        {
            if (shieldEnabled)
            {
                shieldEnabled = false;
                shield.renderer.enabled = false;
				shield.gameObject.SetActive(false);
            }
            else
                health -= amount;
        }
        else
            health -= amount;
		
		HealthBar.instance.RemoveHealth(amount);
		Hit();
		
        if (health <= 0)
        {
			if(isMiniBoss)
			{
				Variables.instance.IncreaseMoney(20);
				GameManager.miniBossDead++;
			}
	
			
            // kill the enemy and add to stats
			if (shieldEnabled)
            {
                shieldEnabled = false;
                shield.renderer.enabled = false;
				shield.gameObject.SetActive(false);
            }
			
            health = 0;
            
        	v.numberOfEnemiesKilledRound += 1;
        	v.numberOfEnemiesKilledTotal += 1;
			
			
			v.numberOfEnemiesAlive -= 1;
			
            // dead, play explosion
            //GameManager.enemyDestoryList.Add(gameObject);
			
			DeathEffect();
            gameObject.SetActive(false);
            //Destroy(gameObject);
        }
    }
    void DeathEffect()
    {
		int moneyBouns = 0;
		
		if(DontDestoryValues.instance.gameType == 0)
		{
			moneyBouns = (1 + DontDestoryValues.instance.planetNumber) * Variables.instance.comboBouns;
		}
		else if(DontDestoryValues.instance.gameType == 1)
		{
			moneyBouns = (1 + DontDestoryValues.instance.planetNumber);
		}
		
		
		Variables.instance.IncreaseMoney(moneyBouns);
		Variables.instance.AddExperience(25 + (DontDestoryValues.instance.planetNumber * 20));
		
		//Debug.Log(moneyBouns);
		GameManager.ActivateComboText("$" + moneyBouns);
			
		if(Variables.instance.comboBouns > 1)
			GameManager.ActivateComboTDropText("COMBO X" + Variables.instance.comboBouns);
		
		if(DontDestoryValues.instance.gameType == 0)
		{
			v.comboBouns++;
			v.comboTimer = 0.0f;
			v.startCombo = true;
		}
		
        //Instantiate(explosion, t.position, Quaternion.identity);
        //ExplosionManager.ActivateOrangeExplosion(transform.position);
        // drop a crytal of the evacuation game type was selected

        BlowUpEffect(blackHole.transform.position);

        if (DontDestoryValues.instance.gameType == 1)
        {
            int random = Random.Range(0, 2);

            if (random == 0)
            {
                //Instantiate(v.plasmaCrystal, new Vector3(t.position.x, t.position.y, t.position.z - 1.5f), Quaternion.identity);
                if(Variables.instance.playerPlasmaCrystals < 5)
					GameManager.ActivatePlasmaCrystal(new Vector3(t.position.x, t.position.y, t.position.z));
            }
        }
    }

    void Update()
    {
        if(v.gameState == Variables.GameState.GameOver)
        {
            gameObject.SetActive(false);
        }
    }

    void BlowUpEffect(Vector3 position)
    {
        
			int randomExplosion = Random.Range(0, 8);
			
			switch(randomExplosion)
			{
				case 0:
				explosionManager.ActivateOrangeExplosion(position);
				break;
				case 1:
				explosionManager.ActivateRedExplosion(position);
				break;
				case 2:
				explosionManager.ActivateBlueExplosion(position);
				break;
				case 3:
				explosionManager.ActivateExplosion1(position);
				break;
				case 4:
				explosionManager.ActivateExplosion2(position);
				break;
				case 5:
				explosionManager.ActivateExplosion5(position);
				break;
				case 6:
				explosionManager.ActivateExplosion7(position);
				break;
				case 7:
				explosionManager.ActivateExplosion9(position);
				break;
				
				default:
				explosionManager.ActivateExplosion1(position);
				break;
			}
    }
	
	void Hit()
	{
		StartCoroutine(_Hit());
	}
	
	IEnumerator _Hit()
	{
		arms.renderer.materials[0].color = new Color(1, 0, 0, 1);
		arms.renderer.materials[1].color = new Color(1, 0, 0, 1);
		
		for(int i = 0; i < 10; i++)
		{
			arms.renderer.materials[0].color = new Color(1, 0.1f * i, 0.1f * i, 1);
			arms.renderer.materials[1].color = new Color(1, 0.1f * i, 0.1f * i, 1);
			yield return new WaitForSeconds(0.01f);
		}
		
		arms.renderer.materials[0].color = new Color(1, 1, 1, 1);
		arms.renderer.materials[1].color = new Color(1, 1, 1, 1);
	}
}
