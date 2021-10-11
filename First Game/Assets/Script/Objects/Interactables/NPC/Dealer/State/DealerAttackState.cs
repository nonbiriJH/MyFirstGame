using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealerAttackState : DealerState
{

    private float projectDelaySecond;
    private bool canProject;

    //Constructor, Link a StateMachine instance with a Player instance
    public DealerAttackState(Dealer dealer) : base(dealer)
    {
    }

    public override void BeginState()
    {
        base.BeginState();
        //Turn on trigger for stagger
        dealer.hitZone.SetActive(true);
        //reset project delay
        projectDelaySecond = dealer.projectDelay;
        //Turn on physics
        dealer.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        //Disable Interaction Box
        BoxCollider2D[] collider2Ds = dealer.GetComponents<BoxCollider2D>();
        for (int i = 0; i < collider2Ds.Length; i++)
        {
            if (collider2Ds[i].isTrigger)
            {
                collider2Ds[i].enabled = false;
            }
        }
    }

    public override void UpdateLogics()
    {
        if (dealer.targetWhenAttack.gameObject.activeInHierarchy)
        {
            Vector2 direction = dealer.targetWhenAttack.position - dealer.transform.position;
            float dist = Vector2.Distance(dealer.transform.position, dealer.targetWhenAttack.position);
            //get out of radius
            if (dist <= dealer.attackRadius)
            {
                dealer.MoveObject(-1f * direction);
            }
            else if (dist <= dealer.chaseRadius && dist > dealer.attackRadius)
            {
                //stopmove face player
                dealer.StopObject();
                dealer.UpdateWalkAnimParameter(direction);
                //using arrow

                //measure time between frame and trigger prject bool
                projectDelaySecond -= Time.deltaTime;
                if (projectDelaySecond < 0)
                {
                    canProject = true;
                    projectDelaySecond = dealer.projectDelay;
                }

                if (canProject)
                {
                    dealer.LaunchArrow(direction);
                    canProject = false;
                }
            }
            //chase player
            else
            {
                dealer.MoveObject(direction);
            }

        }

    }

    public override void ExitState()
    {
        dealer.StopObject();
    }
}