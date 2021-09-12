using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chest : Interactables
{
    public Item content;
    public GameObject dialogBox;
    public Text dialogText;
    public SignalSender GetItemSignal;
    public BoolValue open;
    private Animator anim;
    public Inventory inventory;//pass chest item to inventory new item


    // Start is called before the first frame update
    public override void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        //change animation; Mainly for senece change
        anim.SetBool("open", open.runtimeValue);
    }

    // Update is called once per frame
    public override void Update()
    {
        if (playerInRange && Input.GetButtonDown("Attack"))
        {
            if(!open.runtimeValue && !dialogBox.activeInHierarchy)
            {
                OpenChest();
            }
            else if (open.runtimeValue && dialogBox.activeInHierarchy)
            {
                StopChestInteraction();
            }
        }
    }

    public void OpenChest()
    {
        //open dialog box
        dialogBox.SetActive(true);
        dialogText.text = content.itemDescription;
        //change bool
        open.runtimeValue = true;
        //change animation
        anim.SetBool("open", true);
        //pass chest item to inventory new item
        inventory.newItem = content;
        //send signal to trigger player get/notGet event
        GetItemSignal.SendSignal();

        //disable content hint
        disableContentHint.SendSignal();
    }

    public void StopChestInteraction()
    {
        //close dialogue
        if (dialogBox.activeInHierarchy)
        {
            dialogBox.SetActive(false);
            //send signal to trigger player get/notGet event
            GetItemSignal.SendSignal();
        }
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (!open.runtimeValue)
        {
            base.OnTriggerEnter2D(other);
        }
        else
        {
            playerInRange = false;
        }
    }

    public override void OnTriggerExit2D(Collider2D other)
    {
        if (!open.runtimeValue)
        {
            base.OnTriggerExit2D(other);
        }
        else
        {
            playerInRange = false;
        }
        
    }
}
