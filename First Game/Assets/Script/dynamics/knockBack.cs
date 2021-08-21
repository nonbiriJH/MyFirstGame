using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class knockBack : MonoBehaviour
{
    public float thrust;
    public float knockBackTime;
    public float damage;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("breakable") && this.gameObject.CompareTag("PlayerAttack"))
        {
            other.GetComponent<pot>().smash();
        }

        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("enemy"))
        {
            Rigidbody2D hit = other.GetComponent<Rigidbody2D>();
            if (hit != null)
            {
                Vector2 difference = other.transform.position - transform.position;
                difference = difference.normalized * thrust;
                hit.AddForce(difference, ForceMode2D.Impulse);

                if (other.gameObject.CompareTag("enemy") && other.isTrigger) //avoid interact with non trigger collider
                {
                    hit.GetComponent<Enemy>().currentState = EnemyState.stagger;
                    hit.GetComponent<Enemy>().Knock(hit, knockBackTime, damage);
                }

                if (other.gameObject.CompareTag("Player") && other.isTrigger
                    && other.gameObject.GetComponent<PlayerMovement>().currentState != PlayerState.stagger)
                {
                    hit.GetComponent<PlayerMovement>().currentState = PlayerState.stagger;
                    hit.GetComponent<PlayerMovement>().Knock(knockBackTime, damage);
                }

            }
        }
    }
}
