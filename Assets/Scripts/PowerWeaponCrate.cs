using UnityEngine;
using System.Collections;

public class PowerWeaponCrate : MonoBehaviour 
{
	private float timeToSpawn = 25.0f;
	public GameObject powerCrate;
	
	private float timer = 0.0f;
	public GameObject[] itemSpawns;

	private int randomSpawn;

	// Update is called once per frame
	void Update () {
		if(Variables.instance.playerCurrentHealth > 0)
			timer += Time.deltaTime;
		
		if(timer > timeToSpawn){
			MoveCrate();
		}
	}
	
	void MoveCrate(){
		randomSpawn = Random.Range(0, itemSpawns.Length - 1);
        powerCrate.SetActive(true);
        powerCrate.transform.position = new Vector3(itemSpawns[randomSpawn].transform.position.x, itemSpawns[randomSpawn].transform.position.y, itemSpawns[randomSpawn].transform.position.z -1.5f);
		
		timer = 0.0f;
	}
}
