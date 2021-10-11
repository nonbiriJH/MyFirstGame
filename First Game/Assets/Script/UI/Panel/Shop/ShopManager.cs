using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public Inventory shopInventory;
    public Inventory playerInventory;//for Access Coin
    public SignalSender coinSignal;// update coin UI when buy

    [Header("Private Variables")]
    [SerializeField] private GameObject itemHolderPrefab;
    [SerializeField] private GameObject content;
    [SerializeField] private TextMeshProUGUI description;
    [SerializeField] GameObject buyButton;
    [SerializeField] private ItemQuantityLookup itemQuantityLookup;
    [SerializeField] private ItemList itemMaster;

    public void OnEnable()
    {
        DeleteExistingItem();
        AddItemToInventoryUI();
    }


    public void AddItemToInventoryUI()
    {
        for (int i = 0; i < shopInventory.itemList.Count; i++)
        {
            Item item = itemMaster.GetItem(shopInventory.itemList[i]);
            if (itemQuantityLookup.GetItemShopQuantity(item.itemName) > 0)
            {
                //instantiate item holder and cache reference
                GameObject itemHolder = Instantiate(itemHolderPrefab);
                //Add new item holder to child of content
                itemHolder.transform.SetParent(content.transform);
                itemHolder.transform.localScale = new Vector3(1, 1, 1);
                //Setup item holder UI
                itemHolder.GetComponent<ShopItemHolderManager>().SetupItemHolder(item);
                //Without clicking, No Description No Buy Button
                description.text = "Select An Item";
                buyButton.SetActive(false);
            }
        }
    }

    public void DeleteExistingItem()
    {
        for (int i = 0; i < content.transform.childCount; i++)
        {
            Destroy(content.transform.GetChild(i).gameObject);
        }
    }

    public void OnItemHolderManagerClick()
    {
        Item item = itemMaster.GetItem(shopInventory.chosenItemName);
        if (item)
        {
            //Description
            description.text = item.itemDescription;
            buyButton.SetActive(true);
            if (playerInventory.coinValue < itemMaster.GetItem(shopInventory.chosenItemName).price)
            {
                buyButton.GetComponent<Button>().interactable = false;
            }
            else
            {
                buyButton.GetComponent<Button>().interactable = true;
            }
        }
    }

    public void OnClickBuyItem()
    {
        Item item = itemMaster.GetItem(shopInventory.chosenItemName);
        if (playerInventory.coinValue >= item.price
            && itemQuantityLookup.GetItemShopQuantity(shopInventory.chosenItemName) >= 1)
        {
            playerInventory.coinValue -= item.price;
            itemQuantityLookup.BuyItem(shopInventory.chosenItemName);
            coinSignal.SendSignal();
        }
        //If the chosen item is quantity 0, deselect item.
        if (itemQuantityLookup.GetItemShopQuantity(shopInventory.chosenItemName) == 0) shopInventory.chosenItemName = null;
        //Refresh Inventory UI
        DeleteExistingItem();
        AddItemToInventoryUI();
        //If used item was not used up, show its desc and use button
        OnItemHolderManagerClick();
    }

    public void OnExitButton()
    {
        this.gameObject.SetActive(false);
    }
}
