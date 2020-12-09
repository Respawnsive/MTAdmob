using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using Google.MobileAds;
using Plugin.AdMobForms.Interfaces;
using UIKit;

namespace Plugin.AdMobForms
{
    /// <summary>
    /// Implementation for iOS
    /// </summary>
    public class AdMobForms : IAdMobForms, IRewardedAdDelegate, IInterstitialDelegate
    {

        public AdMobForms()
        {
            _rewardedAd = new RewardedAd();
        }

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
            MobileAds.SharedInstance.ApplicationVolume = percentage;
        }

        public void SetAdsMuted(bool muted)
        {
            MobileAds.SharedInstance.ApplicationMuted = muted;
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

        private Interstitial _interstitialAd;

        public event EventHandler OnInterstitialLoaded;
        public event EventHandler<AdMobErrorEventArgs> OnInterstitialFailed;
        public event EventHandler OnInterstitialOpened;
        public event EventHandler OnInterstitialClosed;

        public void LoadInterstitial()
        {
            if (IsInTestMode || String.IsNullOrWhiteSpace(AdUnitId_Interstitial))
                AdUnitId_Interstitial = GoogleSamplesAdUnitIds.iOSSampleAdUnitId_Interstitial;
            LoadInterstitial(AdUnitId_Interstitial);
        }

        public void LoadInterstitial(string adUnit)
        {
            if (!CrossAdMob.Current.IsEnabled)
                return;

            if (IsInTestMode || String.IsNullOrWhiteSpace(adUnit))
                adUnit = GoogleSamplesAdUnitIds.iOSSampleAdUnitId_Interstitial;

            CreateInterstitialAd(adUnit);

            _interstitialAd.LoadRequest(GetRequest());
        }

        public bool IsInterstitialLoaded()
        {
            return _interstitialAd != null && _interstitialAd.IsReady;
        }

        public void ShowInterstitial()
        {
            if (!CrossAdMob.Current.IsEnabled)
                return;

            if (_interstitialAd != null && _interstitialAd.IsReady)
            {
                var window = UIApplication.SharedApplication.KeyWindow;
                var vc = window.RootViewController;
                while (vc.PresentedViewController != null)
                {
                    vc = vc.PresentedViewController;
                }

                _interstitialAd.Present(vc);
            }
        }

        private void CreateInterstitialAd(string adUnit)
        {
            try
            {
                if (_interstitialAd != null)
                {
                    _interstitialAd.WillPresentScreen -= _interstitialAd_WillPresentScreen;
                    _interstitialAd.FailedToPresentScreen -= _interstitialAd_FailedToPresentScreen;
                    _interstitialAd.ReceiveAdFailed -= _interstitialAd_ReceiveAdFailed;
                    _interstitialAd.AdReceived -= _interstitialAd_AdReceived;
                    _interstitialAd.ScreenDismissed -= _interstitialAd_ScreenDismissed;
                    _interstitialAd = null;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            _interstitialAd = new Interstitial(adUnit);
            _interstitialAd.WillPresentScreen += _interstitialAd_WillPresentScreen;
            _interstitialAd.FailedToPresentScreen += _interstitialAd_FailedToPresentScreen;
            _interstitialAd.ReceiveAdFailed += _interstitialAd_ReceiveAdFailed;
            _interstitialAd.AdReceived += _interstitialAd_AdReceived;
            _interstitialAd.ScreenDismissed += _interstitialAd_ScreenDismissed;
        }

        private void _interstitialAd_WillPresentScreen(object sender, EventArgs e)
        {
            OnInterstitialLoaded?.Invoke(sender, e);
        }

        private void _interstitialAd_ReceiveAdFailed(object sender, InterstitialDidFailToReceiveAdWithErrorEventArgs e)
        {
            OnInterstitialFailed?.Invoke(sender, new AdMobErrorEventArgs() { Code = (int?)(e?.Error?.Code), Domain = e?.Error?.Domain, Message = e?.Error?.LocalizedDescription, FullStacktrace = e?.Error?.ToString() });
        }

        private void _interstitialAd_FailedToPresentScreen(object sender, EventArgs e)
        {
            OnInterstitialFailed?.Invoke(sender, new AdMobErrorEventArgs() { Message = "Failed To Present Screen" });
        }

        private void _interstitialAd_AdReceived(object sender, EventArgs e)
        {
            OnInterstitialOpened?.Invoke(sender, e);
        }

        private void _interstitialAd_ScreenDismissed(object sender, EventArgs e)
        {
            OnInterstitialClosed?.Invoke(sender, e);
        }

        #endregion

        #region InterstitialVideo

        private Interstitial _interstitialVideoAd;

        public event EventHandler OnInterstitialVideoLoaded;
        public event EventHandler<AdMobErrorEventArgs> OnInterstitialVideoFailed;
        public event EventHandler OnInterstitialVideoOpened;
        public event EventHandler OnInterstitialVideoClosed;

        public void LoadInterstitialVideo()
        {
            if (IsInTestMode || String.IsNullOrWhiteSpace(AdUnitId_InterstitialVideo))
                AdUnitId_InterstitialVideo = GoogleSamplesAdUnitIds.iOSSampleAdUnitId_InterstitialVideo;
            LoadInterstitial(AdUnitId_InterstitialVideo);
        }

        public void LoadInterstitialVideo(string adUnit)
        {
            if (!CrossAdMob.Current.IsEnabled)
                return;

            if (IsInTestMode || String.IsNullOrWhiteSpace(adUnit))
                adUnit = GoogleSamplesAdUnitIds.iOSSampleAdUnitId_InterstitialVideo;

            CreateInterstitialVideoAd(adUnit);

            _interstitialVideoAd.LoadRequest(GetRequest());
        }

        public bool IsInterstitialVideoLoaded()
        {
            return _interstitialVideoAd != null && _interstitialVideoAd.IsReady;
        }

        public void ShowInterstitialVideo()
        {
            if (!CrossAdMob.Current.IsEnabled)
                return;

            if (_interstitialVideoAd != null && _interstitialVideoAd.IsReady)
            {
                var window = UIApplication.SharedApplication.KeyWindow;
                var vc = window.RootViewController;
                while (vc.PresentedViewController != null)
                {
                    vc = vc.PresentedViewController;
                }

                _interstitialVideoAd.Present(vc);
            }
        }

        private void CreateInterstitialVideoAd(string adUnit)
        {
            try
            {
                if (_interstitialVideoAd != null)
                {
                    _interstitialVideoAd.WillPresentScreen -= _interstitialVideoAd_WillPresentScreen;
                    _interstitialVideoAd.FailedToPresentScreen -= _interstitialVideoAd_FailedToPresentScreen;
                    _interstitialVideoAd.ReceiveAdFailed -= _interstitialVideoAd_ReceiveAdFailed;
                    _interstitialVideoAd.AdReceived -= _interstitialVideoAd_AdReceived;
                    _interstitialVideoAd.ScreenDismissed -= _interstitialVideoAd_ScreenDismissed;
                    _interstitialVideoAd = null;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            _interstitialVideoAd = new Interstitial(adUnit);

            _interstitialVideoAd.WillPresentScreen += _interstitialVideoAd_WillPresentScreen;
            _interstitialVideoAd.FailedToPresentScreen += _interstitialVideoAd_FailedToPresentScreen;
            _interstitialVideoAd.ReceiveAdFailed += _interstitialVideoAd_ReceiveAdFailed;
            _interstitialVideoAd.AdReceived += _interstitialVideoAd_AdReceived;
            _interstitialVideoAd.ScreenDismissed += _interstitialVideoAd_ScreenDismissed;

        }


        private void _interstitialVideoAd_WillPresentScreen(object sender, EventArgs e)
        {
            OnInterstitialVideoLoaded?.Invoke(sender, e);
        }

        private void _interstitialVideoAd_ReceiveAdFailed(object sender, InterstitialDidFailToReceiveAdWithErrorEventArgs e)
        {
            OnInterstitialVideoFailed?.Invoke(sender, new AdMobErrorEventArgs() { Code = (int?)(e?.Error?.Code), Domain = e?.Error?.Domain, Message = e?.Error?.LocalizedDescription, FullStacktrace = e?.Error?.ToString() });
        }

        private void _interstitialVideoAd_FailedToPresentScreen(object sender, EventArgs e)
        {
            OnInterstitialVideoFailed?.Invoke(sender, new AdMobErrorEventArgs() { Message = "Failed To Present Screen" });
        }

        private void _interstitialVideoAd_AdReceived(object sender, EventArgs e)
        {
            OnInterstitialVideoOpened?.Invoke(sender, e);
        }

        private void _interstitialVideoAd_ScreenDismissed(object sender, EventArgs e)
        {
            OnInterstitialVideoClosed?.Invoke(sender, e);
        }

        #endregion

        #region RewardedVideo

        RewardedAd _rewardedAd;
        public IntPtr Handle => _rewardedAd.Handle;

        public event EventHandler OnRewardedVideoAdLoaded;
        public event EventHandler<AdMobErrorEventArgs> OnRewardedVideoAdFailedToLoad;
        public event EventHandler OnRewardedVideoAdOpened;
        public event EventHandler<AdMobErrorEventArgs> OnRewardedVideoAdFailedToShow;
        public event EventHandler OnRewardedVideoAdClosed;
        public event EventHandler<AdMobRewardedEventArgs> OnRewardedUser;

        public void LoadRewardedVideo(AdMobRewardedOptions options = null)
        {
            if (IsInTestMode || String.IsNullOrWhiteSpace(AdUnitId_RewardedVideo))
                AdUnitId_RewardedVideo = GoogleSamplesAdUnitIds.iOSSampleAdUnitId_RewardedVideo;
            LoadRewardedVideo(AdUnitId_RewardedVideo, options);
        }

        public void LoadRewardedVideo(string adUnit, AdMobRewardedOptions options = null)
        {
            if (!CrossAdMob.Current.IsEnabled)
                return;

            if (_rewardedAd == null)
                _rewardedAd = new RewardedAd(adUnit);

            //RewardBasedVideoAd.SharedInstance.CustomRewardString = options?.CustomData;
            _rewardedAd.LoadRequest(GetRequest(), LoadCompletionHandler);
        }

        private void LoadCompletionHandler(RequestError error)
        {
            if (error != null)
            {
                OnRewardedVideoAdFailedToLoad?.Invoke(_rewardedAd.AdUnitId, new AdMobErrorEventArgs() { Code = (int?)error?.Code, Domain = error?.Domain, Message = error?.LocalizedDescription, FullStacktrace = error?.ToString() });
            }
            else
            {
                OnRewardedVideoAdLoaded?.Invoke(_rewardedAd.AdUnitId, null);
            }
        }

        public bool IsRewardedVideoLoaded()
        {
            return _rewardedAd != null && _rewardedAd.IsReady;
        }

        public void ShowRewardedVideo()
        {
            if (!CrossAdMob.Current.IsEnabled)
                return;

            if (_rewardedAd.IsReady)
            {
                var window = UIApplication.SharedApplication.KeyWindow;
                var vc = window.RootViewController;
                while (vc.PresentedViewController != null)
                {
                    vc = vc.PresentedViewController;
                }
                _rewardedAd.Present(vc, this);
            }
        }

        public void UserDidEarnReward(RewardedAd rewardedAd, AdReward reward)
        {
            OnRewardedUser?.Invoke(rewardedAd.AdUnitId, new AdMobRewardedEventArgs() { RewardAmount = (int)reward.Amount, RewardType = reward.Type });
        }

        public void DidFailToPresent(RewardedAd rewardedAd, NSError error)
        {
            OnRewardedVideoAdFailedToShow?.Invoke(rewardedAd.AdUnitId, new AdMobErrorEventArgs() { Code = (int?)error?.Code, Domain = error?.Domain, Message = error?.LocalizedDescription, FullStacktrace = error?.ToString() });
        }

        public void DidPresent(RewardedAd rewardedAd)
        {
            OnRewardedVideoAdOpened?.Invoke(rewardedAd.AdUnitId, null);
        }

        public void DidDismiss(RewardedAd rewardedAd)
        {
            OnRewardedVideoAdClosed?.Invoke(rewardedAd.AdUnitId, null);
        }


        #endregion

        public static Request GetRequest()
        {
            UpdateRequestConfiguration();

            bool addExtra = false;
            var request = Request.GetDefaultRequest();
            var dict = new Dictionary<string, string>();

            if (!CrossAdMob.Current.UserPersonalizedAds)
            {
                dict.Add(new NSString("npa"), new NSString("1"));
                addExtra = true;
            }

            if (CrossAdMob.Current.UseRestrictedDataProcessing)
            {
                dict.Add(new NSString("rdp"), new NSString("1"));
                addExtra = true;
            }

            if (CrossAdMob.Current.CustomParameters.Count > 0)
            {
                foreach (KeyValuePair<string, string> param in CrossAdMob.Current.CustomParameters)
                {
                    dict.Add(new NSString(param.Key), new NSString(param.Value));
                }
                addExtra = true;
            }

            if (addExtra)
            {
                var extras = new Extras
                {
                    AdditionalParameters = NSDictionary.FromObjectsAndKeys(dict.Values.ToArray(), dict.Keys.ToArray())
                };
                request.RegisterAdNetworkExtras(extras);
            }

            return request;
        }

        private static void UpdateRequestConfiguration()
        {
            //TestDeviceIds
            if (CrossAdMob.Current.TestDevices != null)
            {
                MobileAds.SharedInstance.RequestConfiguration.TestDeviceIdentifiers = CrossAdMob.Current.TestDevices.ToArray();
            }
            //TagForChildDirectedTreatment
            if (CrossAdMob.Current.TagForChildDirectedTreatment.HasValue)
            {
                MobileAds.SharedInstance.RequestConfiguration.TagForChildDirectedTreatment(CrossAdMob.Current.TagForChildDirectedTreatment.Value); 
            }
            //TagForUnderAgeOfConsent
            if (CrossAdMob.Current.TagForUnderAgeOfConsent.HasValue)
            {
                MobileAds.SharedInstance.RequestConfiguration.TagForUnderAgeOfConsent(CrossAdMob.Current.TagForUnderAgeOfConsent.Value);
            }
            //MaxAdContentRating
            switch (CrossAdMob.Current.MaxAdContentRating)
            {
                case MaxAdContentRating.GeneralAudiences:
                    MobileAds.SharedInstance.RequestConfiguration.MaxAdContentRating = "GADMaxAdContentRatingGeneral";
                    break;
                case MaxAdContentRating.MatureAudiences:
                    MobileAds.SharedInstance.RequestConfiguration.MaxAdContentRating = "GADMaxAdContentRatingMatureAudience";
                    break;
                case MaxAdContentRating.ParentalGuidance:
                    MobileAds.SharedInstance.RequestConfiguration.MaxAdContentRating = "GADMaxAdContentRatingParentalGuidance";
                    break;
                case MaxAdContentRating.Teen:
                    MobileAds.SharedInstance.RequestConfiguration.MaxAdContentRating = "GADMaxAdContentRatingTeen";
                    break;
            }

        }

        public void Dispose()
        {
            _rewardedAd = null;
        }
    }
}