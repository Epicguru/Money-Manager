using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeBalanceView : MonoBehaviour {

    public static ChangeBalanceView Instance;

    public InputField BalanceChange;
    public InputField Notes;

    private SqlAccount account;
    private bool done;

    public ChangeBalanceView()
    {
        Instance = this;
    }

    public void Open(SqlAccount account)
    {
        gameObject.SetActive(true);

        BalanceChange.text = "";
        Notes.text = "";

        this.account = account;
    }

    public void Close()
    {
        gameObject.SetActive(false);

        account = null;

        AccountsView.Instance.Refresh();
        AccountDetailsView.Instance.OpenAccount(AccountDetailsView.Instance.ID);
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

        Loading.Instance.SetOpen(true);
        Connection.Instance.AddLog(account.ID, note);
        Connection.Instance.UpdateAccountBalance(account.ID, balanceChange, DoneThreaded);

    }

    public void Update()
    {
        if(done == true)
        {
            done = false;

            Done();
        }
    }

    private void DoneThreaded()
    {
        done = true;
    }

    private void Done()
    {
        Loading.Instance.SetOpen(false);
        Close();
    }
}