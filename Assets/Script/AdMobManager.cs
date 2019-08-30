
using System;
using UnityEngine;
using GoogleMobileAds.Api;

public class AdMobManager : MonoBehaviour
{
    private BannerView banner;
    private RewardBasedVideoAd rewardBasedVideo;

    public void Start()
    {
#if UNITY_ANDROID
        string appId = "ca-app-pub-8988512880714262~4909913308";
#elif UNITY_IPHONE
            string appId = "ca-app-pub-3940256099942544~1458002511";
#else
            string appId = "unexpected_platform";
#endif
        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(appId);
   
        this.RequestBanner();



        this.rewardBasedVideo = RewardBasedVideoAd.Instance;

        // Called when an ad request has successfully loaded.
        rewardBasedVideo.OnAdLoaded += HandleRewardBasedVideoLoaded;
        // Called when an ad request failed to load.
        rewardBasedVideo.OnAdFailedToLoad += HandleRewardBasedVideoFailedToLoad;
        // Called when an ad is shown.
        rewardBasedVideo.OnAdOpening += HandleRewardBasedVideoOpened;
        // Called when the ad starts to play.
        rewardBasedVideo.OnAdStarted += HandleRewardBasedVideoStarted;
        // Called when the user should be rewarded for watching a video.
        rewardBasedVideo.OnAdRewarded += HandleRewardBasedVideoRewarded;
        // Called when the ad is closed.
        rewardBasedVideo.OnAdClosed += HandleRewardBasedVideoClosed;
        // Called when the ad click caused the user to leave the application.
        rewardBasedVideo.OnAdLeavingApplication += HandleRewardBasedVideoLeftApplication;

        this.RequestRewardBasedVideo();
    }

    private void RequestRewardBasedVideo()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-3940256099942544/5224354917";
#elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-3940256099942544/1712485313";
#else
            string adUnitId = "unexpected_platform";
#endif

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded video ad with the request.
        this.rewardBasedVideo.LoadAd(request, adUnitId);
    }

    private void RequestBanner()
    {
#if UNITY_ANDROID
        string AdUnitID = "ca-app-pub-3940256099942544/6300978111"; //테스트 아이디
#else
        string AdUnitID = "unDefind";
#endif

        banner = new BannerView(AdUnitID, AdSize.Banner, AdPosition.Bottom);

        // Called when an ad request has successfully loaded.
        banner.OnAdLoaded += HandleOnAdLoaded_banner;
        // Called when an ad request failed to load.
        banner.OnAdFailedToLoad += HandleOnAdFailedToLoad_banner;
        // Called when an ad is clicked.
        banner.OnAdOpening += HandleOnAdOpened_banner;
        // Called when the user returned from the app after an ad click.
        banner.OnAdClosed += HandleOnAdClosed_banner;
        // Called when the ad click caused the user to leave the application.
        banner.OnAdLeavingApplication += HandleOnAdLeavingApplication_banner;

        AdRequest request = new AdRequest.Builder().Build();

        banner.LoadAd(request);
        
    }

    public void ShowBanner()
    {
        banner.Show();
    }

    public void HideBanner()
    {
        banner.Hide();
    }

    public void HandleOnAdLoaded_banner(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLoaded event received_banner");
        
        
    }

    public void HandleOnAdFailedToLoad_banner(object sender, AdFailedToLoadEventArgs args)
    {
        MonoBehaviour.print("HandleFailedToReceiveAd_banner event received with message: "
                            + args.Message);
    }

    public void HandleOnAdOpened_banner(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdOpened event received_banner");
    }

    public void HandleOnAdClosed_banner(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdClosed event received_banner");
    }

    public void HandleOnAdLeavingApplication_banner(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLeavingApplication event received_banner");
    }


    // 보상형
    public void HandleRewardBasedVideoLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoLoaded event received");
    }

    public void HandleRewardBasedVideoFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        MonoBehaviour.print(
            "HandleRewardBasedVideoFailedToLoad event received with message: "
                             + args.Message);
    }

    public void HandleRewardBasedVideoOpened(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoOpened event received");
    }

    public void HandleRewardBasedVideoStarted(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoStarted event received");
    }

    public void HandleRewardBasedVideoClosed(object sender, EventArgs args)
    {
        this.RequestRewardBasedVideo();
        MonoBehaviour.print("HandleRewardBasedVideoClosed event received");
    }

    public void HandleRewardBasedVideoRewarded(object sender, Reward args)
    {
        string type = args.Type;
        double amount = args.Amount;
        MonoBehaviour.print(
            "HandleRewardBasedVideoRewarded event received for "
                        + amount.ToString() + " " + type);
    }

    public void HandleRewardBasedVideoLeftApplication(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoLeftApplication event received");
    }

    public void UserOptToWatchAd()
    {
        if (GetComponent<TimeManager>().rewardTimeChk() && rewardBasedVideo.IsLoaded())
        {
            rewardBasedVideo.Show();
            GetComponent<TimeManager>().setRewardTime();
        }
    }
}
