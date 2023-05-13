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
    public class RewardedAdClient : AndroidJavaProxy, IRewardedAdClient
    {
        private AndroidJavaObject rewardedAd;
        
        public event EventHandler<EventArgs> OnRewardedAdLoaded;
        public event EventHandler<AdFailureEventArgs> OnRewardedAdFailedToLoad;
        public event EventHandler<EventArgs> OnReturnedToApplication;
        public event EventHandler<EventArgs> OnLeftApplication;
        public event EventHandler<EventArgs> OnAdClicked;
        public event EventHandler<EventArgs> OnRewardedAdShown;
        public event EventHandler<EventArgs> OnRewardedAdDismissed;
        public event EventHandler<ImpressionData> OnImpression;
        public event EventHandler<AdFailureEventArgs> OnRewardedAdFailedToShow;
        public event EventHandler<Reward> OnRewarded;

        public RewardedAdClient(string blockId) : base(Utils.UnityRewardedAdListenerClassName)
        {
            AndroidJavaClass playerClass = new AndroidJavaClass(Utils.UnityActivityClassName);

            AndroidJavaObject activity =
                playerClass.GetStatic<AndroidJavaObject>("currentActivity");

            rewardedAd = new AndroidJavaObject(
                Utils.RewardedAdClassName,
                activity,
                blockId);
            rewardedAd.Call("setUnityRewardedAdListener", this);
        }

        public void LoadAd(AdRequest request)
        {
            rewardedAd.Call("loadAd", Utils.GetAdRequestJavaObject(request));
        }

        public bool IsLoaded()
        {
            return rewardedAd.Call<bool>("isRewardedAdLoaded");
        }

        public void Show()
        {
            rewardedAd.Call("showRewardedAd");
        }

        public void Destroy()
        {
            rewardedAd.Call("clearUnityRewardedAdListener");
            rewardedAd.Call("destroyRewardedAd");
        }

        public void onRewardedAdLoaded()
        {
            if (OnRewardedAdLoaded != null)
            {
                OnRewardedAdLoaded(this, EventArgs.Empty);
            }
        }

        public void onRewardedAdFailedToLoad(string errorReason)
        {
            if (OnRewardedAdFailedToLoad != null)
            {
                AdFailureEventArgs args = new AdFailureEventArgs()
                {
                    Message = errorReason
                };
                OnRewardedAdFailedToLoad(this, args);
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

        public void onRewardedAdShown()
        {
            if (OnRewardedAdShown != null)
            {
                OnRewardedAdShown(this, EventArgs.Empty);
            }
        }

        public void onRewardedAdDismissed()
        {
            if (OnRewardedAdDismissed != null)
            {
                OnRewardedAdDismissed(this, EventArgs.Empty);
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

        public void onRewardedAdFailedToShow(string errorReason)
        {
            if (OnRewardedAdFailedToShow != null)
            {
                AdFailureEventArgs args = new AdFailureEventArgs()
                {
                    Message = errorReason
                };
                OnRewardedAdFailedToShow(this, args);
            }
        }

        public void onRewarded(int amount, string type)
        {
            if (OnRewarded != null)
            {
                Reward reward = new Reward(amount, type);
                OnRewarded(this, reward);
            }
        }
    }
}