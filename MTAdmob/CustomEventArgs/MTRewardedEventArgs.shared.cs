using System;

namespace MarcTron.Plugin.CustomEventArgs
{
    public class MTRewardedEventArgs : EventArgs
    {
        public int RewardAmount;
        public string RewardType;
    }
}
