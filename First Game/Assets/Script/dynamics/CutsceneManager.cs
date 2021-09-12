using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CutsceneManager : MonoBehaviour
{
    public PlayableDirector playableDirector;
    public GlobalVariables globalVariables;


    // Start is called before the first frame update
    void Awake()
    {
        if (globalVariables.openningPlayed == false)
        {
            playableDirector.Play();
            globalVariables.openningPlayed = true;
        }
    }
}
