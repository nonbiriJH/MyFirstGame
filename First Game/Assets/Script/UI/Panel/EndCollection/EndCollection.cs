using UnityEngine;
using TMPro;

public class EndCollection : MonoBehaviour
{
    [SerializeField]
    private EndingList endingList;

    [Header("Private Variables")]
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
        for (int i = 0; i < endingList.endings.Count; i++)
        {
            Endings endings = endingList.endings[i];
            if (endings.attained)
            {
                //instantiate ending holder and cache reference
                GameObject endingHolder = Instantiate(endingHolderPrefab);
                //Add new ending holder to child of content
                endingHolder.transform.SetParent(panel.transform);
                //Setup item holder UI
                endingHolder.GetComponent<EndingHolder>().SetupEndingHolder(endings);
                //Without clicking, No Description
                description.text = "";
            }
        }

        if (panel.transform.childCount == 0)
        {
            description.text = "No endings to display.";
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
        Endings endings = endingList.chosenEnding;
        if (endings)
        {
            //Description
            description.text = endings.description;
        }
    }
}
