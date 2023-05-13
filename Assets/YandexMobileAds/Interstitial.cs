/*
 * This file is a part of the Yandex Advertising Network
 *
 * Version for Unity (C) 2018 YANDEX
 *
 * You may not use this file except in compliance with the License.
 * You may obtain a copy of the License at https://legal.yandex.com/partner_ch/
 */

using System;
using YandexMobileAds.Base;
using YandexMobileAds.Common;
using YandexMobileAds.Platforms;

namespace YandexMobileAds
{
    /// <summary>
    /// A class for loading an interstitial ad.
    /// </summary>
    public class Interstitial
    {
        private AdRequestCreator adRequestFactory;
        private IInterstitialClient client;
        private volatile bool loaded;

        /// <summary>
        /// Notifies that the ad loaded successfully.
        /// </summary>
        public event EventHandler<EventArgs> OnInterstitialLoaded;

        /// <summary>
        /// Notifies that the ad failed to load.
        /// </summary>
        public event EventHandler<AdFailureEventArgs> OnInterstitialFailedToLoad;

        /// <summary>
        /// Called when user returned to application after click.
        /// </summary>
        public event EventHandler<EventArgs> OnReturnedToApplication;

        /// <summary>
        /// Notifies that the app will run in the background now because the user clicked the ad and is switching to a different application (Phone, App Store, and so on).
        /// </summary>
        public event EventHandler<EventArgs> OnLeftApplication;

        /// <summary>
        /// Notifies that the user has clicked on the ad.
        /// </summary>
        public event EventHandler<EventArgs> OnAdClicked;

        /// <summary>
        /// Called after the full-screen ad appears.
        /// </summary>
        public event EventHandler<EventArgs> OnInterstitialShown;

        /// <summary>
        /// Called after hiding the full-screen ad.
        /// </summary>
        public event EventHandler<EventArgs> OnInterstitialDismissed;

        /// <summary>
        /// Notifies delegate when an impression was tracked.
        /// </summary>
        public event EventHandler<ImpressionData> OnImpression;

        /// <summary>
        /// Notifies that the ad can’t be displayed.
        /// </summary>
        public event EventHandler<AdFailureEventArgs> OnInterstitialFailedToShow;

        /// <summary>
        /// Initializes an object of the YMAInterstitialAd class with a full-screen ad.
        /// </summary>
        /// <param name="blockId"> Unique ad placement ID created at partner interface. Example: R-M-DEMO-240x400-context.</param>
        public Interstitial(string blockId)
        {
            adRequestFactory = new AdRequestCreator();
            client = YandexMobileAdsClientFactory.BuildInterstitialClient(blockId);

            MainThreadDispatcher.initialize();
            ConfigureInterstitialEvents();
        }

        /// <summary>
        /// Preloads the ad by setting the data for targeting.
        /// </summary>
        /// <param name="request">Data for targeting.</param>
        public void LoadAd(AdRequest request)
        {
            loaded = false;
            client.LoadAd(adRequestFactory.CreateAdRequest(request));
        }

        /// <summary>
        /// Notifies that the ad is loaded and ready to be displayed.
        /// After the property takes the YES value, the OnInterstitialLoaded delegate method is called.
        /// </summary>
        /// <returns>
        /// true if this interstitial ad has been successfully loaded
        /// and is ready to be shown, otherwise false.
        /// </returns>
        public bool IsLoaded()
        {
            return loaded;
        }

        /// <summary>
        /// Shows interstitial ad, only if it has been loaded.
        /// </summary>
        public void Show()
        {
            client.Show();
        }

        /// <summary>
        /// Destroys Interstitial entirely and cleans up resources.
        /// </summary>
        public void Destroy()
        {
            client.Destroy();
        }

        private void ConfigureInterstitialEvents()
        {
            client.OnInterstitialLoaded += (sender, args) =>
            {
                loaded = true;
                if (OnInterstitialLoaded != null)
                {
                    MainThreadDispatcher.EnqueueAction(() =>
                    {
                        OnInterstitialLoaded(this, args);
                    });
                }
            };

            client.OnInterstitialFailedToLoad += (sender, args) =>
            {
                if (OnInterstitialFailedToLoad != null)
                {
                    MainThreadDispatcher.EnqueueAction(() =>
                    {
                        OnInterstitialFailedToLoad(this, args);
                    });
                }
            };

            client.OnReturnedToApplication += (sender, args) =>
            {
                if (OnReturnedToApplication != null)
                {
                    MainThreadDispatcher.EnqueueAction(() =>
                    {
                        OnReturnedToApplication(this, args);
                    });
                }
            };

            client.OnLeftApplication += (sender, args) =>
            {
                if (OnLeftApplication != null)
                {
                    MainThreadDispatcher.EnqueueAction(() =>
                    {
                        OnLeftApplication(this, args);
                    });
                }
            };

            client.OnAdClicked += (sender, args) =>
            {
                if (OnAdClicked != null)
                {
                    MainThreadDispatcher.EnqueueAction(() =>
                    {
                        OnAdClicked(this, args);
                    });
                }
            };

            client.OnInterstitialShown += (sender, args) =>
            {
                if (OnInterstitialShown != null)
                {
                    MainThreadDispatcher.EnqueueAction(() =>
                    {
                        OnInterstitialShown(this, args);
                    });
                }
            };

            client.OnInterstitialDismissed += (sender, args) =>
            {
                if (OnInterstitialDismissed != null)
                {
                    MainThreadDispatcher.EnqueueAction(() =>
                    {
                        OnInterstitialDismissed(this, args);
                    });
                }
            };

            client.OnImpression += (sender, args) =>
            {
                if (OnImpression != null)
                {
                    MainThreadDispatcher.EnqueueAction(() =>
                    {
                        OnImpression(this, args);
                    });
                }
            };

            client.OnInterstitialFailedToShow += (sender, args) =>
            {
                if (OnInterstitialFailedToShow != null)
                {
                    MainThreadDispatcher.EnqueueAction(() =>
                    {
                        OnInterstitialFailedToShow(this, args);
                    });
                }
            };

        }
    }
}