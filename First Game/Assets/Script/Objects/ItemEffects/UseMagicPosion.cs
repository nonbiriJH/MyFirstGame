using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseMagicPosion : MonoBehaviour
{
    public floatValue magicValue;
    public SignalSender magicSignal;//change UI

    public void UseItem(float powerUpSize)
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
