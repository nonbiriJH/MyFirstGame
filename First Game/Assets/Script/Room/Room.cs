using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Room : MonoBehaviour
{
    [Header("Components Whose Game Object will be (De)Activated")]
    public List<Log> enemies;
    public pot[] pots;

    [Header("Virtual Camera")]
    public GameObject virtualCamera;


    [Header("Room Move Text")]
    public bool needText;
    public string placeName;
    public GameObject text;
    public Text placeText;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && !other.isTrigger)
        {
            //Activate Game Objects
            for (int i = 0; i < enemies.Count; i++)
            {
                ChangeActivation(enemies[i], true);
            }
            for (int i = 0; i < pots.Length; i++)
            {
                ChangeActivation(pots[i], true);
            }

            //Switch On Next Room Virtual Camera
            virtualCamera.SetActive(true);

            //Place Text
            if (needText)
            {
                StartCoroutine(placeNameCo());
            }
        } 
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            //Deactivate game Objects
            for (int i = 0; i < enemies.Count; i++)
            {
                ChangeActivation(enemies[i], false);
            }
            for (int i = 0; i < pots.Length; i++)
            {
                ChangeActivation(pots[i], false);
            }

            //Switch Off Current Room Virtual Camera
            virtualCamera.SetActive(false);
        }       
    }

    public void ChangeActivation(Component component, bool isActive)
    {
        component.gameObject.SetActive(isActive);
    }

    public void updateRoomRegister()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i].isDying)
            {
                enemies.RemoveAt(i);
            }
        }
    }

    public void TurnToEvil()
    {
        for(int i= 0; i < enemies.Count; i++)
        {
            enemies[i].BecomeEvil();
        }
    }

    public void RemoveAllRoomEnemies()
    {
        while (enemies.Count > 0)
        {
            enemies[0].gameObject.SetActive(false);
            enemies.RemoveAt(0);
        }
    }

    private IEnumerator placeNameCo()
    {
        text.SetActive(true);
        placeText.text = placeName;
        yield return new WaitForSeconds(4f);
        text.SetActive(false);
    }
}
