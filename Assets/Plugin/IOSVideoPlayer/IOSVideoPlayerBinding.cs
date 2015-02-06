using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;



public class IOSVideoPlayerBinding  : MonoBehaviour
{
	[ DllImport ( "__Internal" ) ]
	private static extern void playMovieWithName( string movieName );

	[ DllImport ( "__Internal" ) ]
	private static extern void playMovieWithURL( string movieNameWithPath );

	[ DllImport ( "__Internal" ) ]
	private static extern void addSkipButtonWithImageNameAndPosition( string imageFileName, float coordinateX, float coordinateY );

	[ DllImport ( "__Internal" ) ]
	private static extern void removeSkipButton();

	[ DllImport ( "__Internal" ) ]
	private static extern void shouldPauseUnity( bool thisValue );

	[ DllImport ( "__Internal" ) ]
	private static extern void setObjectName( string objectName );



	public static IOSVideoPlayerBinding instance;
//	private string goName = null;
	void Start()
	{
		instance = this;
		//DontDestroyOnLoad( gameObject );
		
		if( ( gameObject != null ) && ( gameObject.name != null ) )
		{
			if( !Application.isEditor )
			{
//				goName = gameObject.name;
//				setObjectName( goName );
				setObjectName( gameObject.name );
			}
		}
		else
		{
			Debug.LogWarning( "Watch out! This is a warning!" );
		}
	}
	
	// Play a video from "*.app/Data/Raw" place video in the StreamingAssets folder and just call this function with the name of the video file.
	public void PlayVideo( string name )
	{
		Debug.Log( "playMovieWithName" );
	
		if ( !Application.isEditor )
		{
			if( System.IO.File.Exists( Application.dataPath+"/"+name ) )
			{
				playMovieWithName(name);
			}
			else if( System.IO.File.Exists( Application.dataPath+"/Raw/"+name ) )
			{
				playMovieWithName( "/Data/Raw/"+name );
			}
			else
			{
				Debug.Log( "File Not found at "+Application.dataPath+"/Raw/"+name );
			}
		}	
	} 

	// Play a video from "*.app/Data/Raw" place video in the StreamingAssets folder and just call this function with the name of the video file + the top left pixal position of the skip button.
	public void PlayVideo( string name, float xPosition, float yPosition )
	{
		Debug.Log( "playMovieWithName" );
		if ( !Application.isEditor )
		{
			if( System.IO.File.Exists( Application.dataPath+"/"+name ) )
			{

				playMovieWithName(name);
				addSkipButtonWithImageNameAndPosition( "skip-button.png", xPosition, yPosition );
			}
			else if( System.IO.File.Exists( Application.dataPath+"/Raw/"+name ) )
			{
				playMovieWithName( "/Data/Raw/"+name );
				addSkipButtonWithImageNameAndPosition( "skip-button.png", xPosition, yPosition );
			}
			else
			{
				Debug.Log( "File Not found at "+Application.dataPath+"/Raw/"+name );
			}
		}	
	} 

	// Play a video from absolute path. and file name.
	public void PlayVideo(string path,string name)
	{
		Debug.Log( "playMovieWithURL" );
		if ( !Application.isEditor )
		{
			if( path.EndsWith( "/" ) )
			{
				if( System.IO.File.Exists( path+name ) )
				{
					playMovieWithURL( path+name );
				}
				else
				{
					Debug.Log( "File Not found at "+path+name );
				}
			}
			else 
			{
				if( System.IO.File.Exists( path+"/"+name ) )
				{
					playMovieWithURL( path+"/"+name );
				}
				else
				{
					Debug.Log( "File Not found at "+path+"/"+name );
				}
			}
		}	
	}
	
	// Play a video using absolute path. and file name. + the top left pixal position of the skip button
	public void PlayVideo( string path, string name, float xPosition, float yPosition )
	{
		Debug.Log( "playMovieWithURL" );
		if ( !Application.isEditor )
		{
			if( path.EndsWith( "/" ) )
			{
				if( System.IO.File.Exists( path+name ) )
				{
					playMovieWithURL( path+name );
					addSkipButtonWithImageNameAndPosition( "skip-button.png", xPosition, yPosition );
				}
				else
				{
					Debug.Log("File Not found at "+path+name);
				}
			}
			else 
			{
				if( System.IO.File.Exists( path+"/"+name ) )
				{
					playMovieWithURL( path+"/"+name );
					addSkipButtonWithImageNameAndPosition( "skip-button.png", xPosition, yPosition );
				}
				else
				{
					Debug.Log( "File Not found at "+path+"/"+name );
				}
			}
		}	
	}	
	
	//Should OR Should Not Pause Unity during video playback
	public void ShouldPauseUnity( bool pauseStatus )
	{
		if( !Application.isEditor )
		{
			shouldPauseUnity( pauseStatus );
		}
	}
	
	// This function is called when Video is finished or skiped.
	public void VideoDonePlaying( string msg )
	{
		Fade.instance.GoToLevel();
		Debug.Log( msg );
	}
}