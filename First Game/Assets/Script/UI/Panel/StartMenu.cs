using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject endingMenu;

    public void QuitGame()
    {
        Application.Quit();
    }
    public void EndingMenu(bool isOn)
    {
        endingMenu.SetActive(isOn);
    }
}
