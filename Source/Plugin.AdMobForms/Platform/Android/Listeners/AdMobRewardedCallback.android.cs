using System;
using Android.Gms.Ads;
using Android.Gms.Ads.Rewarded;

namespace Plugin.AdMobForms.Platform.Android
{
    public class AdMobRewardedCallback : RewardedAdCallback
    {
        private string _adUnit;

        public AdMobRewardedCallback(string adUnit)
        {
            _adUnit = adUnit;
        }

        public event EventHandler AdOpened;
        public event EventHandler<AdMobErrorEventArgs> AdFailedToShow;
        public event EventHandler AdClosed;
        public event EventHandler<AdMobRewardedEventArgs> UserEarnedReward;

        public override void OnRewardedAdOpened()
        {
            base.OnRewardedAdOpened();
            AdOpened?.Invoke(_adUnit, null);
            Console.WriteLine($"OnRewardedAdOpened({_adUnit})");
        }

        public override void OnRewardedAdFailedToShow(AdError error)
        {
            base.OnRewardedAdFailedToShow(error);
            AdFailedToShow?.Invoke(_adUnit, new AdMobErrorEventArgs() { Code = error?.Code, Domain = error?.Domain, Message = error?.Message, FullStacktrace = error?.ToString() });
            Console.WriteLine($"OnRewardedAdFailedToShow({_adUnit})");
        }

        public override void OnRewardedAdClosed()
        {
            base.OnRewardedAdClosed();
            AdClosed?.Invoke(_adUnit, null);
            Console.WriteLine($"OnRewardedAdClosed({_adUnit})");
        }


        public override void OnUserEarnedReward(IRewardItem reward)
        {
            UserEarnedReward?.Invoke(_adUnit, new AdMobRewardedEventArgs() { RewardAmount=reward.Amount, RewardType=reward.Type });
            Console.WriteLine($"OnUserEarnedReward({_adUnit}) type({reward.Type}) amount({reward.Amount})");
        }

    }
}