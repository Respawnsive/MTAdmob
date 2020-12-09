using System;

namespace Plugin.AdMobForms
{
    // ReSharper disable once InconsistentNaming
    public class AdMobErrorEventArgs : EventArgs
    {
        public int? Code;
        public string Domain;
        public string Message;
        public string FullStacktrace;
    }
}
