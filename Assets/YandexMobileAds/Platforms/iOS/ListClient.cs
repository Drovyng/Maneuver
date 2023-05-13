/*
 * This file is a part of the Yandex Advertising Network
 *
 * Version for iOS (C) 2019 YANDEX
 *
 * You may not use this file except in compliance with the License.
 * You may obtain a copy of the License at https://legal.yandex.com/partner_ch/
 */

using System;

namespace YandexMobileAds.Platforms.iOS
{
    #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
    
    public class ListClient : IDisposable
    {
        public string ObjectId { get; private set; }

        public ListClient()
        {
            ObjectId = ListBridge.YMAUnityCreateList();
        }

        public void Add(string value)
        {
            ListBridge.YMAUnityAddToList(ObjectId, value);
        }

        public void Destroy()
        {
            ObjectBridge.YMAUnityDestroyObject(ObjectId);
        }

        public void Dispose()
        {
            Destroy();
        }

        ~ListClient()
        {
            Destroy();
        }
    }

    #endif
}