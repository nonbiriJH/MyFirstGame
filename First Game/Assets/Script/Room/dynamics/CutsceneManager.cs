using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CutsceneManager : MonoBehaviour
{
    public PlayableDirector playableDirector;
    public BoolValue boolValue;
    public string[] openDialog;

    [SerializeField]
    private GameObject dialogBox;
    [SerializeField]
    private Player player;



    // Start is called before the first frame update
    void Awake()
    {
        if (boolValue.runtimeValue == false)
        {
            dialogBox.GetComponent<Dialog>().dialog = openDialog;
            playableDirector.Play();
        }
    }

    private void Update()
    {

        if(playableDirector.state != PlayState.Playing && boolValue.runtimeValue == false)
        {
            player.animator.SetFloat("MoveX", -1);
            player.animator.SetFloat("MoveY", 0);
            boolValue.runtimeValue = true;
        }
    }
}
