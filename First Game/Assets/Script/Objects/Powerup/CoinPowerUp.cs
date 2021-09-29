using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPowerUp : PowerUp
{
    [Header("Coin Variable")]
    public int coinValue;
    public SignalSender coinSignal;

    [Header("Checkpoint")]
    [SerializeField]
    private CheckPointR1 checkPointR1;
    [SerializeField]
    private SignalSender regPositionOnCheckPoint;

    [Header("Utility Variable")]
    public Inventory inventory;

    public override void ApplyPowerup()
    {
        inventory.coinValue += coinValue;
        coinSignal.SendSignal();
        if(inventory.coinValue > 10000)
        {
            checkPointR1.ReachMoney = true;
            regPositionOnCheckPoint.SendSignal();
        }
        base.ApplyPowerup();
    }
}
