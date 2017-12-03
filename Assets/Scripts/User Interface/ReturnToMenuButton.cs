using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToMenuButton : MonoBehaviour {

	public void Pressed()
    {
        Loading.Instance.SetOpen(true);
        SceneManager.LoadSceneAsync("Main Menu", LoadSceneMode.Single);
    }

}
