using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnGrass : MonoBehaviour
{
    public Transform grassParent;
    public Shop2 shop2;
    public GrassClipper grassClipper;
    private int[] respawnUpgrade = new int[]{0, 5, 10, 15, 30};
    private float[] moneyUpgrade = new float[]{1f, 2f, 3f, 4f, 5f};
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RespawnWhenGone();
    }

    void RespawnWhenGone()
    {
        int activeChildren = 0;
        int children = grassParent.childCount;
        for (int i = 0; i < children; ++i)
        {
            if (grassParent.GetChild(i).gameObject.activeSelf)
            {
                activeChildren += 1;
            }
        }
        if (activeChildren <= respawnUpgrade[shop2.typeRank[4]])
        {
            grassClipper.money += 100 * moneyUpgrade[shop2.typeRank[0]];
            for (int i = 0; i < children; ++i)
            {
                grassParent.GetChild(i).gameObject.SetActive(true);
            }
        }
    }
}
