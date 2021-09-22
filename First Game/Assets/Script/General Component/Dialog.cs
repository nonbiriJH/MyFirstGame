using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialog : MonoBehaviour
{
    public string[] dialog;
    public float typeSpeed;

    [Header("UI")]
    public TextMeshProUGUI textDisplay;

    [Header("Signal")]
    public SignalSender dialogFinishSignal;
    public bool dialogFinish = true;
    public int nInvoked = 0;//differentiate dialogBox Disable states.

    private bool finishedType = true;
    private int sentenceIndex;

    //when the game object is activated
    void OnEnable()
    {
        finishedType = true;
        sentenceIndex = 0;
        dialogFinish = false;//when dialogbox enabled dialog starts
        nInvoked += 1;

        //type first line
        Type(sentenceIndex);
        sentenceIndex++;
    }


    // Update is called once per frame
    void Update()
    {
        if(finishedType == true && Input.GetButtonDown("Attack"))
        {
            if(sentenceIndex < dialog.Length)
            {
                Type(sentenceIndex);
                sentenceIndex++;
            }
            else
            {
                dialogFinish = true;
                //dialogFinishSignal.SendSignal();
                this.gameObject.SetActive(false);
            }
        }
        
    }

    private void Type(int index)
    {
        string comingSentence = dialog[index];
        if (comingSentence != "")
        {
            StartCoroutine(TypeCo(comingSentence));
        }
    }

    private IEnumerator TypeCo(string sentence)
    {
        finishedType = false;
        textDisplay.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typeSpeed);
        }
        finishedType = true;
    }
}
