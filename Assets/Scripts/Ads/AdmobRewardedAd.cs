using UnityEngine;
using GoogleMobileAds.Api;
using System;
using System.IO;

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
            loadAd();
        });
       
    }

    private void loadAd()
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

    public Boolean ShowRewardedAds()
    {
        if (rewardedAd != null && rewardedAd.CanShowAd())
        {
            rewardedAd.Show((Reward reward) => { });
            return true;
        }
        else {
            return false;
        }
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
            adsCallbacks.OnRewardedAdsClose();
            loadAd();
         
        };
        // Raised when the ad failed to open full screen content.
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            loadAd();
        };
    }
}