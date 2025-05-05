using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class InGameLogger : MonoBehaviour
{
    public Text logText;
    private Queue<string> logs = new Queue<string>();
    public int maxLines = 20;

    void OnEnable()
    {
        Application.logMessageReceived += HandleLog;
    }

    void OnDisable()
    {
        Application.logMessageReceived -= HandleLog;
    }

    void HandleLog(string logString, string stackTrace, LogType type)
    {
        logs.Enqueue(logString);
        if (logs.Count > maxLines)
            logs.Dequeue();

        logText.text = string.Join("\n", logs.ToArray());
    }

    public void Copy()
    {
        GUIUtility.systemCopyBuffer = logText.text;
    }
}
