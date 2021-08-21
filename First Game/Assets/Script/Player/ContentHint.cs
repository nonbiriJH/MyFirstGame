using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//create events for content hint listener
public class ContentHint : MonoBehaviour
{
    public GameObject contentHint;

    public void Enable()
    {
        contentHint.SetActive(true);
    }

    public void Disable()
    {
        contentHint.SetActive(false);
    }
}
