using System;
using UnityEngine;

namespace MagicWebAds.Core.Android
{
    public class AndroidWebAdsCallback : AndroidJavaProxy
    {
        WebAdsListener _listener;

        public AndroidWebAdsCallback(WebAdsListener listener)
            : base("wold.magic.webads.adr.AndroidWebAdsCallback") => _listener = listener;

        void onClosed() => _listener.OnClosed.Invoke();
        void onLoaded() => _listener.OnLoaded.Invoke();
        void onFailed(string error) => _listener.OnFailed.Invoke(error);
        void onClicked(string url) => _listener.OnClicked.Invoke(url);
        void onButtonClicked(int index) => _listener.OnButtonClicked.Invoke(index);
        void onProgressChanged(int progress) => _listener.OnProgressChanged.Invoke(progress);
        void onStartedLoading() => _listener.OnStartedLoading.Invoke();
        void onPageStarted(string url) => _listener.OnPageStarted.Invoke(url);
        void onError(string error) => _listener.OnError.Invoke(error);
        void onHttpError(int statusCode, String url, String responseText) 
        {
            httpError.statusCode = statusCode;
            httpError.url = url;
            httpError.responseText = responseText;
            _listener.OnHttpError.Invoke(httpError);
        } 
        HttpErrorInfo httpError = new();
    }
}