using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

//Object applying damage should have a Collider2D Trigger
[RequireComponent(typeof(Collider2D))]

public class GenericDamage : MonoBehaviour
{

    public float damage;

    [Header("KnockBack")]
    [SerializeField] private float thrust;
    [SerializeField] private float knockBackTime;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.isTrigger)
        {       
            if (other.gameObject.CompareTag("enemy"))
            {
                KnockEnemy(other);
            }
            else if (other.gameObject.CompareTag("Player"))
            {
                KnockPlayer(other);
            }
            else if (other.gameObject.CompareTag("breakable"))
            {
                KnockBreakable(other);
            }
            else if (other.GetComponent<GenericKnockBack>())
            {
                KnockRest(other);
            }
        }
    }

    public void ApplyForce(Collider2D other)
    {
        Rigidbody2D otherRigidBody = other.GetComponentInParent<Rigidbody2D>();
        if (otherRigidBody != null)
        {
            Vector3 difference = other.transform.position - transform.position;
            difference = difference.normalized * thrust;
            otherRigidBody.DOMove(otherRigidBody.transform.position + difference, knockBackTime);
            //otherRigidBody.AddForce(difference, ForceMode2D.Impulse);
        }     
    }

    public virtual void KnockEnemy(Collider2D other)
    {
        ApplyForce(other);
        other.GetComponent<EnemyKnockBack>().Knock(knockBackTime, damage);
    }

    public virtual void KnockPlayer(Collider2D other)
    {
        ApplyForce(other);
        other.GetComponent<PlayerKnockBack>().Knock(knockBackTime, damage);
    }

    public virtual void KnockBreakable(Collider2D other)
    {
        other.GetComponent<pot>().smash();
    }

    public virtual void KnockRest(Collider2D other)
    {
        if (other.GetComponent<GenericKnockBack>())
        {
            other.GetComponent<GenericKnockBack>().Knock();
        }
    }


}
