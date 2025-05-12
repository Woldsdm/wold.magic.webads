using System;
using UnityEngine;
using UnityEngine.Events;

namespace MagicWebAds.Core
{
    [Serializable]
    public class WebAdsListener
    {
        [SerializeField] UnityEvent onLoaded = new();
        [SerializeField] UnityEvent onClosed = new();
        [SerializeField] UnityEvent<string> onFailed = new();

        public UnityEvent OnLoaded => onLoaded;
        public UnityEvent OnClosed => onClosed;
        public UnityEvent<string> OnFailed => onFailed;
    }
}