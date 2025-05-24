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

        void SetAdLayout(RectTransform rectTransform);
        void SetAdLayoutFullScreen();
        void UpdateAdLayout();

        int AddButton(RectTransform rectTransform, Sprite sprite);
        void UpdateButton(int index, RectTransform rectTransform, Sprite sprite);
        void SetButtonActive(int index, bool active);
        void RemoveButton(int index);
        void ResetAllButtons();
    }
}