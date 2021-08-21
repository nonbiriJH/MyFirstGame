using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class vectorValue : ScriptableObject, ISerializationCallbackReceiver
{
    public Vector2 initialValue;

    public Vector2 runtimeValue;

    void ISerializationCallbackReceiver.OnAfterDeserialize()
    {
        runtimeValue = initialValue;
    }

    void ISerializationCallbackReceiver.OnBeforeSerialize()
    {

    }
}
