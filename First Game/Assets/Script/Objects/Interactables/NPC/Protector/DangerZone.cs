using UnityEngine;

public class DangerZone : Interactables
{
    public Protector protector;
    public string[] openDialog;
    public GameObject moneyLog;
    [SerializeField]
    private CheckPointR1 checkPointR1;
    [SerializeField]
    private SignalSender regPositionOnCheckPoint;

    private bool startDialog = false;

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PlayerAttack") && !protector.isSignState)
        {
            if (protector.gameObject.activeInHierarchy && !protector.attacking)
            {
                protector.playerInDangerZone = true;
                
                checkPointR1.yellowAttack = true;//reg check point
                regPositionOnCheckPoint.SendSignal();//reg player position for load
            }
            else if (!protector.gameObject.activeInHierarchy)
            {
                dialogBoxState = dialogBox.GetComponent<Dialog>().nInvoked;
                interactStep = 0;
                startDialog = true;
            }
        }

    }

    public override void OnTriggerExit2D(Collider2D other)
    {

    }


    //Mono
    private void Start()
    {
        if (checkPointR1.ReachMoney == true)
        {
            gameObject.SetActive(false);
        }
        else if (checkPointR1.moneyTreeAppear == true)
        {
            GenerateTrees();
            gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (startDialog == true)
        {
            OneTimeDialog(openDialog);
        }
        
    }

    private void OneTimeDialog(string[] dialog)
    {
        
        if (interactStep == 0)
        {
            if (!dialogBox.activeInHierarchy
              && dialogBoxState == dialogBox.GetComponent<Dialog>().nInvoked)//same as start dialog condition
            {
                //player enter interact state
                interactSignal.SendSignal();
            }
            StartDialog(dialog);
            AddStep();
        }
        else if (interactStep == 1 && !dialogBox.activeInHierarchy)
        {
            //player quite interact state
            InteractEnd();
            GenerateTrees();

            checkPointR1.moneyTreeAppear = true;//reg check point
            regPositionOnCheckPoint.SendSignal();//reg player position for load
            gameObject.SetActive(false);
        }
    }

    private void GenerateTrees()
    {
        for (int i = 0; i <= 10; i++)
        {
            Vector2 birthCentroid = new Vector2(-10, 5);
            Vector2 birthRelativePostion = new Vector2(-3 * Mathf.Cos(36 * Mathf.Deg2Rad * i), 3 * Mathf.Sin(36 * Mathf.Deg2Rad * i));
            Debug.Log(birthCentroid + birthRelativePostion);
            Instantiate(moneyLog, birthCentroid + birthRelativePostion, Quaternion.identity);
        }
    }
}
