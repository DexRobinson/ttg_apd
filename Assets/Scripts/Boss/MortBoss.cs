using UnityEngine;
using System.Collections;

public class MortBoss : MonoBehaviour 
{
    public Cannon leftCannon;
    public Cannon rightCannon;
    public GameObject laser;

    private float health = 900;
    //private float speed = 15;
    private Variables v;
    private Transform myTrans;
    //private float fireRate = 0.0f;
    //private float fireTime = 0.0f;
	
	private float bombTimer = 0.0f;
	private float rotSpeed = 0.0f;
	private Renderer rend;
	private GameObject go;
    private ExplosionManager explosionManager;

    void Start()
    {
        v = Variables.instance;
		go = gameObject;
        myTrans = transform;
		v.numberOfEnemiesAlive += 1;
        laser.SetActive(false);
		GameManager.boss1Alive = true;
		GameManager.boss1Spawned = true;
		rend = renderer;
		HealthBar.instance.AddToCurrentHealth(health);
        explosionManager = GameObject.FindGameObjectWithTag("Variables").GetComponent<ExplosionManager>();
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
	
    void OnTriggerEnter(Collider coll)
    {
        switch (coll.tag)
        {
            case "Mines":
                Variables.instance.numberOfMinesAlive--;
                ChangeHealth(5);
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


            case "Laser":
                ChangeHealth(v.playerBulletDamage);
                v.numberOfHitsRound += 1;
                v.numberOfHitsTotal += 1;

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
				coll.gameObject.SetActive(false);
                break;

            case "Bomb":
                ChangeHealth(10);
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
		if (!leftCannon.isAlive && !rightCannon.isAlive)
		{
			Hit();
        	health -= amount;
			HealthBar.instance.RemoveHealth(amount);
		}

        if (health <= 0)
        {
            // kill the enemy and add to stats
			Variables.instance.IncreaseMoney(20);
            health = 0;
            v.numberOfEnemiesKilledRound += 1;
            v.numberOfEnemiesKilledTotal += 1;
            v.numberOfEnemiesAlive -= 1;
			GameManager.boss1Alive = false;
			v.IncreaseMoney(200);
			explosionManager.ActivateRedExplosion(transform.position);
            //GameManager.enemyDestoryList.Add(gameObject);
            go.SetActive(false);
            //Destroy(gameObject);
        }
    }

    void Update()
    {
		if(!animation.isPlaying)
		{
			if(rotSpeed <= 15.0f)
			{
				rotSpeed += Time.deltaTime * 2f;
			}
			
        	myTrans.RotateAround(new Vector3(0, -5, 0), Vector3.forward, rotSpeed * Time.deltaTime);
		}

        if (!leftCannon.isAlive && !rightCannon.isAlive)
        {
			if(Variables.instance.playerCurrentHealth > 0)
			{
	            laser.SetActive(true);
				go.tag = "Enemy";
			}
			else
			{
				laser.SetActive(false);
			}
        }

        if (laser.activeSelf)
        {
            Player.instance.AdjustPlanetHealth(-3f);
        }
		
		if(Variables.instance.playerCurrentHealth <= 0)
			laser.SetActive(false);
    }
	
	void Hit()
	{
		StartCoroutine(_Hit());
	}
	
	IEnumerator _Hit()
	{
		rend.material.color = new Color(1, 0, 0, 1);
		
		for(int i = 0; i < 10; i++)
		{
			rend.material.color = new Color(1, 0.1f * i, 0.1f * i, 1);
			yield return new WaitForSeconds(0.05f);
		}
		
		rend.material.color = new Color(1, 1, 1, 1);
	}
}
