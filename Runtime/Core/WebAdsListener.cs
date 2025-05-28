using System;
using UnityEngine.Events;
using UnityEngine;

[Serializable]
public class WebAdsListener
{
    [SerializeField] UnityEvent onLoaded = new();
    [SerializeField] UnityEvent onClosed = new();
    [SerializeField] UnityEvent<string> onFailed = new();
    [SerializeField] UnityEvent<string> onClicked = new();
    [SerializeField] UnityEvent<int> onButtonClicked = new();
    [SerializeField] UnityEvent<int> onProgressChanged = new();
    [SerializeField] UnityEvent onStartedLoading = new();
    [SerializeField] UnityEvent<string> onPageStarted = new();
    [SerializeField] UnityEvent<string> onError = new();
    [SerializeField] HttpErrorEvent onHttpError = new();

    public UnityEvent OnLoaded => onLoaded;
    public UnityEvent OnClosed => onClosed;
    public UnityEvent<string> OnFailed => onFailed;
    public UnityEvent<string> OnClicked => onClicked;
    public UnityEvent<int> OnButtonClicked => onButtonClicked;
    public UnityEvent<int> OnProgressChanged => onProgressChanged;
    public UnityEvent OnStartedLoading => onStartedLoading;
    public UnityEvent<string> OnPageStarted => onPageStarted;
    public UnityEvent<string> OnError => onError;
    public HttpErrorEvent OnHttpError => onHttpError;

    public void Dispose()
    {
        onLoaded.RemoveAllListeners();
        onClosed.RemoveAllListeners();
        onFailed.RemoveAllListeners();
        onClicked.RemoveAllListeners();
        onButtonClicked.RemoveAllListeners();
        onProgressChanged.RemoveAllListeners();
        onStartedLoading.RemoveAllListeners();
        onPageStarted.RemoveAllListeners();
        onError.RemoveAllListeners();
        onHttpError.RemoveAllListeners();
    }
}

[Serializable]
public class HttpErrorInfo
{
    public int statusCode;
    public string url;
    public string responseText;
}

[Serializable]
public class HttpErrorEvent : UnityEvent<HttpErrorInfo> { }
