using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Rigidbody2D myRigidBody;
    [SerializeField] private float speed;

    public virtual void MoveObject(Vector2 direction)
    {
        direction.Normalize();
        myRigidBody.velocity = direction * speed;
    }
}
