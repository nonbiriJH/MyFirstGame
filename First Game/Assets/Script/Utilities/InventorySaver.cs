using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InventorySaver : MonoBehaviour
{
    public Inventory inventory;

    private string GetExt(bool isBase)
    {
        if (isBase)
        {
            return ".inv.base";
        }
        else
        {
            return ".inv.dat";
        }
    }

    private void DeleteAFile(string fileName)
    {
        File.Delete(Application.persistentDataPath + fileName);
    }

    private void SaveAScriptable(string fileName, ScriptableObject scriptable) 
    {
        // Create File as i.dat
        FileStream file = File.Create(Application.persistentDataPath + fileName);
        //Create Binary Formatter
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        //Put Objects to JSON
        var json = JsonUtility.ToJson(scriptable);
        //Use Binary Formatter to Serialise JSON and Save to Files
        binaryFormatter.Serialize(file, json);
        file.Close();
    }

    private void LoadAScriptable(string fileName, ScriptableObject scriptable)
    {
        //Open File
        FileStream file = File.Open(Application.persistentDataPath + fileName, FileMode.Open);
        //Create Binary Formatter
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        //Deserialise File + Cast File to String + Overwrite to Item
        string data = (string)binaryFormatter.Deserialize(file);
        Debug.Log(data);
        Debug.Log(scriptable);
        JsonUtility.FromJsonOverwrite(data, scriptable);
        file.Close();
    }


    public void DeleteData(bool isBase)
    {
        //Delete Item Files
        string fileRegEx = "/{0}" + GetExt(isBase);
        int i = 0;
        while (File.Exists(Application.persistentDataPath + string.Format(fileRegEx, i)))
        {
            DeleteAFile(string.Format(fileRegEx, i));
            i++;
        }
        //Delete Inventory file
        DeleteAFile(string.Format(fileRegEx, "inventory"));
    }

    public void SaveData(bool isBase)
    {
        //Delete Previously Saved Data
        DeleteData(isBase);
        //Push Scriptable Objects - Item to Files
        string fileRegEx = "/{0}" + GetExt(isBase);
        for (int i = 0; i < inventory.itemList.Count; i++)
        {
            SaveAScriptable(string.Format(fileRegEx, i), inventory.itemList[i]);
        }
        //Push Scriptable Inventory - Item to Files
        SaveAScriptable(string.Format(fileRegEx, "inventory"), inventory);
    }

    public void LoadData(bool isBase)
    {

        string fileRegEx = "/{0}" + GetExt(isBase);

        //Load Inventory Scriptable Object
        LoadAScriptable(string.Format(fileRegEx, "inventory"), inventory);
        //LoadAScriptable(string.Format("/{0}.base", "6"), inventory);
        //LoadInve();

        //Load Item Scriptable Objects.
        int i = 0;
        while (File.Exists(Application.persistentDataPath + string.Format(fileRegEx, i)))
        {
            LoadAScriptable(string.Format(fileRegEx, i), inventory.itemList[i]);
            i++;

        }
    }

    private void LoadInve()
    {
        //Open File
        FileStream file = File.Open(Application.persistentDataPath + string.Format("/{0}.base", "6"), FileMode.Open);
        //Create Binary Formatter
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        //Deserialise File + Cast File to String + Overwrite to Object
        JsonUtility.FromJsonOverwrite((string)binaryFormatter.Deserialize(file), inventory);
        file.Close();
    }


}
