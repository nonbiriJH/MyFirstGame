using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
[CreateAssetMenu(fileName = "Option", menuName = "Scriptable Objects/Option")]
public class Options : ScriptableObject
{
    public string question;
    public int selectedIndex;
    public string[] options;

    public void AsignSelectedInx(string option)
    {
        selectedIndex = -1;
        for (int i = 0; i < options.Length; i++)
        {
            if (option == options[i])
            {
                selectedIndex = i;
            }
        }
    }
}
