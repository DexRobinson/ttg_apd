using UnityEngine;
using System.Collections;

public class Cannon : MonoBehaviour 
{
    public bool isAlive = true;
    public bool isLaser;
    public Transform spawnPoint;

	private float health = 750;
    private Variables v;
    //private Transform myTrans;
    private float fireRate = 3.0f;
    private float fireTime = 3.0f;
    private PlaySpriteOnce pso;
	
	private float bombTimer;
	private float onTimer;

	private Renderer rd;
    private ExplosionManager explosionManager;

    void Start()
    {
		rd = renderer;
        v = Variables.instance;
        explosionManager = GameObject.FindGameObjectWithTag("Variables").GetComponent<ExplosionManager>();
        //myTrans = transform;
		//v.numberOfEnemiesAlive += 1;
        if(!isLaser)
            pso = gameObject.GetComponent<PlaySpriteOnce>();
		
		HealthBar.instance.AddToCurrentHealth(health);
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
                ChangeHealth(v.playerBulletDamage * 0.6f);
                v.numberOfHitsRound += 1;
                v.numberOfHitsTotal += 1;

                break;

            case "BulletFlak":
                ChangeHealth(v.playerBulletDamage * 0.4f);
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
                ChangeHealth(15);
                v.numberOfHitsRound += 1;
                v.numberOfHitsTotal += 1;
                break;

            case "FlakShell":
                ChangeHealth(v.playerBulletDamage * 0.4f);
                v.numberOfHitsRound += 1;
                v.numberOfHitsTotal += 1;
                break;
        }
    }

    void ChangeHealth(float amount)
    {
        health -= amount;
		HealthBar.instance.RemoveHealth(amount);
		Hit();
		
        if (health <= 0)
        {
            // kill the enemy and add to stats
			GameManager.ActivateComboText("$30");
            health = 0;
            isAlive = false;
			v.IncreaseMoney(30);
            v.numberOfEnemiesKilledRound += 1;
            v.numberOfEnemiesKilledTotal += 1;
            //v.numberOfEnemiesAlive -= 1;
			explosionManager.ActivateOrangeExplosion(transform.position);
            gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (fireTime > fireRate)
        {
            if (!isLaser)
            {
                if (!pso.PlayAnimaion)
                    pso.PlayFireAnimation();


                if (pso.ReadyToFire)
                    Fire();
            }
            else
            {
                Fire();
            }
        }
        else
        {
            fireTime += Time.deltaTime;
        }
    }

    void Fire()
    {
        if (!isLaser)
        {
            GameManager.ActivateMortMissiles(spawnPoint.position);
            //pso.ReadyToFire = false;
            //pso.PlayAnimaion = false;
            fireTime = 0.0f;
        }
        else if (isLaser)
        {
            Player.instance.AdjustPlanetHealth(-400);
            fireTime = 0.0f;
        }
    }
	
	void Hit()
	{
		StartCoroutine(_Hit());
	}
	
	IEnumerator _Hit()
	{
		rd.material.color = new Color(1, 0, 0, 1);
		
		for(int i = 0; i < 10; i++)
		{
			rd.material.color = new Color(1, 0.1f * i, 0.1f * i, 1);
			yield return new WaitForSeconds(0.02f);
		}
		
		rd.material.color = new Color(1, 1, 1, 1);
	}
}
