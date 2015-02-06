using UnityEngine;
using System.Collections;

public class Centipede : MonoBehaviour 
{
	public int numberOfSpawners = 4;
	public GameObject[] orbsAndWallsOuter;
	public GameObject[] orbsAndWallsInner;
	public GameObject[] spawners;
	public bool isMiniBoss;
	
	void Update()
	{
		if(numberOfSpawners <= 0)
		{
			Variables.instance.IncreaseMoney(20);
			gameObject.SetActive(false);
		}
		else
		{
			if(!isMiniBoss)
			{
				for(int i = 0; i < orbsAndWallsOuter.Length; i++)
				{
					orbsAndWallsOuter[i].transform.RotateAround(new Vector3(0, -5, 0), -Vector3.forward, (0.5f * 35.0f) * Time.deltaTime);
				}
			}
			
			for(int i = 0; i < orbsAndWallsInner.Length; i++)
			{
				orbsAndWallsInner[i].transform.RotateAround(new Vector3(0, -5, 0), -Vector3.forward, (0.5f * -35.0f) * Time.deltaTime);
			}
			
			
			
			for(int i = 0; i < spawners.Length; i++)
			{
				spawners[i].transform.RotateAround(new Vector3(0, -5, 0), -Vector3.forward, (0.5f * 10.0f) * Time.deltaTime);
			}
		}
	}
}
