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
    [Header("Enemy Base Attributes")]
    public floatValue totalHealth;
    private float health;
    public float speed;
    public string enemyName;
    public float baseAttack;

    [Header("Enemy Death")]
    public GameObject deadAnimation;
    public LootTable lootTable;

    [Header("Utilities")]
    public EnemyState currentState;
    public Vector2 homePosition;


    private void Awake()//run before start
    {
        health = totalHealth.initialValue;
        gameObject.GetComponent<knockBack>().damage = baseAttack;
    }

    public virtual void OnEnable()
    {
        transform.position = homePosition;
        health = totalHealth.initialValue;
        currentState = EnemyState.idle;
    }

    private void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            DropLoot();
            DeathEffect();
            this.gameObject.SetActive(false);
            
        }
    }

    public void DeathEffect()
    {
        if(deadAnimation != null)
        {
            GameObject effect = Instantiate(deadAnimation, transform.position, Quaternion.identity);
            Destroy(effect, 1f);
        }
        
    }

    public void DropLoot()
    {
        if (lootTable != null)
        {
            PowerUp newLoot = lootTable.ChooseLoot();
            if (newLoot != null)
            {
                Instantiate(newLoot, transform.position, Quaternion.identity);
            }
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
