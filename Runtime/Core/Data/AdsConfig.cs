using System;
using System.Collections.Generic;
using UnityEngine;

namespace MagicWebAds.Core.Data
{
    public enum RequestMethod
    {
        GET,
        POST
    }

    [Serializable]
    public class Service
    {
        /// <summary>Name of the ad or service provider (e.g., "MyAdService").</summary>
        [Tooltip("A logical name to identify this ad or service provider.")]
        public string name;

        /// <summary>Default settings that apply to all requests in this service (unless overridden).</summary>
        [Tooltip("Default WebView settings for all requests in this service. Individual requests can override these.")]
        public WebAdSettings defaultSettings;

        /// <summary>List of individual web ad requests under this service.</summary>
        [Tooltip("Multiple requests that belong to this service.")]
        public List<RequestConfig> requests;
    }


    [Serializable]
    public class RequestConfig
    {
        /// <summary>Unique name for this specific ad/web request.</summary>
        [Tooltip("Name to identify this individual request.")]
        public string name;

        /// <summary>The URL to which this request should be sent.</summary>
        [Tooltip("The target URL to load for this request.")]
        public string url;

        /// <summary>HTTP method to use (GET or POST).</summary>
        [Tooltip("Select whether to use GET or POST method.")]
        public RequestMethod method;

        /// <summary>List of key-value pairs to send with the request.</summary>
        [Tooltip("Optional key-value parameters to include with this request.")]
        public List<Parameter> parameters;

        /// <summary>Settings for how this WebAd should behave when loaded.</summary>
        [Tooltip("Display and behavior options for how the WebView will render this ad.")]
        public WebAdSettings settings;
    }

    [Serializable]
    public class Parameter
    {
        /// <summary>The key/name of the parameter (e.g., 'userId').</summary>
        [Tooltip("The name/key of this parameter.")]
        public string name;

        /// <summary>The value of the parameter to be sent.</summary>
        [Tooltip("The value of this parameter.")]
        public string value;
    }
}