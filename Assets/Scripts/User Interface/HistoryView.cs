using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HistoryView : MonoBehaviour {

    public static HistoryView Instance;
    public GameObject Prefab;

    public Text Title;
    public RectTransform Content;

    public Color Positive, Negative;

    public List<GameObject> Spawned = new List<GameObject>();

    private SqlDescription[] descriptions;
    private SqlAccount account;

    public HistoryView()
    {
        Instance = this;
    }

    public void Open(int accountID)
    {
        gameObject.SetActive(true);

        Loading.Instance.SetOpen(true);

        Connection.Instance.GetSqlAccount(accountID, GotAccountThreaded);
    }

    private void GotAccountThreaded(SqlAccount account)
    {
        this.account = account;
    }

    private void GotLogsThreaded(SqlDescription[] descriptions)
    {
        this.descriptions = descriptions;
    }

    public void Update()
    {
        if(this.account != null)
        {
            this.Title.text = account.Name;
            Connection.Instance.GetAllLogsFor(account.ID, GotLogsThreaded);
            this.account = null;
        }

        if(descriptions != null)
        {
            GotLogs(descriptions);

            Loading.Instance.SetOpen(false);

            descriptions = null;
        }
    }

    public void GotLogs(SqlDescription[] logs)
    {
        System.Array.Reverse(logs);
        SpawnObjects(logs);
    }

    public void SpawnObjects(SqlDescription[] logs)
    {
        ClearObjects();

        int index = 0;

        foreach(SqlDescription desc in logs)
        {
            HistoryItem item = Instantiate(Prefab, Content).GetComponent<HistoryItem>();

            item.RefreshText(Convert.BalanceToCurrency(desc.BalanceChange), desc.BalanceChange > 0 ? Positive : Negative, desc.Time.ToString("dd MMMM, yyyy"), desc.Description);

            (item.transform as RectTransform).anchoredPosition = new Vector2(20, -370 * index - 20);

            Spawned.Add(item.gameObject);

            index++;
        }

        Content.sizeDelta = new Vector2(0, index * 370 + 20);
    }

    public void ClearObjects()
    {
        foreach(GameObject go in Spawned)
        {
            Destroy(go);
        }

        Spawned.Clear();
    }

    public void Close()
    {
        // TODO destroy any spawned objects.

        gameObject.SetActive(false);
    }
}
