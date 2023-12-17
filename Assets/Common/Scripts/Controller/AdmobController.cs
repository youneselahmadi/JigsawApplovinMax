﻿//using UnityEngine;
//using System;
//using GoogleMobileAds;
//using GoogleMobileAds.Api;

//public class AdmobController : MonoBehaviour
//{
//    private BannerView bannerView;
//    public InterstitialAd interstitial;
//    public RewardedAd rewardBasedVideo;

//    public static AdmobController instance;

//    private void Awake()
//    {
//        instance = this;
//    }

//    private void Start()
//    {
//        // Initialize the Google Mobile Ads SDK.
//        MobileAds.Initialize(initStatus => { });
//        MobileAds.SetiOSAppPauseOnBackground(true);

//        if (!CUtils.IsAdsRemoved())
//        {
//            RequestInterstitial();
//        }
//        RequestInterstitial();
//        //InitRewardedVideo();
//        RequestRewardBasedVideo();
        
//    }

//    //private void InitRewardedVideo()
//    //{
//    //    // Get singleton reward based video ad reference.
//    //    this.rewardBasedVideo = RewardBasedVideoAd.Instance;

//    //    // RewardBasedVideoAd is a singleton, so handlers should only be registered once.
//    //    this.rewardBasedVideo.OnAdLoaded += this.HandleRewardBasedVideoLoaded;
//    //    this.rewardBasedVideo.OnAdFailedToLoad += this.HandleRewardBasedVideoFailedToLoad;
//    //    this.rewardBasedVideo.OnAdOpening += this.HandleRewardBasedVideoOpened;
//    //    this.rewardBasedVideo.OnAdStarted += this.HandleRewardBasedVideoStarted;
//    //    this.rewardBasedVideo.OnAdRewarded += this.HandleRewardBasedVideoRewarded;
//    //    this.rewardBasedVideo.OnAdClosed += this.HandleRewardBasedVideoClosed;
//    //    this.rewardBasedVideo.OnAdLeavingApplication += this.HandleRewardBasedVideoLeftApplication;
//    //}

//    public void RequestBanner(string position)
//    {
//        // These ad units are configured to always serve test ads.
//#if UNITY_EDITOR
//        string adUnitId = "unused";
//#elif UNITY_ANDROID
//        string adUnitId = ConfigController.Config.admob.androidBanner.Trim();
//#elif UNITY_IPHONE
//        string adUnitId = ConfigController.Config.admob.iosBanner.Trim();
//#else
//        string adUnitId = "unexpected_platform";
//#endif

//        // Create a 320x50 banner at the top of the screen.
//        this.bannerView = new BannerView(adUnitId, AdSize.Banner,position == "Top" ? AdPosition.Top: AdPosition.Bottom);
        

//        // Register for ad events.
//        this.bannerView.OnAdLoaded += this.HandleAdLoaded;
//        this.bannerView.OnAdFailedToLoad += this.HandleAdFailedToLoad;
//        this.bannerView.OnAdOpening += this.HandleAdOpened;
//        this.bannerView.OnAdClosed += this.HandleAdClosed;

//        // Load a banner ad.
//        AdRequest request = new AdRequest.Builder().Build();
        
//        this.bannerView.LoadAd(request);
//        //this.ShowBanner();
//    }

//    public void RequestInterstitial()
//    {
//        // These ad units are configured to always serve test ads.
//#if UNITY_EDITOR
//        string adUnitId = "unused";
//#elif UNITY_ANDROID
//        string adUnitId = ConfigController.Config.admob.androidInterstitial.Trim();
//#elif UNITY_IPHONE
//        string adUnitId = ConfigController.Config.admob.iosInterstitial.Trim();
//#else
//        string adUnitId = "unexpected_platform";
//#endif

//        // Create an interstitial.
//        this.interstitial = new InterstitialAd(adUnitId);

//        // Register for ad events.
//        this.interstitial.OnAdLoaded += this.HandleInterstitialLoaded;
//        this.interstitial.OnAdFailedToLoad += this.HandleInterstitialFailedToLoad;
//        this.interstitial.OnAdOpening += this.HandleInterstitialOpened;
//        this.interstitial.OnAdClosed += this.HandleInterstitialClosed;

//        // Load an interstitial ad.
       

//        AdRequest request = new AdRequest.Builder().Build();
//        this.interstitial.LoadAd(request);
//    }

//    public void RequestRewardBasedVideo()
//    {
//#if UNITY_EDITOR
//        string adUnitId = "unused";
//#elif UNITY_ANDROID
//        string adUnitId = ConfigController.Config.admob.androidRewarded.Trim();
//#elif UNITY_IPHONE
//        string adUnitId = ConfigController.Config.admob.iosRewarded.Trim();
//#else
//        string adUnitId = "unexpected_platform";
//#endif
//        rewardBasedVideo = new RewardedAd(adUnitId);

//        AdRequest request = new AdRequest.Builder().Build();

//        rewardBasedVideo.LoadAd(request);
//        //this.rewardBasedVideo.LoadAd(this.CreateAdRequest(), adUnitId);

