using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinManager : MonoBehaviour
{
    public Inventory inventory;
    public TextMeshProUGUI coinDisplay;

    public void Start()
    {
        coinDisplay.text = "" + inventory.coinValue;
    }

    public void UpdateCoinDisplay()
    {
        coinDisplay.text = "" + inventory.coinValue;
    }
}
