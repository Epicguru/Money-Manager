using UnityEngine;
using UnityEngine.UI;

public class ManagerAccountItem : MonoBehaviour {

    public Text Name;

    public SqlAccount Account;

    public void SetText(string text)
    {
        Name.text = text;
    }

    public string GetText()
    {
        return Name.text;
    }

    public void Edit()
    {
        Debug.Log("Opening sql for " + Name.text);
        Loading.Instance.SetOpen(true);
        Connection.Instance.GetSqlAccount(Name.text, GotAccount);
    }

    public void GotAccount(SqlAccount account)
    {
        if (account == null)
            return;

        Account = account;        
    }

    public void Update()
    {
        if(Account != null)
        {
            Loading.Instance.SetOpen(false);

            AccountOptionsWindow.Instance.TriggerOpen(Account);

            Account = null;
        }
    }
}