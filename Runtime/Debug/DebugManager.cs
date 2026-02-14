using System;
using System.Collections.Generic;
using UnityEngine;

namespace MagicWebAds.Debugging
{
    public class DebugManager
    {
        public static DebugManager Instance { get; private set; }
        List<EventLog> eventLogs = new();
        List<ConsoleLog> consoleLogs = new();

        public event Action<EventLog> OnEventLog;
        public event Action<ConsoleLog> OnConsoleLog;

        public DebugManager()
        {
            if (Instance != null)
                throw new InvalidOperationException("DebugManager already created.");
            Instance = this;
            Application.logMessageReceived += AddConsoleLog;
        }

        public IReadOnlyList<EventLog> EventLogs => eventLogs;
        public IReadOnlyList<ConsoleLog> ConsoleLogs => consoleLogs;


        public void AddEventLog(UIAdView adView)
        {
            var listener = adView.Listener;
            string objectName = adView.gameObject.name;

            void LogAndNotify(EventType type, string message)
            {
                var log = new EventLog(type, objectName, message);
                eventLogs.Add(log);
                OnEventLog?.Invoke(log);
            }

            listener.OnLoaded.AddListener(() => LogAndNotify(EventType.Loaded, "Ad loaded"));
            listener.OnClosed.AddListener(() => LogAndNotify(EventType.Closed, "Ad closed"));
            listener.OnFailed.AddListener(err => LogAndNotify(EventType.Failed, $"Failed: {err}"));
            listener.OnClicked.AddListener(url => LogAndNotify(EventType.Clicked, $"Clicked URL: {url}"));
            listener.OnButtonClicked.AddListener(i => LogAndNotify(EventType.ButtonClicked, $"Button {i} clicked"));
            listener.OnProgressChanged.AddListener(p => LogAndNotify(EventType.ProgressChanged, $"Progress: {p}%"));
            listener.OnStartedLoading.AddListener(() => LogAndNotify(EventType.StartedLoading, "Started loading"));
            listener.OnPageStarted.AddListener(url => LogAndNotify(EventType.PageStarted, $"Page started: {url}"));
            listener.OnError.AddListener(err => LogAndNotify(EventType.Error, $"Error: {err}"));
            listener.OnHttpError.AddListener(httpErr => LogAndNotify(EventType.HttpError, JsonUtility.ToJson(httpErr)));
        }

        void AddConsoleLog(string condition, string stackTrace, LogType type)
        {
            var log = new ConsoleLog(type, condition, stackTrace);
            consoleLogs.Add(log);
            OnConsoleLog?.Invoke(log);
        }

        public void Dispose()
        {
            Application.logMessageReceived -= AddConsoleLog;
            OnEventLog = null;
            OnConsoleLog = null;
            eventLogs.Clear();
            consoleLogs.Clear();
            Instance = null;
        }
    }
}