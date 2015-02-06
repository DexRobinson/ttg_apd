using UnityEngine;
using System.Collections;

public class Satalite : MonoBehaviour 
{
	private float shootTimer = 0.0f;
	private Transform t;

	void Awake()
	{
		t = transform;
	}

	void Update()
	{
		if(Variables.instance.itemSataliteOn)
		{
			Variables.instance.itemSataliteTime += Time.deltaTime;
			t.RotateAround(new Vector3(0, -5, 0), -Vector3.forward, 30.0f * Time.deltaTime);
			shootTimer += Time.deltaTime;
			
			if(shootTimer > 3.0f)
			{
				//Instantiate(Variables.instance.sataliteBullet, transform.position, Quaternion.identity);
                //Player.ActivateSeekerProjectile(new Vector3(transform.position.x, transform.position.y, -0.07773381f));
				GameManager.ActivateSataliteBullet(new Vector3(transform.position.x, transform.position.y, 0));
				shootTimer = 0.0f;
			}
			
			if(Variables.instance.itemSataliteTime > Variables.instance.itemSataliteTimer)
			{
				Variables.instance.itemSataliteOn = false;
				Variables.instance.satalite.renderer.enabled = false;
			}
		}
	}
}
