using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSaveManager : MonoBehaviour
{
    public List<ScriptableObject> objects = new List<ScriptableObject>();
    public GlobalVariables globalVariables;//use to load scene;

    private string GetExt(bool isBase)
    {
        if (isBase)
        {
            return ".base";
        }
        else
        {
            return ".dat";
        }
    }

    public void DeleteData(bool isBase)
    {
        string fileRegEx = "/{0}" + GetExt(isBase);
        int i = 0;
        while (File.Exists(Application.persistentDataPath + string.Format(fileRegEx, i)))
        {
            //Delete File
            File.Delete(Application.persistentDataPath + string.Format(fileRegEx, i));
            i++;
        }
    }

    public void SaveData(bool isBase)
    {
        //Delete Previously Saved Data
        DeleteData(isBase);
        //Push Scriptable Object to Files
        string fileRegEx = "/{0}" + GetExt(isBase);
        for (int i = 0; i < objects.Count; i++)
        {
            //Create File as i.dat
            FileStream file = File.Create(Application.persistentDataPath + string.Format(fileRegEx, i));
            //Create Binary Formatter
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            //Put Objects to JSON
            var json = JsonUtility.ToJson(objects[i]);
            //Use Binary Formatter to Serialise JSON and Save to Files
            binaryFormatter.Serialize(file, json);
            file.Close();
        }
    }

    public void LoadData(bool isBase)
    {
        string fileRegEx = "/{0}" + GetExt(isBase);
        int i = 0;
        while (File.Exists(Application.persistentDataPath + string.Format(fileRegEx, i)))
        {
            Debug.Log(i);
            //Open File
            FileStream file = File.Open(Application.persistentDataPath + string.Format(fileRegEx, i), FileMode.Open);
            //Create Binary Formatter
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            //Deserialise File + Cast File to String + Overwrite to Object
            string data = (string)binaryFormatter.Deserialize(file);
            JsonUtility.FromJsonOverwrite(data, objects[i]);
            file.Close();
            i++;
        }
        SceneManager.LoadScene(globalVariables.currentScene);
    }
}
