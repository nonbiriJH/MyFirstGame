using UnityEngine;

public class ImoutoWalkState : ImoutoState
{
    private float roundingError = 0.1f;

    public ImoutoWalkState(Imouto imouto) : base(imouto)
    {
    }

    public override void BeginState()
    {
        //move to patrol target
        Vector2 direction = imouto.targetPosition
            - new Vector2(imouto.transform.position.x, imouto.transform.position.y);
        imouto.MoveObject(direction);
        CheckDistance();

    }

    public override void UpdateLogics()
    {
        if (imouto.playerInRange)
        {
            imouto.ChangeState(new ImoutoInteractState(imouto));
        }
        else
        {
            CheckDistance();
        }
    }

    private void CheckDistance()
    {
        float petrolTargetDistance = Vector3.Distance(imouto.targetPosition, imouto.transform.position);
        if (petrolTargetDistance < roundingError)
        {
            imouto.ChangeTarget();
            imouto.ChangeState(new ImoutoIdleState(imouto));
        }
    }


}