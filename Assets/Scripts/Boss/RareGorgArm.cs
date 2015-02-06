using UnityEngine;
using System.Collections;

public class RareGorgArm : MonoBehaviour {
	public Transform spawn;
	
	private float attackTimer = 0.0f;
	private float attackTime = 0.0f;
	private Animation anim;

	void Awake()
	{
		anim = animation;
	}
	void OnEnable()
	{
		attackTime = Random.Range(3.5f, 5.0f);
	}
	
	
	// Update is called once per frame
	void Update () {
		attackTimer += Time.deltaTime;
		
		if(attackTimer > attackTime){
			attackTime = Random.Range(3.5f, 8.0f);
			attackTimer = 0;
			StartCoroutine(GorgAttack());
		}
	}
	
	IEnumerator GorgAttack()
	{
		float timeOfAnimation = anim["attack"].length;
		anim.Play("attack");
		
		yield return new WaitForSeconds(timeOfAnimation - 0.6f);
		Variables.instance.hitByEnemy = true;
		// spawn a orb if mini boss or boss
		if(Variables.instance.currentInvasion == 10 || Variables.instance.currentInvasion == 5)
			GameManager.ActivateGorgMinion(new Vector3(spawn.position.x, spawn.position.y, 0));
		else
			Player.instance.RemovePlanetHealth(300 - (Variables.instance.playerDurLevel * 45));
		yield return new WaitForSeconds(0.6f);
		
		GorgIdle();
	}
	
	void GorgIdle()
	{
		anim.Play("idle");
	}
}
