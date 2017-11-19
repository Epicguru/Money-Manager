using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Connection : MonoBehaviour
{
    public static Connection Instance;
    public IDbConnection Conn;
    public string accountsTable = "accounts";

    private UnityAction<SqlAccount[]> toInvoke;
    private SqlAccount[] invokingAccounts;

    public void Awake()
    {
        Instance = this;
    }

    public void Start()
    {
        OpenConnection();
    }

    public void Update()
    {
        if(toInvoke != null)
        {
            Loading.Instance.SetOpen(false);
            toInvoke.Invoke(invokingAccounts);
            toInvoke = null;
            invokingAccounts = null;
        }
    }

    public void OnDestroy()
    {
        CloseConnection();
    }

    private void OpenConnection()
    {
        Debug.Log("Opening SQL connection...");

        string connectionString =
            "Server=www.exbdirect.com;" +
            "Database=exbdirec_MoneyManager;" +
            "User ID=exbdirec_Test;" +
            "Password=TestPassword;";

        Conn = new MySqlConnection(connectionString);
        Conn.Open();
    }

    public void CloseConnection()
    {
        Debug.Log("Closing SQL connection.");

        Conn.Close();
        Conn = null;
    }

    public IDbCommand CreateCommand()
    {
        if(Conn == null)
        {
            Debug.LogError("Connection is null, cannot create command!");
            return null;
        }

        return Conn.CreateCommand();
    }

    public void ExecuteNonQuery(string command)
    {
        IDbCommand cmd = CreateCommand();

        cmd.CommandText = command;
        cmd.ExecuteNonQuery();

        cmd.Dispose();
        cmd = null;
    }

    public MySqlDataReader ExecuteReader(string command)
    {
        IDbCommand cmd = CreateCommand();

        cmd.CommandText = command;
        MySqlDataReader reader = (MySqlDataReader)cmd.ExecuteReader();

        cmd.Dispose();
        cmd = null;

        return reader;
    }

    public SqlAccount GetSqlAccount(int id)
    {
        using (MySqlDataReader reader = ExecuteReader("SELECT * FROM " + accountsTable + " WHERE id = " + id.ToString()))
        {
            while (reader.Read())
            {
                return ParseAccount(reader);
            }

            Debug.LogError("No account for that ID was found!");
            return null;
        }
    }

    public void GetAllSqlAccounts(UnityAction<SqlAccount[]> done)
    {
        Loading.Instance.SetOpen(true);
        Thread thread = new Thread(() => {


            List<SqlAccount> accounts = new List<SqlAccount>();

            using (MySqlDataReader reader = ExecuteReader("SELECT * FROM " + accountsTable))
            {
                while (reader.Read())
                {
                    accounts.Add(ParseAccount(reader));
                }
            }

            SqlAccount[] array = accounts.ToArray();
            accounts.Clear();
            accounts = null;

            this.invokingAccounts = array;
            this.toInvoke = done;
        });
        thread.Start();
    }

    public bool CreateSqlAccount(string name, int initialBalance)
    {
        string command = "INSERT INTO " + accountsTable + "(name, balance) VALUES ('" + name + "', " + initialBalance + ");";

        try
        {
            this.ExecuteNonQuery(command);
            return true;
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
            return false;
        }
    }

    public SqlAccount ParseAccount(MySqlDataReader reader)
    {
        int id = reader.GetInt32("id");
        string name = reader.GetString("name");
        int balance = reader.GetInt32("balance");

        SqlAccount account = new SqlAccount() { ID = id, Name = name, Balance = balance };

        return account;
    }

    public bool RenameAccount(int id, string newName)
    {
        return true;
    }

    public string MakeLogin(string username, string password, string databaseName, string IP)
    {
        return "User ID = " + username + "; Password = '" + password + "'; Database = " + databaseName + "; Server = " + IP;
    }
}

public class SqlAccount
{
    public int ID;
    public string Name;
    public int Balance;
}