using System;
using System.Collections.Generic;
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

    public static class EventColors
    {
        static readonly Dictionary<EventType, Color> colors = new()
        {

            { EventType.StartedLoading, Color.gray },
            { EventType.Loaded, Color.green },
            { EventType.Failed, Color.red },
            { EventType.Closed, Color.magenta },
            { EventType.Clicked, Color.cyan },
            { EventType.ButtonClicked, Color.blue },
            { EventType.ProgressChanged, Color.yellow },
            { EventType.PageStarted, Color.white },
            { EventType.Error, Color.red },
            { EventType.HttpError, new Color(1f,0.5f,0f) }

        };

        public static Color Get(EventType type)
        {
            return colors.TryGetValue(type, out var c) ? c : Color.white;
        }
    }


    public static class ConsoleColors
    {
        static readonly Dictionary<LogType, Color> colors = new()
        {
            { LogType.Log, Color.white },
            { LogType.Warning, Color.yellow },
            { LogType.Error, Color.red },
            { LogType.Assert, Color.magenta },
            { LogType.Exception, Color.red }
        };

        public static Color Get(LogType type)
        {
            return colors.TryGetValue(type, out var c) ? c : Color.white;
        }
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