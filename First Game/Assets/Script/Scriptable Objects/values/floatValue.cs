using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class floatValue : ScriptableObject, ISerializationCallbackReceiver
{
    public float initialValue;
    public float runtimeValue;

    void ISerializationCallbackReceiver.OnAfterDeserialize()
    {
        runtimeValue = initialValue;
    }

    void ISerializationCallbackReceiver.OnBeforeSerialize()
    {

    }
}
