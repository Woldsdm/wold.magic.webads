using System;

namespace MagicWebAds.Core
{
    public interface IWebAdsDriver
    {
        void Load(string url);
        void Show();
        void Close();
        void Dispose();
    }
}