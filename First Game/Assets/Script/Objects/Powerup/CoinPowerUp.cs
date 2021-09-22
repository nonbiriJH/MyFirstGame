using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPowerUp : PowerUp
{
    [Header("Coin Variable")]
    public int coinValue;
    public SignalSender coinSignal;

    [Header("Utility Variable")]
    public Inventory inventory;

    public override void ApplyPowerup()
    {
        inventory.coinValue += coinValue;
        coinSignal.SendSignal();
        base.ApplyPowerup();
    }
}
