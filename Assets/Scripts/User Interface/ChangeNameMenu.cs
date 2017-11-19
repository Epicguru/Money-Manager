using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeNameMenu : MonoBehaviour {

    public static ChangeNameMenu Instance;

    public Animator Animator;
    public InputField Input;

    public ChangeNameMenu()
    {
        Instance = this;
    }

    public void TriggerOpen()
    {
        gameObject.SetActive(true);
        Animator.SetBool("Open", true);
        Animator.enabled = true;
        Input.text = AccountOptionsWindow.Instance.account.Name;
    }

    public void TriggerClose()
    {
        Animator.SetBool("Open", false);
        Animator.enabled = true;
    }

    public void AnimationOpen()
    {
        Animator.enabled = false;
        (transform as RectTransform).rotation = Quaternion.Euler(0, 0, 0);
    }

    public void AnimationClose()
    {
        Animator.enabled = false;
        gameObject.SetActive(false);
    }
}
