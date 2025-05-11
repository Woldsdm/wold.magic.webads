using MagicWebAds.Core;
using System;
using UnityEngine;

public class AndroidWebAdsCallback : AndroidJavaProxy
{
    WebAdsListener _listener;
    public AndroidWebAdsCallback(WebAdsListener listener) : base("wold.magic.webads.adr.AndroidWebAdsCallback") => _listener = listener;

    void onDispose() => _listener.OnDispose.Invoke();
    void onClosed() => _listener.OnClosed.Invoke();
    void onLoaded() => _listener.OnLoaded.Invoke();
    void onFailed(string error) => _listener.OnFailed.Invoke(error);
}