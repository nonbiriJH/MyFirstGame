using UnityEngine;

public class Imouto : Interactables
{
    public string[] openDialog;

    private string[] nextDialog;
    private Animator animator;
    private Vector2 facingDirection;
    private Transform targetTransform;


    //animation
    public void UpdateWalkAnimParameter(Vector2 direction)
    {
        animator.SetFloat("MoveX", direction.x);
        animator.SetFloat("MoveY", direction.y);
    }

    //dialog
    private void OneTimeDialog(string[] dialog)
    {
        if (interactStep == 0)
        {
            if (Input.GetButtonDown("Attack")) StartDialog(dialog);
            AddStep();
        }
        else if (interactStep == 1
            && !dialogBox.activeInHierarchy)
        {
            //player quite interact state
            InteractEnd();
        }
    }

    //monobehaviour
    private void Start()
    {
        nextDialog = openDialog;
        targetTransform = GameObject.Find("Player").transform;
        animator = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        if (playerInRange)
        {
            facingDirection = targetTransform.position - transform.position;
            UpdateWalkAnimParameter(facingDirection);
            OneTimeDialog(nextDialog);
        }
    }


}
