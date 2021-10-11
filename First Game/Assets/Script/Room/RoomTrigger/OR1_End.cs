using UnityEngine;

public class OR1_End : TriggerPlayerDialog
{
    [SerializeField]
    private CheckPointR1 checkPointR1;
    [SerializeField]
    private Player player;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        triggered = checkPointR1.revenge;
        if (triggered)
        {
            Destroy(gameObject);
        }
    }

    public override bool ConditionMet()
    {
        return checkPointR1.imoutoDie && !checkPointR1.revenge;
    }


    public override void PreDestroy()
    {
        base.PreDestroy();
        checkPointR1.revenge = triggered;
        player.TurnRed();
    }
}
