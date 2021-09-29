using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CutsceneManagerEnableActor : MonoBehaviour
{
    public PlayableDirector playableDirector;
    public bool played;
    public GameObject[] actors;


    // Start is called before the first frame update
    private void Update()
    {
        if (!played)
        {
            for (int i = 0; i < actors.Length; i++)
            {
                if (!actors[i].activeInHierarchy)
                {
                    actors[i].SetActive(true);
                }
            }
            playableDirector.Play();
            played = true;
        }
    }

}
