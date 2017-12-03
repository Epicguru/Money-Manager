using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HistoryView : MonoBehaviour {

    public static HistoryView Instance;
    public GameObject Prefab;

    public HistoryView()
    {
        Instance = this;
    }

    public void Open()
    {
        // TODO add account param.
        gameObject.SetActive(true);

        // TODO spawn objects.
    }

    public void Close()
    {
        // TODO destroy any spawned objects.

        gameObject.SetActive(false);
    }
}
