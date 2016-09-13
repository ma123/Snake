using UnityEngine;
using System.Collections;
using GoogleMobileAds;
using GoogleMobileAds.Api;

/*
 * Download the package here - https://github.com/googleads/googleads-mobile-unity/releases
 * Get the Ad Units from here - https://www.google.com/admob/
*/

public class AdManager : MonoBehaviour 
{
	/*#if CW_Admob
	#if UNITY_ANDROID
	public string BannerAdUnitId = "INSERT_ANDROID_BANNER_AD_UNIT_ID_HERE";
	#elif UNITY_IPHONE
	public string BannerAdUnitId = "INSERT_IOS_BANNER_AD_UNIT_ID_HERE";
	#else
	public const string BannerAdUnitId = "unexpected_platform";
	#endif*/

	#if UNITY_ANDROID
	public string InterstitialAdUnitId = "ca-app-pub-1882232042439946/2875082311";
	#elif UNITY_IPHONE
	public string InterstitialAdUnitId = "INSERT_IOS_INTERSTITIAL_AD_UNIT_ID_HERE";
	#else
	public const string InterstitialAdUnitId = "unexpected_platform";
	#endif

	//public int AdCounter;

	//private int c;

	//internal BannerView bannerView;
	private InterstitialAd interstitial;

	private void Awake()
	{
		//RequestBanner();
		RequestInterstitial();
	}

	/*private void Start()
	{
		c=AdCounter;
	}*/

	/*private void RequestBanner()
	{
		// Create a 320x50 banner at the top of the screen.
		bannerView = new BannerView(BannerAdUnitId, AdSize.Banner, AdPosition.Bottom);
	
		// Create an empty ad request.
		AdRequest request = new AdRequest.Builder().Build();

		// Load the banner with the request.
		bannerView.LoadAd(request);
	}*/

	private void RequestInterstitial()
	{
		// Initialize an InterstitialAd.
		interstitial = new InterstitialAd(InterstitialAdUnitId);

		// Create an empty ad request.
		AdRequest request = new AdRequest.Builder().Build();

		// Load the interstitial with the request.
		interstitial.LoadAd(request);
	}

	public void ShowInterstitial() {
		//if (AdCounter <= 0) {
			if (interstitial.IsLoaded ()) {
				interstitial.Show ();
				RequestInterstitial ();
				//AdCounter = c;
			} else {
				print ("Interstitial is not ready yet.");
			}
		/*} else {
			if (AdCounter > 0) {
				AdCounter -= 1;
			}
		}*/
	}


	void OnDisable() {
		//bannerView.Destroy();
		interstitial.Destroy();
	}
	//#endif
}