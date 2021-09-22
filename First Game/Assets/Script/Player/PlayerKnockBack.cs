using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class PlayerKnockBack : GenericKnockBack
{
    //Death UI
    [Header("Death UI")]
    [SerializeField]
    private GameObject deathPanel;
    [SerializeField]
    private float fadeSeconds;
    
    //Invulnerable Frame
    [Header("Invulnerable Frame")]
    public Color hitColor;
    public Color normColor;
    public SpriteRenderer spriteRenderer;
    public float invulnerableDurartionSecond;
    public int flashPerSecond;

    private Collider2D triggerColider;
    private Rigidbody2D myRigidBody;
    private bool isDead = false;

    //Cinemachine Impulse Source
    private CinemachineImpulseSource source;

    private void Start()
    {
        triggerColider = this.GetComponent<BoxCollider2D>();
        myRigidBody = this.GetComponentInParent<Rigidbody2D>();
        source = this.gameObject.GetComponent<CinemachineImpulseSource>();
    }

    //Being Knocked Back
    public void Knock(float knockBackTime, float damage)
    {
        if (!isDead)
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
                //generate screen shake
                source.GenerateImpulse();
                //Turn Off Damage and Flash
                StartCoroutine(InvulnerableFrameCo());
            }
            else
            {
                //this.GetComponentInParent<Player>().Death();
                isDead = true;
                StartCoroutine(DeathCo(fadeSeconds));
            }
        }
       
    }

    private IEnumerator DeathCo(float fadeSeconds)
    {
        Instantiate(deathPanel, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(fadeSeconds);
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("StartMenu");
        while (!asyncOperation.isDone)
        {
            yield return null;
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
