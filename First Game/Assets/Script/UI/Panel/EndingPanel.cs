using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class EndingPanel : MonoBehaviour
{
    [SerializeField]
    private float startWaitSec;
    public string EndDesc;
    public string EndTitle;
    public string EndSubTitle;
    [SerializeField]
    private float typeSpeed;

    [Header("UI")]
    public TextMeshProUGUI DescDisplay;
    public TextMeshProUGUI TitleDisplay;
    public TextMeshProUGUI SubTitleDisplay;


    private bool finishedType = false;
    private int step;

    private void Start()
    {
        DescDisplay.text = "";
        TitleDisplay.text = "";
        SubTitleDisplay.text = "";
        finishedType = false;
        //wait for animation
        StartCoroutine(StartCo());
        finishedType = true;
        step = 0;
    }

    private IEnumerator StartCo()
    {
        yield return new WaitForSeconds(startWaitSec);
    }

    // Update is called once per frame
    void Update()
    {
        if (finishedType == true && Input.GetButtonDown("Attack"))
        {
            if (step == 0)
            {
                StartCoroutine(TypeCo(EndDesc, DescDisplay));
                step++;
            }
            else if (step == 1)
            {
                StartCoroutine(TypeCo(EndTitle, TitleDisplay));
                step++;
            }
            else if (step == 2)
            {
                StartCoroutine(TypeCo(EndSubTitle, SubTitleDisplay));
                step++;
            }
            else
            {

                SceneManager.LoadSceneAsync("StartMenu");
            }
        }

    }

    private IEnumerator TypeCo(string sentence, TextMeshProUGUI textDisplay)
    {
        finishedType = false;
        foreach (char letter in sentence.ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typeSpeed);
        }
        finishedType = true;
    }
}
