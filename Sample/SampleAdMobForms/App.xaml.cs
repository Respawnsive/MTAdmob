using System.Collections.Generic;
using Plugin.AdMobForms;
using Xamarin.Forms;

namespace SampleAdMobForms
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            if (CrossAdMob.IsSupported)
            {
                //General AdMob Ads Enabling -> if a user has a paid subscription without Ads -> just set it to false !
                CrossAdMob.Current.IsEnabled = true;

                //Configure your General AdMob Options
                CrossAdMob.Current.TagForChildDirectedTreatment = true;
                CrossAdMob.Current.TagForUnderAgeOfConsent = true;
                CrossAdMob.Current.MaxAdContentRating = MaxAdContentRating.GeneralAudiences;
                CrossAdMob.Current.SetAdsVolume(0.75f);
                CrossAdMob.Current.SetAdsMuted(false);

                //NetworkExtra
                //CrossAdMob.Current.UserPersonalizedAds = true;
                //CrossAdMob.Current.UseRestrictedDataProcessing = true;
                //You can even pass custom key/value to AdMob Ads
                //var customparam = new Dictionary<string, string>();
                //customparam.Add("mykey", "myvalue");
                //CrossAdMob.Current.CustomParameters = customparam;

#if DEBUG
                //Set default Google Samples AdUnitIds
                CrossAdMob.Current.TestDevices = new List<string>() { "1F9B42C3D255D46C491C2CBA133DA3DD" };//{ "YOUR PHONE1 TEST ID HERE", "YOUR PHONE2 TEST ID HERE", "YOUR PHONE3 TEST ID HERE" };
                CrossAdMob.Current.IsInTestMode = true;
#else
                //Set your production AdUnitIds
                //CrossAdMob.Current.IsInTestMode = false; <- default value, can be forced.
                CrossAdMob.Current.AdUnitId_Banner = Device.RuntimePlatform == Device.Android ? "YourAndroidAdMobBannerAdUnitID" : "YourIOSAdMobBannerAdUnitID";
                CrossAdMob.Current.AdUnitId_Interstitial = Device.RuntimePlatform == Device.Android ? "YourAndroidAdMobInterstitialAdUnitID" : "YourIOSAdMobInterstitialAdUnitID";
                CrossAdMob.Current.AdUnitId_InterstitialVideo = Device.RuntimePlatform == Device.Android ? "YourAndroidAdMobInterstitialVideoAdUnitID" : "YourIOSAdMobInterstitialVideoAdUnitID";
                CrossAdMob.Current.AdUnitId_NativeAdvanced = Device.RuntimePlatform == Device.Android ? "YourAndroidAdMobNativeAdvancedAdUnitID" : "YourIOSAdMobNativeAdvancedAdUnitID";
                CrossAdMob.Current.AdUnitId_NativeAdvancedVideo = Device.RuntimePlatform == Device.Android ? "YourAndroidAdMobNativeAdvancedVideoAdUnitID" : "YourIOSAdMobNativeAdvancedVideoAdUnitID";
                CrossAdMob.Current.AdUnitId_RewardedVideo = Device.RuntimePlatform == Device.Android ? "YourAndroidAdMobRewardedVideoAdUnitID" : "YourIOSAdMobRewardedVideoAdUnitID";
#endif
            }
            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
