using System;
using System.Collections.Generic;
using MarcTron.Plugin.CustomEventArgs;
using MarcTron.Plugin.Enums;
using MarcTron.Plugin.Helpers;

namespace MarcTron.Plugin.Interfaces
{
    // ReSharper disable once InconsistentNaming
    public interface IMTAdmob
    {

        #region Common properties

        /// <summary>
        /// Is the plugin enabled.
        /// </summary>
        bool IsEnabled { get; set; }

        /// <summary>
        /// Is in test mode -> use SampleAdsId
        /// </summary>
        bool IsInTestMode { get; set; }

        bool UserPersonalizedAds { get; set; }

        bool UseRestrictedDataProcessing { get; set; }

        bool? TagForChildDirectedTreatment { get; set; }

        bool? TagForUnderAgeOfConsent { get; set; }

        MaxAdContentRating MaxAdContentRating { get; set; }

        List<string> TestDevices { get; set; }

        Dictionary<string, string> CustomParameters { get; set; }

        #endregion

        #region Project-level AdUnitIds

        string AdUnitId_Banner { get; set; }
        string AdUnitId_Interstitial { get; set; }
        string AdUnitId_InterstitialVideo { get; set; }
        string AdUnitId_RewardedVideo { get; set; }
        string AdUnitId_NativeAdvanced { get; set; }
        string AdUnitId_NativeAdvancedVideo { get; set; }

        #endregion

        #region Interstitial

        event EventHandler OnInterstitialLoaded;
        event EventHandler<MTErrorEventArgs> OnInterstitialFailed;
        event EventHandler OnInterstitialOpened;
        event EventHandler OnInterstitialLeftApplication;
        event EventHandler OnInterstitialClosed;

        void LoadInterstitial();
        void LoadInterstitial(string CustomAdUnit);
        bool IsInterstitialLoaded();
        void ShowInterstitial();

        #endregion

        #region InterstitialVideo

        event EventHandler OnInterstitialVideoLoaded;
        event EventHandler<MTErrorEventArgs> OnInterstitialVideoFailed;
        event EventHandler OnInterstitialVideoOpened;
        event EventHandler OnInterstitialVideoLeftApplication;
        event EventHandler OnInterstitialVideoClosed;

        void LoadInterstitialVideo();
        void LoadInterstitialVideo(string CustomAdUnit);
        bool IsInterstitialVideoLoaded();
        void ShowInterstitialVideo();

        #endregion

        #region RewardedVideo

        event EventHandler OnRewardedVideoAdLoaded;
        event EventHandler<MTErrorEventArgs> OnRewardedVideoAdFailedToLoad;
        event EventHandler OnRewardedVideoAdOpened;
        event EventHandler<MTErrorEventArgs> OnRewardedVideoAdFailedToShow;
        event EventHandler OnRewardedVideoAdClosed;
        event EventHandler<MTRewardedEventArgs> OnRewardedUser;

        public void LoadRewardedVideo(MTRewardedAdOptions options = null);
        public void LoadRewardedVideo(string CustomAdUnit, MTRewardedAdOptions options = null);
        bool IsRewardedVideoLoaded();
        void ShowRewardedVideo();

        #endregion

    }
}
