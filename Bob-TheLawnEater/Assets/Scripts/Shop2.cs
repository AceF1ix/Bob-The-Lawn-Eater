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
        for(int i = 0; i < contentParent.childCount; i++)
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
    }
}
