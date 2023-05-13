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
    
    public class AdRequestClient : IDisposable
    {
        public string ObjectId { get; private set; }
        private readonly LocationClient location;
        private readonly string contextQuery;
        private readonly ListClient contextTags;
        private readonly DictionaryClient parameters;
        private readonly string age;
        private readonly string gender;

        public AdRequestClient(AdRequest adRequest)
        {
            if (adRequest.Location != null) 
            {
                location = new LocationClient(adRequest.Location);
            }
            contextQuery = adRequest.ContextQuery;
            age = adRequest.Age;
            gender = adRequest.Gender;
            contextTags = new ListClient();
            if (adRequest.ContextTags != null)
            {
                foreach (string item in adRequest.ContextTags)
                {
                    contextTags.Add(item);
                }
            }
            parameters = new DictionaryClient();
            if (adRequest.Parameters != null)
            {
                foreach (KeyValuePair<string, string> entry in adRequest.Parameters)
                {
                    parameters.Put(entry.Key, entry.Value);
                }
            }
            string locationId = location != null ? 
                                    location.ObjectId : null;
            string contextTagsId = contextTags != null ? 
                                       contextTags.ObjectId : null;
            string parametersId = parameters != null ? 
                                      parameters.ObjectId : null;
            ObjectId = AdRequestBridge.YMAUnityCreateAdRequest(
                locationId, contextQuery, contextTagsId, parametersId, age, gender);
        }

        public void Destroy()
        {
            ObjectBridge.YMAUnityDestroyObject(ObjectId);
        }

        public void Dispose()
        {
            Destroy();
        }

        ~AdRequestClient()
        {
            Destroy();
        }
    }

    #endif
}