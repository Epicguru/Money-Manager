using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewAccountButton : MonoBehaviour {

    public GameObject NewAccount;

	public void UponButtonPressed()
    {
        // Open the creation menu...
        NewAccount.SetActive(true);
    }
}
