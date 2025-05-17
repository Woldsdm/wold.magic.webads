using MagicWebAds.Core;
using System.Collections.Generic;
using UnityEngine;

namespace MagicWebAds
{
    [AddComponentMenu("Magic WebAds/WebAds Manager")]
    public class WebAdsManager : MonoBehaviour
    {
        [SerializeField] List<Service> services;
        [SerializeField] WebAdsListener listeners;

        void Start()
        {

        }

        void Update()
        {

        }
    }
}
