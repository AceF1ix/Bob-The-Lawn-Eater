using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Shop2 : MonoBehaviour, IDataPersistence
{
    public GrassClipper grassClipper;
    public TextMeshProUGUI shopMoneyText;
    public Transform contentParent;
    public string[] typeNames;
    public int[] typeRank;
    private string[] upgradeNumerals = new string[]{"I", "II", "III", "IV", "Max"};
    public int[] upgradeCosts;
    public int[] singleCosts;
    public Material[] materials;
    public GameObject[] cosmetics;
    public Sprite[] upgrades;


    public void LoadData(GameData data)
    {
        for (int i = 0; i < this.contentParent.childCount; i++)
        {
            this.typeRank[i] = data.rank[i];
        }
    }

    public void SaveData(ref GameData data)
    {
        for (int i=0; i < this.contentParent.childCount; i++)
        {
            data.rank[i] = this.typeRank[i];
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        shopMoneyText.text = grassClipper.money.ToString();
        for(int i = 0; i < contentParent.childCount - cosmetics.Length; i++) // upgrades
        {
            Transform upgrade = contentParent.GetChild(i);
            if(typeRank[i] < 4) // not maxed
            {
                upgrade.GetChild(0).GetComponent<TMP_Text>().text = typeNames[i] + " " + upgradeNumerals[typeRank[i]].ToString();
                upgrade.GetChild(2).GetChild(0).GetComponent<TMP_Text>().text = upgradeCosts[typeRank[i]].ToString();
            }
            else // is maxed
            {
                upgrade.GetChild(0).GetComponent<TMP_Text>().text = typeNames[i] + " " + upgradeNumerals[typeRank[i]].ToString();
                upgrade.GetChild(2).gameObject.SetActive(false);
                upgrade.GetChild(3).gameObject.SetActive(false);
            }
        }
        for(int n = upgrades.Length; n < contentParent.childCount; n++) // cosmetics
        {
            Transform cosmetic = contentParent.GetChild(n);
            if(typeRank[n] < 1)
            {
                cosmetic.GetChild(0).GetComponent<TMP_Text>().text = typeNames[n];
                cosmetic.GetChild(2).GetChild(0).GetComponent<TMP_Text>().text = singleCosts[n - upgrades.Length].ToString();
            }
            else
            {
                cosmetic.GetChild(0).GetComponent<TMP_Text>().text = typeNames[n] + " Sold";
                cosmetic.GetChild(2).gameObject.SetActive(false);
                cosmetic.GetChild(3).gameObject.SetActive(false);
            }
        }
        for(int q = upgrades.Length + cosmetics.Length; q < contentParent.childCount; q++)
        {
            Transform material = contentParent.GetChild(q);
            if(typeRank[q] < 1)
            {
                material.GetChild(0).GetComponent<TMP_Text>().text = typeNames[q];
                material.GetChild(2).GetChild(0).GetComponent<TMP_Text>().text = singleCosts[q - upgrades.Length].ToString();
            }
            else
            {
                material.GetChild(0).GetComponent<TMP_Text>().text = typeNames[q] + " Sold";
                material.GetChild(2).gameObject.SetActive(false);
                material.GetChild(3).gameObject.SetActive(false);
            }
        }
    }
}
