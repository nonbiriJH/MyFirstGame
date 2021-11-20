using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    [SerializeField]
    private Sounds sounds;
    private AudioSource audioSource;
    private bool isFadingIn;
    private bool isFadingOut;
    private float fadeSpeed;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = true;
        isFadingIn = false;
        isFadingOut = false;
        fadeSpeed = 0.1f;
    }

    public void PlayBGM(string BGMName)
    {
        foreach (Sound sound in sounds.soundList)
        {
            if (sound.soundName == BGMName)
            {
                StartCoroutine(FadeIn(sound.audioClip, sound.volume));
            }
        }
    }

    public void StopBGM()
    {
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeIn(AudioClip audioClip, float maxVolume)
    {
        if (!isFadingOut)
        {
            audioSource.volume = 0;
            audioSource.clip = audioClip;
            audioSource.Play();
            isFadingIn = true;
            while (audioSource.volume < maxVolume)
            {
                audioSource.volume += fadeSpeed;
                yield return new WaitForSeconds(.1f);
            }
            isFadingIn = false;
        }
        else
        {
            yield return new WaitForSeconds(.5f);
        }
    }

    private IEnumerator FadeOut()
    {
        if (audioSource != null)
        {
            if (!isFadingIn)
            {
                isFadingOut = true;
                while (audioSource.volume > 0)
                {
                    audioSource.volume -= fadeSpeed;
                    yield return new WaitForSeconds(.1f);
                }
                audioSource.Stop();
                audioSource.clip = null;
                isFadingOut = false;
            }
            else
            {
                yield return new WaitForSeconds(.5f);
            }
        }
        
    }
}