using UnityEngine;
using System.Collections;

public class LogBossPureState : LogState
{
    private float knockBackTime;
    private Vector2 targetPosition;
    private CheckPointR2 checkPointR2;

    public LogBossPureState(Log log, float knockBackTime, Vector2 targetPosition, CheckPointR2 checkPointR2) : base(log)
    {
        this.knockBackTime = knockBackTime;
        this.targetPosition = targetPosition;
        this.checkPointR2 = checkPointR2;
    }

    public override IEnumerator BeginStateCo()
    {
        log.MoveObject(Vector2.zero);
        log.animator.SetBool("Walk", false);
        yield return new WaitForSeconds(knockBackTime);
        log.animator.SetBool("Walk", true);
    }

    public override void UpdateLogics()
    {
        if (!checkPointR2.boosPrePurified)
        {
            Vector2 currentPosition = new Vector2(log.transform.position.x, log.transform.position.y);
            float distDiff = Vector2.Distance(currentPosition, targetPosition);
            
            if (distDiff > 0.1f)
            {
                Vector2 tempPosition = targetPosition - currentPosition;
                log.MoveObject(tempPosition);
            }
            else
            {
                log.MoveObject(Vector2.zero);
                log.animator.SetBool("Walk", false);
                checkPointR2.boosPrePurified = true;
            }

        }
    }

}