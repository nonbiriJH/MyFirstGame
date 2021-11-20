using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class R2ED : CutSceGeneric
{
    [SerializeField]
    private CheckPointR2 checkPointR2;

    [SerializeField]
    private GameObject edPanel;
    [SerializeField]
    private Endings ed2;
    [SerializeField]
    private string[] sleepDialog;
    [SerializeField]
    private string[] sleepDialog2;
    [SerializeField]
    private string[] getAppleDialog;
    [SerializeField]
    private string[] eatAppleDialog;
    [SerializeField]
    private string[] finishDialog;
    [SerializeField]
    private SignalSender interactSignal;

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
        interactSignal.SendSignal();
        base.StartWhenConditionMet();
    }

    public override void EndPlayHandle()
    {
        checkPointR2.bossPurified = true;
        GameObject EDPanel = Instantiate(edPanel, transform.position, Quaternion.identity);
        EDPanel.GetComponent<EndingPanel>().EndDesc = ed2.description;
        EDPanel.GetComponent<EndingPanel>().EndTitle = ed2.title;
        EDPanel.GetComponent<EndingPanel>().EndSubTitle = ed2.subTitle;
        checkPointR2.endGood = true;
    }


    private void Awake()
    {
        dialogs.Add(sleepDialog);
        dialogs.Add(sleepDialog2);
        dialogs.Add(getAppleDialog);
        dialogs.Add(eatAppleDialog);
        dialogs.Add(finishDialog);
    }
}
