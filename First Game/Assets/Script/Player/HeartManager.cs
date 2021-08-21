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
    public floatValue healthContainer;
    public floatValue playerHealth;

    private void Start()
    {
        InitHealth();
    }

    private void InitHealth()
    {
        for (int i = 0; i < heartContainer.Count; i++)
        {
            if (i < healthContainer.initialValue)
            {
                heartContainer[i].gameObject.SetActive(true);
                heartContainer[i].sprite = fullHeart;
            }
            else
            {
                heartContainer[i].gameObject.SetActive(false);
            }
        }
    }

    public void UpdateHeath() //involke when signal sent from PlayerMovement
    {
        float temp = playerHealth.runtimeValue / 2; //2 health point = 1 full heart
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
