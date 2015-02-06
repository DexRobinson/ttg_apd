using UnityEngine;
using System.Collections;

public class GorgAnimations : MonoBehaviour 
{
	public GameObject[] gorgArms;
	
	IEnumerator GorgAttack(int arm)
	{
		float timeOfAnimation = gorgArms[arm].animation["attack"].length;
		gorgArms[arm].animation.Play("attack");
		yield return new WaitForSeconds(timeOfAnimation);
		GorgIdle(arm);
	}
	
	void GorgIdle(int arm)
	{
		gorgArms[arm].animation.Play("idle");
	}
	
	void Update()
	{
		if(Input.GetKey(KeyCode.Q))
		{
			StartCoroutine(GorgAttack(0));
		}
		if(Input.GetKey(KeyCode.W))
		{
			StartCoroutine(GorgAttack(1));
		}
		if(Input.GetKey(KeyCode.E))
		{
			StartCoroutine(GorgAttack(2));
		}
		if(Input.GetKey(KeyCode.R))
		{
			StartCoroutine(GorgAttack(3));
		}
	}
}
