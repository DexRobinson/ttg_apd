using UnityEngine;
using System.Collections;

public class GorgHead : MonoBehaviour
{
    public int numberOfArms = 4;
    public float health = 1000;
    public GameObject beam;
    public GameObject shield;
    public GorgArms[] arms;
	public GameObject gorgHitObject;
	
    private Variables v;
    private float stealTimer = 0.0f;
    private float damage = 0.0f;
    private float attackTimer = 0.0f;
    private float attackingTime = 2.5f;
    private Transform t;
	private int hitLoop = 0;
	private bool isHit;
	private float bombTimer = 0.0f;
	private float rotSpeed = 0.0f;
    private ExplosionManager explosionManager;

    void Start()
    {
        damage = 300;
        beam.renderer.enabled = false;
        v = Variables.instance;
        v.numberOfEnemiesAlive++;
        t = transform;
		HealthBar.instance.AddToCurrentHealth(health);
        explosionManager = GameObject.FindGameObjectWithTag("Variables").GetComponent<ExplosionManager>();
    }

    void FixedUpdate()
    {
		if(!animation.isPlaying)
		{
			if(rotSpeed <= 9.0f)
			{
				rotSpeed += Time.deltaTime;
			}
			
        	t.RotateAround(new Vector3(0, -5, 0), Vector3.forward, rotSpeed * Time.deltaTime);
		}

        if (health < 0)
        {
            // blow up
            explosionManager.ActivateOrangeExplosion(transform.position);
			Variables.instance.IncreaseMoney(20);
            v.boss3BothDead--;
            v.numberOfEnemiesAlive--;
            gameObject.SetActive(false);
        }
        else
        {
            attackTimer += Time.deltaTime;

            if (attackTimer >= attackingTime)
            {
                Attack();
                attackTimer = 0.0f;
            }
			
			if(isHit)
			{
				StartCoroutine(Hit());
			}
        }

        if (numberOfArms == 0)
        {
            beam.renderer.enabled = true;
            //shield.renderer.enabled = false;

            stealTimer += Time.deltaTime;

            if (stealTimer > 3.0f)
            {
                stealTimer = 0.0f;
                Player.instance.AdjustPlanetHealth(-damage);
            }
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
                RemoveHealth(v.playerBulletDamage);
                v.numberOfHitsRound += 1;
                v.numberOfHitsTotal += 1;

                break;

            case "BulletFlak":
                RemoveHealth(v.playerBulletDamage);
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
                RemoveHealth(v.playerBulletDamage);
                v.numberOfHitsRound += 1;
                v.numberOfHitsTotal += 1;
                break;
        }
    }

    void RemoveHealth(float amount)
    {
        if (numberOfArms == 0)
        {
            health -= amount;
			
			HealthBar.instance.RemoveHealth(amount);
			isHit = true;
			hitLoop = 0;
        }
    }

    void PlayArmIdleAnimation(GorgArms arm)
    {
        arm.GorgIdle();
    }

    void PlayArmAttackAnimation(GorgArms arm)
    {
        StartCoroutine(arm.GorgAttack());
    }

    void Attack()
    {
        int randomArm = Random.Range(0, arms.Length);

        if(arms[randomArm].gameObject.activeSelf == true)
            PlayArmAttackAnimation(arms[randomArm]);
    }
	
	IEnumerator Hit()
	{
		isHit = false;
		gorgHitObject.renderer.enabled = true;
		gorgHitObject.renderer.material.color = new Color(1, 1, 1, 1.0f - (hitLoop * 0.1f));
		yield return new WaitForSeconds(0.02f);
		hitLoop++;
		
		if(hitLoop >= 10)
		{
			isHit = false;
			gorgHitObject.renderer.enabled = false;
		}
		else
		{
			isHit = true;
		}
	}
}
