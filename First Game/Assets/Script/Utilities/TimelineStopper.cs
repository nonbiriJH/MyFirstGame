using UnityEngine;
using UnityEngine.Playables;


public class TimelineStopper : MonoBehaviour
{
    public PlayableDirector playableDirector;
    public GameObject dialogBox;

    // Start is called before the first frame update
    private void OnEnable()
    {
        //pause if the dialog still running
        if (dialogBox.activeInHierarchy)
        {
            playableDirector.playableGraph.GetRootPlayable(0).SetSpeed(0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //resume until dialog done
        if (!dialogBox.activeInHierarchy)
        {
            playableDirector.playableGraph.GetRootPlayable(0).SetSpeed(1);
        }
    }

}
