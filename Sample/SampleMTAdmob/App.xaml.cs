using System.Collections.Generic;
using MarcTron.Plugin;
using MarcTron.Plugin.Enums;
using Xamarin.Forms;

namespace SampleMTAdmob
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //CrossMTAdmob.Current.UserPersonalizedAds = true;
            //CrossMTAdmob.Current.MaxAdContentRating = MaxAdContentRating.GeneralAudiences;
            //CrossMTAdmob.Current.TagForChildDirectedTreatment = true;
            //CrossMTAdmob.Current.TagForUnderAgeOfConsent = true;
            //CrossMTAdmob.Current.UseRestrictedDataProcessing = true;
            //var customparam = new Dictionary<string, string>();
            //customparam.Add("mykey", "myvalue");
            //CrossMTAdmob.Current.CustomParameters = customparam;
            //CrossMTAdmob.Current.TestDevices = new List<string>() {"YOUR PHONE IDS HERE", "", ""};

            CrossMTAdmob.Current.IsInTestMode = true;

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
