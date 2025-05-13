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
                    break;
            }
        }

        public void ApplySettings()
        {
            driver.SetSettings(settings);
        }
    }
}