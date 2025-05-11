using System;
using UnityEngine;

namespace MagicWebAds.Core.Android
{
    public class AndroidWebAdsDriver : IWebAdsDriver, IDisposable
    {
        private AndroidJavaObject _javaAdManager;
        private AndroidWebAdsCallback _callback;

        public AndroidWebAdsDriver(WebAdsListener listener)
        {
            _callback = new AndroidWebAdsCallback(listener);

            _javaAdManager = new AndroidJavaObject("wold.magic.webads.adr.WebAdsManager");

            _javaAdManager.Call("init", GetActivity(), _callback);
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
            _javaAdManager?.Call("load", url);
        }

        public void Show()
        {
            _javaAdManager?.Call("show");
        }

        public void Close()
        {
            _javaAdManager?.Call("close");
        }

        public void Dispose()
        {
            _javaAdManager?.Call("dispose");
            _javaAdManager?.Dispose();
            _javaAdManager = null;
        }
    }
}
