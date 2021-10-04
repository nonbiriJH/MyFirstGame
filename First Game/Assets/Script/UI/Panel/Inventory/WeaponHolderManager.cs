using UnityEngine;
using UnityEngine.UI;

public class WeaponHolderManager : MonoBehaviour
{
    public Inventory inventory;

    [Header("Private Variables")]
    //Internal but need to assign from the inspector
    [SerializeField] private Image itemImage;
    [SerializeField] private Item itemHeld;

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
        inventory.chosenItem = itemHeld;
        //Apply item effects
        inventory.chosenItem.useItem();
    }
}
