using MagicWebAds.Core.Data;
using System;
using UnityEngine;

namespace MagicWebAds.Core.Android
{
    public class AndroidWebAdsDriver : IWebAdsDriver
    {
        AndroidJavaObject androidJava, layout;
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

        public void InjectHtml(string html)
        {
            androidJava?.Call("injectHtml", html);
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
                settings.openLinksInSystemBrowser,
                settings.isTransparent,
                settings.enableJavaScript,
                settings.enableZoom,
                settings.enableScroll,
                settings.allowMixedContent,
                settings.useCustomUserAgent,
                settings.customUserAgent,
                settings.enableCookies,
                settings.cacheEnabled,
                settings.clearCacheOnStart,
                settings.fitContentToView,
                settings.disableUserZoom,
                settings.maintainAspectRatio
            );
        }

        public void SetAdLayout(RectTransform rectTransform)
        {
            AndroidHelper.ConvertRectTransform(rectTransform, out int x, out int y, out int width, out int height);
            layout?.Call("setLayout", x, y, width, height);
        }

        public void SetAdLayoutFullScreen()
        {
            layout?.Call("setFullScreen");
        }

        public void UpdateAdLayout()
        {
            androidJava?.Call("updateLayout");
        }

        public int AddButton(RectTransform rectTransform, Sprite sprite)
        {
            AndroidHelper.ConvertRectTransform(rectTransform, out int x, out int y, out int width, out int height);
            return androidJava?.Call<int>("addButton", AndroidHelper.SpriteToBytes(sprite, 256, 256), x, y, width, height) ?? -1;
        }

        public void UpdateButton(int index, Sprite sprite)
        {
            androidJava?.Call("updateButton", index, AndroidHelper.SpriteToBytes(sprite, 256, 256));
        }

        public void SetButtonActive(int index, bool active)
        {
            androidJava?.Call("setButtonActive", index, active);
        }

        public void RemoveButton(int index)
        {
            androidJava?.Call("removeButton", index);
        }

        public void ResetAllButtons()
        {
            androidJava?.Call("resetAllButtons");
        }

        public void Dispose()
        {
            androidJava?.Call("dispose");
            androidJava?.Dispose();
            layout?.Dispose();
            androidJava = null;
            layout = null;
        }
    }
}
