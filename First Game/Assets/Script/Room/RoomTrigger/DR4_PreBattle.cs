using UnityEngine;

public class DR4_PreBattle : MonoBehaviour
{
    [SerializeField]
    private BossLog bossLog;
    [SerializeField]
    private Player player;
    [SerializeField]
    private Transform CMTransform;
    public bool played;
    public float journeyTime;

    private bool start;
    private float startTime;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Change Player state
        player.interacted = false;
        player.ChangeState(new PlayerInteractState(player));
        //Change Log State
        bossLog.playerInRange = true;
        bossLog.ChangeState(new LogInteractState(bossLog));

        startTime = Time.time;
        start = true;
    }

    private void Update()
    {
        if (!played && start)
        {
            float distanceToPlayer = Vector3.Distance(CMTransform.position, player.transform.position);
            float distanceToLog = Vector3.Distance(CMTransform.position, bossLog.transform.position);
            //camera move to log
            if(distanceToLog > 0.1 && bossLog.currentState is LogInteractState)
            {
                float journeyFraction = (Time.time - startTime) / journeyTime;
                CMTransform.position = Vector3.Lerp(player.transform.position, bossLog.transform.position, journeyFraction);
                
            }
            //log dialog
            else if (distanceToLog <= 0.1 && bossLog.currentState is LogInteractState)
            {
                bossLog.CMMoveStop = true;
            }
            //log dialog finish
            else if(distanceToLog <= 0.1 && bossLog.currentState is LogIdleState)
            {
                startTime = Time.time;
                bossLog.BecomeEvil();
                this.gameObject.GetComponent<BoxCollider2D>().enabled = false;

            }
            //camera move back
            if (bossLog.isEvil && distanceToPlayer > 0.1)
            {
                float journeyFraction = (Time.time - startTime) / journeyTime;
                CMTransform.position = Vector3.Lerp(bossLog.transform.position, player.transform.position, journeyFraction);

            }
            else if (bossLog.isEvil && distanceToPlayer < 0.1)
            {
                played = true;
                this.gameObject.SetActive(false);
            }
        }
    }
}
