namespace MagicWebAds.Core
{
    public interface IWebAdsCallback
    {
        void OnLoaded();
        void OnClosed();
        void OnFailed(string error);
        void OnDispose();
    }
}