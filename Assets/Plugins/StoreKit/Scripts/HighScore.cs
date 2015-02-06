using UnityEngine;
using System.Collections;
using UnityEngine.SocialPlatforms;

public class HighScore : MonoBehaviour {
	// use the leaderboard ID in iTunesConnect
	private string leaderboardID = "TTS_001";

	void Start () {
#if UNITY_IPHONE
		// Authenticate and register a ProcessAuthentication callback
		// This call needs to be made before we can proceed to other calls in the Social API
		Social.localUser.Authenticate (ProcessAuthentication);
#endif  
	}
	
	// This function gets called when Authenticate completes
	// Note that if the operation is successful, Social.localUser will contain data from the server. 
	void ProcessAuthentication (bool success) {
		if (success) {
			Debug.Log ("Authenticated, checking achievements");
			
			// Request loaded achievements, and register a callback for processing them
			Social.LoadAchievements (ProcessLoadedAchievements);
		}
		else
			Debug.Log ("Failed to authenticate");
	}

	// This function gets called when the LoadAchievement call completes
	void ProcessLoadedAchievements (IAchievement[] achievements) {
		if (achievements.Length == 0)
			Debug.Log ("Error: no achievements found");
		else
			Debug.Log ("Got " + achievements.Length + " achievements");
	}

	public void ReportScoreSingle(int score)
	{
        #if UNITY_IPHONE
		ReportScore (score, leaderboardID);
        #endif
	}
    public void ReportScoreByName(int score, string _leaderboardID)
    {
        #if UNITY_IPHONE
        ReportScore (score, _leaderboardID);
        #endif
    }

	void ReportScore(long score, string _leaderboardID) {
		Debug.Log ("Reporting score " + score + " on leaderboard " + _leaderboardID);
		Social.ReportScore (score, _leaderboardID, success => {
			Debug.Log(success ? "Reported score successfully" : "Failed to report score");
		});
	}

	// for the name, use the ID number
	public void ReportAchievementProgress(string name, float progress)
	{
		// You can also call into the functions like this
		Social.ReportProgress (name, (double)progress, result => {
			if (result)
				Debug.Log ("Successfully reported achievement progress");
			else
				Debug.Log ("Failed to report achievement");
		});
	}

	public void ShowLeaderboard()
	{
		Social.ShowLeaderboardUI();
	}
	public void ShowAchievementUI()
	{
		Social.ShowAchievementsUI ();
	}
}
