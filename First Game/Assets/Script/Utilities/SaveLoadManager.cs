using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveLoadManager : MonoBehaviour
{
    [SerializeField]
    private floatValue playerHealth;
    [SerializeField]
    private floatValue playerMagic;
    [SerializeField]
    private GlobalVariables globalVariables;
    [SerializeField]
    private vectorValue playerPosition;
    [SerializeField]
    private Inventory inventory;
    [SerializeField]
    private ItemQuantityLookup itemQuantityLookup;


    [Header("Check Points")]
    [SerializeField]
    private CheckPointNoneRoute checkPointR0;
    [SerializeField]
    private CheckPointR1 checkPointR1;

    [Header("Items")]
    [SerializeField]
    private Item evilBlade;
    [SerializeField]
    private Item elfBow;
    [SerializeField]
    private Item key;
    [SerializeField]
    private Item pill;
    [SerializeField]
    private Item healthPosion;
    [SerializeField]
    private Item magicPosion;

    private void ResetCheckPoint(ScriptableObject checkpoint)
    {
        var properties = checkpoint.GetType().GetFields();
        for (int i = 0; i < properties.Length; i++)
        {
            properties[i].SetValue(checkpoint, false);
        }
    }

    private void ResetFloatValue(floatValue toBeSet, float initial, float runtime, float max)
    {
        toBeSet.initialValue = initial;
        toBeSet.runtimeValue = runtime;
        toBeSet.maxValue = max;
    }

    private void ResetItem(Item item, int itemNumber, int shopQuantity)
    {
        item.itemNumber = itemNumber;
        item.shopQuantity = shopQuantity;
    }

    private void ResetVectorValue(vectorValue vectorValue, float x, float y)
    {
        vectorValue.runtimeValue = new Vector2(x, y);
    }

    private void ResetStringValue(GlobalVariables globalVariables, string newValue)
    {
        globalVariables.currentScene = newValue;
    }

    private void ResetInventory()
    {
        //create new item list
        List<Item> newItemList = new List<Item> { evilBlade, elfBow, key, pill, healthPosion, magicPosion };
        inventory.itemList = newItemList;
        inventory.newItem = null;
        inventory.chosenItem = null;
        inventory.coinValue = 0;
    }

    public void GameStart()
    {
        //Check Point uncheck
        ResetCheckPoint(checkPointR0);
        ResetCheckPoint(checkPointR1);
        //Float values
        ResetFloatValue(playerHealth, 6, 6, 6);
        ResetFloatValue(playerMagic, 10, 10, 20);
        //Vector Value
        ResetVectorValue(playerPosition, 0, -3);
        //String Value
        ResetStringValue(globalVariables, "Room");
        //Inventory
        ResetInventory();
        //Items
        ResetItem(evilBlade, 0, 1);
        ResetItem(elfBow, 0, 0);
        ResetItem(key, 0, 0);
        ResetItem(pill, 0, 0);
        ResetItem(healthPosion, 0, 999);
        ResetItem(magicPosion, 0, 999);

        SceneManager.LoadScene(globalVariables.currentScene);
    }

    private void SaveSingleData(string fileName, ScriptableObject scriptableObject)
    {
        string filePath = Application.persistentDataPath + fileName;
        //Delete File if exists
        if(File.Exists(filePath)) File.Delete(filePath);
        //Create File as i.dat
        FileStream file = File.Create(filePath);
        //Create Binary Formatter
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        //Put Objects to JSON
        var json = JsonUtility.ToJson(scriptableObject);
        if (fileName == "itemQuantityLookup.dat") Debug.Log(json);
        //Use Binary Formatter to Serialise JSON and Save to Files
        binaryFormatter.Serialize(file, json);
        file.Close();
    }

    private void LoadSingleData(string fileName, ScriptableObject scriptableObject)
    {
        string filePath = Application.persistentDataPath + fileName;
        if (File.Exists(filePath))
        {
            //Open File
            FileStream file = File.Open(filePath, FileMode.Open);
            //Create Binary Formatter
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            //Deserialise File + Cast File to String + Overwrite to Object
            string data = (string)binaryFormatter.Deserialize(file);
            if (fileName == "itemQuantityLookup.dat") Debug.Log(data);
            JsonUtility.FromJsonOverwrite(data, scriptableObject);
            file.Close();
        }
    }


    public void SaveData()
    {
        SaveSingleData("checkPointR0.dat", checkPointR0);
        SaveSingleData("checkPointR1.dat", checkPointR1);

        SaveSingleData("playerHealth.dat", playerHealth);
        SaveSingleData("playerMagic.dat", playerMagic);
        SaveSingleData("playerPosition.dat", playerPosition);
        SaveSingleData("globalVariables.dat", globalVariables);
        SaveSingleData("itemQuantityLookup.dat", itemQuantityLookup);

        SaveSingleData("inventory.dat", inventory);
        SaveSingleData("evilBlade.dat", evilBlade);
        SaveSingleData("elfBow.dat", elfBow);
        SaveSingleData("key.dat", key);
        SaveSingleData("healthPosion.dat", healthPosion);
        SaveSingleData("magicPosion.dat", magicPosion);
        SaveSingleData("pill.dat", pill);
    }

    public void LoadData()
    {
        LoadSingleData("checkPointR0.dat", checkPointR0);
        LoadSingleData("checkPointR1.dat", checkPointR1);

        LoadSingleData("playerHealth.dat", playerHealth);
        LoadSingleData("playerMagic.dat", playerMagic);
        LoadSingleData("playerPosition.dat", playerPosition);
        LoadSingleData("globalVariables.dat", globalVariables);

        LoadSingleData("inventory.dat", inventory);
        LoadSingleData("evilBlade.dat", evilBlade);
        LoadSingleData("elfBow.dat", elfBow);
        LoadSingleData("key.dat", key);
        LoadSingleData("healthPosion.dat", healthPosion);
        LoadSingleData("magicPosion.dat", magicPosion);
        LoadSingleData("pill.dat", pill);

        SceneManager.LoadScene(globalVariables.currentScene);
    }

}
