using System;
using UnityEngine;
using YandexMobileAds;
using YandexMobileAds.Base;

public class AdStarter : MonoBehaviour
{
    public static AdStarter instance;
    private Interstitial interstitial;
    private void Start()
    {
        instance = this;

        if (PlayerPrefs.HasKey("Fails") && PlayerPrefs.GetInt("Fails") % 10 == 0)
        {
            AdStarter.instance.RequestInterstitial();
        }
    }
    public void RequestInterstitial()
    {
        MobileAds.SetAgeRestrictedUser(true);

        string adUnitId = "R-M-2386422-1";

        if (interstitial != null)
        {
            interstitial.Destroy();
        }

        interstitial = new Interstitial(adUnitId);

        interstitial.OnInterstitialLoaded += HandleInterstitialLoaded;
        interstitial.OnInterstitialFailedToLoad += HandleInterstitialFailedToLoad;
        interstitial.OnReturnedToApplication += HandleReturnedToApplication;
        interstitial.OnLeftApplication += HandleLeftApplication;
        interstitial.OnAdClicked += HandleAdClicked;
        interstitial.OnInterstitialShown += HandleInterstitialShown;
        interstitial.OnInterstitialDismissed += HandleInterstitialDismissed;
        interstitial.OnImpression += HandleImpression;
        interstitial.OnInterstitialFailedToShow += HandleInterstitialFailedToShow;

        interstitial.LoadAd(CreateAdRequest());
        DisplayMessage("Interstitial is requested");
    }

    private void ShowInterstitial()
    {
        interstitial.Show();
    }

    private AdRequest CreateAdRequest()
    {
        return new AdRequest.Builder().Build();
    }

    private void DisplayMessage(string message)
    {
        print(message);
    }

    #region Interstitial callback handlers

    public void HandleInterstitialLoaded(object sender, EventArgs args)
    {
        ShowInterstitial();
    }

    public void HandleInterstitialFailedToLoad(object sender, AdFailureEventArgs args)
    {
        DisplayMessage("HandleInterstitialFailedToLoad event received with message: " + args.Message);
    }

    public void HandleReturnedToApplication(object sender, EventArgs args)
    {
        DisplayMessage("HandleReturnedToApplication event received");
    }

    public void HandleLeftApplication(object sender, EventArgs args)
    {
        DisplayMessage("HandleLeftApplication event received");
    }

    public void HandleAdClicked(object sender, EventArgs args)
    {
        DisplayMessage("HandleAdClicked event received");
    }

    public void HandleInterstitialShown(object sender, EventArgs args)
    {
        DisplayMessage("HandleInterstitialShown event received");
    }

    public void HandleInterstitialDismissed(object sender, EventArgs args)
    {
        DisplayMessage("HandleInterstitialDismissed event received");
    }

    public void HandleImpression(object sender, ImpressionData impressionData)
    {
        var data = impressionData == null ? "null" : impressionData.rawData;
        DisplayMessage("HandleImpression event received with data: " + data);
    }

    public void HandleInterstitialFailedToShow(object sender, AdFailureEventArgs args)
    {
        DisplayMessage("HandleInterstitialFailedToShow event received with message: " + args.Message);
    }

    #endregion
}