using Newtonsoft.Json;
using UnityEngine;

namespace MagicWebAds.Core.Data
{
    /// <summary>
    /// Platform-agnostic configuration for how the WebView ad should behave and appear.
    /// </summary>
    [CreateAssetMenu(fileName = "WebAdSettings", menuName = "MagicWebAds/WebAdSettings")]
    public class WebAdSettings : ScriptableObject
    {
        /// <summary>
        /// Whether the ad should automatically be shown after it is fully loaded.
        /// </summary>
        [Tooltip("If enabled, the ad will be shown immediately after it finishes loading.")]
        public bool showOnLoadComplete = false;

        /// <summary>
        /// If true, clicking on links will open them in the system browser instead of inside the WebView.
        /// </summary>
        [Tooltip("If enabled, link clicks will open in the device's default browser instead of the WebView.")]
        public bool openLinksInSystemBrowser = true;

        /// <summary>
        /// Determines whether the WebView background should be transparent.
        /// </summary>
        [Tooltip("Makes the WebView background transparent if supported by the platform.")]
        public bool isTransparent = true;

        /// <summary>
        /// Enables JavaScript execution inside the WebView.
        /// </summary>
        [Tooltip("Allows JavaScript code to run in the WebView.")]
        public bool enableJavaScript = true;

        /// <summary>
        /// Enables pinch-to-zoom and zoom controls in the WebView.
        /// </summary>
        [Tooltip("Allow users to zoom in/out inside the WebView.")]
        public bool enableZoom = false;

        /// <summary>
        /// Allows scrolling inside the WebView content.
        /// </summary>
        [Tooltip("Enables vertical and horizontal scrolling in the WebView.")]
        public bool enableScroll = false;

        /// <summary>
        /// Whether to allow mixed content (HTTP and HTTPS) in the WebView.
        /// </summary>
        [Tooltip("Allow both HTTP and HTTPS content to load in the WebView.")]
        public bool allowMixedContent = true;

        /// <summary>
        /// If enabled, pressing the back button closes the ad instead of navigating back.
        /// </summary>
        [Tooltip("Close the ad when the user presses the back button.")]
        public bool backButtonClosesAd = true;

        /// <summary>
        /// If enabled, overrides the WebView's user-agent with a custom string.
        /// </summary>
        [Tooltip("Use a custom User-Agent string in the WebView.")]
        public bool useCustomUserAgent = false;

        /// <summary>
        /// The custom User-Agent string to use if <see cref="useCustomUserAgent"/> is enabled.
        /// </summary>
        [Tooltip("The value of the custom User-Agent to apply to the WebView.")]
        public string customUserAgent = "";

        /// <summary>
        /// Enables cookies inside the WebView.
        /// </summary>
        [Tooltip("Enable cookie support for the WebView.")]
        public bool enableCookies = true;

        /// <summary>
        /// Enables internal caching for the WebView.
        /// </summary>
        [Tooltip("Allows WebView to cache data for faster loading.")]
        public bool cacheEnabled = false;

        /// <summary>
        /// If enabled, the WebView's cache will be cleared before loading a new ad.
        /// </summary>
        [Tooltip("Clear all cached data in the WebView before loading.")]
        public bool clearCacheOnStart = true;

        /// <summary>Serialize this WebAdSettings to JSON.</summary>
        public string Serialize() => JsonConvert.SerializeObject(this);
    }
}