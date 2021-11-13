using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OptionButtonHolder : MonoBehaviour
{
    public OptionManager optionManager;//Communicate to Option Manager

    [Header("Private Variables")]
    //Internal but need to assign from the inspector
    [SerializeField] private TextMeshProUGUI buttonText;
    private SoundManager soundManager;

    private void Start()
    {
        soundManager = (SoundManager)FindObjectOfType(typeof(SoundManager));
    }

    //Pass Item information to Item Holder UI
    public void SetupButtonHolder(string singleOption)
    {
        if (singleOption != null)
        {
            buttonText.text = singleOption;
        }
    }

    //Pass UI information to inventory
    public void OnClick()
    {
        soundManager.PlaySound("Click");
        Options newOption = optionManager.options;
        newOption.AsignSelectedInx(buttonText.text);
        optionManager.currentOptionState.OnClick(newOption.selectedIndex);
        optionManager.gameObject.SetActive(false);
    }
}
