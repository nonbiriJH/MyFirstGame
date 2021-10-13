using UnityEngine;
using UnityEngine.Playables;

public class CutsceneManager : MonoBehaviour
{
    public PlayableDirector playableDirector;
    public CheckPointNoneRoute checkPointNoneRoute;
    public string[] openDialog;

    [SerializeField]
    private GameObject dialogBox;
    [SerializeField]
    private Player player;
    [SerializeField]
    private SignalSender interactSignal;
    [SerializeField]
    private Transform cameramFollowTransform;
    [SerializeField]
    private ImoutoBed imoutoBed;



    // Start is called before the first frame update
    void Start()
    {
        if (checkPointNoneRoute.Open == false)
        {
            dialogBox.GetComponent<Dialog>().dialog = openDialog;
            interactSignal.SendSignal();
            playableDirector.Play();
            //set facing direction when timeline ends.
            player.animator.SetFloat("MoveX", 0);
            player.animator.SetFloat("MoveY", 1);
        }
    }

    private void Update()
    {
        if (playableDirector.state == PlayState.Playing
            && cameramFollowTransform.position == new Vector3(-7, 0))
        {
            imoutoBed.IsSleepInBed(false);
        }

        if(playableDirector.state != PlayState.Playing
            && !checkPointNoneRoute.Open
            && !dialogBox.activeInHierarchy)
        {
            interactSignal.SendSignal();
            checkPointNoneRoute.Open = true;
            cameramFollowTransform.position = new Vector3(-7, -2);
        }
    }
}
