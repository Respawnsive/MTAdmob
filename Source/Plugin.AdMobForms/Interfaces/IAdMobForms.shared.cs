using System;
using System.Collections.Generic;

namespace Plugin.AdMobForms.Interfaces
{
    // ReSharper disable once InconsistentNaming
    public interface IAdMobForms
    {

        #region General Settings


        /// <summary>
        /// Is the plugin enable ?
        /// </summary>
        bool IsEnabled { get; set; }

        /// <summary>
        /// Is in test mode -> force Google Sample AdUnitIds
        /// </summary>
        bool IsInTestMode { get; set; }

        bool UserPersonalizedAds { get; set; }

        bool UseRestrictedDataProcessing { get; set; }

        bool? TagForChildDirectedTreatment { get; set; }

        bool? TagForUnderAgeOfConsent { get; set; }

        MaxAdContentRating MaxAdContentRating { get; set; }

        List<string> TestDevices { get; set; }

        Dictionary<string, string> CustomParameters { get; set; }

        /// <summary>
        /// Set the ads volume based on a percentage of the device current volume
        /// </summary>
        /// <param name="percentage">between 0.0 to 1.0</param>
        void SetAdsVolume(float percentage);

        /// <summary>
        /// Set the ads to mute (or revert to the previous level)
        /// </summary>
        /// <param name="muted"></param>
        void SetAdsMuted(bool muted);

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
        event EventHandler<AdMobErrorEventArgs> OnInterstitialFailed;
        event EventHandler OnInterstitialOpened;
        event EventHandler OnInterstitialClosed;

        void LoadInterstitial();
        void LoadInterstitial(string CustomAdUnit);
        bool IsInterstitialLoaded();
        void ShowInterstitial();

        #endregion

        #region InterstitialVideo

        event EventHandler OnInterstitialVideoLoaded;
        event EventHandler<AdMobErrorEventArgs> OnInterstitialVideoFailed;
        event EventHandler OnInterstitialVideoOpened;
        event EventHandler OnInterstitialVideoClosed;

        void LoadInterstitialVideo();
        void LoadInterstitialVideo(string CustomAdUnit);
        bool IsInterstitialVideoLoaded();
        void ShowInterstitialVideo();

        #endregion

        #region RewardedVideo

        event EventHandler OnRewardedVideoAdLoaded;
        event EventHandler<AdMobErrorEventArgs> OnRewardedVideoAdFailedToLoad;
        event EventHandler OnRewardedVideoAdOpened;
        event EventHandler<AdMobErrorEventArgs> OnRewardedVideoAdFailedToShow;
        event EventHandler OnRewardedVideoAdClosed;
        event EventHandler<AdMobRewardedEventArgs> OnRewardedUser;

        public void LoadRewardedVideo(AdMobRewardedOptions options = null);
        public void LoadRewardedVideo(string CustomAdUnit, AdMobRewardedOptions options = null);
        bool IsRewardedVideoLoaded();
        void ShowRewardedVideo();

        #endregion

    }
}
