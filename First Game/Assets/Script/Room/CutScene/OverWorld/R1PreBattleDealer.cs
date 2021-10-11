using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class R1PreBattleDealer : CutSceGeneric
{
    public CheckPointR1 checkPointR1;
    public string[] singleDialog;
    private bool playerInRage;
    [SerializeField]
    private SignalSender interactSignal;
    [SerializeField]
    private Dealer dealer;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(checkPointR1.revenge && !checkPointR1.preBattleDealer)
        {
            playerInRage = true;
            Debug.Log(PlayCondition());
        }
    }

    public override bool PlayCondition()
    {
        return checkPointR1.revenge && !checkPointR1.preBattleDealer && playerInRage;
    }


    public override void EndPlayHandle()
    {
        Debug.Log(actors[0].transform.position);
        Debug.Log(replacedGameObjects[0].transform.position);
        SyncObjectPosition(actors[0], replacedGameObjects[0]);
        Debug.Log(actors[0].transform.position);
        Debug.Log(replacedGameObjects[0].transform.position);
        dealer.R1EndStart();
        checkPointR1.preBattleDealer = true;
        interactSignal.SendSignal();
    }

    public override void StartHandle()
    {
        interactSignal.SendSignal();
    }


    private void Awake()
    {
        playerInRage = false;
        dialogs.Add(singleDialog);
    }
}
