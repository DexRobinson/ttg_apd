using UnityEngine;
using System.Collections;

public class AdManager : MonoBehaviour {

	//private ADBannerView banner = null;
	private int bannerIsEnabled; // this is used to check if ads can be run using this...

	//private ADInterstitialAd ad = null;
	private bool isTablet;


	void Start()
	{
		/*if (Application.platform == RuntimePlatform.IPhonePlayer) {
			bannerIsEnabled = PlayerPrefs.GetInt ("bannerIsEnabled", 0);

			if (iPhone.generation == iPhoneGeneration.iPad1Gen || iPhone.generation == iPhoneGeneration.iPad2Gen || iPhone.generation == iPhoneGeneration.iPad3Gen || iPhone.generation == iPhoneGeneration.iPad4Gen ||
			    iPhone.generation == iPhoneGeneration.iPad5Gen || iPhone.generation == iPhoneGeneration.iPadMini1Gen || iPhone.generation == iPhoneGeneration.iPadMini2Gen)
			{
				isTablet = true;
				
				ad = new ADInterstitialAd();
				ADInterstitialAd.onInterstitialWasLoaded += OnFullScreenLoaded;
			}

			banner = new ADBannerView (ADBannerView.Type.Banner, ADBannerView.Layout.Top);
			ADBannerView.onBannerWasClicked += OnBannerClicked;
			ADBannerView.onBannerWasLoaded += OnBannerLoaded;

		}*/
	}

   	void OnFullScreenLoaded()
   	{
		//if(isTablet)
			//ad.Show();
	}

	void OnBannerClicked()
	{
		Debug.Log("Clicked!\n");
	}

	void OnBannerLoaded()
	{
		if (bannerIsEnabled == 0) 
		{
			ShowBanner();
		}
	}

	// hide the ad banner
	public void HideBanner()
	{
		//banner.visible = false;
	}
	// display the ad banner
	public void ShowBanner()
	{
		if(bannerIsEnabled == 1)
		{
			//banner.visible = true;
		}
	}

	// this is for when you unlock ads
	public void UnlockAds()
	{
		PlayerPrefs.SetInt ("bannerIsEnabled", 1);
		HideBanner();
	}
}
