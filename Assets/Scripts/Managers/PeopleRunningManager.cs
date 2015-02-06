using UnityEngine;
using System.Collections;

public class PeopleRunningManager : MonoBehaviour {

	public PeopleRunning[] people;
	public Renderer planetBackground;

	public Texture2D[] backgroundImages;

	private PeopleRunning[] spawnedPeople;
	private int maxPeople = 8;
	
	//private float testTimer = 0f;
	private int randomPeople;

	void Start()
	{
		spawnedPeople = new PeopleRunning[maxPeople];
		planetBackground.material.mainTexture = backgroundImages[DontDestoryValues.instance.planetNumber];

		for(int i = 0; i < maxPeople; i++)
		{
			spawnedPeople[i] = Instantiate(people[DontDestoryValues.instance.planetNumber]) as PeopleRunning;
			spawnedPeople[i].gameObject.SetActive(false);
		}
	}
	
	public void SpawnPeople()
	{
		randomPeople = Random.Range(1, 5);
		
		for(int i = 0; i < randomPeople; i++)
		{
			Spawn();
		}
	}
	
	private void Spawn()
    {
        for (int i = 0; i < maxPeople; i++)
        {
            if (spawnedPeople[i].gameObject.activeSelf == false)
            {
				spawnedPeople[i].transform.position = new Vector3(0, -5, 0.6f);
                spawnedPeople[i].gameObject.SetActive(true);
				return;
			}
		}
	}
}
