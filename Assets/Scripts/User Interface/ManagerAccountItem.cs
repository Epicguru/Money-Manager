using UnityEngine;
using UnityEngine.UI;

public class ManagerAccountItem : MonoBehaviour {

    public Text Name;

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
        Loading.Instance.SetOpen(true);
        Connection.Instance.GetSqlAccount(Name.text, GotAccount);
    }

    public void GotAccount(SqlAccount account)
    {
        if (account == null)
            return;

        Loading.Instance.SetOpen(false);

        AccountOptionsWindow.Instance.TriggerOpen(account);
    }
}