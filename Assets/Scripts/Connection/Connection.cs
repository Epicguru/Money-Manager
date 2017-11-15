using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class Connection : MonoBehaviour
{
    public IDbConnection Conn;

    public void Start()
    {
        OpenConnection();

        SqlAccount[] accounts = GetAllSqlAccounts();
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
        using (MySqlDataReader reader = ExecuteReader("SELECT * FROM accounts WHERE id = " + id.ToString()))
        {
            while (reader.Read())
            {
                return MakeAccount(reader);
            }

            Debug.LogError("No account for that ID was found!");
            return null;
        }
    }

    public SqlAccount[] GetAllSqlAccounts()
    {
        List<SqlAccount> accounts = new List<SqlAccount>();

        using (MySqlDataReader reader = ExecuteReader("SELECT * FROM accounts"))
        {
            while (reader.Read())
            {                
                accounts.Add(MakeAccount(reader));
            }
        }

        SqlAccount[] array = accounts.ToArray();
        accounts.Clear();
        accounts = null;

        return array;
    }

    public SqlAccount MakeAccount(MySqlDataReader reader)
    {
        int id = reader.GetInt32("id");
        string name = reader.GetString("name");
        int balance = reader.GetInt32("balance");

        SqlAccount account = new SqlAccount() { ID = id, Name = name, Balance = balance };

        return account;
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