//        // RewardBasedVideoAd is a singleton, so handlers should only be registered once.
//        this.rewardBasedVideo.OnAdLoaded += this.HandleRewardBasedVideoLoaded;
//        this.rewardBasedVideo.OnAdFailedToLoad += this.HandleRewardBasedVideoFailedToLoad;
//        this.rewardBasedVideo.OnAdOpening += this.HandleRewardBasedVideoOpened;
//        this.rewardBasedVideo.OnUserEarnedReward += this.HandleRewardBasedVideoRewarded;
//        this.rewardBasedVideo.OnAdClosed += this.HandleRewardBasedVideoClosed;
        
//    }

//    // Returns an ad request with custom ad targeting.
//    //private AdRequest CreateAdRequest()
//    //{
//    //    return new AdRequest.Builder()
//    //            .AddTestDevice(AdRequest.TestDeviceSimulator)
//    //            .AddTestDevice("0123456789ABCDEF0123456789ABCDEF")
//    //            .AddKeyword("game")
//    //            .TagForChildDirectedTreatment(false)
//    //            .AddExtra("color_bg", "9B30FF")
//    //            .Build();
//    //}

//    public void ShowInterstitial(InterstitialAd ad)
//    {
//        if (ad != null && ad.IsLoaded())
//        {
//            ad.Show();
//        }
//    }

//    public void ShowBanner()
//    {
       
//        if (bannerView != null)
//        {
//            bannerView.Show();
//        }
//    }

//    public void HideBanner()
//    {
//        if (bannerView != null)
//        {
//            Debug.Log("hid banner !!!!!!!!!!!");
//            bannerView.Hide();
//            bannerView.Destroy();
//        }
//    }
//    public void ShowInterstitialAd()
//    {
//        if (interstitial != null && interstitial.IsLoaded())
//        {
//            interstitial.Show();
          
//        }
       
//    }
//    public bool ShowInterstitial()
//    {
//        if (interstitial != null && interstitial.IsLoaded())
//        {
//            interstitial.Show();
//            return true;
//        }
//        return false;
//    }

//    public void ShowRewardBasedVideo()
//    {
//        if (this.rewardBasedVideo.IsLoaded())
//        {
//            this.rewardBasedVideo.Show();
//        }
//        else
//        {
//            MonoBehaviour.print("Reward based video ad is not ready yet");
//        }
//    }

//    #region Banner callback handlers

//    public void HandleAdLoaded(object sender, EventArgs args)
//    {
//        print("HandleAdLoaded event received.");
//    }

//    private void HandleAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
//    {
//        Debug.Log("Ad Failed to Load: " + args.LoadAdError);
//    }

//    public void HandleAdOpened(object sender, EventArgs args)
//    {
//        print("HandleAdOpened event received");
//    }

//    public void HandleAdClosed(object sender, EventArgs args)
//    {
//        print("HandleAdClosed event received");
//    }

//    public void HandleAdLeftApplication(object sender, EventArgs args)
//    {
//        print("HandleAdLeftApplication event received");
//    }

//    #endregion

//    #region Interstitial callback handlers

//    public void HandleInterstitialLoaded(object sender, EventArgs args)
//    {
//        print("HandleInterstitialLoaded event received.");
//    }

//    public void HandleInterstitialFailedToLoad(object sender, AdFailedToLoadEventArgs args)
//    {
//        print("HandleInterstitialFailedToLoad event received with message: " + args.LoadAdError);
//    }

//    public void HandleInterstitialOpened(object sender, EventArgs args)
//    {
//        print("HandleInterstitialOpened event received");
//    }

//    public void HandleInterstitialClosed(object sender, EventArgs args)
//    {
//        print("HandleInterstitialClosed event received");
//        RequestInterstitial();
//    }

//    public void HandleInterstitialLeftApplication(object sender, EventArgs args)
//    {
//        print("HandleInterstitialLeftApplication event received");
//    }

//    #endregion

//    #region RewardBasedVideo callback handlers

//    public void HandleRewardBasedVideoLoaded(object sender, EventArgs args)
//    {
//        MonoBehaviour.print("HandleRewardBasedVideoLoaded event received");
//    }

//    public void HandleRewardBasedVideoFailedToLoad(object sender, AdFailedToLoadEventArgs args)
//    {
//        MonoBehaviour.print(
//            "HandleRewardBasedVideoFailedToLoad event received with message: " + args.LoadAdError);
//    }

//    public void HandleRewardBasedVideoOpened(object sender, EventArgs args)
//    {
//        MonoBehaviour.print("HandleRewardBasedVideoOpened event received");
//    }

//    public void HandleRewardBasedVideoStarted(object sender, EventArgs args)
//    {
//        MonoBehaviour.print("HandleRewardBasedVideoStarted event received");
//    }

//    public void HandleRewardBasedVideoClosed(object sender, EventArgs args)
//    {
//        RequestRewardBasedVideo();
//        MonoBehaviour.print("HandleRewardBasedVideoClosed event received");
//    }

//    public void HandleRewardBasedVideoRewarded(object sender, Reward args)
//    {
//        string type = args.Type;
//        double amount = args.Amount;
//        MonoBehaviour.print(
//            "HandleRewardBasedVideoRewarded event received for " + amount.ToString() + " " + type);
//    }

//    public void HandleRewardBasedVideoLeftApplication(object sender, EventArgs args)
//    {
//        MonoBehaviour.print("HandleRewardBasedVideoLeftApplication event received");
//    }

//    #endregion
//}
