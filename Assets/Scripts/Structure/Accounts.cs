
using System.Collections.Generic;
using UnityEngine;

public static class Accounts
{
    private static Dictionary<string, Account> accounts;

    public static void Init()
    {
        accounts = new Dictionary<string, Account>();
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

        return null;
    }
}