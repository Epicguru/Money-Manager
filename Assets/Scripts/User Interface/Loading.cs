using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loading : MonoBehaviour {

    public static Loading Instance;

    public bool open = false;

    private bool active = true;

    public Loading()
    {
        Instance = this;
    }

    public void SetOpen(bool open)
    {
        if (!this.active)
            return;
        this.open = open;
        if(gameObject != null)
            gameObject.SetActive(open);
    }

    public void OnDestroy()
    {
        this.active = false;
    }
}
