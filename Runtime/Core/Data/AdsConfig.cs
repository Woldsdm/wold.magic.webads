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

        string postData = null;

        /// <summary>
        /// Generates and returns a URL-encoded query string based on the list of parameters.
        /// This string can be used in GET requests or cached for POST body usage.
        /// </summary>
        /// <returns>
        /// A cached or freshly generated URL-encoded parameter string,
        /// or an empty string if no parameters exist.
        /// </returns>
        public string GetParametersData()
        {
            if (!string.IsNullOrEmpty(postData))
                return postData;

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

            postData = sb.ToString();
            return postData;
        }

        /// <summary>
        /// Constructs and returns the full URL for the request based on the request method.
        /// If the method is GET, appends the parameter string as a query string.
        /// </summary>
        /// <returns>
        /// The full URL including parameters for GET requests,
        /// or just the base URL for POST requests.
        /// </returns>
        public string GetURL()
        {
            if (method == RequestMethod.GET)
            {
                string paramData = GetParametersData();
                if (string.IsNullOrEmpty(paramData))
                    return url;

                return url.Contains("?") ? url + "&" + paramData : url + "?" + paramData;
            }
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