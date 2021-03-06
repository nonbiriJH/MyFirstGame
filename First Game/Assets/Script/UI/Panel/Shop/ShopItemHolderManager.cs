using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopItemHolderManager : MonoBehaviour
{
    public Inventory shopInventory;
    public SignalSender buySignal;//Communicate to Shop Inventory Manager

    [Header("Private Variables")]
    //Internal but need to assign from the inspector
    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI itemPrice;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private Item itemHeld;
    private SoundManager soundManager;

    private void Start()
    {
        soundManager = (SoundManager)FindObjectOfType(typeof(SoundManager));
    }

    //Pass Item information to Item Holder UI
    public void SetupItemHolder(Item newItem)
    {
        if (newItem)
        {
            itemHeld = newItem;
            itemImage.sprite = newItem.itemSprite;
            itemPrice.text = "" + newItem.price;
            itemName.text = "" + newItem.itemName;
        }
    }

    //Pass UI information to inventory
    public void OnClick()
    {
        soundManager.PlaySound("Click");
        shopInventory.chosenItemName = itemHeld.itemName;
        buySignal.SendSignal();
    }
}
