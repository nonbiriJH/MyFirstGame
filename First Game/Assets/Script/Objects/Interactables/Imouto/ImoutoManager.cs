using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImoutoManager : MonoBehaviour
{
    [SerializeField]
    private CheckPointNoneRoute cp0;
    [SerializeField]
    private CheckPointR1 cp1;

    //Dialog
    [SerializeField]
    private string[] openDialog;
    [SerializeField]
    private string[] sleepDialog;
    [SerializeField]
    private string[] PreBreakfastDialog;
    [SerializeField]
    private string[] breakfastDialog;

    private Imouto imouto;
    private ImoutoBed imoutoBed;


    private void genericStart()
    {
        imouto = GetComponentInChildren<Imouto>();
        imoutoBed = GetComponentInChildren<ImoutoBed>();
    }

    private void checkPointStart()
    {
        if (!cp1.yellowAttack)
        {
            //imouto in bed until openGate
            imouto.gameObject.SetActive(false);
            imoutoBed.gameObject.SetActive(true);
            imoutoBed.IsInbed(true);
            imoutoBed.IsSleepInBed(true);
            if (!cp0.OutHome)
            {
                imoutoBed.nextDialog = openDialog;
                imoutoBed.wakeable = true;
            }
            else
            {
                imoutoBed.wakeable = false;
                imoutoBed.nextDialog = sleepDialog;
            }
        }
        else
        {
            //imouto out bed
            imoutoBed.IsInbed(false);
            imouto.gameObject.SetActive(true);
            imouto.SetPajama(!cp1.openGate);
            if (!cp1.openGate)
            {
                imouto.SetDialog(PreBreakfastDialog, true);
            }
            else
            {
                imouto.SetDialog(breakfastDialog, true);
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        genericStart();
        checkPointStart();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
