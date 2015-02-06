using UnityEngine;
using System.Collections;

public class CentipedeWall : MonoBehaviour 
{
	private float health = 15.0f;
	public GameObject wallObject;
	
	private bool isDead;
	private Variables v;
	private float deadTimer = 0.0f;
    private int hitLoop = 0;
    private bool isHit;
	private float bombTimer = 0.0f;
    private ExplosionManager explosionManager;

	void Start()
	{
		v = Variables.instance;
        explosionManager = GameObject.FindGameObjectWithTag("Variables").GetComponent<ExplosionManager>();
	}

	void Update()
	{
		//if(GameManager.boss2Dead)
			//RemoveHealth(1000);
		
		if(isDead)
		{
			deadTimer += Time.deltaTime;
			
			if(deadTimer > 6.0f)
			{
				health = 15.0f;
                wallObject.renderer.material.color = new Color(1, 1, 1);
				//gameObject.renderer.enabled = true;
				gameObject.collider.enabled = true;
				wallObject.gameObject.SetActive(true);
				isDead = false;
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
				RemoveHealth(1);
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
                RemoveHealth(v.playerBulletDamage * 0.6f);
                v.numberOfHitsRound += 1;
                v.numberOfHitsTotal += 1;
				//coll.gameObject.SetActiveRecursively(false);
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
                RemoveHealth(1);
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
        wallObject.renderer.material.color = new Color(1, hitLoop, hitLoop);
        isHit = true;

		if(health <= 0)
		{
            isHit = false;
			isDead = true;
			deadTimer = 0.0f;
			Explode();
			//gameObject.renderer.enabled = false;
			gameObject.collider.enabled = false;
			wallObject.gameObject.SetActive(false);
		}
	}

    IEnumerator Hit()
    {
        isHit = false;
        wallObject.renderer.material.color = new Color(1, (hitLoop * 0.1f), (hitLoop * 0.1f));
        yield return new WaitForSeconds(0.1f);
        hitLoop++;

        if (hitLoop >= 10)
        {
            isHit = false;
            wallObject.renderer.material.color = new Color(1, 1, 1);
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
