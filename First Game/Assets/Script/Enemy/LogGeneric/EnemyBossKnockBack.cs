using UnityEngine;

public class EnemyBossKnockBack : EnemyKnockBack
{
    [SerializeField]
    private CheckPointR2 checkPointR2;
    [SerializeField]
    private Player player;
    [SerializeField]
    private SignalSender interactSignal;
    [SerializeField]
    private Vector2 playerActorPosition;
    public Vector2 targetPosition;

    private bool startPure= false;
    private bool playerInActor = false;


    public void Purify(float knockBackTime)
    {
        if(startPure == false)
        {
            startPure = true;
            interactSignal.SendSignal();
            this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            logRef.ChangeState(new LogBossPureState(logRef, knockBackTime, targetPosition, checkPointR2));
        }
        
    }

    private void Update()
    {
        if(checkPointR2.boosPrePurified && !checkPointR2.bossPurifyStart)
        {
            //Move Player to Actor Place when prepurify is done
            if (!playerInActor) MovePlayerToActorPosition();
            else checkPointR2.bossPurifyStart = true;
        }
    }


    private void MovePlayerToActorPosition()
    {
        float distancePlayerToActor = Vector3.Distance(player.transform.position, playerActorPosition);
        if (distancePlayerToActor > 0.1)
        {
            if (!player.animator.GetBool("Walking")) player.animator.SetBool("Walking", true);
            Vector2 movePosition = playerActorPosition - new Vector2(player.transform.position.x, player.transform.position.y);
            player.MoveObject(movePosition, 0.2f);
            player.SetFacingAnim(movePosition);
        }
        else
        {
            player.animator.SetBool("Walking", false);
            player.MoveObject(Vector2.zero);
            player.SetFacingAnim(Vector2.up);
            playerInActor = true;
        }
    }

}
