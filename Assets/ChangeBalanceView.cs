using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeBalanceView : MonoBehaviour {

    public static ChangeBalanceView Instance;

    public InputField BalanceChange;
    public InputField Notes;

    public ChangeBalanceView()
    {
        Instance = this;
    }

    public void Open()
    {
        // TODO add acount input.

        gameObject.SetActive(true);

        BalanceChange.text = "";
        Notes.text = "";
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    public void Submit()
    {
        // TODO implement.

        Debug.Log("Submitting ");

        bool worked = true;

        if (worked)
        {
            Close();
        }
    }
}
