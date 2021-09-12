using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericHealth : MonoBehaviour
{
    //FloatValue Scriptable to define initial and max health
    //Share by multiple game objects
    public floatValue Health;

    //Each game object has its own runTimeHealth
    public float runTimeHealth;

    public virtual void Start()
    {
        runTimeHealth = Health.initialValue;
    }

    public virtual void TakeDamage(float damage)
    {
        runTimeHealth -= damage;
        if (runTimeHealth < 0)
        {
            runTimeHealth = 0;
        }
    }

    public virtual void Heal (float heal)
    {
        runTimeHealth += heal;
        if (runTimeHealth > Health.maxValue)
        {
            runTimeHealth = Health.maxValue;
        }
    }

    public virtual void DirectDeath()
    {
        runTimeHealth = 0;
    }

    public virtual void MaxHeal()
    {
        runTimeHealth = Health.maxValue;
    }
}
