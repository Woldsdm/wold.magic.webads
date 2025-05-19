using MagicWebAds.Core.Data;
using System;

namespace MagicWebAds.Core
{
    public interface IWebAdsDriver : IDisposable
    {
        void Load(WebAdRequest request);
        void Show();
        void Close();
        void SetSettings(WebAdSettings settings);
    }
}