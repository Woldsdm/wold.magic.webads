using MagicWebAds.Core.Data;
using UnityEngine;

namespace MagicWebAds.Core.Android
{
    public class AndroidWebAdsDriver : IWebAdsDriver
    {
        AndroidJavaObject androidJava;
        AndroidJavaObject layout;
        AndroidWebAdsCallback _callback;

        public AndroidWebAdsDriver(WebAdsListener listener)
        {
            _callback = new AndroidWebAdsCallback(listener);

            androidJava = new AndroidJavaObject("wold.magic.webads.adr.WebAdsManager");

            androidJava.Call("init", GetActivity(), _callback);

            layout = androidJava.Get<AndroidJavaObject>("layout");
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

        public void SetAdLayout(RectTransform rectTransform)
        {
            Vector3[] worldCorners = new Vector3[4];
            rectTransform.GetWorldCorners(worldCorners);

            Vector3 bottomLeft = worldCorners[0];
            Vector3 topRight = worldCorners[2];

            Vector2 bottomLeftScreenPoint = RectTransformUtility.WorldToScreenPoint(null, bottomLeft);
            Vector2 topRightScreenPoint = RectTransformUtility.WorldToScreenPoint(null, topRight);

            int x = Mathf.RoundToInt(bottomLeftScreenPoint.x);
            int y = Mathf.RoundToInt(Screen.height - topRightScreenPoint.y);
            int width = Mathf.RoundToInt(topRightScreenPoint.x - bottomLeftScreenPoint.x);
            int height = Mathf.RoundToInt(topRightScreenPoint.y - bottomLeftScreenPoint.y);

            layout.Call("setLayout", x, y, width, height);
        }

        public void SetAdLayoutFullScreen()
        {
            layout.Call("setFullScreen");
        }

        public void UpdateAdLayout()
        {
            androidJava?.Call("updateLayout");
        }

        public void Dispose()
        {
            androidJava?.Call("dispose");
            androidJava?.Dispose();
            androidJava = null;
        }
    }
}
