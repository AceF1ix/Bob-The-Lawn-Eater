using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI : MonoBehaviour
{
    public GrassClipper grassClipper;
    public TextMeshProUGUI countText;
    public TextMeshProUGUI moneyText;
    public Slider grassSlider;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {        
        countText.text = (Mathf.Abs(grassClipper.grassClipped / grassClipper.capacity) * 100) + "%";
        moneyText.text = grassClipper.money.ToString();
        grassSlider.value = (grassClipper.grassClipped / grassClipper.capacity);
    }
}
