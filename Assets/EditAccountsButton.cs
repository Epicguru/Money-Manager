using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EditAccountsButton : MonoBehaviour {

    public bool IsLoading = false;

	public void EditAccounts()
    {
        StartCoroutine(LoadScene());
    }

    public IEnumerator LoadScene()
    {
        if (IsLoading)
            yield return null;

        IsLoading = true;
        Loading.Instance.SetOpen(true);

        AsyncOperation operation = SceneManager.LoadSceneAsync("Account Managing", LoadSceneMode.Single);


        while (!operation.isDone)
        {
            yield return null;
        }
    }
}
