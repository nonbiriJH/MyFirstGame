using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Endings", menuName = "Scriptable Objects/Endings/Ending")]
public class Endings : ScriptableObject
{
    public Sprite sprite;
    public string title;
    public string subTitle;
    public string description;
    public bool attained;
}
