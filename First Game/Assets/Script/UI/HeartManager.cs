using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartManager : MonoBehaviour
{
    public List<Image> heartContainer = new List<Image>();
    public Sprite fullHeart;
    public Sprite halfHeart;
    public Sprite emptyHeart;
    public floatValue playerHealth;

    private void Start()
    {
        //Creat Heart Container
        UpdateHeathContainer();
        //Fill Heart Container
        UpdateHeath();
    }

    private void UpdateHeathContainer()
    {
        float temp = playerHealth.maxValue / 2f; //2 health point = 1 full heart
        //Within UI Max Heart
        for (int i = 0; i < heartContainer.Count; i++)
        {
            if (i < temp)
            {
                heartContainer[i].gameObject.SetActive(true);
            }
            else
            {
                heartContainer[i].gameObject.SetActive(false);
            }
        }
    }

    public void UpdateHeath() //involke when signal sent from PlayerMovement
    {
        float temp = playerHealth.runtimeValue / 2f; //2 health point = 1 full heart
        for (int i = 0; i < heartContainer.Count; i++)
        {
            if (i <= temp - 1)
            {
                heartContainer[i].sprite = fullHeart;
            }
            else if (i >= temp)
            {
                heartContainer[i].sprite = emptyHeart;
            }
            else
            {
                heartContainer[i].sprite = halfHeart;
            }
        }
    }
}
