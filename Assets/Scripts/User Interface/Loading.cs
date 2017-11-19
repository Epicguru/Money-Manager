using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loading : MonoBehaviour {

    public static Loading Instance;

    public bool open = false;

    public Loading()
    {
        Instance = this;
    }

    public void SetOpen(bool open)
    {
        this.open = open;
        gameObject.SetActive(open);
    }
}
