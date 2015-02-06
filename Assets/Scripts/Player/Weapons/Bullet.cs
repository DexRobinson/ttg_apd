using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour 
{
    public float life = 2.0f;
    public bool multiShotBullet;
	public bool escapePod;
	public bool seeker;
	public bool singleShot;
	
    private float timeToDie;
    //private GameObject playerGO;
	private GameObject bulletCollider;
	private float seekerTimer = 0.0f;
	private GameObject shield;
	private float bulletSpeed = 0.0f;
	private Transform t;
	private Rigidbody r;
	private GameObject go;
    private Variables variables;

    void Awake()
    {
		t = transform;
		r = rigidbody;
		go = gameObject;
        variables = GameObject.FindGameObjectWithTag("Variables").GetComponent<Variables>();
        //playerGO = Variables.playerGameObject;
		shield = GameObject.FindGameObjectWithTag("Shield");
		
		Physics.IgnoreCollision(collider, shield.collider);
		
		if(singleShot)
		{
            t.localScale *= (0.4f + (0.1f * variables.upgradeLevel[0]));
		}
    }

    void FixedUpdate()
    {
		if(multiShotBullet)
		{
			Physics.IgnoreLayerCollision(7, 7);
			Physics.IgnoreLayerCollision(7, 7);
		}
		else
		{
		}
		
        CountDown();
        Move();
    }

	public void Activate()
    {
        go.SetActive(true);

		r.velocity = Vector3.zero;
        r.angularVelocity = Vector3.zero;
        timeToDie = Time.time + life;
		bulletSpeed = variables.playerBulletSpeed;

        t.rotation = Variables.playerGameObject.transform.rotation;//playerGO.transform.rotation;

        if (multiShotBullet)
        {
            t.position = Player.instance.bulletSpawn[Random.Range(0, Player.instance.bulletSpawn.Length)].position; 
        }
        else
		{
			t.position = Player.instance.singleBulletSpawn[Random.Range(0, Player.instance.singleBulletSpawn.Length)].position; 
        	//this.transform.position = Player.instance.bulletSpawn[0].position;
		}
    }
	
    public void ActivateSataliteSeeker()
    {
        r.velocity = Vector3.zero;
        r.angularVelocity = Vector3.zero;
        timeToDie = Time.time + life;
        t.rotation = Variables.playerGameObject.transform.rotation;
    }

    public void Deactivate()
    {
		if(escapePod)
		{
            if (variables.gameState == Variables.GameState.Tutorial)
            {
				GameManager.ActivateComboText("POD ESCAPED!");
			}
			else{
                variables.playerEscapePodsRecused++;
                variables.IncreaseMoney(50);
				GameManager.ActivateComboText("POD ESCAPED +$50");
			}
		}
		
        go.SetActive(false);
    }

    private void CountDown()
    {
        if (timeToDie < Time.time)
        {
            Deactivate();
        }
    }

    private void Move()
    {
		if(!seeker)
		{
			if(escapePod)
			{
				r.AddRelativeForce(Vector3.up * 75 * Time.deltaTime);
			}
			else
			{
				//transform.Translate(Vector3.up * Time.deltaTime * 5f);
	        	r.AddRelativeForce(Vector3.up * bulletSpeed * Time.deltaTime);
			}
		}
		else
		{
			if(seekerTimer > 1.5f)
			{
				
			}
			else
			{
				seekerTimer += Time.deltaTime;
				t.Translate(Vector3.up * Time.deltaTime * 2.6f);
			}
		}
    }
}
