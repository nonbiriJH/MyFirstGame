using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseHealthPosion : MonoBehaviour
{
    public floatValue playerHealth;
    public SignalSender healthSignal;//change UI

    public void UseItem(float powerUpSize)
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
