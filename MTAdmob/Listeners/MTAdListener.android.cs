using System;
using Android.Gms.Ads;
using MarcTron.Plugin.CustomEventArgs;

namespace MarcTron.Plugin.Listeners
{
    public class MTAdListener : AdListener
    {
        private string _adUnit;

        public MTAdListener(string adUnit)
        {
            _adUnit = adUnit;
        }

        public event EventHandler AdLoaded;
        public event EventHandler<MTErrorEventArgs> AdFailedToLoad;
        public event EventHandler AdOpened;
        public event EventHandler AdLeftApplication;
        public event EventHandler AdClosed;

        public override void OnAdLoaded()
        {
            base.OnAdLoaded();

            AdLoaded?.Invoke(_adUnit, null);
            Console.WriteLine($"OnAdLoaded({_adUnit})");
        }

        public override void OnAdFailedToLoad(LoadAdError error)
        {
            base.OnAdFailedToLoad(error);
            AdFailedToLoad?.Invoke(_adUnit, new MTErrorEventArgs() { Code = error?.Code, Domain = error?.Domain, Message = error?.Message, FullStacktrace=error?.ToString() });
            Console.WriteLine($"OnAdFailedToLoad({_adUnit})");
        }

        public override void OnAdOpened()
        {
            base.OnAdOpened();
            AdOpened?.Invoke(_adUnit, null);
            Console.WriteLine($"OnAdOpened({_adUnit})");
        }

        public override void OnAdLeftApplication()
        {
            base.OnAdLeftApplication();
            AdLeftApplication?.Invoke(_adUnit, null);
            Console.WriteLine($"OnAdLeftApplication({_adUnit})");
        }

        public override void OnAdClosed()
        {
            base.OnAdClosed();
            AdClosed?.Invoke(_adUnit, null);
            Console.WriteLine($"OnAdClosed({_adUnit})");
        }
    }
}