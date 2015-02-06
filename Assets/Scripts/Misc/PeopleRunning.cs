using UnityEngine;
using System.Collections;

public class PeopleRunning : MonoBehaviour {
	private float speed = 2.0f;
	
	private Transform t;
	private bool isDead;
	private float dieTimer;
	
	void Awake()
	{
		t = transform;
		speed = Random.Range(0.8f, 2.2f);
		
	}
	
	// Use this for initialization
	void OnEnable () 
	{
		t.rotation = Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 360))); //Variables.playerGameObject.transform.rotation;
		dieTimer = 0;
	}
	
	// Update is called once per frame
	void Update () 
	{
		t.Translate(Vector3.up * Time.deltaTime * speed);
		
		dieTimer += Time.deltaTime;
		
		if(dieTimer > 12.0f)
		{
			DeSpawn();
		}
	}
	
	
	void DeSpawn()
	{
		dieTimer = 0f;
		gameObject.SetActive(false);
	}
}
