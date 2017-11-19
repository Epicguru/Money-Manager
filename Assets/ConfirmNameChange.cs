using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmNameChange : MonoBehaviour {

    public InputField Inp;

    public bool DoWork;
    public bool CloseLoading;

	public void Pressed()
    {
        string name = Inp.text.Trim();

        if (string.IsNullOrEmpty(name))
        {
            return;
        }

        Loading.Instance.SetOpen(true);
        Connection.Instance.RenameAccount(AccountOptionsWindow.Instance.account.ID, name, Response);
    }

    public void Response(bool worked)
    {
        if (!worked)
        {
            Debug.LogError("Failed to rename!");
            CloseLoading = true;
            return;
        }
        else
        {
            DoWork = true;
        }
    }

    public void Update()
    {
        if (CloseLoading)
        {
            CloseLoading = false;
            Loading.Instance.SetOpen(false);
        }

        if (DoWork)
        {
            DoWork = false;
            Loading.Instance.SetOpen(false);
            ChangeNameMenu.Instance.TriggerClose();
            AccountOptionsWindow.Instance.TriggerClose();
            ManagerAccountView.Instance.RefreshAccountView();
        }
    }
}
