using MagicWebAds.Core.Data;
using System;

namespace MagicWebAds.Core
{
    public interface IWebAdsDriver : IDisposable
    {
        void Load(string url);
        void Show();
        void Close();
        void SetSettings(WebAdSettings settings);
    }
}