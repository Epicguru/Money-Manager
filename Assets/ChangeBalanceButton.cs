using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeBalanceButton : MonoBehaviour {

    public Button Button;

    public void Enable()
    {
        Button.interactable = true;
    }

    public void Disable()
    {
        Button.interactable = false;
    }

    public void Pressed()
    {
        ChangeBalanceView.Instance.Open();
    }

}
