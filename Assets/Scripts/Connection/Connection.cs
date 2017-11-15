using MySql.Data.MySqlClient;
using System.Data;
using UnityEngine;

public static class Connection
{
    public static void Test()
    {
        // Connect to database
        string connectionString =
          "Server=www.exbdirect.com;" +
          "Database=exbdirec_MoneyManager;" +
          "User ID=exbdirec_Test;" +
          "Password=TestPassword;";

        IDbConnection dbcon;
        dbcon = new MySqlConnection(connectionString);
        dbcon.Open();
        Debug.Log(dbcon.State);
        IDbCommand dbcmd = dbcon.CreateCommand();

        string command = "CREATE TABLE accounts (id INT PRIMARY KEY, name VARCHAR(16), balance INT DEFAULT 0);";

        dbcmd.CommandText = command;
        dbcmd.ExecuteNonQuery();

        dbcmd.Dispose();
        dbcmd = null;
        dbcon.Close();
        dbcon = null;
    }

    public static string MakeLogin(string username, string password, string databaseName, string IP)
    {
        return "User ID = " + username + "; Password = '" + password + "'; Database = " + databaseName + "; Server = " + IP;
    }
}