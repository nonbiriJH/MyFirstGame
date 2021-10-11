using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemHolderManager : MonoBehaviour
{
    public Inventory inventory;
    public SignalSender itemChosenSignal;//Communicate to Inventory Manager

    [Header("Private Variables")]
    //Internal but need to assign from the inspector
    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI itemNumber;
    [SerializeField] private Item itemHeld;
    [SerializeField] private ItemQuantityLookup itemQuantityLookup;

    //Pass Item information to Item Holder UI
    public void SetupItemHolder(Item newItem)
    {
        if (newItem)
        {
            itemHeld = newItem;
            itemImage.sprite = newItem.itemSprite;
            itemNumber.text = "" + itemQuantityLookup.GetItemNumber(newItem.itemName);
            if (newItem.RBG != Vector3.zero)
            {
                itemImage.color = new Color(newItem.RBG.x, newItem.RBG.y, newItem.RBG.z);
            }
        }
    }

    //Pass UI information to inventory
    public void OnClick()
    {
        inventory.chosenItemName = itemHeld.itemName;
        itemChosenSignal.SendSignal();
    }

}
