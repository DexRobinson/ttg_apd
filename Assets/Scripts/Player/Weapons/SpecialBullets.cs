using UnityEngine;
using System.Collections;

public class SpecialBullets : MonoBehaviour 
{
    public Variables.SeekerType seekerType;

    private GameObject closestObject;
    private float timer;
    private float seekingTimer = 1.5f;
    private int numberOfBounces;
    //private float seekerFuelTime = 0;
    private float seekerMaxFuelTime;
    private float seekerSpeed;
    private int seekerHits = 1;
	private Transform t;
	private GameObject go;
	private Rigidbody r;
    private ExplosionManager explosionManager;

	void Awake()
	{
		t = transform;
		go = gameObject;
		r = rigidbody;

        explosionManager = GameObject.FindGameObjectWithTag("Variables").GetComponent<ExplosionManager>();
	}

    void OnEnable()
    {
        //seekerHits = 1 + (Variables.instance.upgradeLevel[3]);
        
        switch (seekerType)
        {
            case Variables.SeekerType.Enemy:
				timer = 0;
				closestObject = null;
                seekerMaxFuelTime = Variables.instance.seekerFuelTime;
                seekerSpeed = Variables.instance.seekerSpeed;

                if (Variables.instance.numberOfEnemiesAlive > 0)
                    StartCoroutine(Seek(1.5f));
                else
                {
                    //StartCoroutine(Kill(0.1f));
                }
                break;
            case Variables.SeekerType.Player:
                if (Variables.instance.playerCurrentHealth > 0)
                    closestObject = FindClosestPlayer();
                break;
            case Variables.SeekerType.Crate:
                seekingTimer = 0;
                seekerMaxFuelTime = 4;
                seekerSpeed = 12;
				timer = 10;
                if (Variables.instance.numberOfEnemiesAlive > 0)
                    closestObject = FindClosestEnemy();
                else
                {
                    StartCoroutine(KillMortShot(seekerMaxFuelTime));
                }
                break;
            case Variables.SeekerType.Bomb:
                {
					StartCoroutine(BombExplode()); 
                    //StartCoroutine(Kill(0.1f));
                }
                break;
            case Variables.SeekerType.FlakShell:
				Physics.IgnoreLayerCollision(1, 1);
                StartCoroutine(Kill(0.1f));
                break;
            case Variables.SeekerType.FlakBullet:
                StartCoroutine(FlakShot());
                break;
            case Variables.SeekerType.Normal:
                StartCoroutine(Kill(3.0f));
                break;
        }
    }

    void Update()
    {
        if (seekerType == Variables.SeekerType.Enemy || seekerType == Variables.SeekerType.Crate)
        {
            if (timer >= seekingTimer)
            {
                closestObject = FindClosestEnemy();

                if (closestObject)
                {
                    Transform target = closestObject.transform;
                    t.LookAt(new Vector3(target.position.x, target.position.y, 0));
					
					if (seekerType == Variables.SeekerType.Enemy)
        			{
						if(Variables.instance.previousWeapon == Variables.Weapon.Laser || (Variables.instance.weapon == Variables.Weapon.Laser && Variables.instance.previousWeapon == Variables.Weapon.Seeker))
						{
							seekerSpeed = 25;
						}
						else
						{
							seekerSpeed = Variables.instance.seekerSpeed;
						}
						
						t.Translate(Vector3.forward * seekerSpeed * Time.deltaTime);
					}
					else
					{
						t.Translate(Vector3.forward * 3 * Time.deltaTime);
					}
                }
            }
            else
            {
            	timer += Time.deltaTime;
            }
        }

        else if (seekerType == Variables.SeekerType.Player)
        {
            closestObject = FindClosestPlayer();

            if (closestObject)
            {
                Transform target = closestObject.transform;
                //transform.LookAt(target);
				t.LookAt(new Vector3(target.position.x, target.position.y, 0));
                t.Translate(Vector3.forward * 1.0f * Time.deltaTime);
            }
        }
    }

    void OnTriggerEnter(Collider coll)
    {
        if (seekerType == Variables.SeekerType.Player)
        {
            if (coll.tag == "Planet")
            {
				explosionManager.ActivateRandomExplosion(transform.position);
                Variables.instance.playerCurrentHealth -= 350f;
                go.GetComponent<Explosion>().Deactivate();
            }
			if(coll.tag == "Shield")
			{
				explosionManager.ActivateRandomExplosion(transform.position);
				Variables.instance.RemovePlayerShield(1);
				go.GetComponent<Explosion>().Deactivate();
			}
			
			if(coll.tag == "Bomb")
			{
				go.GetComponent<Explosion>().Deactivate();
			}
        }

        else if (seekerType == Variables.SeekerType.Enemy)
        {
            seekerHits--;
            if (seekerHits < 1)
            {
                go.SetActive(false);
            }
        }
        else if (seekerType == Variables.SeekerType.Crate)
        {
            if (coll.tag == "Enemy")
            {
                go.SetActive(false);
            }
			if(coll.tag == "Bomb")
			{
				go.SetActive(false);
			}
        }
		
		if (coll.tag == "Bullet")
        {
			explosionManager.ActivateRandomExplosion(transform.position);
            go.SetActive(false);
        }
    }

    GameObject FindClosestEnemy()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Enemy");

        GameObject closest = null;
        float distance = Mathf.Infinity;

        Vector3 position = transform.position;

        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;

            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }

        return closest;
    }

    GameObject FindClosestItem()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Item");

        GameObject closest = null;
        float distance = Mathf.Infinity;

        Vector3 position = transform.position;

        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;

            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }

        return closest;
    }

    GameObject FindClosestPlayer()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Player");

        GameObject closest = null;
        float distance = Mathf.Infinity;

        Vector3 position = transform.position;

        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;

            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }

        return closest;
    }

    IEnumerator FlakShot()
    {
        yield return new WaitForSeconds(0.65f);
		r.velocity = Vector3.zero;
        r.angularVelocity = Vector3.zero;
		yield return new WaitForSeconds(0.65f);
        //GameBoarder.cleanUpObjects.Add(gameObject);
        //gameObject.SetActive(false);
		if(!Variables.instance.isPowerMode)
        	explosionManager.ActivateFlakExplosion(transform.position);
		else
			explosionManager.ActivateFlakExplosionPower(transform.position);
		
		explosionManager.ActivateFlakShell(transform.position);
    }

    IEnumerator Kill(float time)
    {
        yield return new WaitForSeconds(time);
		go.SetActive(false);
        //gameObject.GetComponent<Bullet>().Deactivate();
    }
	
	IEnumerator Seek(float times)
    {
        yield return new WaitForSeconds(times);
		closestObject = FindClosestEnemy();
        //gameObject.GetComponent<Bullet>().Deactivate();
    }
	
    IEnumerator KillMortShot(float time)
    {
        yield return new WaitForSeconds(time);
        go.SetActive(false);
        Debug.Log("Destory");
    }

	IEnumerator BombExplode()
	{
		t.localScale = Vector3.one;
		for(int i = 0; i < 60; i++)
		{
			t.localScale += new Vector3(0.9f, 0.9f, 0.01f);
			yield return new WaitForSeconds(0.02f);
		}
	}
}
