using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeBalanceView : MonoBehaviour {

    public static ChangeBalanceView Instance;

    public InputField BalanceChange;
    public InputField Notes;

    private SqlAccount account;

    public ChangeBalanceView()
    {
        Instance = this;
    }

    public void Open(SqlAccount account)
    {
        // TODO add acount input.

        gameObject.SetActive(true);

        BalanceChange.text = "";
        Notes.text = "";

        this.account = account;
    }

    public void Close()
    {
        gameObject.SetActive(false);

        account = null;
    }

    public void Submit()
    {
        if (string.IsNullOrEmpty(BalanceChange.text.Trim()))
            return;

        float currency = float.Parse(BalanceChange.text.Trim());

        if (currency == 0)
            return;

        int balanceChange = Convert.CurrencyToBalance(currency);

        string note = Notes.text.Trim();

        Connection.Instance.AddLog(account.ID, note);

         Close();
    }

    public void LogDone()
    {

    }
}
