using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sign : Interactables
{

    public GameObject dialogBox;
    public Text dialogText;
    public string dialog;

    // Update is called once per frame
    public override void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.Space))
        {
            if (dialogBox.activeInHierarchy)
            {
                dialogBox.SetActive(false);
            }
            else
            {
                dialogBox.SetActive(true);
                dialogText.text = dialog;
            }
        }
    }

    public override void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            dialogBox.SetActive(false);
            base.OnTriggerExit2D(other);
        }
    }
}
