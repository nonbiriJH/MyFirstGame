using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Playables;

public class CutSceGenBasic : MonoBehaviour
{

    public PlayableDirector playableDirector;
    private bool startPrePlayHandle = false;//when condition met, run once before the rest of updates

    //methods to override by childs
    public virtual bool PlayCondition()
    {
        return false;
    }

    public virtual void StartWhenConditionMet()
    {
    }

    public virtual void UpdateDuringPlay()
    {

    }

    public virtual void Update()
    {
        if (PlayCondition())
        {
            if (!startPrePlayHandle)
            {
                StartWhenConditionMet();
                playableDirector.Play();
                startPrePlayHandle = true;
            }
            UpdateDuringPlay();
        }
    }

}
