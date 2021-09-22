using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OptionTrigger : Interactables
{

    public GameObject optionPanel;
    public Options options;
    public string[] dialog1;
    public string[] dialog2;

    private Dialog dialog;

    public void Start()
    {
        dialog = dialogBox.GetComponent<Dialog>();
        dialog.dialogFinish = true;
    }

    // Update is called once per frame
    public override void Update()
    {
        if (playerInRange && Input.GetButtonDown("Attack") && dialog.dialogFinish)
        {
            if (!optionPanel.activeInHierarchy)
            {
                OptionManager optionManager = optionPanel.GetComponent<OptionManager>();
                optionManager.options = options;
                optionManager.currentOptionState = new OptStTest1(optionManager, this.gameObject);
                optionPanel.SetActive(true);
            }
        }
    }

    public void ChoiceActions(int index)
    {
        if (index == 2)
        {
            if (!dialogBox.activeInHierarchy)
            {
                dialogBox.GetComponent<Dialog>().dialog = dialog1;
                dialogBox.SetActive(true);
            }
        }
        else
        {

            if (!dialogBox.activeInHierarchy)
            {
                dialogBox.GetComponent<Dialog>().dialog = dialog2;
                dialogBox.SetActive(true);
            }
    }

}
}
