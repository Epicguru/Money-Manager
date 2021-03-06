﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccountsView : MonoBehaviour {

    public static AccountsView Instance;

    public GameObject Prefab;
    public RectTransform Content;

    public Color Neutral, Positive, Negative;

    public List<GameObject> Spawned = new List<GameObject>();

    public AccountsView()
    {
        Instance = this;
    }

    public void Start()
    {
        Refresh();
    }

    public void Refresh()
    {
        Loading.Instance.SetOpen(true);

        Connection.Instance.GetAllSqlAccounts(AccountsLoaded);

        Debug.Log("Refreshing...");
    }

    private void AccountsLoaded(SqlAccount[] accounts)
    {
        Loading.Instance.SetOpen(false);

        SpawnObjects(accounts);
    }

    private void SpawnObjects(SqlAccount[] accounts)
    {
        ClearObjects();

        foreach(SqlAccount account in accounts)
        {
            AccountItem itemInstance = Instantiate(Prefab, Content).GetComponent<AccountItem>();

            itemInstance.Name = account.Name;
            itemInstance.Balance = Convert.BalanceToCurrency(account.Balance);
            itemInstance.BalanceColour = account.Balance == 0 ? Neutral : account.Balance > 0 ? Positive : Negative;
            itemInstance.ID = account.ID;

            Spawned.Add(itemInstance.gameObject);
        }        
    }

    private void ClearObjects()
    {
        foreach(GameObject go in Spawned)
        {
            Destroy(go);
        }

        Spawned.Clear();
    }
}
