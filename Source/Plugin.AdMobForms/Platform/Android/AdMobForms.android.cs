using Android.App;
using Android.Gms.Ads;
using Android.Gms.Ads.Rewarded;
using Android.OS;
using Google.Ads.Mediation.Admob;
using Plugin.AdMobForms.Interfaces;
using Plugin.AdMobForms.Platform.Android;
using System;
using System.Collections.Generic;

namespace Plugin.AdMobForms
{
    /// <summary>
    /// Implementation for Android
    /// </summary>
    public class AdMobForms : IAdMobForms
    {
        #region General Configuration

        public bool IsEnabled { get; set; } = true;
        public bool IsInTestMode { get; set; } = false;
        public bool UserPersonalizedAds { get; set; } = false;
        public bool UseRestrictedDataProcessing { get; set; } = false;
        public List<string> TestDevices { get; set; }
        public Dictionary<string, string> CustomParameters { get; set; } = new Dictionary<string, string>();
        public bool? TagForChildDirectedTreatment { get; set; }
        public bool? TagForUnderAgeOfConsent { get; set; }
        public MaxAdContentRating MaxAdContentRating { get; set; } = MaxAdContentRating.GeneralAudiences;

        public void SetAdsVolume(float percentage)
        {
            MobileAds.SetAppVolume(percentage);
        }

        public void SetAdsMuted(bool muted)
        {
            MobileAds.SetAppMuted(muted);
        }

        #endregion

        #region Project-level AdUnitIds

        public string AdUnitId_Banner { get; set; }
        public string AdUnitId_Interstitial { get; set; }
        public string AdUnitId_InterstitialVideo { get; set; }
        public string AdUnitId_RewardedVideo { get; set; }
        public string AdUnitId_NativeAdvanced { get; set; }
        public string AdUnitId_NativeAdvancedVideo { get; set; }

        #endregion

        #region Interstitial

        private InterstitialAd _interstitialAd;

        public event EventHandler OnInterstitialLoaded;
        public event EventHandler<AdMobErrorEventArgs> OnInterstitialFailed;
        public event EventHandler OnInterstitialOpened;
        public event EventHandler OnInterstitialClosed;

        public void LoadInterstitial()
        {
            if (IsInTestMode || String.IsNullOrWhiteSpace(AdUnitId_Interstitial))
                AdUnitId_Interstitial = GoogleSamplesAdUnitIds.AndroidSampleAdUnitId_Interstitial;
            LoadInterstitial(AdUnitId_Interstitial);
        }

        public void LoadInterstitial(string adUnit)
        {
            if (!CrossAdMob.Current.IsEnabled)
                return;

            if (IsInTestMode || String.IsNullOrWhiteSpace(adUnit))
                adUnit = GoogleSamplesAdUnitIds.AndroidSampleAdUnitId_Interstitial;

            if (_interstitialAd == null || _interstitialAd?.AdUnitId != adUnit)
                CreateInterstitialAd(adUnit);

            if (!_interstitialAd.IsLoaded && !_interstitialAd.IsLoading)
            {
                _interstitialAd.LoadAd(GetRequest());
            }
            else
            {
                Console.WriteLine("Interstitial already loaded");
            }
        }

        public bool IsInterstitialLoaded()
        {
            return _interstitialAd != null && _interstitialAd.IsLoaded;
        }

        public void ShowInterstitial()
        {
            if (!CrossAdMob.Current.IsEnabled)
                return;

            if (_interstitialAd != null && _interstitialAd.IsLoaded)
            {
                _interstitialAd.Show();
            }
            else
            {
                Console.WriteLine("Interstitial not loaded");
            }
        }

        private void CreateInterstitialAd(string adUnit)
        {
            //Free previous listener ?
            //try
            //{
            //    if (_interstitialAd != null && _interstitialAd.AdListener != null && _interstitialAd.AdListener is AdMobListener adMobListener)
            //    {
            //        adMobListener.AdLoaded -= InterstitialAdListener_AdLoaded;
            //        adMobListener.AdFailedToLoad -= InterstitialAdListener_AdFailedToLoad;
            //        adMobListener.AdOpened -= InterstitialAdListener_AdOpened;
            //        adMobListener.AdClosed -= InterstitialAdListener_AdClosed;
            //    }
            //}
            //catch { }
            var context = Application.Context;
            _interstitialAd = new InterstitialAd(context) {AdUnitId = adUnit};
            var interstitialAdListener = new AdMobListener(adUnit);
            interstitialAdListener.AdLoaded += InterstitialAdListener_AdLoaded;
            interstitialAdListener.AdFailedToLoad += InterstitialAdListener_AdFailedToLoad;
            interstitialAdListener.AdOpened += InterstitialAdListener_AdOpened;
            interstitialAdListener.AdClosed += InterstitialAdListener_AdClosed;
            _interstitialAd.AdListener = interstitialAdListener;
        }

