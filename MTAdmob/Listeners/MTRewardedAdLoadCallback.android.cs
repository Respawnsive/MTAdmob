using System;
using Android.Gms.Ads;
using Android.Gms.Ads.Rewarded;
using MarcTron.Plugin.CustomEventArgs;

namespace MarcTron.Plugin.Listeners
{
    public class MTRewardedAdLoadCallback : RewardedAdLoadCallback
    {
        private string _adUnit;

        public MTRewardedAdLoadCallback(string adUnit)
        {
            _adUnit = adUnit;
        }

        public event EventHandler AdLoaded;
        public event EventHandler<MTErrorEventArgs> AdFailedToLoad;

        public override void OnRewardedAdLoaded()
        {
            base.OnRewardedAdLoaded();
            AdLoaded?.Invoke(_adUnit, null);
            Console.WriteLine($"OnRewardedAdLoaded({_adUnit})");
        }

        public override void OnRewardedAdFailedToLoad(LoadAdError error)
        {
            base.OnRewardedAdFailedToLoad(error);
            AdFailedToLoad?.Invoke(_adUnit, new MTErrorEventArgs() { Code = error?.Code, Domain = error?.Domain, Message = error?.Message, FullStacktrace = error?.ToString() });
            Console.WriteLine($"OnRewardedAdFailedToLoad({_adUnit})");
        }
    }
}