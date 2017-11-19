using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CancelNameChange : MonoBehaviour {

	public void CanceNameChange()
    {
        ChangeNameMenu.Instance.TriggerClose();
    }
}