        private void InterstitialAdListener_AdLoaded(object sender, EventArgs e)
        {
            OnInterstitialLoaded?.Invoke(sender, e);
        }

        private void InterstitialAdListener_AdFailedToLoad(object sender, AdMobErrorEventArgs e)
        {
            OnInterstitialFailed?.Invoke(sender, e);
        }

        private void InterstitialAdListener_AdOpened(object sender, EventArgs e)
        {
            OnInterstitialOpened?.Invoke(sender, e);
        }

        private void InterstitialAdListener_AdClosed(object sender, EventArgs e)
        {
            OnInterstitialClosed?.Invoke(sender, e);
        }

        #endregion

        #region InterstitialVideo

        private InterstitialAd _interstitialVideoAd;

        public event EventHandler OnInterstitialVideoLoaded;
        public event EventHandler<AdMobErrorEventArgs> OnInterstitialVideoFailed;
        public event EventHandler OnInterstitialVideoOpened;
        public event EventHandler OnInterstitialVideoClosed;

        public void LoadInterstitialVideo()
        {
            if (IsInTestMode || String.IsNullOrWhiteSpace(AdUnitId_InterstitialVideo))
                AdUnitId_InterstitialVideo = GoogleSamplesAdUnitIds.AndroidSampleAdUnitId_InterstitialVideo;
            LoadInterstitial(AdUnitId_InterstitialVideo);
        }

        public void LoadInterstitialVideo(string adUnit)
        {
            if (!CrossAdMob.Current.IsEnabled)
                return;

            if (IsInTestMode || String.IsNullOrWhiteSpace(adUnit))
                adUnit = GoogleSamplesAdUnitIds.AndroidSampleAdUnitId_InterstitialVideo;

            if (_interstitialVideoAd == null || _interstitialVideoAd?.AdUnitId != adUnit)
                CreateInterstitialVideoAd(adUnit);

            if (!_interstitialVideoAd.IsLoaded && !_interstitialVideoAd.IsLoading)
            {
                _interstitialVideoAd.LoadAd(GetRequest());
            }
            else
            {
                Console.WriteLine("InterstitialVideo already loaded");
            }
        }

        public bool IsInterstitialVideoLoaded()
        {
            return _interstitialVideoAd != null && _interstitialVideoAd.IsLoaded;
        }

        public void ShowInterstitialVideo()
        {
            if (!CrossAdMob.Current.IsEnabled)
                return;

            if (_interstitialVideoAd != null && _interstitialVideoAd.IsLoaded)
            {
                _interstitialVideoAd.Show();
            }
            else
            {
                Console.WriteLine("Interstitial Video not loaded");
            }
        }

        private void CreateInterstitialVideoAd(string adUnit)
        {
            //Free previous listener ?
            //try
            //{
            //    if (_interstitialVideoAd != null && _interstitialVideoAd.AdListener != null && _interstitialVideoAd.AdListener is AdMobListener adMobListener)
            //    {
            //        adMobListener.AdLoaded -= InterstitialVideoAdListener_AdLoaded;
            //        adMobListener.AdFailedToLoad -= InterstitialVideoAdListener_AdFailedToLoad;
            //        adMobListener.AdOpened -= InterstitialVideoAdListener_AdOpened;
            //        adMobListener.AdClosed -= InterstitialVideoAdListener_AdClosed;
            //    }
            //}
            //catch { }
            var context = Application.Context;
            _interstitialVideoAd = new InterstitialAd(context) { AdUnitId = adUnit };
            var interstitialVideoAdListener = new AdMobListener(adUnit);

            interstitialVideoAdListener.AdLoaded += InterstitialVideoAdListener_AdLoaded;
            interstitialVideoAdListener.AdFailedToLoad += InterstitialVideoAdListener_AdFailedToLoad;
            interstitialVideoAdListener.AdOpened += InterstitialVideoAdListener_AdOpened;
            interstitialVideoAdListener.AdClosed += InterstitialVideoAdListener_AdClosed;

            _interstitialVideoAd.AdListener = interstitialVideoAdListener;
        }

