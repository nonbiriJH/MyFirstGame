using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupMagic : PowerUp
{
    public float powerUpSize;
    public floatValue magicValue;
    public SignalSender magicSignal;//change UI

    public override void ApplyPowerup()
    {
        magicValue.runtimeValue += powerUpSize;
        if (magicValue.runtimeValue > magicValue.maxValue)
        {
            magicValue.runtimeValue = magicValue.maxValue;
        }
        //send signal to update UI
        magicSignal.SendSignal();
    }
}
