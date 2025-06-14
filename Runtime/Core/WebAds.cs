using MagicWebAds.Core.Android;
using MagicWebAds.Core.Data;
using UnityEngine;

namespace MagicWebAds.Core
{
    public class WebAds
    {
        public IWebAdsDriver driver { get; private set; }
        public WebAdSettings settings = ScriptableObject.CreateInstance<WebAdSettings>();
        public WebAds(WebAdsListener listener)
        {
            switch (Application.platform)
            {
                case RuntimePlatform.Android:
                    driver = new AndroidWebAdsDriver(listener);
                    break;
                default:
                    Debug.LogWarning($"The current platform ({Application.platform}) is not yet supported. Please test on an Android device.");
                    break;
            }
            ApplySettings();
        }

        public void ApplySettings()
        {
            driver?.SetSettings(settings);
        }

        public void Dispose()
        {
            driver?.Dispose();
            driver = null;
        }
    }
}