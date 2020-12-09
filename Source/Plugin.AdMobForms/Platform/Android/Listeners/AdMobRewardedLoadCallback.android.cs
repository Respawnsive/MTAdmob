using System;
using Android.Gms.Ads;
using Android.Gms.Ads.Rewarded;

namespace Plugin.AdMobForms.Platform.Android
{
    public class AdMobRewardedLoadCallback : RewardedAdLoadCallback
    {
        private string _adUnit;

        public AdMobRewardedLoadCallback(string adUnit)
        {
            _adUnit = adUnit;
        }

        public event EventHandler AdLoaded;
        public event EventHandler<AdMobErrorEventArgs> AdFailedToLoad;

        public override void OnRewardedAdLoaded()
        {
            base.OnRewardedAdLoaded();
            AdLoaded?.Invoke(_adUnit, null);
            Console.WriteLine($"OnRewardedAdLoaded({_adUnit})");
        }

        public override void OnRewardedAdFailedToLoad(LoadAdError error)
        {
            base.OnRewardedAdFailedToLoad(error);
            AdFailedToLoad?.Invoke(_adUnit, new AdMobErrorEventArgs() { Code = error?.Code, Domain = error?.Domain, Message = error?.Message, FullStacktrace = error?.ToString() });
            Console.WriteLine($"OnRewardedAdFailedToLoad({_adUnit})");
        }
    }
}