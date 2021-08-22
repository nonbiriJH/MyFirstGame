using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class floatValue : ScriptableObject, ISerializationCallbackReceiver
{
    public float initialValue;
    public float runtimeValue;
    public float maxValue;

    void ISerializationCallbackReceiver.OnAfterDeserialize()
    {
        runtimeValue = initialValue;
        maxValue = initialValue;
    }

    void ISerializationCallbackReceiver.OnBeforeSerialize()
    {

    }
}
