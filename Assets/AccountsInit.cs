
using UnityEngine;

public class AccountsInit : MonoBehaviour
{
    public void Awake()
    {
        if (Accounts.Initilised)
            return;

        Accounts.Init();
        Accounts.AddAccount(new Account() { Name = "James", Balance = 100 });
        Accounts.AddAccount(new Account() { Name = "Curtis", Balance = 100 });
        Accounts.AddAccount(new Account() { Name = "Zak", Balance = 100 });
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Connection.Test();
        }
    }
}