using UnityEngine;

public class EventDoor : MonoBehaviour
{
    [SerializeField]
    private BoxCollider2D boxCollider2D;
    [SerializeField]
    private CheckPointR2 checkPointR2;
    [SerializeField]
    private Sprite openSprite;
    [SerializeField]
    private Sprite closeSprite;

    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        if (checkPointR2.gateLogR2LOpenGate)
        {
            boxCollider2D.enabled = false;
            spriteRenderer.sprite = openSprite;
        }
        else
        {
            boxCollider2D.enabled = true;
            spriteRenderer.sprite = closeSprite;
        }

    }
}
