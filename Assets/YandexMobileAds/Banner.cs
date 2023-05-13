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
    /// A class for displaying banner ad view.
    /// </summary>
    public class Banner
    {
        private AdRequestCreator adRequestFactory;
        private IBannerClient client;

        /// <summary>
        /// Notifies that the banner is loaded. At this time, you can add banner if you haven’t done so yet.
        /// </summary>
        public event EventHandler<EventArgs> OnAdLoaded;

        /// <summary>
        /// Notifies that the banner failed to load.
        /// </summary>
        public event EventHandler<AdFailureEventArgs> OnAdFailedToLoad;
        
        /// <summary>
        /// Called when user returned to application after click.
        /// </summary>
        public event EventHandler<EventArgs> OnReturnedToApplication;

        /// <summary>
        /// Notifies that the app will become inactive now because the user clicked on the banner
        /// ad and is about to switch to a different application (Phone, App Store, and so on).
        /// </summary>
        public event EventHandler<EventArgs> OnLeftApplication;

        /// <summary>
        /// Notifies that the user has clicked on the banner.
        /// </summary>
        public event EventHandler<EventArgs> OnAdClicked;

        /// <summary>
        /// Notifies delegate when an impression was tracked.
        /// </summary>
        public event EventHandler<ImpressionData> OnImpression;

        /// <summary>
        /// Initializes an object of the Banner class to display the banner with the specified size.
        /// <param name="blockId"> Unique ad placement ID created at partner interface. Example: R-M-DEMO-320x50.</param>
        /// <param name="adSize"> The size of banner ad. <see cref="YandexMobileAds.Base.AdSize"/></param>
        /// <param name="position"> Banner position on screen <see cref="YandexMobileAds.Base.AdPosition"/></param>
        /// </summary>
        public Banner(string blockId, AdSize adSize, AdPosition position)
        {
            adRequestFactory = new AdRequestCreator();
            client = YandexMobileAdsClientFactory.BuildBannerClient(blockId, adSize, position);

            MainThreadDispatcher.initialize();
            ConfigureBannerEvents();
        }

        /// <summary>
        /// Loads Banner with data for targeting.
        /// </summary>
        /// <param name="request">Data for targeting.</param>
        public void LoadAd(AdRequest request)
        {
            client.LoadAd(adRequestFactory.CreateAdRequest(request));
        }

        /// <summary>
        /// Hides Banner from screen.
        /// </summary>
        public void Hide()
        {
            client.Hide();
        }

        /// <summary>
        /// Shows Banner on screen.
        /// </summary>
        public void Show()
        {
            client.Show();
        }

        /// <summary>
        /// Destroys Banner.
        /// </summary>
        public void Destroy()
        {
            client.Destroy();
        }

        private void ConfigureBannerEvents()
        {
            client.OnAdLoaded += (sender, args) =>
            {
                if (OnAdLoaded != null)
                {
                    MainThreadDispatcher.EnqueueAction(() =>
                    {
                        OnAdLoaded(this, args);
                    });
                }
            };

            client.OnAdFailedToLoad += (sender, args) =>
            {
                if (OnAdFailedToLoad != null)
                {
                    MainThreadDispatcher.EnqueueAction(() =>
                    {
                        OnAdFailedToLoad(this, args);
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
        }
    }
}