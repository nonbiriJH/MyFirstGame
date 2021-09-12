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
    public Vector2 homePosition;
    [Header("Utilities No Need Asign")]
    public EnemyState currentState;


    public virtual void OnEnable()
    {
        transform.position = homePosition;
        currentState = EnemyState.idle;
    }

}
