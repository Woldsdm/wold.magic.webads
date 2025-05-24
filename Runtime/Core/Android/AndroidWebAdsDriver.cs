using MagicWebAds.Core.Data;
using UnityEngine;

namespace MagicWebAds.Core.Android
{
    public class AndroidWebAdsDriver : IWebAdsDriver
    {
        AndroidJavaObject androidJava, layout, buttonManager;
        AndroidWebAdsCallback _callback;

        public AndroidWebAdsDriver(WebAdsListener listener)
        {
            _callback = new AndroidWebAdsCallback(listener);

            androidJava = new AndroidJavaObject("wold.magic.webads.adr.WebAdsManager");

            androidJava.Call("init", GetActivity(), _callback);

            layout = androidJava.Get<AndroidJavaObject>("layout");
            buttonManager = androidJava.Get<AndroidJavaObject>("buttonManager");
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
            AndroidHelper.ConvertRectTransform(rectTransform, out int x, out int y, out int width, out int height);
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

        public int AddButton(RectTransform rectTransform, Sprite sprite)
        {
            AndroidHelper.ConvertRectTransform(rectTransform, out int x, out int y, out int width, out int height);
            return buttonManager?.Call<int>("addButton", AndroidHelper.SpriteToBase64(sprite), x, y, width, height) ?? -1;
        }

        public void UpdateButton(int index, RectTransform rectTransform, Sprite sprite)
        {
            AndroidHelper.ConvertRectTransform(rectTransform, out int x, out int y, out int width, out int height);
            buttonManager?.Call("updateButton", index, AndroidHelper.SpriteToBase64(sprite), x, y, width, height);
        }

        public void SetButtonActive(int index, bool active)
        {
            buttonManager?.Call("setButtonActive", index, active);
        }

        public void RemoveButton(int index)
        {
            buttonManager?.Call("removeButton", index);
        }

        public void ResetAllButtons()
        {
            buttonManager?.Call("resetAllButtons");
        }

        public void Dispose()
        {
            androidJava?.Call("dispose");
            androidJava?.Dispose();
            layout?.Dispose();
            buttonManager?.Dispose();
            androidJava = null;
            layout = null;
            buttonManager = null;
        }
    }
}
