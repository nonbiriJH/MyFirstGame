using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKnockBack : GenericKnockBack
{
    
    //Invulnerable Frame
    [Header("Invulnerable Frame")]
    public Color hitColor;
    public Color normColor;
    public SpriteRenderer spriteRenderer;
    public float invulnerableDurartionSecond;
    public int flashPerSecond;

    private Collider2D triggerColider;
    private Rigidbody2D myRigidBody;

    private void Start()
    {
        triggerColider = this.GetComponent<BoxCollider2D>();
        myRigidBody = this.GetComponentInParent<Rigidbody2D>();
    }

    //Being Knocked Back
    public void Knock(float knockBackTime, float damage)
    {
        Player playerReference = this.GetComponentInParent<Player>();
        playerReference.staggerState.knockBackTime = knockBackTime;
        playerReference.ChangeState(playerReference.staggerState);

        PlayerHealth myHealth = this.gameObject.GetComponent<PlayerHealth>();

        //Take Damage
        if (myHealth)
        {
            myHealth.TakeDamage(damage);
        }

        if (myHealth.Health.runtimeValue > 0)
        {
            //StartCoroutine(KnockCo(knockBackTime));
            //Turn Off Damage and Flash
            StartCoroutine(InvulnerableFrameCo());
        }
        else
        {
            this.GetComponentInParent<Player>().Death();
        }
    }

    //Invulnerable Frame
    private IEnumerator InvulnerableFrameCo()
    {
        //Turn off Damage Trigger
        triggerColider.enabled = false;
        //Flash Duration
        float flashDuration = 1f / (flashPerSecond * 2f);
        //Flashing Animation
        for (int i = 0; i < invulnerableDurartionSecond * flashPerSecond; i++)
        {
            //Change to Hit Color
            spriteRenderer.color = hitColor;
            yield return new WaitForSeconds(flashDuration);
            //Change back Color
            spriteRenderer.color = normColor;
            yield return new WaitForSeconds(flashDuration);
        }
        triggerColider.enabled = true;
    }
}
