//using System;
//using System.Collections.Generic;
//using System.Linq;
//using Foundation;
//using Google.MobileAds;
//using Plugin.AdMobForms.Interfaces;
//using UIKit;

//namespace Plugin.AdMobForms
//{
//    /// <summary>
//    /// Implementation for iOS
//    /// </summary>
//    public class AdMobInterstitialDelegate : IInterstitialDelegate
//    {
//        public IntPtr Handle { get; }

//        public AdMobInterstitialDelegate(IntPtr handle)
//        {
//            Handle = handle;
//        }

//        public event EventHandler OnInterstitialLoaded;
//        public event EventHandler<AdMobErrorEventArgs> OnInterstitialFailed;
//        public event EventHandler OnInterstitialOpened;
//        public event EventHandler OnInterstitialClosed;

//        public void DidReceiveAd(Interstitial e)
//        {
//            OnInterstitialLoaded?.Invoke(null, null);
//        }

//        private void _interstitialAd_ReceiveAdFailed(object sender, InterstitialDidFailToReceiveAdWithErrorEventArgs e)
//        {
//            OnInterstitialFailed?.Invoke(sender, new AdMobErrorEventArgs() { Code = (int?)(e?.Error?.Code), Domain=e?.Error?.Domain, Message = e?.Error?.LocalizedDescription, FullStacktrace=e?.Error?.ToString() });
//        }

//        private void _interstitialAd_FailedToPresentScreen(object sender, EventArgs e)
//        {
//            OnInterstitialFailed?.Invoke(sender, new AdMobErrorEventArgs() { Message = "Failed To Present Screen" });
//        }

//        private void _interstitialAd_AdReceived(object sender, EventArgs e)
//        {
//            OnInterstitialOpened?.Invoke(sender, e);
//        }

//        private void _interstitialAd_ScreenDismissed(object sender, EventArgs e)
//        {
//            OnInterstitialClosed?.Invoke(sender, e);
//        }

//        private void InterstitialAdLoaded(object sender, EventArgs e)
//        {
//            OnInterstitialLoaded?.Invoke(sender, e);
//        }

//        private void InterstitialAdOpened(object sender, EventArgs e)
//        {
//            OnInterstitialOpened?.Invoke(sender, e);
//        }

//        private void InterstitialAdClosed(object sender, EventArgs e)
//        {
//            OnInterstitialClosed?.Invoke(sender, e);
//        }

//        public void Dispose()
//        {
//            throw new NotImplementedException();
//        }
//    }
//}