using UnityEngine;
using System.Collections;

public class CentipedeSpawner : MonoBehaviour 
{
	public float health = 350.0f;
	
	private Variables v;
	private float attackTimer = 0.0f;
	private float attackTime = 5.5f;
    public GameObject hitOrbTexture;
    private int hitLoop = 0;
    private bool isHit;
    private Transform t;
	public bool isMiniBoss;
	private float bombTimer = 0.0f;
    private ExplosionManager explosionManager;

	void Start()
	{
        t = transform;
		v = Variables.instance;
        explosionManager = GameObject.FindGameObjectWithTag("Variables").GetComponent<ExplosionManager>();
		if(isMiniBoss)
			health = 450;
		
		HealthBar.instance.AddToCurrentHealth(health);
	}

	void Update()
	{
		if(GameManager.boss2Dead)
			health = 0;
		
		if(health > 0)
		{
			attackTimer += Time.deltaTime;
            t.Rotate(0, 0, 0.5f);

			if(attackTimer > attackTime)
			{
				Attack();
			}
		}

        if (isHit)
        {
            StartCoroutine(Hit());
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
				RemoveHealth(10);
			}
		}
	}
	
	void OnTriggerEnter(Collider coll)
    {
        switch (coll.tag)
        {
            case "Bullet":
                RemoveHealth(v.playerBulletDamage);
                v.numberOfHitsRound += 1;
                v.numberOfHitsTotal += 1;
                coll.gameObject.SetActive(false);
                break;
            case "Laser":
                RemoveHealth(v.playerBulletDamage * 0.5f);
                v.numberOfHitsRound += 1;
                v.numberOfHitsTotal += 1;
				coll.gameObject.SetActive(false);
                break;

            case "BulletFlak":
                RemoveHealth(v.playerBulletDamage * 0.2f);
                v.numberOfHitsRound += 1;
                v.numberOfHitsTotal += 1;
                break;

            case "Seeker":
                RemoveHealth(v.playerBulletDamage);
                v.numberOfHitsRound += 1;
                v.numberOfHitsTotal += 1;
                coll.gameObject.SetActive(false);
                break;

            case "Bomb":
                RemoveHealth(15);
                v.numberOfHitsRound += 1;
                v.numberOfHitsTotal += 1;
                break;

            case "FlakShell":
                RemoveHealth(v.playerBulletDamage * 0.2f);
                v.numberOfHitsRound += 1;
                v.numberOfHitsTotal += 1;
                break;
        }
    }
	void RemoveHealth(float amount)
    {
        health -= amount;
		HealthBar.instance.RemoveHealth(amount);
        hitLoop = 0;
        isHit = true;

		if(health <= 0)
		{
			Explode();
			Variables.instance.IncreaseMoney(20);
			if(isMiniBoss)
			{
				GameManager.miniBossDead++;
			}
			else
			{
				GameManager.numberOfSpawnersLeft--;
			}
			
			gameObject.SetActive(false);
		}
	}
	
	void Attack()
	{
		attackTimer = 0.0f;
		attackTime = Random.Range(3.0f, 7.0f);
		explosionManager.ActivateShockwaveExplosion(transform.position);
		GameManager.ActivateCentipedeEnemy(transform.position);
	}

    IEnumerator Hit()
    {
        isHit = false;
        hitOrbTexture.renderer.enabled = true;
        hitOrbTexture.renderer.material.color = new Color(1, 1, 1, 1.0f - (hitLoop * 0.1f));
        yield return new WaitForSeconds(0.1f);
        hitLoop++;

        if (hitLoop >= 10)
        {
            isHit = false;
            hitOrbTexture.renderer.enabled = false;
        }
        else
        {
            isHit = true;
        }
    }
	
	void Explode()
	{
		int randomExplosion = Random.Range(0, 8);
			
		switch(randomExplosion)
		{
			case 0:
			explosionManager.ActivateOrangeExplosion(transform.position);
			break;
			case 1:
            explosionManager.ActivateRedExplosion(transform.position);
			break;
			case 2:
            explosionManager.ActivateBlueExplosion(transform.position);
			break;
			case 3:
            explosionManager.ActivateExplosion1(transform.position);
			break;
			case 4:
            explosionManager.ActivateExplosion2(transform.position);
			break;
			case 5:
            explosionManager.ActivateExplosion5(transform.position);
			break;
			case 6:
            explosionManager.ActivateExplosion7(transform.position);
			break;
			case 7:
            explosionManager.ActivateExplosion9(transform.position);
			break;
			
			default:
            explosionManager.ActivateExplosion1(transform.position);
			break;
		}
	}
}
