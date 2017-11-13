using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EditAccountsButton : MonoBehaviour {

	public void EditAccounts()
    {
        SceneManager.LoadScene("Account Managing", LoadSceneMode.Single);
    }
}
