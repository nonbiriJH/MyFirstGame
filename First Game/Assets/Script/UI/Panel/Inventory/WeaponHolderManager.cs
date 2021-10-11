using UnityEngine;
using UnityEngine.UI;

public class WeaponHolderManager : MonoBehaviour
{
    public Inventory inventory;

    [Header("Private Variables")]
    //Internal but need to assign from the inspector
    [SerializeField] private Image itemImage;
    [SerializeField] private Item itemHeld;
    [SerializeField] private ItemList itemMaster;

    //Pass Item information to Item Holder UI
    public void SetupItemHolder(Item newItem)
    {
        if (newItem)
        {
            itemHeld = newItem;
            itemImage.sprite = newItem.itemSprite;
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
        //Apply item effects
        itemMaster.GetItem(inventory.chosenItemName).useItem();
    }
}
