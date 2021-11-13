using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject endingMenu;
    private SoundManager soundManager;

    private void Start()
    {
        soundManager = (SoundManager)FindObjectOfType(typeof(SoundManager));
    }

    public void QuitGame()
    {
        soundManager.PlaySound("Click");
        Application.Quit();
    }
    public void EndingMenu(bool isOn)
    {
        soundManager.PlaySound("Click");
        endingMenu.SetActive(isOn);
    }
}
