using System.Collections;
using UnityEngine;

public class ProtectorDeathState : ProtectorState
{
    private bool animFinish = false;
    //Constructor, Link a StateMachine instance with a Player instance
    public ProtectorDeathState(Protector protector) : base(protector)
    {
    }

    public override IEnumerator BeginStateCo()
    {
        //sync current dialog state
        protector.dialogBoxState = protector.dialogBox.GetComponent<Dialog>().nInvoked;
        protector.interactStep = 0;
        //player enter interact state
        protector.interactSignal.SendSignal();

        protector.animator.SetBool("Death", true);
        yield return new WaitForSeconds(2);//wait for animation finishes
        animFinish = true;

    }

    public override void UpdateLogics()
    {
        if (animFinish)
        {
            OneTimeDialog(protector.deathDialog);
        }

    }

    private void OneTimeDialog(string[] dialog)
    {
        if (protector.interactStep == 0)
        {

            protector.StartDialog(dialog);
            protector.AddStep();
        }
        else if (protector.interactStep == 1 && !protector.dialogBox.activeInHierarchy)
        {
            //player quite interact state
            protector.InteractEnd();

            protector.checkPointRoute1.yellowDie = true;//reg check point
            protector.regPositionOnCheckPoint.SendSignal();//reg player position for load

            protector.gameObject.SetActive(false);
        }
    }
}

