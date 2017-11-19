using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewAccountCancel : MonoBehaviour {

	public void Pressed()
    {
        NewAccountWindow.Instance.AnimClose();
    }

}
