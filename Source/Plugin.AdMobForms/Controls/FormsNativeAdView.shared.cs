//using System;
//using Xamarin.Forms;

//namespace Plugin.AdMobForms.Controls
//{
//    // ReSharper disable once InconsistentNaming
//    public class FormsNativeAdView : View
//    {
//        public event EventHandler AdsClosed;
//        public event EventHandler AdsOpened;
//        public event EventHandler AdsFailedToLoad;
//        public event EventHandler AdsLoaded;

//        public static readonly BindableProperty AdsIdProperty = BindableProperty.Create("AdsId", typeof(string), typeof(FormsBannerAdView));

//        public string AdsId
//        {
//            get => (string)GetValue(AdsIdProperty);
//            set => SetValue(AdsIdProperty, value);
//        }

//        internal void AdClosed(object sender, EventArgs e)
//        {
//            AdsClosed?.Invoke(sender, e);
//        }

//        internal void AdOpened(object sender, EventArgs e)
//        {
//            AdsOpened?.Invoke(sender, e);
//        }

//        internal void AdFailedToLoad(object sender, EventArgs e)
//        {
//            AdsFailedToLoad?.Invoke(sender, e);
//        }

//        internal void AdLoaded(object sender, EventArgs e)
//        {
//            AdsLoaded?.Invoke(sender, e);
//        }
//    }
//}
