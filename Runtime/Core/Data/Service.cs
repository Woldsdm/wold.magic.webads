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
    public List<WebAdRequest> requests;


    /// <summary>
    /// Creates a runtime clone of this Service instance.
    /// Note: This does not duplicate the asset in the Project folder.
    /// </summary>
    public Service Clone()
    {
        Service clone = CreateInstance<Service>();
        clone.serviceName = this.serviceName;
        clone.defaultSettings = this.defaultSettings; // shallow copy

        if (this.requests != null)
        {
            clone.requests = new List<WebAdRequest>();
            foreach (var req in this.requests)
            {
                clone.requests.Add(req.Clone());
            }
        }

        return clone;
    }
}