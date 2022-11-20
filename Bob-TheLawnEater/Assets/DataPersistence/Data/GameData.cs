using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class GameData
{
    public float moneyCount;
    public int[] rank;

    // The values in the constructor will be default values
    // the game starts with when there's no data to load

    public GameData()
    {
        this.moneyCount = 0f; // money
        this.rank = new int[]{0, 0, 0, 0, 0, 0, 0, 0}; // upgrade and cosmetic ranks
    }
}
