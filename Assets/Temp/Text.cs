using UnityEngine;
using System.Collections;

public class TextA : MonoBehaviour 
{
	private TextMesh tm;
	
	// Use this for initialization
	void Start () {
		tm = GetComponent<TextMesh>();
		
		tm.text = "Press \nA \nPlanet";
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
