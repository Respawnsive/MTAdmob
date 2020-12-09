using Android.Content;
using Android.Gms.Ads;
using Android.Graphics.Drawables;
using Android.Util;
using Android.Views;
using Android.Widget;
using Plugin.AdMobForms.Controls;
using Plugin.AdMobForms.Platform.Android;
using System;
using System.ComponentModel;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(FormsBannerAdView), typeof(FormsBannerAdViewRenderer))]

namespace Plugin.AdMobForms.Platform.Android
{
    public class FormsBannerAdViewRenderer : ViewRenderer<FormsBannerAdView, AdView>
    {
        string _adUnitId = string.Empty;
        AdView _adView;
        FormsBannerAdView _formsAdView;
        Context _context;

        public FormsBannerAdViewRenderer(Context context) : base(context)
        {
            _context = context;
        }


        protected override void OnElementChanged(ElementChangedEventArgs<FormsBannerAdView> e)
        {
            base.OnElementChanged(e);
            if (!CrossAdMob.Current.IsEnabled)
                return;

            if (Control == null && e.NewElement != null)
            {
                _formsAdView = e.NewElement;
                _formsAdView.AdsLoaded += _formsAdView_AdsLoaded;
                CreateNativeAdView();
                SetNativeControl(_adView);
            }
        }

        private void _formsAdView_AdsLoaded(object sender, EventArgs e)
        {
            UpdateFormsHeighRatioFromWidth();
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == "Width")
            {
                UpdateFormsHeighRatioFromWidth();
            }
        }


        private void CreateNativeAdView()
        {
            if (!CrossAdMob.Current.IsEnabled)
                return;

            if (_adView != null)
                return;

            //adsId
            if (CrossAdMob.Current.IsInTestMode)
                _adUnitId = GoogleSamplesAdUnitIds.AndroidSampleAdUnitId_Banner;
            else
                _adUnitId = !String.IsNullOrEmpty(_formsAdView.AdsId) ?
                    _formsAdView.AdsId : !String.IsNullOrEmpty(CrossAdMob.Current.AdUnitId_Banner) ? 
                    CrossAdMob.Current.AdUnitId_Banner : GoogleSamplesAdUnitIds.AndroidSampleAdUnitId_Banner;
            if (string.IsNullOrEmpty(_adUnitId))
            {
                Console.WriteLine("You must set the adsID before using it");
            }

            var listener = new AdMobListener(_formsAdView.AdsId);
            listener.AdClosed += _formsAdView.AdClosed;
            listener.AdOpened += _formsAdView.AdOpened;
            listener.AdFailedToLoad += _formsAdView.AdFailedToLoad;
            listener.AdLoaded += _formsAdView.AdLoaded;
            var adsize = getAdSize();
            var back = new ColorDrawable(Color.Transparent.ToAndroid());
            _adView = new AdView(Context)
            {
                Background = back,
                AdSize = adsize,
                AdUnitId = _adUnitId,
                AdListener = listener,
                
                LayoutParameters = new LinearLayout.LayoutParams(LayoutParams.WrapContent, LayoutParams.WrapContent)
            };
            _adView.LoadAd(AdMobForms.GetRequest());
        }

        private AdSize getAdSize()
        {
            var adsize = AdSize.SmartBanner;
            var devicewidth = (int)(DeviceDisplay.MainDisplayInfo.Width / DeviceDisplay.MainDisplayInfo.Density);

            if (_formsAdView.WidthRequest == -1 && _formsAdView.HeightRequest == -1)
            {
                //Get Adaptive AdSize from device width    
                adsize = AdSize.GetCurrentOrientationAnchoredAdaptiveBannerAdSize(_context, devicewidth);
                //Set Forms HeightRequest from Calculated AdSize and ratio DeviceWidth/AdWidth
                _formsAdView.HeightRequest = adsize.Height * (devicewidth / adsize.Width);
            }
            else if (_formsAdView.WidthRequest != -1 && _formsAdView.HeightRequest != -1)
            {
                //Get custom specific AdSize
                adsize = new AdSize((int)_formsAdView.WidthRequest, (int)_formsAdView.HeightRequest);
                //Adjust HeightRatio from AdSize
                _formsAdView.HeightRequest = adsize.Height * (_formsAdView.WidthRequest / adsize.Width);
            }
            else if (_formsAdView.WidthRequest != -1)
            {
                //Get Adaptive AdSize from WidthRequest
                adsize = AdSize.GetCurrentOrientationAnchoredAdaptiveBannerAdSize(_context, (int)_formsAdView.WidthRequest);
                //Set Forms HeightRequest from Calculated AdSize and ratio WidthRequest/AdWidth
                _formsAdView.HeightRequest = adsize.Height * (_formsAdView.WidthRequest / adsize.Width);
            }
            return adsize;

        }

        private void UpdateFormsHeighRatioFromWidth()
        {
            var test = _formsAdView.Width;
            var test2 = _formsAdView.Height;
            var test3 = _adView.AdSize.Width;
            var test4 = _adView.AdSize.Height;
            //Adjust HeightRatio from AdSize
            _formsAdView.HeightRequest = _adView.AdSize.Height * (_formsAdView.Width / _adView.AdSize.Width);
        }
    }
}