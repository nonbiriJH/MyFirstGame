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

    private bool startPlay = false;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player") && !played)
        {
            dealer.gameObject.SetActive(true);
            dealer.shopOnly = true;
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
            {  player.interacted = false;
                player.ChangeState(player.idleState);
                played = true;
                startPlay = false;
                disableContentHint.SendSignal();
            }
        }
    }

}
