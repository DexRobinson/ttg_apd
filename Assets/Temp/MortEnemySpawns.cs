using UnityEngine;
using System.Collections;

public class MortEnemySpawns : MonoBehaviour 
{
    private float health = 18;
    private float orgHealth = 18;
    private Variables v;
    private float shrinkAmount = 0.16f;   // this is used for special enemies to come in closer to the planet and smash into it
    private float shrinkTimer = 0.2f;    // how long before it moves into the planet
    private float shrinkOrginalAmount = 0.16f;
    private GameObject target;
    private Transform t;
    private float speed = 100.0f;
    public GameObject mortObj;
    private float newTime = 0.0f;

	private Renderer rend;
	private Vector3 delta;
	private float moveSpeed;
    private ExplosionManager explosionManager;

    void Awake()
    {
        //mortObj = GameObject.Find("RCannonSpawn");
        v = Variables.instance;
        t = transform;
		rend = renderer;
        target = GameObject.FindGameObjectWithTag("Player");
        t.localScale = new Vector3(t.localScale.x, t.localScale.y, t.localScale.z);

        explosionManager = GameObject.FindGameObjectWithTag("Variables").GetComponent<ExplosionManager>();
		//transform.rotation = mortObj.transform.rotation;
    }

    void OnEnable()
    {
		renderer.material.color = new Color(1, 1, 1, 1);
		
		if(mortObj == null)
			mortObj = GameObject.Find("RCannonSpawn");
		
        shrinkAmount = 0.1f;
        shrinkTimer = 0.2f;
        shrinkOrginalAmount = 0.1f;
        health = orgHealth;
        newTime = Time.time;
		v.numberOfEnemiesAlive += 1;
		
        if(GameManager.boss1Spawned)
            t.rotation = mortObj.transform.rotation;
    }
	
    // switch between the things that hit the enemy and adjust their health accordingly
    void OnTriggerEnter(Collider coll)
    {
        switch (coll.tag)
        {
            case "Mines":
                Variables.instance.numberOfMinesAlive--;
                ChangeHealth(1500);
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
                v.numberOfEnemiesKilledRound -= 1;
                v.numberOfEnemiesKilledTotal -= 1;
                ChangeHealth(100);
                v.hitByEnemy = true;
                Player.instance.AdjustPlanetHealth(-650f);
                break;

            case "Shield":
                v.numberOfEnemiesKilledRound -= 1;
                v.numberOfEnemiesKilledTotal -= 1;
                ChangeHealth(100);
                v.RemovePlayerShield(1);
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
                ChangeHealth(100);
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
        health -= amount;
		Hit();
		
        if (health <= 0)
        {
            // Variables.instance.littleMortsKilled++;
            // kill the enemy and add to stats
            health = 0;
            v.numberOfEnemiesKilledRound += 1;
            v.numberOfEnemiesKilledTotal += 1;
            v.numberOfEnemiesAlive -= 1;
            // dead, play explosion
            //GameManager.enemyDestoryList.Add(gameObject);
			explosionManager.ActivateRedExplosion(t.position);
            gameObject.SetActive(false);
            //Destroy(gameObject);
        }
    }

    void Update()
    {
        if (shrinkTimer > 0)
        {
            //t.Rotate(new Vector3(0f, 0f, rotationAmount));
            if (Time.time - newTime > shrinkOrginalAmount)
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

        t.RotateAround(new Vector3(0, -5, 0), Vector3.forward, (0.5f * speed) * Time.deltaTime);
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
			yield return new WaitForSeconds(0.03f);
		}
		
		rend.material.color = new Color(1, 1, 1, 1);
	}
}
