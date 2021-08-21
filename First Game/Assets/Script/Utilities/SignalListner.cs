﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SignalListner : MonoBehaviour
{
    public SignalSender signal;
    public UnityEvent singleEvent;

    public void OnSignalRaised()
    {
        singleEvent.Invoke();
    }

    private void OnEnable()
    {
        signal.RegisterListner(this);
    }

    private void OnDisable()
    {
        signal.DeregisterListner(this);
    }
}
