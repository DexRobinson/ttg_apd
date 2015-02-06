using UnityEngine;
using System.Collections;

public class GorgArms : MonoBehaviour 
{
	public GorgHead gorgHead;
    public Transform spawnPoint;
	public GameObject arm;
	
    private float health = 450.0f;
    private Variables v;
	private float bombTimer = 0.0f;
    private ExplosionManager explosionManager;

    void Start()
    {
        v = Variables.instance;
        v.numberOfEnemiesAlive++;
        explosionManager = GameObject.FindGameObjectWithTag("Variables").GetComponent<ExplosionManager>();
		HealthBar.instance.AddToCurrentHealth(health);
    }

	// Use this for initialization
	void OnEnable() 
	{
		GorgIdle();
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
                RemoveHealth(v.playerBulletDamage / 2);
                v.numberOfHitsRound += 1;
                v.numberOfHitsTotal += 1;

                break;

            case "BulletFlak":
                RemoveHealth(v.playerBulletDamage / 4);
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
                RemoveHealth(20);
                v.numberOfHitsRound += 1;
                v.numberOfHitsTotal += 1;
                break;

            case "FlakShell":
                RemoveHealth(v.playerBulletDamage / 4);
                v.numberOfHitsRound += 1;
                v.numberOfHitsTotal += 1;
                break;
        }
    }
	
	void Attack()
	{
		StartCoroutine(GorgAttack());
	}
	
	public IEnumerator GorgAttack()
	{
        float spawnTimer = 24f / 39f;
		float timeOfAnimation = gameObject.animation["attack"].length - spawnTimer;

		gameObject.animation.Play("attack");
		yield return new WaitForSeconds(timeOfAnimation);
        GameManager.ActivateGorgMinion(spawnPoint.position);
        yield return new WaitForSeconds(spawnTimer);
		GorgIdle();
	}

	public void GorgIdle()
	{
		gameObject.animation.Play("idle");
	}

    void RemoveHealth(float amount)
    {
        health -= amount;
		HealthBar.instance.RemoveHealth(amount);
		Hit();
		
        if (health < 1)
        {
			Variables.instance.IncreaseMoney(20);
            explosionManager.ActivateOrangeExplosion(spawnPoint.position);
            gorgHead.numberOfArms--;
            v.numberOfEnemiesAlive--;
            gameObject.SetActive(false);
        }
    }
	
	void Hit()
	{
		StartCoroutine(_Hit());
	}
	
	IEnumerator _Hit()
	{
		arm.renderer.materials[0].color = new Color(1, 0, 0, 1);
		arm.renderer.materials[1].color = new Color(1, 0, 0, 1);
		
		for(int i = 0; i < 10; i++)
		{
			arm.renderer.materials[0].color = new Color(1, 0.1f * i, 0.1f * i, 1);
			arm.renderer.materials[1].color = new Color(1, 0.1f * i, 0.1f * i, 1);
			yield return new WaitForSeconds(0.01f);
		}
		
		arm.renderer.materials[0].color = new Color(1, 1, 1, 1);
		arm.renderer.materials[1].color = new Color(1, 1, 1, 1);
	}
}

