using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardedVideoButton : MonoBehaviour
{

    public void OnClick()
    {
        //if (IsAvailableToShow())
        //{
//#if UNITY_EDITOR
//        ApplovinMaxManager.Instance.ShowRewardAd();
//        // Superpow.Utils.RewardVideoAd();
//#else
            ApplovinMaxManager.Instance.OnCustomRewardedVideoCompleted += HandleRewardBasedVideoRewarded;
            ApplovinMaxManager.Instance.ShowRewardAd();
             
//#endif
        //}
        //else
        //{
        //    Toast.instance.ShowMessage("Ad is not available at the moment, please wait..");
        //}

        Sound.instance.PlayButton();
    }
    public void HandleRewardBasedVideoRewarded(int sender, string args)
    {
        Superpow.Utils.RewardVideoAd();
        ApplovinMaxManager.Instance.OnCustomRewardedVideoCompleted -= HandleRewardBasedVideoRewarded;
    }
    //public bool IsAvailableToShow()
    //{
    //    return IsActionAvailable() && IsAdAvailable();
    //}

    //private bool IsActionAvailable()
    //{
    //    return CUtils.IsActionAvailable("rewarded_video", ConfigController.Config.rewardedVideoPeriod);
    //}

    //private bool IsAdAvailable()
    //{
    //    if (AdmobController.instance.rewardBasedVideo == null) return false;
    //    bool isLoaded = AdmobController.instance.rewardBasedVideo.IsLoaded();
    //    return isLoaded;
    //}
  

  
}
