using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnGrass : MonoBehaviour
{
    public Transform grassParent;
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
        if (activeChildren == 0)
        {
            for (int i = 0; i < children; ++i)
            {
                grassParent.GetChild(i).gameObject.SetActive(true);
            }
        }
    }
}
