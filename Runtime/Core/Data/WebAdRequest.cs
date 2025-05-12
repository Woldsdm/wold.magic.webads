using Newtonsoft.Json;

namespace MagicWebAds.Core.Data
{
    /// <summary>
    /// Configuration related to the ad request itself, such as URL and POST data.
    /// </summary>
    [System.Serializable]
    public class WebAdRequest
    {
        /// <summary>The URL of the ad page to load.</summary>
        public string url;

        /// <summary>Use POST method instead of GET to load the ad.</summary>
        public bool usePost;

        /// <summary>Data to be sent via POST method (if enabled).</summary>
        public string postData;

        /// <summary>Serialize this WebAdRequest to JSON.</summary>
        public string Serialize() => JsonConvert.SerializeObject(this);
    }
}