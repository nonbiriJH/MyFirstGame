using UnityEngine;

public class KeyDoor : Door
{
    [Header("Key Door Variable")]
    //Required key
    public Item requiredKey;
    public Options options;
    public GameObject optionPanel;

    //open door method by door type
    public override void InteractApply()
    {
        //Stop Door Update
        interactStep = -1;
        //pop up options
        if (!optionPanel.activeInHierarchy)
        {
            OptionManager optionManager = optionPanel.GetComponent<OptionManager>();
            optionManager.options = options;
            optionManager.currentOptionState = new DoorOption(optionManager, this.gameObject);
            optionPanel.SetActive(true);
        }
    }

    public void ChooseAction(int index)
    {
        //Resume Door Update
        interactStep = 2;
        if (index == 0)
        {
            //use key
            if (requiredKey.itemNumber > 0)
            {
                requiredKey.ReduceAmount();
                success = true;
                disableContentHint.SendSignal();
            }
        }
        else
        {
            return;
        }
    }
}
