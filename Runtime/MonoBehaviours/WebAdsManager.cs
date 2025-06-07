using MagicWebAds.Core.Data;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MagicWebAds
{
    [AddComponentMenu("Magic WebAds/WebAds Manager")]
    public class WebAdsManager : MonoBehaviour
    {
        public static WebAdsManager Instance;

        [SerializeField] List<Service> services;

        List<Service> runtimeServices;

        List<UIAdView> adViews = new();

        void Awake()
        {
            if (Instance == null)
                Instance = this;


            runtimeServices = new List<Service>();
            foreach (var service in services)
            {
                if (service != null)
                {
                    runtimeServices.Add(service.Clone());
                }
            }
        }

        public void AddAdViews(UIAdView adView)
        {
            adViews.Add(adView);
        }

        public List<WebAdRequest> GetAdRequests(List<string> filters)
        {
            List<WebAdRequest> result = new List<WebAdRequest>();

            foreach (var service in runtimeServices)
            {
                foreach (var request in service.requests)
                {
                    if (request.settings == null && service.defaultSettings != null)
                    {
                        request.settings = service.defaultSettings;
                    }

                    if (filters.Count == 0 || filters.Any(filter => request.name.Contains(filter)))
                    {
                        result.Add(request);
                    }
                }
            }

            return result;
        }

        void OnDisable()
        {
            if (adViews.Count > 0)
            {
                foreach (var adView in adViews)
                {
                    adView.Dispose();
                }
                adViews = new();
            }
        }
    }
}