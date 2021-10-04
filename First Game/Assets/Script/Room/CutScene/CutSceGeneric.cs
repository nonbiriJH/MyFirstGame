using UnityEngine;
using UnityEngine.Playables;
using Cinemachine;
using System.Collections.Generic;

public class CutSceGeneric : MonoBehaviour
{
    public PlayableDirector playableDirector;
    public GameObject dialogBox;
    public DialogTrigger dialogTrigger;
    public TimelineStopper timelineStopper;
    public List<string[]> dialogs = new List<string[]>();
    public GameObject[] actors;
    public GameObject[] replacedGameObjects;
    [SerializeField]
    private CinemachineVirtualCamera cm;
    [SerializeField]
    private Transform cmFollowDuringPlay;
    [SerializeField]
    private Transform cmFollowPostPlay;
    private int dialogTriggerCount;
    private int dialogInx;

    public void SetGameObjectsActive(GameObject[] gameObjects, bool isActive)
    {
        for (int i = 0; i < gameObjects.Length; i++)
        {
            gameObjects[i].SetActive(isActive);
        }
    }

    public void SyncObjectPosition(GameObject sourceObject, GameObject targetObject)
    {
        targetObject.transform.position = sourceObject.transform.position;
    }

    /*private void AssignDialogTrack()
    {
        TimelineAsset timelineAsset = (TimelineAsset)playableDirector.playableAsset;
        IEnumerable<TrackAsset> trackAssets = timelineAsset.GetOutputTracks();
        foreach (TrackAsset track in trackAssets)
        {
            if (playableDirector.GetGenericBinding(track) == dialogBox)
            {
                dialogTrack = track;
            }
        }
    }*/

    public virtual bool PlayCondition()
    {
        return false;
    }

    public virtual void StartHandle()
    {

    }

    public virtual void DuringPlayHandle()
    {

    }

    public virtual void EndPlay()
    {
        if (playableDirector.duration == playableDirector.time
            && PlayCondition()
            && !dialogBox.activeInHierarchy)
        {
            playableDirector.Stop();
            SetGameObjectsActive(actors, false);
            SetGameObjectsActive(replacedGameObjects, true);
            cm.Follow = cmFollowPostPlay;
            EndPlayHandle();
        }
    }

    public virtual void EndPlayHandle()
    {

    }

    public void SetDialog()
    {
        if(dialogInx < dialogs.Count)
        {
            dialogTrigger.nextDialog = dialogs[dialogInx];
            dialogInx += 1;
            dialogTriggerCount = dialogTrigger.triggerCount;
        }
    }

    //Mono
    public virtual void Start()
    {
        if (PlayCondition())
        {
            dialogTrigger.dialogBox = dialogBox;
            timelineStopper.dialogBox = dialogBox;
            timelineStopper.playableDirector = playableDirector;
            dialogInx = 0;
            dialogTriggerCount = dialogTrigger.triggerCount;
            SetDialog();

            StartHandle();
            SetGameObjectsActive(actors, true);
            SetGameObjectsActive(replacedGameObjects, false);
            cm.Follow = cmFollowDuringPlay;
            playableDirector.Play();
        }
    }

    public virtual void Update()
    {
        if (playableDirector.state == PlayState.Playing)
        {
            if(dialogTriggerCount != dialogTrigger.triggerCount)
            {
                SetDialog();
            }
            DuringPlayHandle();
        }
        EndPlay();
    }
}
