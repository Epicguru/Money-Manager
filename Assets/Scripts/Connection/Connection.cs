using System.Net;
using System.Data.SqlClient;
using System;
using UnityEngine;
using System.Data;

public static class Connection
{
    public static void Test()
    {

        //Connecting to remote Microsoft SQL Database Server

        const string USERNAME = "exbdirec_Admin";
        const string PASSWORD = ";%+6WUSunXbH";
        const string DATABASE_NAME = "exbdirec_MoneyManager";
        const string IP = "176.67.162.22";

        const string TABLE_NAME = "accounts";

        SqlConnection conn = new SqlConnection(MakeLogin(USERNAME, PASSWORD, DATABASE_NAME, IP));
        conn.Open();
        SqlCommand cmd = new SqlCommand();
        cmd.CommandType = CommandType.TableDirect;
        cmd.CommandText =
            "CREATE TABLE " + TABLE_NAME + " ( " + 
            "id INT PRIMARY KEY," + 
            "name VARCHAR NOT NULL, " + 
            "balance INT DEFAULT 0" + 
            ");"
        ;
        cmd.Connection = conn;
        try
        {
            cmd.ExecuteNonQuery();
        }
        catch(Exception e)
        {
            // Output Error
            Debug.LogError(e.StackTrace);
        }

        conn.Close();
    }

    public static string MakeLogin(string username, string password, string databaseName, string IP, string port)
    {
        return "User ID = " + username + "; Password = '" + password + "'; Database = " + databaseName + "; Server = " + IP;
    }
}