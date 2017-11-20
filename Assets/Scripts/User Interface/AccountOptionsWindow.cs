using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AccountOptionsWindow : MonoBehaviour {

    public static AccountOptionsWindow Instance;

    public Text Name;

    public Animator Animator;

    public SqlAccount account;

    public AccountOptionsWindow()
    {
        Instance = this;
    }

    public void UponOpen()
    {
        Animator.enabled = false;
    }

    public void UponClose()
    {
        Animator.enabled = false;
        gameObject.SetActive(false);
        account = null;
    }

    public void TriggerOpen(SqlAccount account)
    {
        gameObject.SetActive(true);
        Animator.SetBool("Open", true);
        Animator.enabled = true;

        Name.text = account.Name;

        this.account = account;
    }

    public void TriggerClose()
    {
        gameObject.SetActive(true);
        Animator.SetBool("Open", false);
        Animator.enabled = true;
    }
}
