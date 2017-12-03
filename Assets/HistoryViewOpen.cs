using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HistoryViewOpen : MonoBehaviour {

    public static HistoryViewOpen Instance;

    public Button Button;

    public HistoryViewOpen()
    {
        Instance = this;
    }

    public void Disable()
    {
        Button.interactable = false;
    }

    public void Enable()
    {
        Button.interactable = true;
    }

    public void Pressed()
    {
        HistoryView.Instance.Open();
    }
}
