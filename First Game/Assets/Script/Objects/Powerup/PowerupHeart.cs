using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupHeart : PowerUp
{
    public float powerUpSize;
    public floatValue playerHealth;
    public SignalSender healthSignal;//change UI

    public override void ApplyPowerup()
    {
        playerHealth.runtimeValue += powerUpSize;
        if (playerHealth.runtimeValue > playerHealth.maxValue)
        {
            playerHealth.runtimeValue = playerHealth.maxValue;
        }
        //send signal to update UI
        healthSignal.SendSignal();
    }
}
