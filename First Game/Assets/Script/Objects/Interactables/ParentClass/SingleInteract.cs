using UnityEngine;

public class SingleInteract : Interactables
{
    [Header("One Time Interactable Variable")]
    public BoolValue interacted;

    // Update is called once per frame
    void Update()
    {
        if (playerInRange
            && Input.GetButtonDown("Attack")
            && !interacted.runtimeValue)
        {
            interacted.runtimeValue = true;
            disableContentHint.SendSignal();
        }
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (!interacted.runtimeValue)
        {
            //if not interacted, trun on playerInRage and send contentHint signal.
            base.OnTriggerEnter2D(other);
        }
        else
        {
            playerInRange = false;
        }
    }

    public override void OnTriggerExit2D(Collider2D other)
    {
        if (!interacted.runtimeValue)
        {
            base.OnTriggerExit2D(other);
        }
        else
        {
            playerInRange = false;
        }

    }
}
