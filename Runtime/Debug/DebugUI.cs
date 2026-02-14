using MagicWebAds.Debugging;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DebugUI : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] Transform content;
    [SerializeField] GameObject textPrefab;

    [Header("Filters")]
    [SerializeField] Toggle eventsToggle;
    [SerializeField] Toggle consoleToggle;

    [Header("Pool")]
    [SerializeField] int maxLogs = 50;

    Queue<Text> pool = new();
    List<Text> active = new();

    DebugManager Debug => DebugManager.Instance;

    // =========================
    void Awake()
    {
        eventsToggle.onValueChanged.AddListener(_ => ReloadLogs());
        consoleToggle.onValueChanged.AddListener(_ => ReloadLogs());
    }

    void OnEnable()
    {
        ReloadLogs();

        if (Debug == null) return;

        Debug.OnEventLog += OnEventLog;
        Debug.OnConsoleLog += OnConsoleLog;
    }

    void OnDisable()
    {
        if (Debug == null) return;

        Debug.OnEventLog -= OnEventLog;
        Debug.OnConsoleLog -= OnConsoleLog;
    }

    // =========================
    void ReloadLogs()
    {
        ClearAll();

        if (Debug == null)
        {
            AddLog("Debug Mode Disabled", Color.yellow);
            return;
        }

        var merged = new List<(DateTime time, string msg, Color color)>();

        if (eventsToggle.isOn)
        {
            foreach (var e in Debug.EventLogs)
            {
                merged.Add((ParseTime(e.timestamp),
                    $"[{e.objectName}] {e.type} - {e.message}", EventColors.Get(e.type)));
            }
        }

        if (consoleToggle.isOn)
        {
            foreach (var c in Debug.ConsoleLogs)
            {
                merged.Add((ParseTime(c.timestamp),
                    $"[{c.type}] {c.message}", ConsoleColors.Get(c.type)));
            }
        }

        foreach (var log in merged
                     .OrderByDescending(l => l.time)
                     .Take(maxLogs)
                     .Reverse())
        {
            AddLog(log.msg, log.color);
        }
    }

    // =========================
    void OnEventLog(EventLog log)
    {
        if (!eventsToggle.isOn) return;

        AddLog($"[{log.objectName}] {log.type} - {log.message}", EventColors.Get(log.type));
    }

    void OnConsoleLog(ConsoleLog log)
    {
        if (!consoleToggle.isOn) return;

        AddLog($"[{log.type}] {log.message}", ConsoleColors.Get(log.type));
    }

    // =========================
    void AddLog(string message, Color color)
    {
        Text txt;

        if (pool.Count > 0)
        {
            txt = pool.Dequeue();
        }
        else if (active.Count < maxLogs)
        {
            txt = Instantiate(textPrefab, content).GetComponent<Text>();
        }
        else
        {
            txt = active[0];
            active.RemoveAt(0);
        }

        txt.text = message;
        txt.color = color;
        txt.transform.SetAsLastSibling();
        txt.gameObject.SetActive(true);

        active.Add(txt);
    }

    // =========================
    void ClearAll()
    {
        foreach (var txt in active)
        {
            txt.gameObject.SetActive(false);
            pool.Enqueue(txt);
        }

        active.Clear();
    }

    // =========================
    DateTime ParseTime(string t)
    {
        DateTime.TryParse(t, out var dt);
        return dt;
    }
}
