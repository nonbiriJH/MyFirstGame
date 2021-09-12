using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Skill
{
    public string skillName;
    public GameObject skill;
    public int magicCost;
}

[CreateAssetMenu]
public class SkillLookup : ScriptableObject
{

    public Skill[] skills;
}
