﻿using MySql.Data.MySqlClient;
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
    public string logTable = "changeLog";

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
            if(Loading.Instance != null)
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

    public void GetSqlAccount(string name, UnityAction<SqlAccount> callback)
    {
        Thread thread = new Thread(() =>
        {
            using (MySqlDataReader reader = ExecuteReader("SELECT * FROM " + accountsTable + " WHERE name = '" + name + "'"))
            {

                while (reader.Read())
                {
                    SqlAccount acc = ParseAccount(reader);
                    callback.Invoke(acc);
                    return;
                }

                Debug.LogError("No account for that name was found!");
                callback.Invoke(null);
            }
        });
        thread.Start();
    }

    public void GetSqlAccount(int id, UnityAction<SqlAccount> callback)
    {
        Thread thread = new Thread(() =>
        {
            using (MySqlDataReader reader = ExecuteReader("SELECT * FROM " + accountsTable + " WHERE id = " + id.ToString()))
            {

                while (reader.Read())
                {
                    SqlAccount acc = ParseAccount(reader);
                    callback.Invoke(acc);
                    return;
                }

                Debug.LogError("No account for that ID was found!");
                callback.Invoke(null);
            }
        });
        thread.Start();
    }

    public SqlAccount GetSqlAccount(int id)
    {
        using (MySqlDataReader reader = ExecuteReader("SELECT * FROM " + accountsTable + " WHERE id = " + id.ToString()))
        {

            while (reader.Read())
            {
                SqlAccount acc = ParseAccount(reader);
                return acc;
            }

            Debug.LogError("No account for that ID was found!");
            return null;
        }
    }

    public void GetAllSqlAccounts(UnityAction<SqlAccount[]> done)
    {
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

    public void UpdateAccountBalance(int Id, int balanceChange, UnityAction done)
    {
        Thread thread = new Thread(() => 
        {
            SqlAccount account = GetSqlAccount(Id);

            int currentBalance = account.Balance;
            int updated = currentBalance + balanceChange;

            ExecuteNonQuery("UPDATE " + accountsTable + " SET balance = " + updated.ToString() + " WHERE id = " + account.ID);

            done.Invoke();
        });
        thread.Start();
    }

    public SqlAccount ParseAccount(MySqlDataReader reader)
    {
        int id = reader.GetInt32("id");
        string name = reader.GetString("name");
        int balance = reader.GetInt32("balance");

        SqlAccount account = new SqlAccount() { ID = id, Name = name, Balance = balance };

        return account;
    }

    public void RenameAccount(int id, string newName, UnityAction<bool> done)
    {
        Thread thread = new Thread(() =>
        {
            string cmd = "UPDATE " + accountsTable + " SET name = '" + newName.Trim() + "' WHERE id = " + id.ToString();
            try
            {
                ExecuteNonQuery(cmd);
                done.Invoke(true);
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
                done.Invoke(false);
            }
        });
        thread.Start();
    }

    public void RemoveAccount(int id, UnityAction<bool> done)
    {
        Thread thread = new Thread(() =>
        {
            string cmd = "DELETE FROM " + accountsTable + " WHERE id = " + id.ToString();

            try
            {
                ExecuteNonQuery(cmd);
                done.Invoke(true);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                done.Invoke(false);
            }
        });
        thread.Start();
    }

    public string MakeLogin(string username, string password, string databaseName, string IP)
    {
        return "User ID = " + username + "; Password = '" + password + "'; Database = " + databaseName + "; Server = " + IP;
    }

    public void AddLog(int id, string log, int balanceChange)
    {
        string cmd = "INSERT INTO " + logTable + " (id, description, balanceChange) VALUES (" + id.ToString() + ", '" + log.Trim() + "', " + balanceChange.ToString() + ");";

        // Automatically sets the current date and time.

        ExecuteNonQuery(cmd);
    }

    public void GetAllLogsFor(int id, UnityAction<SqlDescription[]> done)
    {
        // TODO finish me.
        Thread thread = new Thread(() => 
        {
            List<SqlDescription> descriptions = new List<SqlDescription>();

            string cmd = "SELECT * FROM " + logTable + " WHERE id = " + id.ToString() + ";";

            using (MySqlDataReader reader = ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    string notes = reader.GetString("description");
                    DateTime time = reader.GetDateTime("time");
                    int balanceChange = reader.GetInt32("balanceChange");

                    SqlDescription description = new SqlDescription() { Description = notes, Time = time, BalanceChange = balanceChange };

                    descriptions.Add(description);
                }
            }

            done.Invoke(descriptions.ToArray());
        });
        thread.Start();
    }
}

public class SqlAccount
{
    public int ID;
    public string Name;
    public int Balance;
}

public class SqlDescription
{
    public DateTime Time;
    public string Description;
    public int BalanceChange;
}