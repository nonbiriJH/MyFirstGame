using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]//create new class
public class Loot
{
    public PowerUp loot;
    public int probability;
}

[CreateAssetMenu]
public class LootTable : ScriptableObject
{
    public Loot[] loots;

    public PowerUp ChooseLoot()
    {
        int targetNumber = 0;
        int randomNumber = Random.Range(0, 100);

        for (int i = 0; i < loots.Length; i++)
        {
            targetNumber += loots[i].probability;
            if (randomNumber < targetNumber)
            {
                return loots[i].loot;
            }
        }
        return null;
    }
}
