using UnityEngine;
using System.Collections;

public class MoneyDisplay : MonoBehaviour 
{
	TextMesh tm;
	
	// Use this for initialization
	void Start () {
		tm = GetComponent<TextMesh>();
		tm.text = "Money: " + PlayerPrefs.GetInt("Money");
		
		InvokeRepeating("CheckMoney", 0.0f, 1.0f);
	}
	
	void CheckMoney()
	{
		tm.text = "Money: " + PlayerPrefs.GetInt("Money");
	}
}
