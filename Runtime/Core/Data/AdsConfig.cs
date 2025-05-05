using System;
using System.Collections.Generic;

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
        public string name;
        public List<RequestConfig> requests;
    }

    [Serializable]
    public class RequestConfig
    {
        public string name;
        public string url;
        public RequestMethod method;
        public List<Parameter> parameters;
    }

    [Serializable]
    public class Parameter
    {
        public string name;
        public string value;
    }
}
