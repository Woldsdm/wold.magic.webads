using MagicWebAds.Core.Data;
using System;
using UnityEngine;

namespace MagicWebAds.Core
{
    public interface IWebAdsDriver : IDisposable
    {
        void Load(WebAdRequest request);
        void Show();
        void Close();
        void SetSettings(WebAdSettings settings);
        public void SetAdLayout(RectTransform rectTransform);
        public void SetAdLayoutFullScreen();
        public void UpdateAdLayout();
    }
}