using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerAccountView : MonoBehaviour
{

    public static ManagerAccountView Instance;
    public GameObject ItemPrefab;
    public List<ManagerAccountItem> Spawned = new List<ManagerAccountItem>();
    public float ItemHeight;

    public void Awake()
    {
        Instance = this;
    }

    public void Start()
    {
        RefreshAccountView();
    }

    public void RefreshAccountView()
    {
        Loading.Instance.SetOpen(true);
        foreach (ManagerAccountItem item in Spawned)
        {
            Destroy(item.gameObject);
        }
        Spawned.Clear();

        if(Loading.Instance != null)
        {
            Loading.Instance.SetOpen(true);
        }
        Connection.Instance.GetAllSqlAccounts(CreateObjectsFromAccounts);        
    }

    private void CreateObjectsFromAccounts(SqlAccount[] accounts)
    {
        foreach (SqlAccount a in accounts)
        {
            ManagerAccountItem spawned = Instantiate(ItemPrefab, transform).GetComponent<ManagerAccountItem>();
            (spawned.transform as RectTransform).anchoredPosition = new Vector2(0, -ItemHeight * Spawned.Count);
            Spawned.Add(spawned);
        }

        (transform as RectTransform).sizeDelta = new Vector2(0, ItemHeight * Spawned.Count);

        RefreshNames(accounts);
    }

    public void RefreshNames(SqlAccount[] accounts)
    {
        // Only use when you are sure that no accounts have been removed or added since last refresh.

        if (Spawned.Count != accounts.Length)
        {
            Debug.LogError("Cannot name refresh, the names and accounts are desynced.");
            return;
        }

        int i = 0;
        foreach (var x in accounts)
        {
            Spawned[i++].SetText(x.Name);
        }

        Loading.Instance.SetOpen(false);
    }
}