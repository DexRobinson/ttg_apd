using UnityEngine;
using System.Collections;

public class ButtonPulse : MonoBehaviour {
	public float pulseUpSpeed = 2.0f;
	public float pulseDownSpeed = 1.0f;
	
	private bool fadeIn = true;
	private float alpha = 0f;
	
	void Update()
	{
		if(fadeIn)
		{
			gameObject.renderer.material.color = new Color(1, 1, 1, alpha);
			
			if(alpha < 1f)
			{
				alpha += Time.deltaTime * pulseUpSpeed;
				if(alpha >= 0.99f)
				{
					alpha = 0.99f;
					fadeIn = false;
				}
			}
		}
		else
		{
			gameObject.renderer.material.color = new Color(1, 1, 1, alpha);
			
			if(alpha > 0f)
			{
				alpha -= Time.deltaTime * pulseDownSpeed;
				if(alpha <= 0f)
				{
					alpha = 0;
					StartCoroutine(Wait());
				}
			}
		}
	}
	
	IEnumerator Wait()
	{
		yield return new WaitForSeconds(0.03f);
		fadeIn = true;
	}
}
