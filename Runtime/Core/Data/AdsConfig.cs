using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace MagicWebAds.Core.Data
{
    public enum RequestMethod
    {
        GET,
        POST
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

        string PostData = "";
        public string GetParametersData()
        {
            if (PostData != "") return PostData;

            if (parameters == null || parameters.Count == 0)
                return string.Empty;

            var sb = new System.Text.StringBuilder();
            for (int i = 0; i < parameters.Count; i++)
            {
                if (i > 0) sb.Append("&");
                sb.Append(UnityWebRequest.EscapeURL(parameters[i].name));
                sb.Append("=");
                sb.Append(UnityWebRequest.EscapeURL(parameters[i].value));
            }
            return sb.ToString();
        }

        public string GetURL()
        {
            if (method == RequestMethod.GET) return url + GetParametersData();
            return url;
        }
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