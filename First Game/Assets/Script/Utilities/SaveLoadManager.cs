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
    [SerializeField]
    private CheckPointR2 checkPointR2;
    private SoundManager soundManager;

    private void Start()
    {
        soundManager = (SoundManager)FindObjectOfType(typeof(SoundManager));
    }

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
        List<string> newItemNameList = new List<string> { "Evil Blade", "Elf Bow", "Dungeon Key", "Contaminated Pill", "Health Posion", "Magic Posion", "Pure Drop", "Saint Arrow", "The Heal" };
        inventory.itemList = newItemNameList;
        inventory.newItemName = null;
        inventory.chosenItemName = null;
        inventory.coinValue = 0;
    }

    private void ResetItemQuantityLookup()
    {
        List<ItemMapping> newItemQuantityLookup = new List<ItemMapping> { new ItemMapping("Evil Blade", 0, 1)
            , new ItemMapping("Elf Bow", 0, 0)
            , new ItemMapping("Dungeon Key", 0, 0)
            , new ItemMapping("Contaminated Pill", 0, 0)
            , new ItemMapping("Health Posion", 0, 99)
            , new ItemMapping("Magic Posion", 0, 99)
            , new ItemMapping("Pure Drop", 0, 0)
            , new ItemMapping("Saint Arrow", 0, 0)
            , new ItemMapping("The Heal", 0, 0) };
        itemQuantityLookup.itemMappings = newItemQuantityLookup;
    }

    public void GameStart()
    {
        soundManager.PlaySound("Click");
        //Check Point uncheck
        ResetCheckPoint(checkPointR0);
        if (checkPointR1.endBad)
        {
            ResetCheckPoint(checkPointR1);
            checkPointR1.endBad = true;
        }
        else ResetCheckPoint(checkPointR1);

        if (checkPointR2.endGood)
        {

            ResetCheckPoint(checkPointR2);
            checkPointR2.endGood = true;
        }
        else ResetCheckPoint(checkPointR2);

        //Float values
        ResetFloatValue(playerHealth, 6, 6, 6);
        ResetFloatValue(playerMagic, 10, 10, 20);
        //Vector Value
        ResetVectorValue(playerPosition, 0, -3);
        //String Value
        ResetStringValue(globalVariables, "Room");
        //Inventory
        ResetInventory();
        //Item Quantity
        ResetItemQuantityLookup();

        SceneManager.LoadScene(globalVariables.currentScene);
    }

    private void SaveSingleData(string fileName, ScriptableObject scriptableObject)
    {
        string filePath = Application.persistentDataPath + "/" + fileName;
        //Delete File if exists
        if(File.Exists(filePath)) File.Delete(filePath);
        //Create File as i.dat
        FileStream file = File.Create(filePath);
        //Create Binary Formatter
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        //Put Objects to JSON
        var json = JsonUtility.ToJson(scriptableObject);
        //Use Binary Formatter to Serialise JSON and Save to Files
        binaryFormatter.Serialize(file, json);
        file.Close();
    }

    private void LoadSingleData(string fileName, ScriptableObject scriptableObject)
    {
        string filePath = Application.persistentDataPath + "/" + fileName;
        if (File.Exists(filePath))
        {
            //Open File
            FileStream file = File.Open(filePath, FileMode.Open);
            //Create Binary Formatter
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            //Deserialise File + Cast File to String + Overwrite to Object
            string data = (string)binaryFormatter.Deserialize(file);
            JsonUtility.FromJsonOverwrite(data, scriptableObject);
            file.Close();
        }
    }


    public void SaveData()
    {
        soundManager.PlaySound("Click");

        SaveSingleData("checkPointR0.dat", checkPointR0);
        SaveSingleData("checkPointR1.dat", checkPointR1);
        SaveSingleData("checkPointR2.dat", checkPointR2);

        SaveSingleData("playerHealth.dat", playerHealth);
        SaveSingleData("playerMagic.dat", playerMagic);
        SaveSingleData("playerPosition.dat", playerPosition);
        SaveSingleData("globalVariables.dat", globalVariables);
        SaveSingleData("itemQuantityLookup.dat", itemQuantityLookup);
        SaveSingleData("inventory.dat", inventory);
    }

    public void LoadData()
    {
        soundManager.PlaySound("Click");

        LoadSingleData("checkPointR0.dat", checkPointR0);
        LoadSingleData("checkPointR1.dat", checkPointR1);
        LoadSingleData("checkPointR2.dat", checkPointR2);

        LoadSingleData("playerHealth.dat", playerHealth);
        LoadSingleData("playerMagic.dat", playerMagic);
        LoadSingleData("playerPosition.dat", playerPosition);
        LoadSingleData("globalVariables.dat", globalVariables);
        LoadSingleData("itemQuantityLookup.dat", itemQuantityLookup);
        LoadSingleData("inventory.dat", inventory);

        SceneManager.LoadScene(globalVariables.currentScene);
    }

}
