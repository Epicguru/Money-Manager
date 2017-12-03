using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenAccountView : MonoBehaviour {

	public void Pressed()
    {
        Loading.Instance.SetOpen(true);

        SceneManager.LoadSceneAsync("Account View", LoadSceneMode.Single);
    }

}
