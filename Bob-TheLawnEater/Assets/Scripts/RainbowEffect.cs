using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainbowEffect : MonoBehaviour
{
    public float rainbowSpeed;
    private float hue;
    private float sat;
    private float bri;
    public Shop2 shop2;
    public Material rainbowMaterial;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Color.RGBToHSV(rainbowMaterial.color, out hue, out sat, out bri);

        hue += rainbowSpeed / 10000;
        if (hue >= 1)
        {
            hue = 0;
        }
        sat = 1;
        bri = 1;

        rainbowMaterial.color = Color.HSVToRGB(hue, sat, bri);
    }
}
