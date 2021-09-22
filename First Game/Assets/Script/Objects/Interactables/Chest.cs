using UnityEngine;

public class Chest : SingleInteract
{
    [Header("Chest Variables")]
    public Item content;
    public Inventory inventory;//pass chest item to inventory new item
    private Animator anim;

    // Start is called before the first frame update
    private void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        //change animation; Mainly for senece change
        anim.SetBool("open", interacted.runtimeValue);
    }

    public override void Update()
    {
        if (playerInRange && !interacted.runtimeValue)
        {
            if (interactStep == 0)
            {
                if (Input.GetButtonDown("Attack"))
                {
                    //open dialog box
                    dialogBox.GetComponent<Dialog>().dialog = content.getDescription;
                    dialogBox.SetActive(true);
                    //change animation
                    anim.SetBool("open", true);
                    //pass chest item to inventory new item
                    inventory.newItem = content;
                }

                AddStep();
            }

            else if (interactStep == 1)//set bool value, exit interact
            {
                disableContentHint.SendSignal();
                interacted.runtimeValue = true;
                //send signal to trigger player get event
                interactSignal.SendSignal();
            }

        }
    }
}
