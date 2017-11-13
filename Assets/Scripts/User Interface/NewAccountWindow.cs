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

        string result = Accounts.AddAccount(new Account() { Name = newName });

        bool success = result == null;

        return success;
    }

    public void CreateButtonPressed()
    {
        bool close = Create(Input.text.Trim());
        if (!close)
            return;
        Animator.SetBool("Open", false);
        Animator.enabled = true;
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
