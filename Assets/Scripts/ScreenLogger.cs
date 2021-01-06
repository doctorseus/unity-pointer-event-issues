using System;
using UnityEngine;

public class ScreenLogger : MonoBehaviour
{
    private Vector2 scrollViewVector = Vector2.zero;

    private string logText = "";
    private const int kMaxLogSize = 16382;

    public void Log(object message)
    {
        Debug.Log(message);
        WriteLogOutput("[DEBUG] - " + message);
    }

    // Output text to the debug log text field, as well as the console.
    private void WriteLogOutput(string s)
    {
        logText += s + "\n";
        while (logText.Length > kMaxLogSize)
        {
            int index = logText.IndexOf("\n", StringComparison.Ordinal);
            logText = logText.Substring(index + 1);
        }

        scrollViewVector.y = int.MaxValue;
    }

    // Render the log output in a scroll view.
    void GUIDisplayLog()
    {
        scrollViewVector = GUILayout.BeginScrollView(scrollViewVector);
        GUILayout.Label(logText);
        GUILayout.EndScrollView();
    }

    // Render the GUI:
    void OnGUI()
    {
        const int pad = 10;

        var logArea = new Rect(pad, Screen.height * (1.0f / 3.0f), Screen.width - 2 * pad, Screen.height * (2.0f / 3.0f));
        GUILayout.BeginArea(logArea);
        GUIDisplayLog();
        GUILayout.EndArea();
    }
}
