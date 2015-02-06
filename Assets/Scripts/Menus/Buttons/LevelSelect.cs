using UnityEngine;
using System.Collections;

public class LevelSelect : MonoBehaviour 
{
	public static LevelSelect instance;
	
	public GameObject[] levelButtons;
	public Texture2D[] completed;
	public Texture2D notCompleted;
	
	void Awake()
	{
		instance = this;
	}
	
	public void DrawLevelSelect(int levelId)
	{
		Variables.highestLevelCompleted = PlayerPrefs.GetInt("planet" + levelId + "highestLevel", 1);
		
		for(int i = 0; i < levelButtons.Length; i++)
		{
			if(i < Variables.highestLevelCompleted)
			{
				levelButtons[i].renderer.material.mainTexture = completed[i];
				levelButtons[i].GetComponent<LevelButton>().isUnlocked = true;
			}
			else
			{
				levelButtons[i].renderer.material.mainTexture = notCompleted;
			}
		}
	}
}
