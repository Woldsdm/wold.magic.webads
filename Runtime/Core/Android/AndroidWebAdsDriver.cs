using MagicWebAds.Core.Data;
using UnityEngine;

namespace MagicWebAds.Core.Android
{
    public class AndroidWebAdsDriver : IWebAdsDriver
    {
        AndroidJavaObject androidJava;
        AndroidWebAdsCallback _callback;

        public AndroidWebAdsDriver(WebAdsListener listener)
        {
            _callback = new AndroidWebAdsCallback(listener);

            androidJava = new AndroidJavaObject("wold.magic.webads.adr.WebAdsManager");

            androidJava.Call("init", GetActivity(), _callback);
        }

        AndroidJavaObject GetActivity()
        {
            using (var unityPlayerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            {
                return unityPlayerClass.GetStatic<AndroidJavaObject>("currentActivity");
            }
        }

        public void Load(WebAdRequest request)
        {
            androidJava?.Call("load", request.GetURL(),
            request.method == RequestMethod.POST, request.GetParametersData());
        }

        public void Show()
        {
            androidJava?.Call("show");
        }

        public void Close()
        {
            androidJava?.Call("close");
        }

        public void SetSettings(WebAdSettings settings)
        {
            androidJava?.Call("setSettings",
                settings.showOnLoadComplete,
                settings.openLinksInSystemBrowser,
                settings.isTransparent,
                settings.enableJavaScript,
                settings.enableZoom,
                settings.enableScroll,
                settings.allowMixedContent,
                settings.backButtonClosesAd,
                settings.useCustomUserAgent,
                settings.customUserAgent,
                settings.enableCookies,
                settings.cacheEnabled,
                settings.clearCacheOnStart
            );
        }

        public void Dispose()
        {
            androidJava?.Call("dispose");
            androidJava?.Dispose();
            androidJava = null;
        }
    }
}
