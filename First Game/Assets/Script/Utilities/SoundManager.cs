using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField]
    private Sounds sounds;
    [HideInInspector]
    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(string soundName)
    {
        foreach(Sound sound in sounds.soundList)
        {
            if(sound.soundName == soundName)
            {
                audioSource.volume = sound.volume;
                audioSource.PlayOneShot(sound.audioClip);
            }
        }
    }
}
