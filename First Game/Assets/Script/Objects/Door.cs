using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DoorType
{
    KeyedDoor,
    EnemyDoor,
    ButtonDoor
        
}

public class Door : Interactables
{
    [Header("Door Variable")]
    //The door type
    public DoorType doorType;
    //Is open
    public bool isOpen = false;
    //Close Sprite
    public Sprite closeSprite;
    //Open Sprite
    public Sprite openSprite;

    [Header("Utility Variable")]
    //inventory for get key to open
    public Inventory inventory;
    //control sprite for door open
    public SpriteRenderer spriteRenderer;
    //disable collider when open
    public BoxCollider2D physicCollide;

    public override void Start()
    {
        if (!isOpen)
        {
            spriteRenderer.sprite = closeSprite;
        }
        else
        {
            spriteRenderer.sprite = openSprite;
        }
        
        base.Start();
    }

    public override void Update()
    {
        if (playerInRange && Input.GetButtonDown("Attack"))
        {
            if (!isOpen)
            {
                OpenDoorByType();
            }
        }
        base.Update();
    }

    //open door method by door type
    public void OpenDoorByType()
    {
        if (doorType == DoorType.KeyedDoor)
        {
            if (playerInRange && inventory.numKey > 0)
            {
                inventory.numKey--;
                OpenDoor();
            }
        }
    }

    //door open generic
    public void OpenDoor()
    {
        //change openbool
        isOpen = true;
        //change sprite to open
        spriteRenderer.sprite = openSprite;
        //diable collider
        physicCollide.enabled = false;
        //diable content hint
        disableContentHint.SendSignal();
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (!isOpen)
        {
            base.OnTriggerEnter2D(other);
        }
    }

    public override void OnTriggerExit2D(Collider2D other)
    {
        if (!isOpen)
        {
            base.OnTriggerExit2D(other);

        }

    }
}
