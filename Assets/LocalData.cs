using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LocalData : MonoBehaviour {

    public static LocalData Instance;

    public LocalData()
    {
        Instance = this;
    }

    public void Start()
    {
        Debug.Log(Application.persistentDataPath);
    }

    public bool WriteText(string path, string text)
    {
        try
        {
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
}
