using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    public Inventory inventory;

    [Header("Private Variables")]
    [SerializeField] private GameObject itemHolderPrefab;
    [SerializeField] private GameObject weaponHolderPrefab;
    [SerializeField] private GameObject content;
    [SerializeField] private GameObject weaponContent;
    [SerializeField] private TextMeshProUGUI description;
    [SerializeField] GameObject useButton;

    public void OnEnable()
    {
        DeleteExistingItem();
        AddItemToInventoryUI();
    }

    
    public void AddItemToInventoryUI()
    {
        for (int i = 0; i<inventory.itemList.Count; i++)
        {
            Item item = inventory.itemList[i];
            if (item.itemNumber > 0)
            {
                if (item.weapon)
                {
                    //instantiate item holder and cache reference
                    GameObject itemHolder = Instantiate(weaponHolderPrefab);
                    //Add new item holder to child of content
                    itemHolder.transform.SetParent(weaponContent.transform);
                    itemHolder.transform.localScale = new Vector3(1, 1, 1);
                    //Setup item holder UI
                    itemHolder.GetComponent<WeaponHolderManager>().SetupItemHolder(item);
                    //Without clicking, No Description No Use Button
                    description.text = "Equipt A Weapon";
                    useButton.SetActive(false);
                }
                else
                {
                    //instantiate item holder and cache reference
                    GameObject itemHolder = Instantiate(itemHolderPrefab);
                    //Add new item holder to child of content
                    itemHolder.transform.SetParent(content.transform);
                    itemHolder.transform.localScale = new Vector3(1, 1, 1);
                    //Setup item holder UI
                    itemHolder.GetComponent<ItemHolderManager>().SetupItemHolder(item);
                    //Without clicking, No Description No Use Button
                    description.text = "Select An Item";
                    useButton.SetActive(false);
                }
                
            }
        }
    }

    public void DeleteExistingItem()
    {
        for(int i =0; i < content.transform.childCount; i++)
        {
            Destroy(content.transform.GetChild(i).gameObject);
        }

        for (int i = 0; i < weaponContent.transform.childCount; i++)
        {
            Destroy(weaponContent.transform.GetChild(i).gameObject);
        }
    }

    public void OnItemHolderManagerClick()
    {
        Item item = inventory.chosenItem;
        if (item)
        {
            //Description
            description.text = item.itemDescription;
            //Activate Use Button
            if (item.canUse)
            {
                useButton.SetActive(true);
            }
            else
            {
                useButton.SetActive(false);
            }
        }
    }

    public void OnClickUseItem()
    {
        //Apply item effects
        inventory.chosenItem.useItem();
        //If the chosen item is quantity 0, deselect item.
        if(inventory.chosenItem.itemNumber == 0) inventory.chosenItem = null;
        //Refresh Inventory UI
        DeleteExistingItem();
        AddItemToInventoryUI();
        //If used item was not used up, show its desc and use button
        OnItemHolderManagerClick();
    }

    public void OnWeaponChange()
    {
        if (!weaponContent.activeInHierarchy)
        {
            content.SetActive(false);
            weaponContent.SetActive(true);
        }
    }

    public void OnConsumableChange()
    {
        if (!content.activeInHierarchy)
        {
            weaponContent.SetActive(false);
            content.SetActive(true);
        }
    }
}