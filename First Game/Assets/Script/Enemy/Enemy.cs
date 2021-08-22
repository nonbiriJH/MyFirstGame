using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    idle,
    walk,
    attack,
    stagger
};

public class Enemy : MonoBehaviour
{
    public floatValue totalHealth;
    private float health;
    public float speed;
    public string enemyName;
    public float baseAttack;
    public EnemyState currentState;
    public GameObject deadAnimation;

    private void Awake()//run before start
    {
        health = totalHealth.initialValue;
        gameObject.GetComponent<knockBack>().damage = baseAttack;
    }

    private void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            this.gameObject.SetActive(false);
            Instantiate(deadAnimation, transform.position, Quaternion.identity);
        }
    }

    public void Knock(Rigidbody2D enemy, float knockBackTime, float damage)
    {
        StartCoroutine(KnockCo(enemy, knockBackTime));
        TakeDamage(damage);
    }


    private IEnumerator KnockCo(Rigidbody2D enemy, float knockBackTime)
    {
        if (enemy != null)
        {
            yield return new WaitForSeconds(knockBackTime);
            enemy.velocity = Vector2.zero;
            enemy.GetComponent<Enemy>().currentState = EnemyState.idle;
        }

    }
}
