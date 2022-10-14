/*
 * Prints Debug Logs in a TextMeshPro Text.
 */

using System.Text;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DebugDisplay : MonoBehaviour
{
    public struct DebugLogMessage {
        public int Type {get;}
        public string Message {get;}
        
        public DebugLogMessage(int type, string message)
        {
            Type = type;
            Message = message;
        }
    }

    public TMP_Text tmpText;
    RectTransform _rt;

    [Header("Show in console")]
    public bool logWarnings = true;
    public bool logErrors = true;
    public bool logAssertions = true;

    Dictionary<string, DebugLogMessage> _debugLogs = new Dictionary<string, DebugLogMessage>();

    void Start()
    {
        _rt = GetComponent<RectTransform>();
        PrintLogsToDisplay();
    }

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
        // Check if error should be logged
        if (
            _debugLogs.Count < CalculateMaxLines() && (
                type == LogType.Log ||
                (logWarnings && type == LogType.Warning) ||
                (logErrors && type == LogType.Error) ||
                (logAssertions && type == LogType.Assert)
            )
        ) {
            string[] _splitString = logString.Split(char.Parse(":"));
            string _debugKey = _splitString[0];
            string _debugValue = _splitString.Length > 1 ? _splitString[1] : "";

            DebugLogMessage _dlm = new DebugLogMessage((int)type, _debugValue);
            if (_debugLogs.ContainsKey(_debugKey))
                _debugLogs[_debugKey] = _dlm;
            else
                _debugLogs.Add(_debugKey, _dlm);
        }

        PrintLogsToDisplay();
    }

    int CalculateMaxLines()
    {
        return (int)(_rt.rect.height -10f)/14;
    }
    void PrintLogsToDisplay()
    {
        StringBuilder _displayText = new StringBuilder();
        foreach(KeyValuePair<string, DebugLogMessage> _log in _debugLogs)
        {
            string _content;
            if (_log.Value.Message == "")
                _content = _log.Key;
            else
                _content = _log.Key + ": " + _log.Value.Message;

            switch (_log.Value.Type) {
                case 0: case 1: case 4: // Error, Assert, Exception
                    _displayText.AppendFormat("<color=\"red\">{0}</color>", _content);
                    break;
                case 2: // Warning
                    _displayText.AppendFormat("<color=\"yellow\">{0}</color>", _content);
                    break;
                default: // Log
                    _displayText.Append(_content);
                    break;
            }
            _displayText.AppendLine();
        }

        // Show the text
        tmpText.text = _displayText.ToString();
    }

    public void ClearConsole()
    {
        _debugLogs.Clear();
        PrintLogsToDisplay();
    }
}
