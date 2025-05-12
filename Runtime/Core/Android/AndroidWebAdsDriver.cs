using System;
using UnityEngine;

namespace MagicWebAds.Core.Android
{
    public class AndroidWebAdsDriver : IWebAdsDriver, IDisposable
    {
        private AndroidJavaObject androidJava;
        private AndroidWebAdsCallback _callback;

        public AndroidWebAdsDriver(WebAdsListener listener)
        {
            _callback = new AndroidWebAdsCallback(listener);

            androidJava = new AndroidJavaObject("wold.magic.webads.adr.WebAdsManager");

            androidJava.Call("init", GetActivity(), _callback);
        }

        private AndroidJavaObject GetActivity()
        {
            using (var unityPlayerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            {
                return unityPlayerClass.GetStatic<AndroidJavaObject>("currentActivity");
            }
        }

        public void Load(string url)
        {
            androidJava?.Call("load", url);
        }

        public void Show()
        {
            androidJava?.Call("show");
        }

        public void Close()
        {
            androidJava?.Call("close");
        }

        public void Dispose()
        {
            androidJava?.Call("dispose");
            androidJava?.Dispose();
            androidJava = null;
        }
    }
}
