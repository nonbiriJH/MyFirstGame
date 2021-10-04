using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class DR2_Preboss : MonoBehaviour
{
    public PlayableDirector playableDirector;
    public bool played;
    public Dealer dealer;
    public Player player;

    [SerializeField]
    private SignalSender enableContentHint;
    [SerializeField]
    private SignalSender disableContentHint;

    [Header("Checkpoint")]
    [SerializeField]
    private CheckPointR1 checkPointR1;
    [SerializeField]
    private SignalSender regPositionOnCheckPoint;

    private bool startPlay = false;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (checkPointR1.redAppear)
        {
            dealer.gameObject.SetActive(true);
            gameObject.SetActive(false);

        }
        else if(other.gameObject.CompareTag("Player") && !played)
        {
            dealer.gameObject.SetActive(true);
            playableDirector.Play();
            player.interacted = false;
            player.ChangeState(new PlayerInteractState(player));
            startPlay = true;
            enableContentHint.SendSignal();
        }
    }

    private void Update()
    {
        if (startPlay)
        {
            Vector3 tempPosition = dealer.transform.position - player.transform.position;
            player.animator.SetFloat("MoveX", tempPosition.x);
            player.animator.SetFloat("MoveY", tempPosition.y);

            if (playableDirector.state != PlayState.Playing)
            {
                player.interacted = false;
                player.ChangeState(player.idleState);
                dealer.PreBossStart();

                played = true;
                startPlay = false;
                disableContentHint.SendSignal();
                checkPointR1.redAppear = true;
                regPositionOnCheckPoint.SendSignal();
            }
        }
    }

}
