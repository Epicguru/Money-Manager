using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeNameButton : MonoBehaviour {

	public void OpenNameMenu()
    {
        ChangeNameMenu.Instance.TriggerOpen();
    }

}
