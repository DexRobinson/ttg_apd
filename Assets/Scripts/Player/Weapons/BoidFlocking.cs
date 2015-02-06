using UnityEngine;
using System.Collections;

public class BoidFlocking : MonoBehaviour
{
    public float minVelocity;
    public float maxVelocity;
    public float randomness;
    public GameObject chasee;
    public GameObject missile;
    public float rotationSpeed = 3.0f;

    private float dieTimer;
    private bool updatePosition;
    //private LineRenderer missileLineRenderer;
    private float lineRenderAlpha = 1.0f;

    private bool inited = false;
    private Transform t;
    private Rigidbody r;
	public float straightTimer = 0.0f;

    void Awake()
    {
        t = transform;
        r = rigidbody;
		minVelocity = maxVelocity;

        //missileLineRenderer = missile.GetComponent<LineRenderer>();

        //chasee = FindClosestEnemy("Enemy");
    }

    void OnEnabled()
    {
		straightTimer = 0f;
        lineRenderAlpha = 1.0f;
        //missileLineRenderer.SetColors(new Color(1, 1, 1, lineRenderAlpha), new Color(1, 1, 1, lineRenderAlpha));
        missile.renderer.enabled = true;
        inited = false;
        collider.enabled = true;
		maxVelocity = minVelocity;
        
		r.velocity = Vector3.zero;
		r.angularVelocity = Vector3.zero;
    }

    void FixedUpdate()
    {
       // Vector3 dir = chasee.transform.position - t.position;
        //float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
       // t.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
		if(chasee)
        	t.rotation = Quaternion.Euler(0f, 0, 0f) * Quaternion.AngleAxis(GetTehAngle(this.transform.position, chasee.transform.position), Vector3.forward);
    }

    private float GetTehAngle(Vector3 infrom, Vector3 into)
    {
        Vector2 from = Vector2.right;
        Vector3 to = into - infrom;

        float ang = Vector2.Angle(from, to);
        Vector3 cross = Vector3.Cross(from, to);

        if (cross.z > 0)
            ang = 360 - ang;

        ang *= -1f;

        return ang;
    }

    void Update()
    {
        if (inited)
        {
			maxVelocity += Time.deltaTime * 0.1f;

            StartCoroutine(BoidSteering());
        }
		else
		{
			if(straightTimer > 1.5f)
			{
				inited = true;
				chasee = FindClosestEnemy("Enemy");
			}
			else
			{
				t.Translate(Vector3.up * Time.deltaTime * maxVelocity);
				straightTimer += Time.deltaTime;
			}
		}
    }

    IEnumerator BoidSteering()
    {
        if (inited)
        {
            if (chasee)
            {
                r.velocity = r.velocity + Calc() * Time.deltaTime;

                // enforce minimum and maximum speeds for the boids
                float speed = rigidbody.velocity.magnitude;
                if (speed > maxVelocity)
                {
                    r.velocity = r.velocity.normalized * maxVelocity;
                }
                else if (speed < minVelocity)
                {
                    r.velocity = r.velocity.normalized * minVelocity;
                }

                float waitTime = Random.Range(0.05f, 0.1f);
                yield return new WaitForSeconds(waitTime);

                chasee = FindClosestEnemy("Enemy");
            }
            else
            {
                chasee = FindClosestEnemy("Enemy");

				if(!chasee)
				{
					chasee = FindClosestEnemy("Mort");
				}
				else
				{
					gameObject.SetActive(false);
				}
            }
        }
    }

    IEnumerator KillMissile()
    {
        rigidbody.velocity = Vector3.zero;
        collider.enabled = false;
        missile.renderer.enabled = false;
        inited = false;

        while (lineRenderAlpha > 0)
        {
            lineRenderAlpha -= 0.04f;
            //missileLineRenderer.SetColors(new Color(1, 1, 1, lineRenderAlpha), new Color(1, 1, 1, lineRenderAlpha));
            yield return new WaitForSeconds(0.03f);
        }

        
        gameObject.SetActive(false);
    }

    private Vector3 Calc()
    {
        Vector3 randomize = new Vector3((Random.value * 2) - 1, (Random.value * 2) - 1, (Random.value * 2) - 1);

        randomize.Normalize();
        //BoidController boidController = Controller.GetComponent<BoidController>();
        Vector3 flockCenter = chasee.transform.position;
        Vector3 flockVelocity = rigidbody.velocity;
        Vector3 follow = chasee.transform.localPosition;

        flockCenter = flockCenter - transform.localPosition;
        flockVelocity = flockVelocity - rigidbody.velocity;
        follow = follow - transform.localPosition;

        return (flockCenter + flockVelocity + follow * 2 + randomize * randomness);
    }

    void OnCollisionEnter( Collision coll )
    {
        if (coll.gameObject.tag == "Enemy")
        {
            StartCoroutine(KillMissile());
        }
    }

	public void Activate()
	{
		gameObject.SetActive(true);
		t.rotation = Variables.playerGameObject.transform.rotation;
		straightTimer = 0f;
		lineRenderAlpha = 1.0f;
		//missileLineRenderer.SetColors(new Color(1, 1, 1, lineRenderAlpha), new Color(1, 1, 1, lineRenderAlpha));
		missile.renderer.enabled = true;
		inited = false;
		collider.enabled = true;
		maxVelocity = minVelocity;
		
		r.velocity = Vector3.zero;
		r.angularVelocity = Vector3.zero;

		//r.velocity = Vector3.zero;
		//r.angularVelocity = Vector3.zero;
		
		//this.transform.rotation = Variables.playerGameObject.transform.rotation;//playerGO.transform.rotation;
		

		this.transform.position = Player.instance.bulletSpawn[Random.Range(0, Player.instance.bulletSpawn.Length)].position; 
		
	}

    GameObject FindClosestEnemy(string tag)
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag(tag);
		GameObject closest = null;
		if(gos.Length > 0)
		{
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
		}

        return closest;
    }
}