using UnityEngine;

public class sign : Interactables
{
    public string[] dialog;

    // Update is called once per frame
    void Update()
    {
        if (playerInRange && Input.GetButtonDown("Attack"))
        {
            if (!dialogBox.activeInHierarchy)
            {
                dialogBox.GetComponent<Dialog>().dialog = dialog;
                dialogBox.SetActive(true);
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
