We.R.Play IOS Video Player for Unity3D

Copyrights We.R.Play 2012.  All rights reserved.

How to use:
===========

Just place the IOSVideoPlayer Prefab in the your initial scene and use the provided functions listed below by IOSVideoPlayer.intance."functionName".

Note:
=====

1. Unity is running in the background so that the user can load scenes or other stuff in the background if he wants. So suspend any code from running before playing the video and resume it afterwards, if you want Unity to be suspended during playback.

2. This internally uses MPMoviePlayerController object to play movies. Therefore, you should expect the same behavior and the same supported formats. MPMoviePlayerController supports any movie or audio files that already play correctly on an iPod or iPhone.

For movie files, this typically means files with the extensions .mov, .mp4, .mpv, .m4v and .3gp and using one of the following compression standards:

H.264 Baseline Profile Level 3.0 video.

Videos Resolutions supported are
1. up to 640 x 480 at 30 fps for iPhone 3GS and below
2. 1024 x 768 at 30fps for iPhone 4/iPod 4 and above and all iPads.

Note that B frames are not supported in the Baseline profile.
MPEG-4 Part 2 video (Simple Profile).

Functions List:
===============

********************************************************************************************************************************

Call this function with the name of the video which is placed in the StreamingAssets folder. Unity automatically copies the video from StreamingAssets folder to "*.app/Data/Raw" folder.
	public void PlayVideo(string name)

Example:
	IOSVideoPlayer.intance.PlayVideo("MovieName.mp4");

********************************************************************************************************************************
	 
Play a video from "*.app/Data/Raw" place video in the StreamingAssets folder and just call this function with the name of the video file + the top left pixel position of the skip button.
      public void PlayVideo(string name, float xPosition, float yPosition)

Example:
	IOSVideoPlayer.intance.PlayVideo("MovieName.mp4", 10, 10);

********************************************************************************************************************************

Play a video from absolute path. and file name.
	public void PlayVideo(string path, string name)

Example:
	IOSVideoPlayer.intance.PlayVideo("file://var/mobile/Applications/XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX/Documents/", "MovieName.mp4");

********************************************************************************************************************************
	
Play a video using absolute path. and file name. + the top left pixel position of the skip button
	public void PlayVideo(string path, string name , float xPosition, float yPosition)

Example:
	IOSVideoPlayer.intance.PlayVideo("file://var/mobile/Applications/XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX/Documents/", "MovieName.mp4", 10, 10);

********************************************************************************************************************************
	
This function is called when video is finished or skipped. Add your own code if you like inside this function.
	public void VideoDonePlaying(string msg)
	

********************************************************************************************************************************	


For further queries or information, email us at support@werplay.com