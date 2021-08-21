using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTest : MonoBehaviour
{
    public float speed;
    private Rigidbody2D myRigidBody;
    private Vector3 change;
 
    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        change = Vector3.zero;
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");
        updateAnimationAndMove();

    }


    void updateAnimationAndMove()
    {
        if (change != Vector3.zero)
        {
            movePlayer();
        }
    }

    void movePlayer()
    {
        change.Normalize();
        myRigidBody.MovePosition(
            transform.position + speed * Time.deltaTime * change
            );
    }
}
