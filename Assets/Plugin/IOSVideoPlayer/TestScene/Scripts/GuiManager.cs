using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO; 
public class GuiManager : MonoBehaviour {

	// Use this for initialization
	bool [] tog= new bool[5];
	void Start () {
		tog = new bool[]{false,false,false,false,true};
	}
	// Update is called once per frame
	void Update () 
	{
		ButtonHeight = Screen.height/5;
		ButtonWidth =  Screen.width/2-20;
	}
	
	float ButtonHeight = Screen.height/5;
	float ButtonWidth = Screen.width/2-20;
	void OnGUI ()
	{
		GUI.color = Color.black;
		float ButtonPosx = 10;
		float ButtonPosy = 10;
		GUI.color = Color.white;
		if(GUI.Button(new Rect(ButtonPosx,ButtonPosy,ButtonWidth,ButtonHeight),"Play Movie with Name"))
		{
				IOSVideoPlayerBinding.instance.PlayVideo("Teaser_Final.mov");
		}
		
		ButtonPosx += ButtonWidth+10;
		if(GUI.Button(new Rect(ButtonPosx,ButtonPosy,ButtonWidth,ButtonHeight),"Play Movie with Path"))
		{
				IOSVideoPlayerBinding.instance.PlayVideo(Application.dataPath+"/Raw/","Teaser_Final.mov");
		}
		
		ButtonPosx -= ButtonWidth+10;
		ButtonPosy += ButtonHeight+30;
		if(GUI.Button(new Rect(ButtonPosx,ButtonPosy,ButtonWidth,ButtonHeight),"Play Movie with Skip Button"))
		{
			IOSVideoPlayerBinding.instance.PlayVideo("Teaser_Final.mov",Screen.width/2-50,10 );
		}
		
		ButtonPosx += ButtonWidth+10;
		if(GUI.Button(new Rect(ButtonPosx,ButtonPosy,ButtonWidth,ButtonHeight),"Play Movie with Path and Skip Button"))
		{
				IOSVideoPlayerBinding.instance.PlayVideo(Application.dataPath+"/Raw/","Teaser_Final.mov",Screen.width-(ButtonWidth+20),Screen.height-(ButtonHeight+20));
		}
		
		ButtonPosx -= ButtonWidth+10;
		ButtonPosy += ButtonHeight+30;
		if (!tog[4])
		{
				GUI.color = Color.green;
			if(GUI.Button(new Rect(ButtonPosx,ButtonPosy,ButtonWidth,ButtonHeight),"PAUSE UNITY\nduring playback"))
			{
					IOSVideoPlayerBinding.instance.ShouldPauseUnity(true);
					tog[4] = !tog[4] ;
			}
			
			GUI.color = Color.white;
			GUI.Label( new Rect( 10, ButtonPosy + ButtonHeight + 5, ButtonWidth, ButtonHeight), "Current Status: Unity will keep running during video playback." );
		}
		else
		{
				GUI.color = Color.white;
			if(GUI.Button(new Rect(ButtonPosx,ButtonPosy,ButtonWidth,ButtonHeight),"DO NOT PAUSE UNITY\nduring playback"))
			{
					IOSVideoPlayerBinding.instance.ShouldPauseUnity(false);
					tog[4] = !tog[4] ;
			}
			
			GUI.color = Color.white;
			GUI.Label( new Rect( 10, ButtonPosy + ButtonHeight + 5, ButtonWidth, ButtonHeight), "Current Status: Unity will be paused during video playback." );
		}
	}
}