        private void InterstitialVideoAdListener_AdLoaded(object sender, EventArgs e)
        {
            OnInterstitialVideoLoaded?.Invoke(sender, e);
        }

        private void InterstitialVideoAdListener_AdFailedToLoad(object sender, AdMobErrorEventArgs e)
        {
            OnInterstitialVideoFailed?.Invoke(sender, e);
        }

        private void InterstitialVideoAdListener_AdOpened(object sender, EventArgs e)
        {
            OnInterstitialVideoOpened?.Invoke(sender, e);
        }

        private void InterstitialVideoAdListener_AdClosed(object sender, EventArgs e)
        {
            OnInterstitialVideoClosed?.Invoke(sender, e);
        }

        #endregion

        #region RewardedVideo

        RewardedAd _rewardedVideoAds;

        //RewardedAdLoadCallback
        public event EventHandler OnRewardedVideoAdLoaded;
        public event EventHandler<AdMobErrorEventArgs> OnRewardedVideoAdFailedToLoad;
        //RewardedAdCallback
        public event EventHandler OnRewardedVideoAdOpened;
        public event EventHandler<AdMobErrorEventArgs> OnRewardedVideoAdFailedToShow;
        public event EventHandler OnRewardedVideoAdClosed;
        public event EventHandler<AdMobRewardedEventArgs> OnRewardedUser;

        public void LoadRewardedVideo(AdMobRewardedOptions options = null)
        {
            if (IsInTestMode || String.IsNullOrWhiteSpace(AdUnitId_RewardedVideo))
                AdUnitId_RewardedVideo = GoogleSamplesAdUnitIds.AndroidSampleAdUnitId_RewardedVideo;
            LoadRewardedVideo(AdUnitId_RewardedVideo, options);
        }

        public void LoadRewardedVideo(string adUnit, AdMobRewardedOptions options = null)
        {
            if (!CrossAdMob.Current.IsEnabled)
                return;

            if (IsInTestMode || String.IsNullOrWhiteSpace(adUnit))
                adUnit = GoogleSamplesAdUnitIds.AndroidSampleAdUnitId_RewardedVideo;

            if (_rewardedVideoAds == null)
            {
                CreateRewardedVideo(adUnit);
            }

            if (!_rewardedVideoAds.IsLoaded)
            {
                //if (options != null)
                //{
                //    _rewardedVideoAds.UserId = options.UserId;
                //    _rewardedVideoAds.CustomData = options.CustomData;
                //}
                var loadCallback = new AdMobRewardedLoadCallback(adUnit);
                loadCallback.AdLoaded += LoadCallback_AdLoaded;
                loadCallback.AdFailedToLoad += LoadCallback_AdFailedToLoad;

                _rewardedVideoAds.LoadAd(GetRequest(), loadCallback);
            }
            else
            {
                Console.WriteLine("RewardedVideo already loaded");
            }
        }

        public bool IsRewardedVideoLoaded()
        {
            return _rewardedVideoAds != null && _rewardedVideoAds.IsLoaded;
        }

        public void ShowRewardedVideo()
        {
            if (!CrossAdMob.Current.IsEnabled)
                return;

            var adUnit = AdUnitId_RewardedVideo;
            if (IsInTestMode || String.IsNullOrWhiteSpace(adUnit))
                adUnit = GoogleSamplesAdUnitIds.AndroidSampleAdUnitId_RewardedVideo;

            if (_rewardedVideoAds != null && _rewardedVideoAds.IsLoaded)
            {
                var rewardedCallback = new AdMobRewardedCallback(adUnit);
                rewardedCallback.AdOpened += RewardedCallback_AdOpened;
                rewardedCallback.AdClosed += RewardedCallback_AdClosed;
                rewardedCallback.AdFailedToShow += RewardedCallback_AdFailedToShow;
                rewardedCallback.UserEarnedReward += RewardedCallback_UserEarnedReward;

                _rewardedVideoAds.Show(Xamarin.Essentials.Platform.CurrentActivity, rewardedCallback);
            }
            else
            {
                Console.WriteLine("Rewarded Video not loaded");
            }
        }

        private void CreateRewardedVideo(string adUnit)
        {
            var context = Application.Context;
            _rewardedVideoAds = new RewardedAd(context, adUnit);

        }

        private void LoadCallback_AdLoaded(object sender, EventArgs e)
        {
            OnRewardedVideoAdLoaded?.Invoke(sender, e);
        }

