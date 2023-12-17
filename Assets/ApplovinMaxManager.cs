using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplovinMaxManager : MonoBehaviour
{
    public static ApplovinMaxManager Instance;
    // Event handler for rewarded video completion
    // Custom event for rewarded video completion
    public  event System.Action<int, string> OnCustomRewardedVideoCompleted;
    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance == null )
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    [Header("Applovin Max Setting")]
    public  string MaxSdkKey = "MEwu0ChhqJR7GwPNFUyvbPWkxi7pUav2xCkAFaNfVRBLlCFTQKHNZVucbI1ls3ixzm6f84U7SXThnAJ-3PhvVg";

	public  string InterstitialAdUnitId = "282d3d6523d4565c";
    public  string RewardedAdUnitId = "65360eb536036427";
    public  string RewardedInterstitialAdUnitId = "ENTER_ANDROID_REWARD_INTER_AD_UNIT_ID_HERE";
    public  string BannerAdUnitId = "63f7e0a1790a58b4";
    public  string MRecAdUnitId = "ENTER_ANDROID_MREC_AD_UNIT_ID_HERE";
    public string AppOpenAdUnitId = "YOUR_ANDROID_AD_UNIT_ID";



    private int interstitialRetryAttempt;
	private int rewardedRetryAttempt;
	private int rewardedInterstitialRetryAttempt;


    [Header("Adjust UI position")]
    public GameObject BottomNavigation ;
    public GameObject ColorList ;
    private int bannerHeight;
    // Start is called before the first frame update
    void Start()
    {

		MaxSdkCallbacks.OnSdkInitializedEvent += sdkConfiguration =>
		{
			// AppLovin SDK is initialized, configure and start loading ads.
			Debug.Log("MAX SDK Initialized");
            MaxSdkCallbacks.AppOpen.OnAdHiddenEvent += OnAppOpenDismissedEvent;

            ShowAdIfReady();
            InitializeInterstitialAds();
            InitializeRewardedAds();
            InitializeBannerAds("top");


        };

		MaxSdk.SetSdkKey(MaxSdkKey);
		MaxSdk.InitializeSdk();
        
	}
    #region Open app Ad Methods
    public void OnAppOpenDismissedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        MaxSdk.LoadAppOpenAd(AppOpenAdUnitId);
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        if (!pauseStatus)
        {
            ShowAdIfReady();
        }
    }
    public void ShowAdIfReady()
    {
        if (MaxSdk.IsAppOpenAdReady(AppOpenAdUnitId))
        {
            MaxSdk.ShowAppOpenAd(AppOpenAdUnitId);
        }
        else
        {
            MaxSdk.LoadAppOpenAd(AppOpenAdUnitId);
        }
    }
    #endregion

    #region Banner Ad Methods

    public void InitializeBannerAds(string position)
    {
        MaxSdkCallbacks.Banner.OnAdLoadedEvent += OnBannerAdLoadedEvent;
        MaxSdkCallbacks.Banner.OnAdLoadFailedEvent += OnBannerAdLoadFailedEvent;
        MaxSdkCallbacks.Banner.OnAdClickedEvent += OnBannerAdClickedEvent;
        MaxSdkCallbacks.Banner.OnAdRevenuePaidEvent += OnBannerAdRevenuePaidEvent;
        MaxSdkCallbacks.Banner.OnAdExpandedEvent += OnBannerAdExpandedEvent;
        MaxSdkCallbacks.Banner.OnAdCollapsedEvent += OnBannerAdCollapsedEvent;

        // Banners are automatically sized to 320x50 on phones and 728x90 on tablets.
        // You may use the utility method `MaxSdkUtils.isTablet()` to help with view sizing adjustments.
        if (position == "top")
        {
            MaxSdk.CreateBanner(BannerAdUnitId, MaxSdkBase.BannerPosition.TopCenter);
        }
        else
        {
            MaxSdk.CreateBanner(BannerAdUnitId, MaxSdkBase.BannerPosition.BottomCenter);
        }
       

        // Set background or background color for banners to be fully functional.
        MaxSdk.SetBannerBackgroundColor(BannerAdUnitId, Color.clear);

        MaxSdk.SetBannerWidth(BannerAdUnitId, 320);
        MaxSdk.ShowBanner(BannerAdUnitId);
       
        




    }
    /// <summary>
    /// Shows the banner ad
    /// </summary>
    public void ShowBannerAd()
    {
       
        MaxSdk.ShowBanner(BannerAdUnitId);
        Debug.Log("show Banner");
    }

    /// <summary>
    /// Hides the banner ad
    /// </summary>
    public void HideBannerAd()
    {
        MaxSdk.HideBanner(BannerAdUnitId);
        Debug.Log("Hide Banner");
    } 
    public void DestroyBannerAd()
    {
        MaxSdk.DestroyBanner(BannerAdUnitId);
        Debug.Log("Hide Banner");
    }

    private void OnBannerAdLoadedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo) {
        // Banner ad loaded successfully, get its height
        Debug.Log("Banner ad loaded");
       
    }

    private void OnBannerAdLoadFailedEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo) { }

    private void OnBannerAdClickedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo) {
        Debug.Log("Banner ad clicked");
    }

    private void OnBannerAdRevenuePaidEvent(string adUnitId, MaxSdkBase.AdInfo adInfo) {
        // Banner ad revenue paid. Use this callback to track user revenue.
        Debug.Log("Banner ad revenue paid");

        // Ad revenue
        double revenue = adInfo.Revenue;

        // Miscellaneous data
        string countryCode = MaxSdk.GetSdkConfiguration().CountryCode; // "US" for the United States, etc - Note: Do not confuse this with currency code which is "USD"!
        string networkName = adInfo.NetworkName; // Display name of the network that showed the ad (e.g. "AdColony")
        string adUnitIdentifier = adInfo.AdUnitIdentifier; // The MAX Ad Unit ID
        string placement = adInfo.Placement; // The placement this ad's postbacks are tied to
    }

    private void OnBannerAdExpandedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo) { }

    private void OnBannerAdCollapsedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo) { }
    

    private void OnBannerAdFailedEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo)
    {
        // Banner ad failed to load. MAX will automatically try loading a new ad internally.
        Debug.Log("Banner ad failed to load with error code: " + errorInfo.Code);
    }


   
    #endregion



    #region Interstitial Ad Methods

    private void InitializeInterstitialAds()
    {
        // Attach callbacks
        MaxSdkCallbacks.Interstitial.OnAdLoadedEvent += OnInterstitialLoadedEvent;
        MaxSdkCallbacks.Interstitial.OnAdLoadFailedEvent += OnInterstitialFailedEvent;
        MaxSdkCallbacks.Interstitial.OnAdDisplayFailedEvent += InterstitialFailedToDisplayEvent;
        MaxSdkCallbacks.Interstitial.OnAdHiddenEvent += OnInterstitialDismissedEvent;
        MaxSdkCallbacks.Interstitial.OnAdRevenuePaidEvent += OnInterstitialRevenuePaidEvent;

        // Load the first interstitial
        LoadInterstitial();
    }

    void LoadInterstitial()
    {
        MaxSdk.LoadInterstitial(InterstitialAdUnitId);
    }

    public void ShowInterstitialAd()
    {
        if (MaxSdk.IsInterstitialReady(InterstitialAdUnitId))
        {
            MaxSdk.ShowInterstitial(InterstitialAdUnitId);
        }
        else
        {
            Debug.Log("Ad not ready");
        }
    } 
    public bool ShowInterstitialAd(string param)
    {
        if (MaxSdk.IsInterstitialReady(InterstitialAdUnitId))
        {
            
            MaxSdk.ShowInterstitial(InterstitialAdUnitId);
            return true;
        }
        else
        {
           
            Debug.Log("Ad not ready");
            return false;
        }
    }

    private void OnInterstitialLoadedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        // Interstitial ad is ready to be shown. MaxSdk.IsInterstitialReady(interstitialAdUnitId) will now return 'true'
        Debug.Log("Interstitial loaded");

        // Reset retry attempt
        interstitialRetryAttempt = 0;
    }

    private void OnInterstitialFailedEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo)
    {
        // Interstitial ad failed to load. We recommend retrying with exponentially higher delays up to a maximum delay (in this case 64 seconds).
        interstitialRetryAttempt++;
        double retryDelay = Math.Pow(2, Math.Min(6, interstitialRetryAttempt));

        Debug.Log("Load failed: " + errorInfo.Code + "\nRetrying in " + retryDelay + "s...");
        Debug.Log("Interstitial failed to load with error code: " + errorInfo.Code);

        Invoke("LoadInterstitial", (float)retryDelay);
    }

    private void InterstitialFailedToDisplayEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo, MaxSdkBase.AdInfo adInfo)
    {
        // Interstitial ad failed to display. We recommend loading the next ad
        Debug.Log("Interstitial failed to display with error code: " + errorInfo.Code);
        LoadInterstitial();
    }

    private void OnInterstitialDismissedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        // Interstitial ad is hidden. Pre-load the next ad
        Debug.Log("Interstitial dismissed");
        LoadInterstitial();
    }

    private void OnInterstitialRevenuePaidEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        // Interstitial ad revenue paid. Use this callback to track user revenue.
        Debug.Log("Interstitial revenue paid");

        // Ad revenue
        double revenue = adInfo.Revenue;

        // Miscellaneous data
        string countryCode = MaxSdk.GetSdkConfiguration().CountryCode; // "US" for the United States, etc - Note: Do not confuse this with currency code which is "USD"!
        string networkName = adInfo.NetworkName; // Display name of the network that showed the ad (e.g. "AdColony")
        string adUnitIdentifier = adInfo.AdUnitIdentifier; // The MAX Ad Unit ID
        string placement = adInfo.Placement; // The placement this ad's postbacks are tied to

       
    }

    #endregion



    #region Rewarded Ad Methods

    private void InitializeRewardedAds()
    {
        // Attach callbacks
        MaxSdkCallbacks.Rewarded.OnAdLoadedEvent += OnRewardedAdLoadedEvent;
        MaxSdkCallbacks.Rewarded.OnAdLoadFailedEvent += OnRewardedAdFailedEvent;
        MaxSdkCallbacks.Rewarded.OnAdDisplayFailedEvent += OnRewardedAdFailedToDisplayEvent;
        MaxSdkCallbacks.Rewarded.OnAdDisplayedEvent += OnRewardedAdDisplayedEvent;
        MaxSdkCallbacks.Rewarded.OnAdClickedEvent += OnRewardedAdClickedEvent;
        MaxSdkCallbacks.Rewarded.OnAdHiddenEvent += OnRewardedAdDismissedEvent;
        MaxSdkCallbacks.Rewarded.OnAdReceivedRewardEvent += OnRewardedAdReceivedRewardEvent;
        MaxSdkCallbacks.Rewarded.OnAdRevenuePaidEvent += OnRewardedAdRevenuePaidEvent;
        // Register a callback for rewarded ad completion
       

        // Load the first RewardedAd
        LoadRewardedAd();
    }

    private void LoadRewardedAd()
    {
        Debug.Log("Loading...");
        MaxSdk.LoadRewardedAd(RewardedAdUnitId);
    }

    public void ShowRewardAd()
    {
        if (MaxSdk.IsRewardedAdReady(RewardedAdUnitId))
        {
            Debug.Log("Showing");
            MaxSdk.ShowRewardedAd(RewardedAdUnitId);
        }
        else
        {
            Debug.Log("Ad not ready");
        }
    }

    private void OnRewardedAdLoadedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        // Rewarded ad is ready to be shown. MaxSdk.IsRewardedAdReady(rewardedAdUnitId) will now return 'true'
        Debug.Log("Rewarded ad loaded");

        // Reset retry attempt
        rewardedRetryAttempt = 0;
    }

    private void OnRewardedAdFailedEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo)
    {
        // Rewarded ad failed to load. We recommend retrying with exponentially higher delays up to a maximum delay (in this case 64 seconds).
        rewardedRetryAttempt++;
        double retryDelay = Math.Pow(2, Math.Min(6, rewardedRetryAttempt));

        Debug.Log("Rewarded ad failed to load with error code: " + errorInfo.Code);

        Invoke("LoadRewardedAd", (float)retryDelay);
    }

    private void OnRewardedAdFailedToDisplayEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo, MaxSdkBase.AdInfo adInfo)
    {
        // Rewarded ad failed to display. We recommend loading the next ad
        Debug.Log("Rewarded ad failed to display with error code: " + errorInfo.Code);
        LoadRewardedAd();
    }

    private void OnRewardedAdDisplayedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        Debug.Log("Rewarded ad displayed");
    }

    private void OnRewardedAdClickedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        Debug.Log("Rewarded ad clicked");
    }

    private void OnRewardedAdDismissedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        // Rewarded ad is hidden. Pre-load the next ad
        Debug.Log("Rewarded ad dismissed");
        LoadRewardedAd();
    }

    private void OnRewardedAdReceivedRewardEvent(string adUnitId, MaxSdk.Reward reward, MaxSdkBase.AdInfo adInfo)
    {
        // Rewarded ad was displayed and user should receive the reward
        Debug.Log("Rewarded ad received rewardaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
        // Invoke the custom event with the relevant data
        OnCustomRewardedVideoCompleted?.Invoke(reward.Amount, reward.Label);
    }

    private void OnRewardedAdRevenuePaidEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        // Rewarded ad revenue paid. Use this callback to track user revenue.
        Debug.Log("Rewarded ad revenue paid");

        // Ad revenue
        double revenue = adInfo.Revenue;

        // Miscellaneous data
        string countryCode = MaxSdk.GetSdkConfiguration().CountryCode; // "US" for the United States, etc - Note: Do not confuse this with currency code which is "USD"!
        string networkName = adInfo.NetworkName; // Display name of the network that showed the ad (e.g. "AdColony")
        string adUnitIdentifier = adInfo.AdUnitIdentifier; // The MAX Ad Unit ID
        string placement = adInfo.Placement; // The placement this ad's postbacks are tied to

       
    }

    #endregion
  

}
