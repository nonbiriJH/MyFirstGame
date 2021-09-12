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

    private bool finishedType = true;
    private int sentenceIndex;

    //when the game object is activated
    void OnEnable()
    {
        finishedType = true;
        sentenceIndex = 0;

        //type first line
        Type(sentenceIndex);
        sentenceIndex++;
    }


    // Update is called once per frame
    void Update()
    {
        if(finishedType == true && Input.GetButtonDown("Attack"))
        {
            Debug.Log(finishedType);
            if(sentenceIndex < dialog.Length)
            {
                Type(sentenceIndex);
                sentenceIndex++;
            }
            else
            {
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