        private void LoadCallback_AdFailedToLoad(object sender, AdMobErrorEventArgs e)
        {
            OnRewardedVideoAdFailedToLoad?.Invoke(sender, e);
        }

        private void RewardedCallback_AdOpened(object sender, EventArgs e)
        {
            OnRewardedVideoAdOpened?.Invoke(sender, e);
        }

        private void RewardedCallback_AdFailedToShow(object sender, AdMobErrorEventArgs e)
        {
            OnRewardedVideoAdFailedToShow?.Invoke(sender, e);
        }

        private void RewardedCallback_AdClosed(object sender, EventArgs e)
        {
            OnRewardedVideoAdClosed?.Invoke(sender, e);
        }

        private void RewardedCallback_UserEarnedReward(object sender, AdMobRewardedEventArgs e)
        {
            OnRewardedUser?.Invoke(sender, e);
        }

        #endregion


        public static AdRequest GetRequest()
        {
            UpdateRequestConfiguration();

            bool addExtra = false;
            Bundle bundleExtra = new Bundle();
            var requestBuilder = new AdRequest.Builder();
            if (!CrossAdMob.Current.UserPersonalizedAds)
            {
                bundleExtra.PutString("npa", "1");
                addExtra = true;
            }
            if (CrossAdMob.Current.UseRestrictedDataProcessing)
            {
                bundleExtra.PutString("rdp", "1");
                addExtra = true;
            }
            if (CrossAdMob.Current.CustomParameters.Count > 0)
            {
                foreach (KeyValuePair<string, string> param in CrossAdMob.Current.CustomParameters)
                {
                    bundleExtra.PutString(param.Key, param.Value);
                }
                addExtra = true;
            }

            if (addExtra)
                requestBuilder = requestBuilder.AddNetworkExtrasBundle(Java.Lang.Class.FromType(typeof(AdMobAdapter)), bundleExtra);

            return requestBuilder.Build();
        }

        private static void UpdateRequestConfiguration()
        {
            //Get the current config
            var requestConfiguration = MobileAds.RequestConfiguration.ToBuilder();
            //TestDeviceIds
            if (CrossAdMob.Current.TestDevices != null)
            {
                requestConfiguration.SetTestDeviceIds(CrossAdMob.Current.TestDevices);
            }
            //TagForChildDirectedTreatment
            if (CrossAdMob.Current.TagForChildDirectedTreatment.HasValue)
            {
                if (CrossAdMob.Current.TagForChildDirectedTreatment.Value)
                    requestConfiguration.SetTagForChildDirectedTreatment(RequestConfiguration.TagForChildDirectedTreatmentTrue);
                else
                    requestConfiguration.SetTagForChildDirectedTreatment(RequestConfiguration.TagForChildDirectedTreatmentFalse);
            }
            else
                requestConfiguration.SetTagForChildDirectedTreatment(RequestConfiguration.TagForChildDirectedTreatmentUnspecified);
            //TagForUnderAgeOfConsent
            if (CrossAdMob.Current.TagForUnderAgeOfConsent.HasValue)
            {
                if (CrossAdMob.Current.TagForUnderAgeOfConsent.Value)
                    requestConfiguration.SetTagForChildDirectedTreatment(RequestConfiguration.TagForUnderAgeOfConsentTrue);
                else
                    requestConfiguration.SetTagForChildDirectedTreatment(RequestConfiguration.TagForUnderAgeOfConsentFalse);
            }
            else
                requestConfiguration.SetTagForChildDirectedTreatment(RequestConfiguration.TagForUnderAgeOfConsentUnspecified);
            //MaxAdContentRating
            switch (CrossAdMob.Current.MaxAdContentRating)
            {
                case MaxAdContentRating.GeneralAudiences:
                    requestConfiguration.SetMaxAdContentRating(RequestConfiguration.MaxAdContentRatingG);
                    break;
                case MaxAdContentRating.MatureAudiences:
                    requestConfiguration.SetMaxAdContentRating(RequestConfiguration.MaxAdContentRatingMa);
                    break;
                case MaxAdContentRating.ParentalGuidance:
                    requestConfiguration.SetMaxAdContentRating(RequestConfiguration.MaxAdContentRatingPg);
                    break;
                case MaxAdContentRating.Teen:
                    requestConfiguration.SetMaxAdContentRating(RequestConfiguration.MaxAdContentRatingT);
                    break;
            }

            //Set the config
            MobileAds.RequestConfiguration = requestConfiguration.Build();
        }

    }
}
