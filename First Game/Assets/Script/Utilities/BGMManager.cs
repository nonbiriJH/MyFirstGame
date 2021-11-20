using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        if(SceneManager.GetActiveScene().name == "Dungeon")
        {
            PlayBGM("Dungeon");
        }
        else if (SceneManager.GetActiveScene().name == "SampleScene")
        {
            PlayBGM("OverWorld");
        }
    }

    private void PlayBGM(string BGMName)
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

    public void ChangeBGM(string BGMName)
    {
        //stop previous BGM
        if(audioSource.clip != null)
        {
            if(audioSource.clip.name != BGMName)
            {
                StopBGM();
            }
            else
            {
                return;
            }
        }
        PlayBGM(BGMName);
    }

    private IEnumerator FadeIn(AudioClip audioClip, float maxVolume)
    {
        //wait fading out finish
        while (isFadingOut)
        {
            yield return new WaitForSeconds(.5f);
        }
        //start fading in
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

    private IEnumerator FadeOut()
    {
        if (audioSource != null)
        {
            //wait fading in finish
            while (isFadingIn)
            {
                yield return new WaitForSeconds(.5f);
            }
            //start fade out
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
    }

}