
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class Accounts
{
    public static UnityEvent AccountsChanged = new UnityEvent();
    public static bool Initilised { get; private set; }
    private static Dictionary<string, Account> accounts;

    public static void Init()
    {
        if (Initilised)
            return;

        accounts = new Dictionary<string, Account>();
        Initilised = true;
    }

    public static string AddAccount(Account newAccount)
    {
        if(accounts == null)
        {
            Debug.LogError("Accounts is null!");
            return "NOT INITIALIZED";
        }
        if(newAccount == null)
        {
            Debug.LogError("Account passed is null!");
            return "NULL ACCOUNT";
        }
        if (accounts.ContainsValue(newAccount))
        {
            Debug.LogError("That account reference already exists!");
            return "Duplicate account [ERROR VERSION]!";

        }
        if (accounts.ContainsKey(newAccount.Name))
        {
            Debug.LogError("An account with that name already exists!");
            return "Duplicate account name!";
        }

        accounts.Add(newAccount.Name, newAccount);

        AccountsChanged.Invoke();

        return null;
    }

    public static Dictionary<string, Account>.ValueCollection GetAccounts()
    {
        if(accounts == null)
        {
            Debug.LogError("Not initialized (null accounts)");
            return null;
        }

        return accounts.Values;
    }
}