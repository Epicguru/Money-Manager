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

    public void Create(string newName)
    {
        Debug.Log("Creating new account '" + newName + "'");

    }

    public void CreateButtonPressed()
    {
        Create(Input.text.Trim());
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
