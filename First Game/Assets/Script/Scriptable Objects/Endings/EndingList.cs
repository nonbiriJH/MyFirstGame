using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
[CreateAssetMenu(fileName = "EndingList", menuName = "Scriptable Objects/Endings/Ending List")]
public class EndingList : ScriptableObject
{
    public Endings chosenEnding;
    public List<Endings> endings = new List<Endings>();


}
