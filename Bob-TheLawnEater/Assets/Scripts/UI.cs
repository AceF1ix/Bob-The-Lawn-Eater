using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI : MonoBehaviour
{
    public GrassClipper grassClipper;
    public TextMeshProUGUI countText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {        
        countText.text = "Grass: " + grassClipper.grassClipped;
    }
}
