using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ApplyPowerup();
            Destroy(this.gameObject);
        }
    }

    public virtual void ApplyPowerup()
    {
    }


}
