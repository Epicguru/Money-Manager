using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccountDeleteOption : MonoBehaviour {

    public bool Refresh = false;

	public void DeleteAccount()
    {
        Loading.Instance.SetOpen(true);
        Connection.Instance.RemoveAccount(AccountOptionsWindow.Instance.account.ID, RemovedFinished);

        AccountOptionsWindow.Instance.TriggerClose();
    }

    public void RemovedFinished(bool x)
    {
        Refresh = true;
    }

    public void Update()
    {
        if (Refresh)
        {
            Loading.Instance.SetOpen(false);
            ManagerAccountView.Instance.RefreshAccountView();
            Refresh = false;
        }
    }
}
