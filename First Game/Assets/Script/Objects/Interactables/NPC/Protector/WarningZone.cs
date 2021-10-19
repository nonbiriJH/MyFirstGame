using UnityEngine;

public class WarningZone : MonoBehaviour
{
    public Protector protector;
    private float nextGapSecond = 5f;
    private float nextGapSecondCountDown = 0f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && nextGapSecondCountDown <= 0 && !protector.checkPointR2.helpYellow)
        {
            protector.playerInWarningZone = true;
            nextGapSecondCountDown = nextGapSecond;
        }
    }


    private void Update()
    {
        if (nextGapSecondCountDown > 0)
        {
            nextGapSecondCountDown -= Time.deltaTime;
        }
    }
}
