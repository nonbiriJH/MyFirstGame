using UnityEngine;

public class DialogTrigger : MonoBehaviour
{

    public GameObject dialogBox;
    public string[] nextDialog;
    public int triggerCount = 0;

    // Start is called before the first frame update
    private void OnEnable()
    {
        dialogBox.GetComponent<Dialog>().dialog = nextDialog;
        //only enable the dialog once per activation
        dialogBox.SetActive(true);
        triggerCount += 1;
    }

}
