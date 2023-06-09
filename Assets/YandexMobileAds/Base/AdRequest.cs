/*
 * This file is a part of the Yandex Advertising Network
 *
 * Version for Unity (C) 2018 YANDEX
 *
 * You may not use this file except in compliance with the License.
 * You may obtain a copy of the License at https://legal.yandex.com/partner_ch/
 */

using System;
using System.Collections.Generic;

namespace YandexMobileAds.Base
{
    /// <summary>
    /// A class with data for a targeted ad request.
    /// </summary>
    public class AdRequest
    {
        /// <summary>
        /// The string representation of user's age.
        /// </summary>
        public string Age { get; private set; }

        /// <summary>
        /// The search query that the user entered in the app.
        /// </summary>
        public string ContextQuery { get; private set; }

        /// <summary>
        /// An array of tags.Matches the context in which the ad will be displayed.
        /// </summary>
        public List<string> ContextTags { get; private set; }

        /// <summary>
        /// The string representation of user's gender. See the list of values in Gender.
        /// </summary>
        public string Gender { get; private set; }

        /// <summary>
        /// User location.
        /// </summary>
        public Location Location { get; private set; }

        /// <summary>
        /// A set of arbitrary input parameters.
        /// </summary>
        public Dictionary<string, string> Parameters { get; private set; }

        private AdRequest(Builder builder)
        {
            Age = builder.Age;
            ContextQuery = builder.ContextQuery;

            if (builder.ContextTags != null)
            {
                ContextTags = new List<string>(builder.ContextTags);
            }

            Gender = builder.Gender;
            Location = builder.Location;

            if (builder.Parameters != null)
            {
                Parameters = new Dictionary<string, string>(builder.Parameters);
            }
        }

        /// <summary>
        /// A class responsible for creating AdRequest objects.
        /// </summary>
        public class Builder
        {

            internal string Age { get; private set; }

            internal string ContextQuery { get; private set; }

            internal List<string> ContextTags { get; private set; }

            internal string Gender { get; private set; }

            internal Location Location { get; private set; }

            internal Dictionary<string, string> Parameters { get; private set; }

            /// <summary>
            /// AdRequest Builder initialized with user's Age for targeting process.
            /// </summary>
            /// <param name="age">The string representation of user's age.</param>
            /// <returns>AdRequest Builder</returns>
            public Builder WithAge(string age)
            {
                Age = age;
                return this;
            }

            /// <summary>
            /// AdRequest Builder initialized with current user query entered inside app.
            /// </summary>
            /// <param name="contextQuery">The search query that the user entered in the app.</param>
            /// <returns>AdRequest Builder</returns>
            public Builder WithContextQuery(string contextQuery)
            {
                ContextQuery = contextQuery;
                return this;
            }

            /// <summary>
            /// AdRequest Builder initialized with tags describing current user context inside app.
            /// </summary>
            /// <param name="contextTags">A list of tags.Matches the context in which the ad will be displayed.</param>
            /// <returns>AdRequest Builder.</returns>
            public Builder WithContextTags(List<string> contextTags)
            {
                ContextTags = contextTags;
                return this;
            }

            /// <summary>
            /// AdRequest Builder initialized with user's Gender for targeting process.
            /// </summary>
            /// <param name="gender">The string representation of user's gender. See the list of values in Gender.</param>
            /// <returns>AdRequest Builder.</returns>
            public Builder WithGender(string gender)
            {
                Gender = gender;
                return this;
            }

            /// <summary>
            /// AdRequest Builder initialized with user's Location for targeting process.
            /// </summary>
            /// <param name="location">User location.</param>
            /// <returns>AdRequest Builder.</returns>
            public Builder WithLocation(Location location)
            {
                Location = location;
                return this;
            }

            /// <summary>
            /// AdRequest Builder initialized with custom Parameters.
            /// </summary>
            /// <param name="parameters">A set of arbitrary input parameters.</param>
            /// <returns>AdRequest Builder.</returns>
            public Builder WithParameters(Dictionary<string, string> parameters)
            {
                Parameters = parameters;
                return this;
            }

            /// <summary>
            /// AdRequest Builder initialized with AdRequest
            /// </summary>
            /// <param name="adRequest">AdRequest.</param>
            /// <returns>AdRequest Builder.</returns>
            public Builder WithAdRequest(AdRequest adRequest)
            {
                if (adRequest != null) 
                {
                    ContextQuery = adRequest.ContextQuery;
                    ContextTags = adRequest.ContextTags;
                    Parameters = adRequest.Parameters;
                    Location = adRequest.Location;
                    Age = adRequest.Age;
                    Gender = adRequest.Gender;
                }
                return this;
            }

            /// <summary>
            /// Creates AdRequest based on current builder parameters.
            /// </summary>
            /// <returns>AdRequest Builder.</returns>
            public AdRequest Build()
            {
                if (Parameters == null) 
                {
                    Parameters = new Dictionary<string, string>();
                }
                return new AdRequest(this);
            }
        }
    }
}