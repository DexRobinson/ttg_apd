using UnityEngine;
using System.Collections;

public class KamakizeMorts : MonoBehaviour 
{
    private float health = 3;
    private float orgHealth = 3;
    private Variables v;
    private float shrinkAmount = 0.08f;   // this is used for special enemies to come in closer to the planet and smash into it
    private float shrinkTimer = 0.4f;    // how long before it moves into the planet
    private float shrinkOrginalAmount = 0.08f;
    private GameObject target;
    private Transform t;
    private float speed = 100.0f;
    private bool isRight;
	private float newTime;
	//private GameObject kSpawn;
    private ExplosionManager explosionManager;

	void Awake()
	{
		v = Variables.instance;
        t = transform;
		//kSpawn = GameObject.Find("KamakizeMortSpawn");
        explosionManager = GameObject.FindGameObjectWithTag("Variables").GetComponent<ExplosionManager>();
		target = GameObject.FindGameObjectWithTag("Player");
		int randomDirection = Random.Range(0, 3);
	
        if (randomDirection == 1)
        {
            isRight = true;
            t.localScale = new Vector3(t.localScale.x, t.localScale.y, t.localScale.z);
        }
        else
        {
            isRight = false;
            t.localScale = new Vector3(t.localScale.x * -1, t.localScale.y, t.localScale.z);
        }
	}
	
    void OnEnable()
    {
		shrinkAmount = 0.08f;
    	shrinkTimer = 0.4f;
    	shrinkOrginalAmount = 0.08f;
        health = orgHealth;
		newTime = Time.time;
		v.numberOfEnemiesAlive++;
		transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
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
                v.ChangePlayerShields(-1);
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

        if (health <= 0)
        {
            // Variables.instance.littleMortsKilled++;
            // kill the enemy and add to stats
            health = 0;
            v.numberOfEnemiesKilledRound += 1;
            v.numberOfEnemiesKilledTotal += 1;
            v.numberOfEnemiesAlive -= 1;
            // dead, play explosion
			explosionManager.ActivateRedExplosion(t.position);
            //GameManager.enemyDestoryList.Add(gameObject);
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
                var delta = target.transform.position - transform.position;
                delta.Normalize();

                var moveSpeed = shrinkAmount;

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

        if(isRight)
            t.RotateAround(new Vector3(0, -5, 0), -Vector3.forward, (0.5f * speed) * Time.deltaTime);
        else
            t.RotateAround(new Vector3(0, -5, 0), Vector3.forward, (0.5f * speed) * Time.deltaTime);
    }
}
