using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccountOptionsCancel : MonoBehaviour {

	public void ButtonPressed()
    {
        AccountOptionsWindow.Instance.TriggerClose();
    }
}
