using MagicWebAds.Core.Android;

namespace MagicWebAds.Core
{
    public class WebAds
    {
        public IWebAdsDriver driver { get; private set; }
        public WebAds(WebAdsListener listener)
        {
#if UNITY_ANDROID
            driver = new AndroidWebAdsDriver(listener);
#endif
        }
    }
}