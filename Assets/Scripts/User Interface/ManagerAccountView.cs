using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerAccountView : MonoBehaviour {

    public GameObject ItemPrefab;
    public List<ManagerAccountItem> Spawned = new List<ManagerAccountItem>();
    public float ItemHeight;

    public void Awake()
    {
        Accounts.AccountsChanged.AddListener(RefreshAccountView);
    }

    public void Start()
    {
        RefreshAccountView();        
    }

    public void RefreshAccountView()
    {
        foreach(ManagerAccountItem item in Spawned)
        {
            Destroy(item.gameObject);
        }
        Spawned.Clear();

        foreach(Account a in Accounts.GetAccounts())
        {
            ManagerAccountItem spawned = Instantiate(ItemPrefab, transform).GetComponent<ManagerAccountItem>();
            (spawned.transform as RectTransform).anchoredPosition = new Vector2(0, -ItemHeight * Spawned.Count);
            Spawned.Add(spawned);
        }

        (transform as RectTransform).sizeDelta = new Vector2(0, ItemHeight * Spawned.Count);

        RefreshNames();
    }

    public void RefreshNames()
    {
        // Only use when you are sure that no accounts have been removed or added since last refresh.

        if(Spawned.Count != Accounts.GetAccounts().Count)
        {
            Debug.LogError("Cannot name refresh, the names and accounts are desynced.");
            return;
        }

        int i = 0;
        foreach(var x in Accounts.GetAccounts())
        {
            Spawned[i++].SetText(x.Name);
        }
    }
}