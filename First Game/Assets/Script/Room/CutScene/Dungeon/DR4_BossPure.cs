using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DR4_BossPure : CutSceGeneric
{
    [SerializeField]
    private CheckPointR2 checkPointR2;
    [SerializeField]
    private string[] startDialog;
    [SerializeField]
    private string[] endDialog;
    [SerializeField]
    private string[] getDialog;
    [SerializeField]
    private string[] afterGetDialog;
    [SerializeField]
    private string[] leadDialog;
    [SerializeField]
    private GlobalVariables globalVariables;
    [SerializeField]
    private string sceneToLoad;

    private void Start()
    {
        if (checkPointR2.bossPurified)
        {
            this.gameObject.SetActive(false);
        }
    }


    public override bool PlayCondition()
    {
        return checkPointR2.bossPurifyStart && !checkPointR2.bossPurified;
    }

    public override void StartWhenConditionMet()
    {
        base.StartWhenConditionMet();
    }

    public override void EndPlayHandle()
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneToLoad);
        globalVariables.currentScene = sceneToLoad;
    }


    private void Awake()
    {
        dialogs.Add(startDialog);
        dialogs.Add(endDialog);
        dialogs.Add(getDialog);
        dialogs.Add(afterGetDialog);
        dialogs.Add(leadDialog);
    }
}
