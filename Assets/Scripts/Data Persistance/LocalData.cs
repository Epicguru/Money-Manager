using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LocalData : MonoBehaviour {

    public static LocalData Instance;

    public RecurringChange[] Logs;

    public LocalData()
    {
        Instance = this;
    }

    public void Start()
    {
        string path = Application.persistentDataPath + "/Local Data/";

        Debug.Log(Application.persistentDataPath);

        DateTime now = DateTime.Now;

        foreach (RecurringChange log in ReadAllRecurringLogs(path))
        {
            Debug.Log(now.Subtract(log.LastDate).Minutes + " minutes since " + log.Name);
        }

        foreach(RecurringChange log in Logs)
        {
            WriteRecurringLog(path + log.Name + ".txt", log);
        }
    }

    public void TriggerLogs()
    {
        // Load logs from local memory...
        // For each log.

        // Apply logs to any accounts that need applying to.

        // Save logs back with current date.

        string path = Application.persistentDataPath + "/Local Data/";

        DateTime now = DateTime.Now;

        foreach (RecurringChange log in ReadAllRecurringLogs(path))
        {
            TimeSpan sinceLastTime = now.Subtract(log.LastDate);

            int daysChange = sinceLastTime.Days;

            while(daysChange >= log.DayInterval)
            {
                // Trigger event.
                
            }
        }
    }

    public bool WriteRecurringLog(string path, RecurringChange log)
    {
        try
        {
            log.LastDate = DateTime.UtcNow;

            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
            settings.DateParseHandling = DateParseHandling.DateTime;
            settings.Formatting = Formatting.Indented;
            string text = JsonConvert.SerializeObject(log, settings);

            StreamWriter writer = null;

            writer = File.CreateText(path);

            writer.Write(text);

            writer.Close();

            return true;
        }
        catch (Exception e)
        {
            Debug.LogError("Failed to write to file '" + path + "'! " + e);
            Debug.LogError(e.StackTrace);
            return false;
        }   
    }

    public RecurringChange ReadRecurringLog(string path)
    {
        try
        {
            string text = File.ReadAllText(path);

            return JsonConvert.DeserializeObject<RecurringChange>(text);
        }
        catch (Exception e)
        {
            Debug.LogError("Failed to read from file '" + path + "'! " + e);
            return null;
        }
    }

    public RecurringChange[] ReadAllRecurringLogs(string directory)
    {
        try
        {
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            string[] files = Directory.GetFiles(directory);
            List<RecurringChange> logs = new List<RecurringChange>();

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