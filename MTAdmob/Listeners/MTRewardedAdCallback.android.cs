using System;
using Android.Gms.Ads;
using Android.Gms.Ads.Rewarded;
using MarcTron.Plugin.CustomEventArgs;

namespace MarcTron.Plugin.Listeners
{
    public class MTRewardedAdCallback : RewardedAdCallback
    {
        private string _adUnit;

        public MTRewardedAdCallback(string adUnit)
        {
            _adUnit = adUnit;
        }

        public event EventHandler AdOpened;
        public event EventHandler<MTErrorEventArgs> AdFailedToShow;
        public event EventHandler AdClosed;
        public event EventHandler<MTRewardedEventArgs> UserEarnedReward;

        public override void OnRewardedAdOpened()
        {
            base.OnRewardedAdOpened();
            AdOpened?.Invoke(_adUnit, null);
            Console.WriteLine($"OnRewardedAdOpened({_adUnit})");
        }

        public override void OnRewardedAdFailedToShow(AdError error)
        {
            base.OnRewardedAdFailedToShow(error);
            AdFailedToShow?.Invoke(_adUnit, new MTErrorEventArgs() { Code = error?.Code, Domain = error?.Domain, Message = error?.Message, FullStacktrace = error?.ToString() });
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
            UserEarnedReward?.Invoke(_adUnit, new MTRewardedEventArgs() { RewardAmount=reward.Amount, RewardType=reward.Type });
            Console.WriteLine($"OnUserEarnedReward({_adUnit}) type({reward.Type}) amount({reward.Amount})");
        }

    }
}