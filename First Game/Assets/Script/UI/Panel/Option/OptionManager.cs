using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OptionManager : MonoBehaviour
{
    public Options options;

    [SerializeField]
    private GameObject buttonPrefab;
    [SerializeField]
    private TextMeshProUGUI question;
    [SerializeField]
    private GameObject content;

    //Option State Machine
    public OptionState currentOptionState;


    public void OnEnable()
    {
        ResetQuestion();
        DeleteExistingOption();
        AddOptionsToUI();
    }

    public void ResetQuestion()
    {
        question.text = options.question;
    }

    public void AddOptionsToUI()
    {
        for (int i = 0; i < options.options.Length; i++)
        {
            string option = options.options[i];
            if (option != null)
            {
                //instantiate option holder and cache reference
                GameObject optionHolder = Instantiate(buttonPrefab);
                //Add new option holder to child of content
                optionHolder.transform.SetParent(content.transform);
                optionHolder.transform.localScale = new Vector3(1, 1, 1);
                optionHolder.GetComponent<OptionButtonHolder>().optionManager = this;
                //Setup option holder UI
                optionHolder.GetComponent<OptionButtonHolder>().SetupButtonHolder(option);
            }
        }
    }

    public void DeleteExistingOption()
    {
        for (int i = 0; i < content.transform.childCount; i++)
        {
            Destroy(content.transform.GetChild(i).gameObject);
        }
    }

}
