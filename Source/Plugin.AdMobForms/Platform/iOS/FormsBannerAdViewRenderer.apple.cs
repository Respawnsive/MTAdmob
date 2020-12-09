using CoreGraphics;
using Google.MobileAds;
using Plugin.AdMobForms.Controls;
using Plugin.AdMobForms.Platform.iOS;
using System;
using System.ComponentModel;
using UIKit;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(FormsBannerAdView), typeof(FormsBannerAdViewRenderer))]

namespace Plugin.AdMobForms.Platform.iOS
{
    public class FormsBannerAdViewRenderer : ViewRenderer<FormsBannerAdView, BannerView>
    {
        string _adUnitId = string.Empty;
        BannerView _adView;
        FormsBannerAdView _formsAdView;

        protected override void OnElementChanged(ElementChangedEventArgs<FormsBannerAdView> e)
        {
            base.OnElementChanged(e);

            if (!CrossAdMob.Current.IsEnabled)
                return;

            if (_adView != null)
                return;

            if (Control == null && e.NewElement != null)
            {
                UIViewController controller = GetVisibleViewController();
                if (controller != null)
                {
                    _formsAdView = e.NewElement;
                    _formsAdView.AdsLoaded += _formsAdView_AdsLoaded;
                    CreateNativeAdView(controller);
                    SetNativeControl(_adView);
                }
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == "Width")
            {
                UpdateFormsHeighRatioFromWidth();
            }
        }

        private void _formsAdView_AdsLoaded(object sender, EventArgs e)
        {
            UpdateFormsHeighRatioFromWidth();
        }

        private void CreateNativeAdView(UIViewController controller)
        {
            if (!CrossAdMob.Current.IsEnabled)
                return;

            if (_adView != null)
                return;

            if (CrossAdMob.Current.IsInTestMode)
                _adUnitId = GoogleSamplesAdUnitIds.iOSSampleAdUnitId_Banner;
            else
                _adUnitId = !String.IsNullOrEmpty(_formsAdView.AdsId) ?
                    _formsAdView.AdsId : !String.IsNullOrEmpty(CrossAdMob.Current.AdUnitId_Banner) ?
                    CrossAdMob.Current.AdUnitId_Banner : GoogleSamplesAdUnitIds.iOSSampleAdUnitId_Banner;
            if (string.IsNullOrEmpty(_adUnitId))
            {
                Console.WriteLine("You must set the adsID before using it");
            }

            _adView = new BannerView(getAdSize(), new CGPoint(0, UIScreen.MainScreen.Bounds.Size.Height - AdSizeCons.Banner.Size.Height))
            {
                AdUnitId = _adUnitId,
                RootViewController = controller,
            };
            _adView.WillPresentScreen += _adView_WillPresentScreen;
            _adView.ReceiveAdFailed += _adView_ReceiveAdFailed;
            _adView.AdReceived += _adView_AdReceived;
            _adView.ScreenDismissed += _adView_ScreenDismissed;

            var request = AdMobForms.GetRequest();
            _adView.LoadRequest(request);
        }

        #region Events

        private void _adView_WillPresentScreen(object sender, EventArgs e)
        {
            _formsAdView?.AdLoaded(sender, e);
        }
        private void _adView_ScreenDismissed(object sender, EventArgs e)
        {
            _formsAdView?.AdClosed(sender, e);
        }

        private void _adView_AdReceived(object sender, EventArgs e)
        {
            _formsAdView?.AdOpened(sender, e);
        }

        private void _adView_ReceiveAdFailed(object sender, BannerViewErrorEventArgs e)
        {
            _formsAdView?.AdFailedToLoad(sender, new AdMobErrorEventArgs() { Code = (int?)(e?.Error?.Code), Domain = e?.Error?.Domain, Message = e?.Error?.LocalizedDescription, FullStacktrace = e?.Error?.ToString() });
        }

        #endregion

        private AdSize getAdSize()
        {
            var adsize = AdSizeCons.SmartBannerPortrait;
            var devicewidth = (int)(DeviceDisplay.MainDisplayInfo.Width / DeviceDisplay.MainDisplayInfo.Density);

            if (_formsAdView.WidthRequest == -1 && _formsAdView.HeightRequest == -1)
            {
                //Get Adaptive AdSize from device width    
                adsize = AdSizeCons.GetFromCGSize(new CGSize(devicewidth, devicewidth*0.15));
                //Set Forms HeightRequest from Calculated AdSize and ratio DeviceWidth/AdWidth
                _formsAdView.HeightRequest = adsize.Size.Height * (devicewidth / adsize.Size.Width);
            }
            else if (_formsAdView.WidthRequest != -1 && _formsAdView.HeightRequest != -1)
            {
                //Get custom specific AdSize
                adsize = AdSizeCons.GetFromCGSize(new CGSize(_formsAdView.WidthRequest, _formsAdView.HeightRequest));
                //Adjust HeightRatio from AdSize
                _formsAdView.HeightRequest = adsize.Size.Height * (_formsAdView.WidthRequest / adsize.Size.Width);
            }
            else if (_formsAdView.WidthRequest != -1)
            {
                //Get Adaptive AdSize from WidthRequest
                adsize = AdSizeCons.GetFromCGSize(new CGSize(_formsAdView.WidthRequest, _formsAdView.WidthRequest * 0.15));
                //Set Forms HeightRequest from Calculated AdSize and ratio WidthRequest/AdWidth
                _formsAdView.HeightRequest = adsize.Size.Height * (_formsAdView.WidthRequest / adsize.Size.Width);
            }
            return adsize;

        }

        private void UpdateFormsHeighRatioFromWidth()
        {
            var test = _formsAdView.Width;
            var test2 = _formsAdView.Height;
            var test3 = _adView.AdSize.Size.Width;
            var test4 = _adView.AdSize.Size.Height;
            //Adjust HeightRatio from AdSize
            _formsAdView.HeightRequest = _adView.AdSize.Size.Height * (_formsAdView.Width / _adView.AdSize.Size.Width);
        }

        UIViewController GetVisibleViewController()
        {
            var rootController = UIApplication.SharedApplication.Delegate?.GetWindow()?.RootViewController;

            if (rootController == null)
                return null;

            if (rootController.PresentedViewController == null)
                return rootController;

            if (rootController.PresentedViewController is UINavigationController controller)
            {
                return controller.VisibleViewController;
            }

            if (rootController.PresentedViewController is UITabBarController barController)
            {
                return barController.SelectedViewController;
            }

            return rootController.PresentedViewController;
        }
    }
}