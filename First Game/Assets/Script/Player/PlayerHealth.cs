using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Player internalises runtimeHealth to floatValue and send signal to Health UI
public class PlayerHealth : GenericHealth
{
    [SerializeField] private SignalSender healthSignal;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        Health.runtimeValue = runTimeHealth;
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        Health.runtimeValue = runTimeHealth;
        healthSignal.SendSignal();//send signal to reduce heart UI.
    }


}
