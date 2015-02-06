using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public Variables.EnemyType  enemyType;      // enemy type
    public float                health = 3.0f;  // max enemy health
    public float                speed = 3.0f;   // enemy speed
	public int                 money = 6;    // how much enemy is worth
    public bool                 crashType;      // is the enemy going to crash into the planet
    public GameObject           shield;         // shield object
    public float                shrinkAmount;   // this is used for special enemies to come in closer to the planet and smash into it
    public float                shrinkTimer;    // how long before it moves into the planet
	public float 				rotationAmount;	// how fast to spin the enemy around
	public bool 				rocket;			// is this a rocket
    private Variables   v;
    private GameObject  target;                 // player
    private Transform   t;
    private float       specialTimer;           // fire rate for attack
    private float       speedPlanetBoost;       // depending on the planet, make speed faster
    private bool        shieldEnabled;          // is the shield enabled?
    private float       shrinkOrginalAmount;
    public bool         isRight;                // determines if the enemy will float right or left
    public bool         isShockwave;            // usees the shockwave explosion
    public bool         isOrangeExplosion = true;      // uses the orange explosion
    public bool         isRedExplosion;
    public bool         isBlueExplosion;
	public bool 		isMiniBoss;
	public bool 		isMortBullet;
	
    private float orgHealth;
    private bool planetHit;
    private bool rerunRocketScript;
	private float spinRate = 0.0f;
	private Vector3 orginalSize;
	//private bool wasSeeker;
	private bool wasTutorial;
	private float bombTimer = 0.0f;
	private bool initSpawn;
	private float orgSpeed = 0.0f;
	private bool isVisable;
	private bool bombHit;

	private Vector3 delta;
	private float moveSpeed;
	private Renderer rend;
    private static ExplosionManager explosionManager;
    
    void Awake()
    {
        v = GameObject.FindGameObjectWithTag("Variables").GetComponent<Variables>();
        explosionManager = GameObject.FindGameObjectWithTag("Variables").GetComponent<ExplosionManager>();

		orginalSize = transform.localScale;
		orgSpeed = speed;
        t = transform;
		initSpawn = true;
		rend = renderer;
        orgHealth = health;
    }
	
	void Start()
	{
		if(isMiniBoss)
		{
			HealthBar.instance.AddToCurrentHealth(health);
			explosionManager.ActivateMortWarpIn(new Vector3(t.position.x, t.position.y, t.position.z - 5));
		}
	}
	
	void OnBecameVisible ()
	{
		isVisable = true;
	}
	
	void OnBecameInvisible()
	{
		isVisable = false;
	}
	
    void OnEnable()
    {
		//wasSeeker = false;
        planetHit = false;
		bombHit = false;
        shrinkOrginalAmount = shrinkAmount + Time.time;
        speedPlanetBoost = DontDestoryValues.instance.planetNumber;
		speed = orgSpeed;
        //Debug.Log(health);
        shield.SetActive(false);
        health = orgHealth;
		this.renderer.material.color = new Color(1, 1, 1, 1);

        //Debug.Log(health);

		if(!rocket)
		{
			if(t.localScale != orginalSize)
				t.localScale = orginalSize;
		}
		
        //if(speedPlanetBoost > 0)
            //speed = speed + (speedPlanetBoost * 0.01f);

        target = GameObject.FindGameObjectWithTag("Player");

        if(speedPlanetBoost > 0)
            health *= speedPlanetBoost * 2f;

        if (enemyType != Variables.EnemyType.MotherShip || enemyType != Variables.EnemyType.Mort || enemyType != Variables.EnemyType.Shoot)
        {
            shield.renderer.enabled = false;
        }

        if (Random.Range(0, 3) == 0)
        {
            isRight = true;
        }
		
        if (enemyType == Variables.EnemyType.Mort)
        {
			GameManager.mortAlive = true;
			isRight = true;
			
			if(isRight)
            	t.localScale = new Vector3(-1.651716f, t.localScale.y, t.localScale.z);
			else
			{
				t.localScale = new Vector3(1.651716f, t.localScale.y, t.localScale.z);
			}
        }
		else if(enemyType == Variables.EnemyType.Normal)
		{
			float newSize = Random.Range(-0.12f, 0.12f);

			t.localScale = new Vector3(orginalSize.x + newSize, orginalSize.y + newSize, orginalSize.z + newSize);
		}
		if(rocket)
		{
			if(t.position.x > 0){
				if(!isMortBullet)
				{
					t.LookAt(target.transform); 
					t.localScale = orginalSize;
				}
				else
				{
					t.LookAt(target.transform); 
					t.localScale = new Vector3(orginalSize.x, orginalSize.y, -1.11948f);
				}
			}
			else
			{
				t.LookAt(target.transform); 
				t.localScale = new Vector3(t.localScale.x, orginalSize.y * -1f, t.localScale.z);
			}
		}
		
		/*if(isShockwave)
		{
			if(DontDestoryValues.instance.planetNumber == 0)
			{
				gameObject.renderer.material.color = new Color(0.8f, 0, 1.0f, 1.0f);
			}
			else if(DontDestoryValues.instance.planetNumber == 1)
			{
				gameObject.renderer.material.color = new Color(0, 1.0f, 0, 1.0f);
			}
			else if(DontDestoryValues.instance.planetNumber == 2)
			{
				gameObject.renderer.material.color = new Color(0, 0, 1.0f, 1.0f);
			}
		}*/
		
		spinRate = Random.Range(-rotationAmount, rotationAmount);
		
		if(enemyType == Variables.EnemyType.MotherShip
			|| enemyType == Variables.EnemyType.Mort
			|| enemyType == Variables.EnemyType.Shoot2x
			|| enemyType == Variables.EnemyType.Steal)
		{
			if(!initSpawn)
			{
				if(enemyType == Variables.EnemyType.Mort)
				{
					explosionManager.ActivateMortWarpIn(new Vector3(t.position.x, t.position.y, t.position.z - 5));
				}
				else
					explosionManager.ActivatePurpleGlow(new Vector3(t.position.x, t.position.y, t.position.z - 5));
			}
		}
		
		initSpawn = false;
    }
	
	void OnTriggerStay(Collider coll)
	{
		if(isMiniBoss)
		{
			if(coll.tag == "Bomb")
			{
				bombTimer += Time.deltaTime;
				
				if(bombTimer > 0.5f)
				{
					bombTimer = 0.0f;
					ChangeHealth(10);
				}
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
                v.numberOfMinesAlive--;
				ChangeHealth (10);
				BulletHitEffect ();
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

                // this will effect the enemy if something changes on impact
                BulletHitEffect();
                break;

			case "Planet":
				//money = 0;
				planetHit = true;
				v.hitByEnemy = true;
                //v.numberOfEnemiesKilledRound -= 1;
                //v.numberOfEnemiesKilledTotal -= 1;
				ChangeHealth(1000);
				
				Player.instance.RemovePlanetHealth(650 - (v.playerDurLevel * 85));
				break;

            case "Shield":
                //money = 0;
                planetHit = true;
                //v.numberOfEnemiesKilledRound -= 1;
                //v.numberOfEnemiesKilledTotal -= 1;
                ChangeHealth(1000);
                v.RemovePlayerShield(1);
                break;

            case "EscapePod":
                //money = 0;
				planetHit = true;
                v.numberOfEnemiesKilledRound -= 1;
                v.numberOfEnemiesKilledTotal -= 1;
				coll.gameObject.SetActive(false);
                ChangeHealth(100);
                break;

            case "Laser":
				if(isMiniBoss)
                	ChangeHealth(v.playerBulletDamage * 0.25f);
				else
					ChangeHealth(v.playerBulletDamage);
                v.numberOfHitsRound += 1;
                v.numberOfHitsTotal += 1;
			
				
                // this will effect the enemy if something changes on impact
                BulletHitEffect();
			
			
                break;

            case "BulletFlak":
                if(isMiniBoss)
                	ChangeHealth(v.playerBulletDamage * 0.15f);
				else
					ChangeHealth(v.playerBulletDamage);
                v.numberOfHitsRound += 1;
                v.numberOfHitsTotal += 1;

                // this will effect the enemy if something changes on impact
                BulletHitEffect();
                break;

            case "Seeker":
				//wasSeeker = true;
                ChangeHealth(v.playerBulletDamage);
                v.numberOfHitsRound += 1;
                v.numberOfHitsTotal += 1;
			
				coll.gameObject.SetActive(false);
                break;

            case "Bomb":
				bombHit = true;
				if(isMiniBoss)
				{
					ChangeHealth(1);
				}
				else{
					ChangeHealth(1000);
				}
                
                v.numberOfHitsRound += 1;
                v.numberOfHitsTotal += 1;
                break;

            case "FlakShell":
                ChangeHealth(v.playerBulletDamage);
                v.numberOfHitsRound += 1;
                v.numberOfHitsTotal += 1;
                break;
        }
    }

    void ChangeHealth(float amount)
    {
		if(isMiniBoss)
		{
			HealthBar.instance.RemoveHealth(amount);
		}
		
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
		
		Hit();
		
        if (health <= 0)
        {
			if(isMiniBoss)
			{
				GameManager.miniBossDead++;
			}
			
			if(v.gameState == Variables.GameState.Tutorial)
			{
				gameObject.collider.enabled = false;
				rend.enabled = false;
				wasTutorial = true;
			}
			
            // kill the enemy and add to stats
			if (shieldEnabled)
            {
                shieldEnabled = false;
                shield.renderer.enabled = false;
				shield.gameObject.SetActive(false);
            }
			
            health = 0;
            
			if(!planetHit)
			{
            	v.numberOfEnemiesKilledRound += 1;
            	v.numberOfEnemiesKilledTotal += 1;
			}
			
			
			v.numberOfEnemiesAlive -= 1;
			
            // dead, play explosion
            rerunRocketScript = false;
            //GameManager.enemyDestoryList.Add(gameObject);
			
			DeathEffect();
            gameObject.SetActive(false);
            //Destroy(gameObject);
        }
    }

    void EnemyMovement()
    {
        // check to see if we need to orbit the planet or just kamakize into it
        if (crashType)
        {
            if (rocket && !rerunRocketScript)
            {
                rerunRocketScript = true;
				
                if (t.position.x > 0){
					if(!isMortBullet){
                    	t.LookAt(target.transform);
						t.localScale = orginalSize;
					}
					else
					{
						t.LookAt(target.transform); 
						t.localScale = new Vector3(orginalSize.x, orginalSize.y, -1.11948f);
					}
				}
                else
                {
					if(!isMortBullet){
                    //t.localScale = new Vector3(t.localScale.x, t.localScale.y, t.localScale.z * -1.0f);
                    t.LookAt(target.transform);
					t.localScale = new Vector3(t.localScale.x, orginalSize.y * -1f, t.localScale.z);
					}
					else
					{
						t.LookAt(target.transform); 
						t.localScale = new Vector3(orginalSize.x, orginalSize.y * -1f, 1.11948f);
					}
                }
            }
			else{
				if(!rocket)
					t.Rotate(new Vector3(0, 0, spinRate));
			}

            delta = target.transform.position - t.position;
            delta.Normalize();

            moveSpeed = (speed + (0 * 0.25f))  * Time.deltaTime;

            // if slow is SetActive, cut speed in half
            if (v.itemSlowEnemies)
            {
                t.position = t.position + (delta * moveSpeed * 0.5f);
            }
            else
            {
                t.position = t.position + (delta * moveSpeed) * Mathf.Sin(3);
            }
        }
        else
        {
            if (shrinkTimer > 0)
            {
				//t.Rotate(new Vector3(0f, 0f, rotationAmount));
				//t.Rotate(new Vector3(0, 0, spinRate));
				
                if (Time.time > shrinkOrginalAmount)
                {
                    delta = target.transform.position - t.position;
                    delta.Normalize();

                    moveSpeed = shrinkAmount;

                    // if slow is SetActive, cut speed in half
                    if (v.itemSlowEnemies)
                    {
                        t.position = t.position + (delta * moveSpeed * 0.5f);
                    }
                    else
                    {
                        t.position = t.position + (delta * moveSpeed);
                    }

                    shrinkOrginalAmount += shrinkTimer;
                }
            }
			
			if(enemyType != Variables.EnemyType.MotherShip)
			{
	            if (isRight)
	            {
	                if (v.itemSlowEnemies)
	                {
	                    t.RotateAround(new Vector3(0, -5, 0), Vector3.forward, (0.5f * speed) * Time.deltaTime);
	                }
	                else
	                    t.RotateAround(new Vector3(0, -5, 0), Vector3.forward, speed * Time.deltaTime);
	            }
	            else
	            {
	                if (v.itemSlowEnemies)
	                {
	                    t.RotateAround(new Vector3(0, -5, 0), -Vector3.forward, (0.5f * speed) * Time.deltaTime);
	                }
	                else
	                    t.RotateAround(new Vector3(0, -5, 0), -Vector3.forward, speed * Time.deltaTime);
	            }
			}
			else
			{
				if(isMiniBoss)
				{
					t.RotateAround(new Vector3(0, -5, 0), Vector3.forward, (0.5f * speed) * Time.deltaTime);
				}
			}
        }
    }

    void BulletHitEffect()
    {
        // grow or shrink the blob when hit
        switch (enemyType)
        {
            case Variables.EnemyType.Grow:
				if(t.localScale.x < 2f)
                	t.localScale *= 1.1f;
				else 	
					t.localScale = new Vector3(2f, 2f, 0);
                break;
			
            case Variables.EnemyType.Shrink:
				if(t.localScale.x > 0.5f)
                	t.localScale *= 0.8f;
				else 
					t.localScale = new Vector3(0.5f, 0.5f, 0);
                break;
        }
    }

    void DeathEffect()
    {
        // give the player money and spawn an explosion
		// each planet gives more money becasue of harder difficulty
		if(!planetHit)
		{
			int moneyBouns = 0;
			
			if(DontDestoryValues.instance.gameType == 0)
			{
				if(!bombHit)
				{
					moneyBouns = (1 + money) * (1 + DontDestoryValues.instance.planetNumber) * v.comboBouns;
					
					v.IncreaseMoney(moneyBouns);	
					//Debug.Log(moneyBouns);
					GameManager.ActivateComboText("$" + moneyBouns);
						
					if(v.comboBouns > 1)
						GameManager.ActivateComboTDropText("COMBO X" + v.comboBouns);
					
					if(DontDestoryValues.instance.gameType == 0)
					{
						v.comboBouns++;
						v.comboTimer = 0.0f;
						v.startCombo = true;
					}
				}
			}
			else if(DontDestoryValues.instance.gameType == 1)
			{
				moneyBouns = 0;
			}
			
			if(enemyType == Variables.EnemyType.Mort)
				GameManager.mortAlive = false;
			v.AddExperience((25 + (DontDestoryValues.instance.planetNumber * 20)) * (int)Modifiers.expMod);
			
		}
		
        //Instantiate(explosion, t.position, Quaternion.identity);
        //explosionManager.ActivateOrangeExplosion(transform.position);
        // drop a crytal of the evacuation game type was selected
		
		if(isVisable || v.gameState == Variables.GameState.Tutorial)
       	 	BlowUpEffect(t.position);

        if (DontDestoryValues.instance.gameType == 1)
        {
            if (!planetHit)
            {
                int random = Random.Range(0, 2);

                if (random == 0)
                {
                    //Instantiate(v.plasmaCrystal, new Vector3(t.position.x, t.position.y, t.position.z - 1.5f), Quaternion.identity);
                    if(v.playerPlasmaCrystals < 5)
						GameManager.ActivatePlasmaCrystal(new Vector3(t.position.x, t.position.y, t.position.z));
                }
            }
        }

        // spawn 2 smaller blobs if that enemy type
        if (enemyType == Variables.EnemyType.Split)
        {
			if(!planetHit)
			{
				//v.numberOfEnemiesSpawned += 2;
				v.numberOfEnemiesAlive += 2;
				v.numberOfEnemiesSpawned += 2;
            	GameManager.ActivateBlob(new Vector3(t.transform.position.x - 1.5f, t.transform.position.y - 1.5f, t.transform.position.z));
            	GameManager.ActivateBlob(new Vector3(t.transform.position.x + 1.5f, t.transform.position.y + 1.5f, t.transform.position.z));
			}
            //GameObject[] clones = new GameObject[2];

            //clones[0] = Instantiate(v.enemyBlobSplitClones[DontDestoryValues.instance.planetNumber], new Vector3(t.transform.position.x - 0.5f, t.transform.position.y, t.transform.position.z), Quaternion.identity) as GameObject;
            //clones[1] = Instantiate(v.enemyBlobSplitClones[DontDestoryValues.instance.planetNumber], new Vector3(t.transform.position.x + 0.5f, t.transform.position.y, t.transform.position.z), Quaternion.identity) as GameObject;
        }
    }

    void Update()
    {
        if (v.gameState == Variables.GameState.Game)
        {
            EnemyMovement();

            if (!crashType)
                SpecialEnemyMovement();
        }
        else if(v.gameState == Variables.GameState.GameOver)
        {
            gameObject.SetActive(false);
        }
		else if(v.gameState == Variables.GameState.Tutorial)
		{
			if(wasTutorial)
			{
				wasTutorial = false;
				StartCoroutine(Tutorial.instance.UpdateIndex(9));
			}
		}
    }

    void SpecialEnemyMovement()
    {
        // shoot, steal, or spawn to kill the player
        switch (enemyType)
        {
		 case Variables.EnemyType.PlanetTwoBall:
            specialTimer += Time.deltaTime;

            if (specialTimer > v.motherShipSpawnTime)
            {
                GameManager.ActivateCentipedeEnemy(t.transform.position);
                specialTimer = 0;
            }

            break;
			
            case Variables.EnemyType.Mort:
                specialTimer += Time.deltaTime;

                if (specialTimer > v.mrMortAttackTime)
                {
                    //Instantiate(v.enemyMortBullet, new Vector3(t.transform.position.x, t.transform.position.y, t.transform.position.z + 1.0f), Quaternion.identity);
                    GameManager.ActivateMortBullet(new Vector3(t.transform.position.x, t.transform.position.y, t.transform.position.z + 1.0f));
                    //Debug.Log("Fire");
                    specialTimer = 0;
                }

                break;
            case Variables.EnemyType.MotherShip:
                specialTimer += Time.deltaTime;

                if (specialTimer > v.motherShipSpawnTime + 4)
                {
					if(isMiniBoss)
					{
						GameManager.ActivateMiniBossSpawn(new Vector3(t.transform.position.x, t.transform.position.y, 0));
					}
                    //Instantiate(v.enemyFleetShip, t.transform.position, Quaternion.identity);
                    GameManager.ActivateMotherShipSpawns(new Vector3(t.transform.position.x, t.transform.position.y, 0), t.transform.rotation);
                    specialTimer = 0;
				
					GameManager.ActivateMotherShipSpawn();
                }

                break;
            case Variables.EnemyType.Shoot:
                specialTimer += Time.deltaTime;

                if (specialTimer > v.fleetShootAttackTime + 1)
                {
                    //Instantiate(v.enemyPlayerSeekerBullet, t.transform.position, Quaternion.identity);
                    GameManager.ActivatePlayerSeekerBullet(t.position);
					//GameManager.ActivateAudioFleetShot();
                    specialTimer = 0;
                }
				

                break;
			case Variables.EnemyType.Shoot2x:
                specialTimer += Time.deltaTime;

                if (specialTimer > v.fleetShootAttackTime)
                {
                    //Instantiate(v.enemyPlayerSeekerBullet, t.transform.position, Quaternion.identity);
                    GameManager.ActivatePlayerSeekerBullet2x(t.position);
					//GameManager.ActivateAudioFleetShot();
                    specialTimer = 0;
                }

                break;
		case Variables.EnemyType.Steal:
			specialTimer += Time.deltaTime;

            if (specialTimer > v.fleetShootAttackTime)
            {
                v.IncreaseMoney(-((DontDestoryValues.instance.planetNumber + 1) * 2));
				GameManager.ActivateComboTDropText("Money Stolen -$" + ((DontDestoryValues.instance.planetNumber + 1) * 2));
				GameManager.ActivateHarvesterAudio();
                specialTimer = 0;
            }
			break;
        }
    }

    void BlowUpEffect(Vector3 position)
    {
        if (isShockwave)
        {
			explosionManager.ActivatePurpleGlow(position);
			
			/*if(DontDestoryValues.instance.planetNumber == 0)
			{
				explosionManager.ActivatePurpleGlow(position);
			}
			else if(DontDestoryValues.instance.planetNumber == 1)
			{
				explosionManager.ActivateGreenGlow(position);
			}
			else if(DontDestoryValues.instance.planetNumber == 2)
			{
				explosionManager.ActivateShockwaveExplosion(position);
			}*/
        }
		else
		{
			int randomExplosion = Random.Range(0, 8);
			
			if(planetHit)
				randomExplosion = 0;
			
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
    }
	
	void Hit()
	{
		if(isVisable)
			StartCoroutine(_Hit());
	}
	
	IEnumerator _Hit()
	{
		rend.material.color = new Color(1, 0, 0, 1);
		//speed = 0;
		for(int i = 0; i < 10; i++)
		{
			rend.material.color = new Color(1, 0.1f * i, 0.1f * i, 1);
			yield return new WaitForSeconds(0.01f);
		}
		speed = orgSpeed;
		rend.material.color = new Color(1, 1, 1, 1);
	}
}
