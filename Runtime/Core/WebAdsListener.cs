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
    [SerializeField] UnityEvent<int> onProgressChanged = new();
    [SerializeField] UnityEvent onStartedLoading = new();
    [SerializeField] UnityEvent<string> onPageStarted = new();
    [SerializeField] UnityEvent<string> onError = new();
    [SerializeField] UnityEvent<int> onHttpResponseCode = new();

    public UnityEvent OnLoaded => onLoaded;
    public UnityEvent OnClosed => onClosed;
    public UnityEvent<string> OnFailed => onFailed;
    public UnityEvent<string> OnClicked => onClicked;
    public UnityEvent<int> OnProgressChanged => onProgressChanged;
    public UnityEvent OnStartedLoading => onStartedLoading;
    public UnityEvent<string> OnPageStarted => onPageStarted;
    public UnityEvent<string> OnError => onError;
    public UnityEvent<int> OnHttpResponseCode => onHttpResponseCode;
}