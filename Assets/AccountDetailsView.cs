using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AccountDetailsView : MonoBehaviour {

    public static AccountDetailsView Instance;

    public Text Title;
    public Text CurrentBalance;
    public HistoryViewOpen HistoryButton;
    public Button ChangeBalanceButton;

    private SqlAccount PendingRefresh;

    public void Start()
    {
        OpenAccount(-1);   
    }

    public AccountDetailsView()
    {
        Instance = this;
    }

    public void Update()
    {
        if(PendingRefresh != null)
        {
            Set(PendingRefresh);
            Loading.Instance.SetOpen(false);
            PendingRefresh = null;
        }
    }

    public void OpenAccount(int ID)
    {
        if(ID == -1)
        {
            Set(null);
            return;
        }

        Loading.Instance.SetOpen(true);
        Connection.Instance.GetSqlAccount(ID, SetPending);
    }

    private void SetPending(SqlAccount account)
    {
        this.PendingRefresh = account;
    }

    public void Set(SqlAccount account)
    {
        Loading.Instance.SetOpen(false);

        if(account == null)
        {
            Title.text = "Account: None Selected";
            CurrentBalance.text = "---";
            HistoryButton.Disable();
            ChangeBalanceButton.interactable = false;
        }
        else
        {
            Title.text = "Account: " + account.Name.Trim();
            CurrentBalance.text = Convert.BalanceToCurrency(account.Balance);
            HistoryButton.Enable();
            ChangeBalanceButton.interactable = true;
        }
    }
}
