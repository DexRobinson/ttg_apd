using UnityEngine;
using System.Collections;

public class ScaleAtStart : MonoBehaviour {
	public bool isOrb;
	public int index;
	private float waitTimer;
	private float size = 0.0f;
	private bool effect;
	private Transform t;
	private float waiting;

    private ExplosionManager explosionManager;
	void Start()
	{
		t = transform;
		t.localScale = new Vector3(0.0f, 0.0f, 0.0f);
        explosionManager = GameObject.FindGameObjectWithTag("Variables").GetComponent<ExplosionManager>();
	}
	
	// Update is called once per frame
	void Update () {
		if(waitTimer > ReturnWaitTime())
		{
			if(!effect)
			{
				effect = true;
				if(isOrb)
				{
					explosionManager.ActivateShockwaveExplosion(new Vector3(transform.position.x, transform.position.y, transform.position.z - 5));
				}
			}
			
			if(size < 1.0f)
			{
				size += Time.deltaTime * 2.0f;
				if(size >= 1.0f)
					size = 1.0f;
				
				t.localScale = new Vector3(size, size, size);
			}
		}
		else
		{
			waitTimer += Time.deltaTime;
		}
	}
	
	float ReturnWaitTime()
	{
		waiting = 0.0f;
		
		if(!isOrb)
		{
			waiting = 3.0f;
		}
		
		
		waiting += 0.25f * index;
		return waiting;
	}
}
