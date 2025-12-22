
using System.Collections.Generic;
using UnityEngine;

namespace MagicWebAds.Debugging
{
    public class DebugManager
    {
        List<EventLog> eventLogs = new();
        List<ConsoleLog> consoleLogs = new();

        public DebugManager()
        {
            Application.logMessageReceived += AddConsoleLog;
        }

        public void AddEventLog(UIAdView adView)
        {
            var listener = adView.Listener;
            string objectName = adView.gameObject.name;

            listener.OnLoaded.AddListener(() =>
                eventLogs.Add(new EventLog(EventType.Loaded, objectName, "Ad loaded")));

            listener.OnClosed.AddListener(() =>
                eventLogs.Add(new EventLog(EventType.Closed, objectName, "Ad closed")));

            listener.OnFailed.AddListener(error =>
                eventLogs.Add(new EventLog(EventType.Failed, objectName, $"Failed: {error}")));

            listener.OnClicked.AddListener(url =>
                eventLogs.Add(new EventLog(EventType.Clicked, objectName, $"Clicked URL: {url}")));

            listener.OnButtonClicked.AddListener(index =>
                eventLogs.Add(new EventLog(EventType.ButtonClicked, objectName, $"Button {index} clicked")));

            listener.OnProgressChanged.AddListener(progress =>
                eventLogs.Add(new EventLog(EventType.ProgressChanged, objectName, $"Progress: {progress}%")));

            listener.OnStartedLoading.AddListener(() =>
                eventLogs.Add(new EventLog(EventType.StartedLoading, objectName, "Started loading")));

            listener.OnPageStarted.AddListener(url =>
                eventLogs.Add(new EventLog(EventType.PageStarted, objectName, $"Page started: {url}")));

            listener.OnError.AddListener(error =>
                eventLogs.Add(new EventLog(EventType.Error, objectName, $"Error: {error}")));

            listener.OnHttpError.AddListener(httpError =>
                eventLogs.Add(new EventLog(EventType.HttpError, objectName, JsonUtility.ToJson(httpError))));
        }


        void AddConsoleLog(string condition, string stackTrace, LogType type)
        {
            consoleLogs.Add(new ConsoleLog(type, condition, stackTrace));
        }


        public void Dispose()
        {
            Application.logMessageReceived -= AddConsoleLog;
        }
    }
}
