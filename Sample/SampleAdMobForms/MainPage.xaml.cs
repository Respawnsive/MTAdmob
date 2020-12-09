using System;
using System.ComponentModel;
using System.Diagnostics;
using Plugin.AdMobForms;
using Plugin.AdMobForms.Controls;
using Xamarin.Forms;

namespace SampleAdMobForms
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            try
            {
                InitializeComponent();
            }
            catch(Exception ex)
            {

            }
        }

        #region TopBanner

        private void topbanner_AdsLoaded(object sender, EventArgs e)
        {

        }

        private void topbanner_AdsOpened(object sender, EventArgs e)
        {

        }


        private void topbanner_AdsClosed(object sender, EventArgs e)
        {

        }

        private void topbanner_AdsFailedToLoad(object sender, AdMobErrorEventArgs e)
        {

        }

        #endregion

        #region Custom Width Banner

        private void customwidthbanner_AdsLoaded(object sender, EventArgs e)
        {

        }

        private void customwidthbanner_AdsOpened(object sender, EventArgs e)
        {

        }


        private void customwidthbanner_AdsClosed(object sender, EventArgs e)
        {

        }

        private void customwidthbanner_AdsFailedToLoad(object sender, AdMobErrorEventArgs e)
        {

        }

        #endregion

        #region Custom Size Banner

        private void customsizebanner_AdsLoaded(object sender, EventArgs e)
        {

        }

        private void customsizebanner_AdsOpened(object sender, EventArgs e)
        {

        }


        private void customsizebanner_AdsClosed(object sender, EventArgs e)
        {

        }

        private void customsizebanner_AdsFailedToLoad(object sender, AdMobErrorEventArgs e)
        {

        }

        #endregion
    }
}
