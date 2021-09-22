using UnityEngine;

public class Door : Single3StepInteract
{
    [Header("Door Variable")]
    //Close Sprite
    public Sprite closeSprite;
    //Open Sprite
    public Sprite openSprite;

    [Header("Utility Variable")]
    //control sprite for door open
    public SpriteRenderer spriteRenderer;
    //disable collider when open
    public BoxCollider2D physicCollide;


    public virtual void Start()
    {
        if (!interacted.runtimeValue)
        {
            spriteRenderer.sprite = closeSprite;
        }
        else
        {
            spriteRenderer.sprite = openSprite;
            //diable collider
            physicCollide.enabled = false;
        }
    }

    public override void InteractSuccess()
    {
        base.InteractSuccess();
        if (dialogBox.GetComponent<Dialog>().dialogFinish)
        {
            //change sprite to open
            spriteRenderer.sprite = openSprite;
            //diable collider
            physicCollide.enabled = false;
        }
    }
}
