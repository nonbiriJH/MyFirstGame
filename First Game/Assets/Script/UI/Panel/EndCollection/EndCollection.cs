using UnityEngine;
using TMPro;

public class EndCollection : MonoBehaviour
{

    public ChosenEnding chosenEnding;

    [Header("Private Variables")]
    [SerializeField]
    private Endings ed1;
    [SerializeField]
    private CheckPointR1 checkPointR1;
    [SerializeField] private GameObject endingHolderPrefab;
    [SerializeField] private GameObject panel;
    [SerializeField] private TextMeshProUGUI description;

    public void OnEnable()
    {
        DeleteExistingItem();
        AddEndingsToEndingCollector();
    }


    public void AddEndingsToEndingCollector()
    {
        DisplayEnding(checkPointR1.endBad, ed1);
        //Without clicking, No Description
        description.text = "";

        if (panel.transform.childCount == 0)
        {
            description.text = "No endings to display.";
        }
        
    }

    private void DisplayEnding(bool canDisplay, Endings endings)
    {
        if (canDisplay)
        {
            //instantiate ending holder and cache reference
            GameObject endingHolder = Instantiate(endingHolderPrefab);
            //Add new ending holder to child of content
            endingHolder.transform.SetParent(panel.transform);
            //Setup item holder UI
            endingHolder.GetComponent<EndingHolder>().SetupEndingHolder(endings);
        }
    }

    public void DeleteExistingItem()
    {
        for (int i = 0; i < panel.transform.childCount; i++)
        {
            Destroy(panel.transform.GetChild(i).gameObject);
        }
    }

    public void OnEndingChosenSignal()
    {
        if (chosenEnding.chosenEnding)
        {
            //Description
            description.text = chosenEnding.chosenEnding.description;
        }
    }
}
