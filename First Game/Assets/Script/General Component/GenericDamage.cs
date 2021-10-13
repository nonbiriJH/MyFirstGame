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
    public float knockBackTime;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.isTrigger)
        {
            if (other.gameObject.CompareTag("enemy"))
            {
                KnockEnemy(other);
            }
            else if (other.gameObject.CompareTag("enemyBoss"))
            {
                KnockEnemyBoss(other);
            }
            else if (other.gameObject.CompareTag("Player"))
            {
                KnockPlayer(other);
            }
            else if (other.gameObject.CompareTag("breakable"))
            {
                KnockBreakable(other);
            }
            else if (other.gameObject.CompareTag("NPC"))
            {
                KnockNPC(other);
            }
            else if (other.gameObject.CompareTag("Dealer"))
            {
                KnockDealer(other);
            }
            else if (other.GetComponent<GenericKnockBack>())
            {
                KnockRest(other);
            }
        }
    }

    public void ApplyForce(Collider2D other, float thrustAmp)
    {
        Rigidbody2D otherRigidBody = other.GetComponentInParent<Rigidbody2D>();
        if (otherRigidBody != null)
        {
            Vector3 difference = other.transform.position - transform.position;
            difference = difference.normalized * thrust * thrustAmp;
            otherRigidBody.DOMove(otherRigidBody.transform.position + difference, knockBackTime);
            //otherRigidBody.AddForce(difference, ForceMode2D.Impulse);
        }     
    }

    public virtual void KnockEnemy(Collider2D other)
    {
        ApplyForce(other, 1f);
        other.GetComponent<EnemyKnockBack>().Knock(knockBackTime, damage);
    }

    public virtual void KnockEnemyBoss(Collider2D other)
    {
        ApplyForce(other, 0.5f);
        other.GetComponent<EnemyKnockBack>().Knock(knockBackTime, damage);
    }

    public virtual void KnockPlayer(Collider2D other)
    {
        ApplyForce(other, 1f);
        other.GetComponent<PlayerKnockBack>().Knock(knockBackTime, damage);
    }

    public virtual void KnockNPC(Collider2D other)
    {
        ApplyForce(other, 1f);
        NPCKnockBack knockBack = other.GetComponent<NPCKnockBack>();
        if(knockBack != null)
        {
            knockBack.Knock(knockBackTime, damage);
        }
    }

    public virtual void KnockDealer(Collider2D other)
    {
        ApplyForce(other, 1f);
        DealerKnockBack knockBack = other.GetComponent<DealerKnockBack>();
        if (knockBack != null)
        {
            knockBack.Knock(knockBackTime, damage);
        }
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
