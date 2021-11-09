using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Sound
{
    public string soundName;
    public AudioClip audioClip;
    [Range(0f, 1f)]
    public float volume;

}

[CreateAssetMenu(fileName = "Sounds", menuName = "Scriptable Objects/Sounds")]
public class Sounds : ScriptableObject
{
    public Sound[] soundList;
}
