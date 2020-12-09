//using System;
//using Android.Content;
//using Android.Gms.Ads;
//using Android.Gms.Ads.Formats;
//using Android.OS;
//using Android.Widget;
//using Google.Ads.Mediation.Admob;
//using Plugin.AdMobForms.Controls;
//using Plugin.AdMobForms.Listeners;
//using Xamarin.Forms;
//using Xamarin.Forms.Platform.Android;
//using Plugin.AdMobForms.Platform.Android;

//[assembly: ExportRenderer(typeof(FormsNativeAdView), typeof(FormsNativeAdViewRenderer))]

//namespace Plugin.AdMobForms.Platform.Android
//{
//    public class FormsNativeAdViewRenderer : ViewRenderer<FormsNativeAdView, AdView>
//    {
//        string _adUnitId = string.Empty;
//        readonly AdSize _adSize = AdSize.SmartBanner;
//        AdView _adView;

//        public FormsNativeAdViewRenderer(Context context) : base(context)
//        {
//        }

//        private void CreateNativeControl(FormsNativeAdView formsBannerAdView, string adsId, bool? personalizedAds)
//        {
//            if (_adView != null)
//                return;

//            _adUnitId = !string.IsNullOrEmpty(adsId) ? adsId : CrossAdMob.Current.AdsId;

//            if (string.IsNullOrEmpty(_adUnitId))
//            {
//                Console.WriteLine("You must set the adsID before using it");
//            }

//            var listener = new MyAdBannerListener();

//            listener.AdClosed += formsBannerAdView.AdClosed;
//            listener.AdOpened += formsBannerAdView.AdOpened;
//            listener.AdFailedToLoad += formsBannerAdView.AdFailedToLoad;
//            listener.AdLoaded += formsBannerAdView.AdLoaded;

//            var adLoader = new AdLoader.Builder(Context, _adUnitId).ForContentAd().WithAdListener(listener).Build();


//            _adView = new AdView(Context)
//            {
//                AdSize = _adSize,
//                AdUnitId = _adUnitId,
//                AdListener = listener,
//                LayoutParameters = new LinearLayout.LayoutParams(LayoutParams.WrapContent, LayoutParams.WrapContent)
//            };

//            var requestBuilder = new AdRequest.Builder();
//            if (CrossAdMob.Current.TestDevices != null)
//            {
//                foreach (var testDevice in CrossAdMob.Current.TestDevices)
//                {
//                    requestBuilder.AddTestDevice(testDevice);
//                }
//            }

//            if ((personalizedAds.HasValue && personalizedAds.Value) || CrossAdMob.Current.UserPersonalizedAds)
//            {
//                adLoader.LoadAd(requestBuilder.Build());
//            }
//            else
//            {
//                Bundle bundleExtra = new Bundle();
//                bundleExtra.PutString("npa", "1");

//                adLoader.LoadAd(requestBuilder.AddNetworkExtrasBundle(Java.Lang.Class.FromType(typeof(AdMobAdapter)), bundleExtra).Build());
//            }
//        }

//        protected override void OnElementChanged(ElementChangedEventArgs<FormsNativeAdView> e)
//        {
//            base.OnElementChanged(e);
//            if (Control == null)
//            {
//                CreateNativeControl(e.NewElement, e.NewElement.AdsId, e.NewElement.PersonalizedAds);
//                SetNativeControl(_adView);
//            }
//        }
//    }
//}