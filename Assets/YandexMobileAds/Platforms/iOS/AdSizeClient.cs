/*
 * This file is a part of the Yandex Advertising Network
 *
 * Version for iOS (C) 2019 YANDEX
 *
 * You may not use this file except in compliance with the License.
 * You may obtain a copy of the License at https://legal.yandex.com/partner_ch/
 */

using System;
using YandexMobileAds.Base;
using System.Collections.Generic;

namespace YandexMobileAds.Platforms.iOS
{
    #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
    
    public class AdSizeClient : IDisposable
    {
        public string ObjectId { get; private set; }

        public AdSizeClient(AdSize adSize)
        {
            if (adSize.AdSizeType == AdSizeType.Sticky)
            {
                ObjectId = AdSizeBridge.YMAUnityCreateStickyAdSize(adSize.Width);
            }
            else if (adSize.AdSizeType == AdSizeType.Flexible)
            {
                ObjectId = AdSizeBridge.YMAUnityCreateFlexibleAdSizeWithSize(adSize.Width, adSize.Height);
            }
            else if (adSize.AdSizeType == AdSizeType.Fixed)
            {
                ObjectId = AdSizeBridge.YMAUnityCreateFixedAdSize(adSize.Width, adSize.Height);
            }
        }

        public void Destroy()
        {
            ObjectBridge.YMAUnityDestroyObject(ObjectId);
        }

        public void Dispose()
        {
            Destroy();
        }

        ~AdSizeClient()
        {
            Destroy();
        }
    }

    #endif
}