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
    private Room room;//disable room text when switch back to real player
    [SerializeField]
    private CinemachineVirtualCamera cm;
    [SerializeField]
    private Transform cmFollowDuringPlay;
    [SerializeField]
    private Transform cmFollowPostPlay;
    private int dialogTriggerCount;
    private int dialogInx;
    private bool roomNeedText;//record if current room need text
    private bool objectHided = false;//Hide game object in the first frame of Update
    private Vector2 deadZoneReg; //record current deadZone Setup
    private bool startHandle;//when condition met, run once before the rest of updates

    //methods to override by childs
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

    public virtual void EndPlayHandle()
    {

    }

    //methods
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

    public void SetDialog()
    {
        if(dialogInx < dialogs.Count)
        {
            dialogTrigger.nextDialog = dialogs[dialogInx];
            dialogInx += 1;
            dialogTriggerCount = dialogTrigger.triggerCount;
        }
    }

    public void TurnOffCMDeadZone(bool isTurnOff)
    {
        var cmComp = cm.GetCinemachineComponent<CinemachineFramingTransposer>();
        if (isTurnOff)
        {
            deadZoneReg = new Vector2(cmComp.m_DeadZoneWidth, cmComp.m_DeadZoneHeight);
            cmComp.m_DeadZoneWidth = 0;
            cmComp.m_DeadZoneHeight = 0;
        }
        else
        {
            cmComp.m_DeadZoneWidth = deadZoneReg.x;
            cmComp.m_DeadZoneHeight = deadZoneReg.y;
        }
    }
    public void TurnOffRoomText(bool isTrunOff)
    {
        if (isTrunOff)
        {
            roomNeedText = room.needText;
            room.needText = false;
        }
        else
        {
            room.needText = roomNeedText;
        }
    }

    public void EndPlay()
    {
        if (playableDirector.duration == playableDirector.time)
        {
            playableDirector.Stop();
            SetGameObjectsActive(replacedGameObjects, true);
            cm.Follow = cmFollowPostPlay;
            SetGameObjectsActive(actors, false);
            TurnOffCMDeadZone(false);
            EndPlayHandle();
        }

        else if (playableDirector.state != PlayState.Playing
            && roomNeedText != room.needText
            && room.playerInRange)
        {
            TurnOffRoomText(false);
        }
    }

    private void StartWhenConditionMet()
    {
        if (!startHandle)
        {
            TurnOffCMDeadZone(true);//avoid camera transition from actor to gameObj.
            TurnOffRoomText(true);//avoid room text diaplay again when transition from actor to gameObj.

            //Set up dialog
            dialogTrigger.dialogBox = dialogBox;
            timelineStopper.dialogBox = dialogBox;
            timelineStopper.playableDirector = playableDirector;
            dialogInx = 0;
            dialogTriggerCount = dialogTrigger.triggerCount;
            SetDialog();

            //actor on stage
            SetGameObjectsActive(actors, true);
            cm.Follow = cmFollowDuringPlay;

            StartHandle();//customise start

            playableDirector.Play();
            startHandle = true;
        }

    }

    //Mono
    public virtual void Start()
    {
        startHandle = false;
    }

    public virtual void Update()
    {
        if (PlayCondition())
        {
            StartWhenConditionMet();
            if (playableDirector.state == PlayState.Playing)
            {
                if (!objectHided)
                {
                    SetGameObjectsActive(replacedGameObjects, false);//hide after start to allow objects' start complete
                }
                if (dialogTriggerCount != dialogTrigger.triggerCount)
                {
                    SetDialog();
                }
                DuringPlayHandle();
            }
            if (!dialogBox.activeInHierarchy)
            {
                EndPlay();
            }
        }
    }

}
