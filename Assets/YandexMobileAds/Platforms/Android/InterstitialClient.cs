/*
 * This file is a part of the Yandex Advertising Network
 *
 * Version for Android (C) 2018 YANDEX
 *
 * You may not use this file except in compliance with the License.
 * You may obtain a copy of the License at https://legal.yandex.com/partner_ch/
 */

using System;
using YandexMobileAds.Base;
using YandexMobileAds.Common;
using UnityEngine;

namespace YandexMobileAds.Platforms.Android
{
    public class InterstitialClient : AndroidJavaProxy, IInterstitialClient
    {
        private AndroidJavaObject interstitial;

        public event EventHandler<EventArgs> OnInterstitialLoaded;
        public event EventHandler<AdFailureEventArgs> OnInterstitialFailedToLoad;
        public event EventHandler<EventArgs> OnReturnedToApplication;
        public event EventHandler<EventArgs> OnLeftApplication;
        public event EventHandler<EventArgs> OnAdClicked;
        public event EventHandler<EventArgs> OnInterstitialShown;
        public event EventHandler<EventArgs> OnInterstitialDismissed;
        public event EventHandler<ImpressionData> OnImpression;
        public event EventHandler<AdFailureEventArgs> OnInterstitialFailedToShow;

        public InterstitialClient(string blockId) : base(Utils.UnityInterstitialAdListenerClassName)
        {
            AndroidJavaClass playerClass = new AndroidJavaClass(Utils.UnityActivityClassName);

            AndroidJavaObject activity =
                playerClass.GetStatic<AndroidJavaObject>("currentActivity");

            interstitial = new AndroidJavaObject(
                Utils.InterstitialClassName,
                activity,
                blockId);
            interstitial.Call("setUnityInterstitialListener", this);
        }

        public void LoadAd(AdRequest request)
        {
            interstitial.Call("loadAd", Utils.GetAdRequestJavaObject(request));
        }

        public bool IsLoaded()
        {
            return interstitial.Call<bool>("isInterstitialLoaded");
        }

        public void Show()
        {
            interstitial.Call("showInterstitial");
        }

        public void Destroy()
        {
            interstitial.Call("clearUnityInterstitialListener");
            interstitial.Call("destroyInterstitial");
        }

        public void onInterstitialLoaded()
        {
            if (OnInterstitialLoaded != null)
            {
                OnInterstitialLoaded(this, EventArgs.Empty);
            }
        }

        public void onInterstitialFailedToLoad(string errorReason)
        {
            if (OnInterstitialFailedToLoad != null)
            {
                AdFailureEventArgs args = new AdFailureEventArgs()
                {
                    Message = errorReason
                };
                OnInterstitialFailedToLoad(this, args);
            }
        }

        public void onReturnedToApplication()
        {
            if (OnReturnedToApplication != null)
            {
                OnReturnedToApplication(this, EventArgs.Empty);
            }
        }

        public void onLeftApplication()
        {
            if (OnLeftApplication != null)
            {
                OnLeftApplication(this, EventArgs.Empty);
            }
        }

        public void onAdClicked()
        {
            if (OnAdClicked != null)
            {
                OnAdClicked(this, EventArgs.Empty);
            }
        }

        public void onInterstitialShown()
        {
            if (OnInterstitialShown != null)
            {
                OnInterstitialShown(this, EventArgs.Empty);
            }
        }

        public void onInterstitialDismissed()
        {
            if (OnInterstitialDismissed != null)
            {
                OnInterstitialDismissed(this, EventArgs.Empty);
            }
        }
        
        public void onImpression(string rawImpressionData)
        {
            if (OnImpression != null)
            {
                ImpressionData impressionData = new ImpressionData(rawImpressionData);
                OnImpression(this, impressionData);
            }
        }

        public void onInterstitialFailedToShow(string errorReason)
        {
            if (OnInterstitialFailedToShow != null)
            {
                AdFailureEventArgs args = new AdFailureEventArgs()
                {
                    Message = errorReason
                };
                OnInterstitialFailedToShow(this, args);
            }
        }
    }
}