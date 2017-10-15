using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class DataSaver : MonoBehaviour {
    
	// Use this for initialization
	void Start () {
        string pathToDataLocation = Application.persistentDataPath + "/user_logs/";
        print(Application.persistentDataPath);

        string fileName =  "test.csv";
        string fullFilePath = pathToDataLocation + fileName;

        if (!Directory.Exists(pathToDataLocation))
            Directory.CreateDirectory(pathToDataLocation);

        try
        {
            StreamWriter writer = new StreamWriter(fullFilePath);
            writer.WriteLine("22, male, meh, noob");
            writer.WriteLine("q1, 1");
            writer.Close();
        }
        catch(Exception e)
        {
            print(e);
        }
        
	}
}
