using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject inventoryPanel;

    private bool isPause;

    private void Start()
    {
        isPause = false;
    }

    //(Un)Trigger Panel by Pause Key
    private void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            //if pause panal active, close it.
            if (pausePanel.activeInHierarchy)
            {
                pausePanel.SetActive(false);
                isPause = false;
                ChangePauseState();
            }
            //if pause panal is not active, open it.
            else
            {
                //make sure inventory panel is closed
                inventoryPanel.SetActive(false);
                pausePanel.SetActive(true);
                isPause = true;
                ChangePauseState();
            }
        }
        if (Input.GetButtonDown("Inventory"))
        {
            //if inventory panal active, close it.
            if (inventoryPanel.activeInHierarchy)
            {
                inventoryPanel.SetActive(false);
                isPause = false;
                ChangePauseState();
            }
            //if inventory panal is not active, open it.
            else
            {
                //make sure pause panel is closed
                pausePanel.SetActive(false);
                inventoryPanel.SetActive(true);
                isPause = true;
                ChangePauseState();
            }
        }
    }

    public void ChangePauseState()
    {
        if (isPause)
        {
            Time.timeScale = 0f; //Pause Frame
        }
        else
        {
            Time.timeScale = 1f; //Start Frame
        }
    }

    //Click Start Menu in Pause Menu. Assume Already Paused
    public void ToStartMenu()
    {
        isPause = false;
        Time.timeScale = 1f;//Start Frame
        SceneManager.LoadScene("StartMenu");
    }

}
