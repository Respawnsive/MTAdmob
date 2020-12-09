using System;
using Xamarin.Forms;

namespace Plugin.AdMobForms.Controls
{
    public class FormsBannerAdView : View
    {
        public event EventHandler AdsClosed;
        public event EventHandler AdsOpened;
        public event EventHandler<AdMobErrorEventArgs> AdsFailedToLoad;
        public event EventHandler AdsLoaded;

        public static readonly BindableProperty AdsIdProperty = BindableProperty.Create("AdsId", typeof(string), typeof(FormsBannerAdView));

        public string AdsId
        {
            get => (string)GetValue(AdsIdProperty);
            set => SetValue(AdsIdProperty, value);
        }

        public void AdClosed(object sender, EventArgs e)
        {
            AdsClosed?.Invoke(sender, e);
        }

        public void AdOpened(object sender, EventArgs e)
        {
            AdsOpened?.Invoke(sender, e);
        }

        public void AdFailedToLoad(object sender, AdMobErrorEventArgs e)
        {
            AdsFailedToLoad?.Invoke(sender, e);
        }

        public void AdLoaded(object sender, EventArgs e)
        {
            AdsLoaded?.Invoke(sender, e);
        }
    }
}
