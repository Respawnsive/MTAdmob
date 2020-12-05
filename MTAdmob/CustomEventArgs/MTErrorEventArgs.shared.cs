using System;

namespace MarcTron.Plugin.CustomEventArgs
{
    // ReSharper disable once InconsistentNaming
    public class MTErrorEventArgs : EventArgs
    {
        public int? Code;
        public string Domain;
        public string Message;
        public string FullStacktrace;
    }
}
