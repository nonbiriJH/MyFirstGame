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
    public bool open;
    private Animator anim;
    public Inventory inventory;//pass chest item to inventory new item


    // Start is called before the first frame update
    public override void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        open = false;
    }

    // Update is called once per frame
    public override void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.Space))
        {
            if(!open && !dialogBox.activeInHierarchy)
            {
                OpenChest();
            }
            else
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
        open = true;
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
        if (!open)
        {
            base.OnTriggerEnter2D(other);
        }
    }

    public override void OnTriggerExit2D(Collider2D other)
    {
        if (!open)
        {
            base.OnTriggerExit2D(other);
        }
        
    }
}
