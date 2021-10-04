using UnityEngine;

public class OR1_Start : TriggerPlayerDialog
{
    [SerializeField]
    private CheckPointNoneRoute checkPointNoneRoute;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        triggered = checkPointNoneRoute.OutHome;
        if (triggered)
        {
            Destroy(gameObject);
        }
    }
    public override void PreDestroy()
    {
        base.PreDestroy();
        checkPointNoneRoute.OutHome = triggered;
    }
}
