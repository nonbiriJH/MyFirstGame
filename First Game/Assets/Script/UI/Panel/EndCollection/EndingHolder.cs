using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndingHolder : MonoBehaviour
{
    [Header("Private Variables")]
    //Internal but need to assign from the inspector
    [SerializeField] private Image endingImage;
    [SerializeField] private TextMeshProUGUI subtitle;
    [SerializeField] private ChosenEnding chosenEnding;
    [SerializeField] private Endings thisEnding;
    [SerializeField] private SignalSender endingChosenSignal;

    //Pass Item information to Item Holder UI
    public void SetupEndingHolder(Endings endings)
    {
        thisEnding = endings;
        endingImage.sprite = endings.sprite;
        subtitle.text = endings.subTitle;
    }

    //Pass UI information to inventory
    public void OnClick()
    {
        chosenEnding.chosenEnding = thisEnding;
        endingChosenSignal.SendSignal();
    }
}
