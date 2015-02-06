using UnityEngine;
using System.Collections;

public class CentipedeOrb : MonoBehaviour 
{
	private float health = 70.0f;
	public GameObject orbWall;
    public GameObject hitOrbTexture;
    private int hitLoop = 0;
    private bool isHit;
	private Variables v;
	private float bombTimer = 0.0f;
    private ExplosionManager explosionManager;

	void Start()
	{
		v = Variables.instance;
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
				//coll.gameObject.SetActive(false);
                break;

            case "BulletFlak":
                RemoveHealth(v.playerBulletDamage * 0.25f);
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
                RemoveHealth(v.playerBulletDamage * 0.25f);
                v.numberOfHitsRound += 1;
                v.numberOfHitsTotal += 1;
                break;
        }
    }
	
	void RemoveHealth(float amount)
    {
        health -= amount;
        hitLoop = 0;
        isHit = true;
		if(health <= 0)
		{
			Explode();
			orbWall.gameObject.SetActive(false);
			if(GameManager.boss2Dead || GameManager.miniBossDead > 1)
			{
				transform.root.gameObject.SetActive(false);
			}
			gameObject.SetActive(false);
		}
	}
    
	void Update()
    {
		
		if(GameManager.boss2Dead || GameManager.miniBossDead > 1)
		{
			RemoveHealth(1000);
		}
		else
		{
	        if (isHit)
	        {
	            StartCoroutine(Hit());
	        }
		}
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
        explosionManager.ActivateRandomExplosion(transform.position);
        /*
		switch(randomExplosion)
		{
			case 0:
			ExplosionManager.ActivateOrangeExplosion(transform.position);
			break;
			case 1:
			ExplosionManager.ActivateRedExplosion(transform.position);
			break;
			case 2:
			ExplosionManager.ActivateBlueExplosion(transform.position);
			break;
			case 3:
			ExplosionManager.ActivateExplosion1(transform.position);
			break;
			case 4:
			ExplosionManager.ActivateExplosion2(transform.position);
			break;
			case 5:
			ExplosionManager.ActivateExplosion5(transform.position);
			break;
			case 6:
			ExplosionManager.ActivateExplosion7(transform.position);
			break;
			case 7:
			ExplosionManager.ActivateExplosion9(transform.position);
			break;
			
			default:
			ExplosionManager.ActivateExplosion1(transform.position);
			break;
		}*/
	}
}
