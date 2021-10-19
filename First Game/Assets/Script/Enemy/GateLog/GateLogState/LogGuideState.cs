using UnityEngine;

public class LogGuideState : GateLogState
{

    private int pathIndex;
    private float tolerance;
    private Vector2 source;
    private Vector2 target;

    public LogGuideState(GateLog gateLog) : base(gateLog)
    {
    }

    public override void BeginState()
    {
        pathIndex = 0;
        tolerance = 0.1f;
        gateLog.animator.SetBool("Walk", true);
        enableTrigger(false);
        
    }

    public override void UpdateLogics()
    {
        if (pathIndex < gateLog.guidePath.Length)
        {
            source = new Vector2(gateLog.transform.position.x, gateLog.transform.position.y);
            target = gateLog.guidePath[pathIndex];
            float dist = Vector2.Distance(source, target);
            if (dist > tolerance) GuideMove(target, source);
            else pathIndex++;
        }
        else
        {
            gateLog.UpdateWalkAnimParameter(Vector2.down);//change walk ani
            enableTrigger(true);
            gateLog.checkPointR2.gateLogR1Move = true;
            gateLog.ChangeState(new GateLogIdleState(gateLog));
        }
    }

    private void enableTrigger(bool isEnable)
    {
        BoxCollider2D[] boxCollider2Ds = gateLog.GetComponents<BoxCollider2D>();
        for (int i = 1; i < boxCollider2Ds.Length; i++)
        {
            if (boxCollider2Ds[i].isTrigger)
            {
                boxCollider2Ds[i].enabled = isEnable;
            }
        }
    }

    private void GuideMove(Vector2 target, Vector2 source)
    {
        Vector2 tempPosition = target - source;
        gateLog.UpdateWalkAnimParameter(tempPosition);//change walk ani
        gateLog.MoveObject(tempPosition);//make displacement
    }

}