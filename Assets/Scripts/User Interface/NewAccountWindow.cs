using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewAccountWindow : MonoBehaviour {

    public Animator Animator;
    public InputField Input;
    private bool closing;

    public void OnEnable()
    {
        Animator.SetBool("Open", true);
        Animator.enabled = true;
        Input.text = "";
    }

    public bool Create(string newName)
    {
        Debug.Log("Creating new account '" + newName + "'...");

        bool worked = Connection.Instance.CreateSqlAccount(newName, 0);

        return worked;
    }

    public void CreateButtonPressed()
    {
        // Create new SQL account: TODO This blocks, indicate progress.
        bool close = Create(Input.text.Trim());

        if (!close)
            return;

        Animator.SetBool("Open", false);
        Animator.enabled = true;

        // Created account! Query for SQL again...
        ManagerAccountView.Instance.RefreshAccountView();
    }

    public void UponClose()
    {
        Animator.enabled = false;
        gameObject.SetActive(false);
    }

    public void UponOpen()
    {
        Animator.enabled = false;
    }
}
