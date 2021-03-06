﻿using System;
using Android.Gms.Ads;

namespace Plugin.AdMobForms.Platform.Android
{
    public class AdMobListener : AdListener
    {
        private string _adUnit;

        public AdMobListener(string adUnit)
        {
            _adUnit = adUnit;
        }

        public event EventHandler AdLoaded;
        public event EventHandler<AdMobErrorEventArgs> AdFailedToLoad;
        public event EventHandler AdOpened;
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
            AdFailedToLoad?.Invoke(_adUnit, new AdMobErrorEventArgs() { Code = error?.Code, Domain = error?.Domain, Message = error?.Message, FullStacktrace=error?.ToString() });
            Console.WriteLine($"OnAdFailedToLoad({_adUnit})");
        }

        public override void OnAdOpened()
        {
            base.OnAdOpened();
            AdOpened?.Invoke(_adUnit, null);
            Console.WriteLine($"OnAdOpened({_adUnit})");
        }

        public override void OnAdClosed()
        {
            base.OnAdClosed();
            AdClosed?.Invoke(_adUnit, null);
            Console.WriteLine($"OnAdClosed({_adUnit})");
        }
    }
}