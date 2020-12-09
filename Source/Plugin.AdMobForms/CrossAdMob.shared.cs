using System;
using Plugin.AdMobForms.Interfaces;

namespace Plugin.AdMobForms
{
    /// <summary>
    ///  Static CrossAdMob
    /// </summary>
    public static class CrossAdMob
    {
        static readonly Lazy<IAdMobForms> Implementation = new Lazy<IAdMobForms>(CreateCrossAdMob, System.Threading.LazyThreadSafetyMode.PublicationOnly);

        /// <summary>
        /// Gets if the plugin is supported on the current platform.
        /// </summary>
        public static bool IsSupported => Implementation.Value != null;

        /// <summary>
        /// Current plugin instance to use
        /// </summary>
        public static IAdMobForms Current
        {
            get
            {
                IAdMobForms ret = Implementation.Value;
                if (ret == null)
                {
                    throw NotImplementedInReferenceAssembly();
                }

                return ret;
            }
        }

        static IAdMobForms CreateCrossAdMob()
        {
#if NETSTANDARD1_0 || NETSTANDARD2_0
            return null;
#else
#pragma warning disable IDE0022 // Use expression body for methods
            return new AdMobForms();
#pragma warning restore IDE0022 // Use expression body for methods
#endif
        }

        internal static Exception NotImplementedInReferenceAssembly() =>
            new NotImplementedException(
                "This functionality is not implemented in the portable version of this assembly.  You should reference the NuGet package from your main application project in order to reference the platform-specific implementation.");
    }
}