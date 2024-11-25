using UnityEngine;
using GoogleMobileAds.Api;
using System;
using System.IO;
using static UnityEngine.Rendering.DebugUI;

public class AdmobRewardedAd
{


#if UNITY_ANDROID
    private string adUnitId = "ca-app-pub-3940256099942544/5224354917";
#elif UNITY_IPHONE
  private string adUnitId = "ca-app-pub-3940256099942544/1712485313";
#else
  private string adUnitId = "unused";
#endif


    private RewardedAd rewardedAd;
    private AdsEventCallback adsCallbacks;
 

    public AdmobRewardedAd()
    {
        MobileAds.RaiseAdEventsOnUnityMainThread = true;
        MobileAds.Initialize(initStatus => {
            LoadAd();
        });
       
    }

    public void LoadAd()
    {
            if (rewardedAd != null)
            {
                rewardedAd.Destroy();
                rewardedAd = null;
            }
 
            var adRequest = new AdRequest();

            RewardedAd.Load(adUnitId, adRequest,
                (RewardedAd ad, LoadAdError error) =>
                {
                    if (error != null || ad == null)
                    {
                        return;
                    }
                    rewardedAd = ad;
                    RegisterEventHandlers(rewardedAd);
                });

    }




    public void SetAdsCallback(AdsEventCallback callbacks)
    {
        this.adsCallbacks = callbacks;
    }

    public Boolean CanShowRewardedAds()
    {
        if (rewardedAd != null && rewardedAd.CanShowAd())
        {
            return true;
        }
        else {
            return false;
        }
    }

    public void ShowAd() {
        rewardedAd.Show((Reward reward) => {});
    }


    private void RegisterEventHandlers(RewardedAd ad)
    {
        // Raised when the ad is estimated to have earned money.
        ad.OnAdPaid += (AdValue adValue) =>
        {
          
        };
        // Raised when an impression is recorded for an ad.
        ad.OnAdImpressionRecorded += () =>
        {
            
        };
        // Raised when a click is recorded for an ad.
        ad.OnAdClicked += () =>
        {
            
        };
        // Raised when an ad opened full screen content.
        ad.OnAdFullScreenContentOpened += () =>
        {
           
        };
        // Raised when the ad closed full screen content.
        ad.OnAdFullScreenContentClosed += () =>
        {
            adsCallbacks.OnRewardedAdsClose(ad.GetRewardItem().Amount);
            LoadAd();
         
        };
        // Raised when the ad failed to open full screen content.
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            LoadAd();
        };
    }

    public void Destroy()
    {
        if (rewardedAd != null)
        {
            rewardedAd.Destroy();
        }
    }
}