using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeBalanceButton : MonoBehaviour {

    public Button Button;

    private SqlAccount account;

    public void Enable()
    {
        Button.interactable = true;
    }

    public void Disable()
    {
        Button.interactable = false;
    }

    public void Pressed()
    {
        int id = AccountDetailsView.Instance.ID;

        if (id == -1)
            return;

        Loading.Instance.SetOpen(true);
        Connection.Instance.GetSqlAccount(id, GotAccountThreaded);
    }

    public void Update()
    {
        if(account != null)
        {
            GotAccount(account);
            account = null;
        }
    }

    private void GotAccountThreaded(SqlAccount account)
    {
        this.account = account;
    }

    public void GotAccount(SqlAccount account)
    {
        Loading.Instance.SetOpen(false);
        ChangeBalanceView.Instance.Open(account);
    }
}
