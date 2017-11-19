using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugUI : MonoBehaviour {

    public Text Text;

    public void OnEnable()
    {
        Application.logMessageReceived += Logged;
    }

    public void OnDisable()
    {
        Application.logMessageReceived -= Logged;
    }

    public void Logged(string message, string stack, LogType type)
    {
        Text.text += "<b>" + type + ": </b>" + message + "\n" + stack;
    }
}
