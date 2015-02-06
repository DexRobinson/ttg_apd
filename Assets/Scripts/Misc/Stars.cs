using UnityEngine;
using System.Collections;

public class Stars : MonoBehaviour {
	private bool fadeIn = true;
	private float alpha = 0f;
	private Renderer rend;
	private Transform t;

	void Awake()
	{
		rend = renderer;
		t = transform;
	}

	void Update()
	{
		if(fadeIn)
		{
			rend.material.color = new Color(1, 1, 1, alpha);
			
			if(alpha < 1f)
			{
				alpha += Time.deltaTime * 2f;
				if(alpha >= 0.99f)
				{
					alpha = 0.99f;
					fadeIn = false;
				}
			}
		}
		else
		{
			rend.material.color = new Color(1, 1, 1, alpha);
			
			if(alpha > 0f)
			{
				alpha -= Time.deltaTime * 2f;
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
		t.Rotate(new Vector3(0, 0, 90));
		yield return new WaitForSeconds(0.3f);
		fadeIn = true;
	}
}
