using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LocalData : MonoBehaviour {

    public static LocalData Instance;

    public RecurringLog[] Logs;

    public LocalData()
    {
        Instance = this;
    }

    public void Start()
    {
        Debug.Log(Application.persistentDataPath);

        foreach(RecurringLog log in Logs)
        {
            WriteRecurringLog(Application.persistentDataPath + "/" + log.Name + ".txt", log);
        }
    }

    public bool WriteRecurringLog(string path, RecurringLog log)
    {
        try
        {
            string text = JsonUtility.ToJson(log, true);

            StreamWriter writer = null;

            writer = File.CreateText(path);

            writer.Write(text);

            writer.Close();

            return true;
        }
        catch (Exception e)
        {
            Debug.LogError("Failed to write to file '" + path + "'! " + e);
            return false;
        }   
    }

    public RecurringLog ReadRecurringLog(string path)
    {
        try
        {
            string text = File.ReadAllText(path);

            return JsonUtility.FromJson<RecurringLog>(text);
        }
        catch (Exception e)
        {
            Debug.LogError("Failed to read from file '" + path + "'! " + e);
            return null;
        }
    }

    public RecurringLog[] ReadAllRecurringLogs(string directory)
    {
        try
        {
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            string[] files = Directory.GetFiles(directory);
            List<RecurringLog> logs = new List<RecurringLog>();

            foreach(string file in files)
            {
                logs.Add(ReadRecurringLog(file));
            }

            return logs.ToArray();
        }
        catch (Exception e)
        {
            Debug.LogError("Failed to read files from '" + directory + "'! " + e);
            return null;
        }
    }
}