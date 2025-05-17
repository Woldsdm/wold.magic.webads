using MagicWebAds.Core.Data;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Service", menuName = "MagicWebAds/Service")]
public class Service : ScriptableObject
{
    /// <summary>Name of the ad or service provider (e.g., "MyAdService").</summary>
    [Tooltip("A logical name to identify this ad or service provider.")]
    public string serviceName;

    /// <summary>Default settings that apply to all requests in this service (unless overridden).</summary>
    [Tooltip("Default WebView settings for all requests in this service. Individual requests can override these.")]
    public WebAdSettings defaultSettings;

    /// <summary>List of individual web ad requests under this service.</summary>
    [Tooltip("Multiple requests that belong to this service.")]
    public List<RequestConfig> requests;
}