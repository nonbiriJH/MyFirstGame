using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float speed;
    public Rigidbody2D myRigidbody;

    //set projectile direction and sprite angle
    public void Setup(Vector2 projectDirection, float spriteDegreeToLeft)
    {
        myRigidbody.velocity = projectDirection.normalized * speed;
        transform.rotation = Quaternion.Euler(0, 0, spriteDegreeToLeft);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.isTrigger || other.CompareTag("enemy"))
        {
            Destroy(this.gameObject);
        }
    }
}
