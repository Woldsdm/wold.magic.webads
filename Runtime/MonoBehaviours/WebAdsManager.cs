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

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }

        public List<WebAdRequest> GetAdRequests(List<string> filters)
        {
            if (filters.Count == 0)
            return services.SelectMany(service => service.requests).ToList();

            return services
                .SelectMany(service => service.requests)
                .Where(request => filters.Any(filter => request.name.Contains(filter)))
                .ToList();
        }
    }
}
