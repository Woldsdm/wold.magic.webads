using System;
using UnityEngine;

namespace MagicWebAds.Debugging
{
    public enum EventType
    {
        StartedLoading,
        Loaded,
        Failed,
        Closed,
        Clicked,
        ButtonClicked,
        ProgressChanged,
        PageStarted,
        Error,
        HttpError
    }


    [Serializable]
    public class EventLog
    {
        public string timestamp;
        public string objectName;
        public EventType type;
        public string message;
        public EventLog(EventType type, string objectName = null, string message = null)
        {
            this.timestamp = DateTime.UtcNow.ToString("o");
            this.type = type;
            this.objectName = objectName;
            this.message = message;
        }
    }


    [Serializable]
    public class ConsoleLog
    {
        public string timestamp;
        public LogType type;
        public string message;
        public string stackTrace;

        public ConsoleLog(LogType type, string message, string stackTrace = null)
        {
            this.timestamp = DateTime.UtcNow.ToString("o");
            this.type = type;
            this.message = message;
            this.stackTrace = stackTrace;
        }
    }

}