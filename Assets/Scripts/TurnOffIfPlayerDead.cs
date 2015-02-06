using UnityEngine;
using System.Collections;

public class TurnOffIfPlayerDead : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Variables.instance.playerCurrentHealth <= 0)
		{
			gameObject.SetActive(false);
		}
	}
}
