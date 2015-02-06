using UnityEngine;
using System.Collections;

public class LaserSS : MonoBehaviour {
	private Renderer rend;

	// Use this for initialization
	void Start () {
		rend = renderer; 
	}
	
	// Update is called once per frame
	void Update () {
		if(Player.instance.isFiring
			&& Variables.instance.weapon == Variables.Weapon.Laser){
			rend.enabled = true;
		}
		else
		{
			rend.enabled = false;
		}
	}
}
