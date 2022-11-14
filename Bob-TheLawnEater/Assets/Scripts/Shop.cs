using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
/*
public class Shop : MonoBehaviour, IDataPersistence
{
    [System.Serializable] public class ShopItem {
        public string ItemName;
        public Sprite Image;
        public int buyRank;
    }

    [SerializeField] public List<ShopItem> ShopItemsList;
    public GameObject contentParent;
    GameObject ItemTemplate;
    GameObject g;
    [SerializeField] Transform ShopScrollView;

    public string[] Rank = new string[]{"I", "II", "III", "IV", "Max"};
    public int[] Prices = new int[]{500, 1000, 1500, 2000};

    void Start()
    {
        // Item Creator
        print(new List<ShopItem>());
        ItemTemplate = ShopScrollView.GetChild(0).gameObject;
        print(ShopItemsList.Count);

        int n_items = ShopItemsList.Count;
        for (int i=0; i < n_items; i++) {
            g = Instantiate(ItemTemplate, ShopScrollView);
            g.transform.GetChild(0).GetComponent<TMP_Text>().text = ShopItemsList[i].ItemName + " " + Rank[ShopItemsList[i].buyRank];
            g.transform.GetChild(1).GetComponent<Image>().sprite = ShopItemsList[i].Image;
            g.transform.GetChild(2).GetChild(0).GetComponent<TMP_Text>().text = Prices[ShopItemsList[i].buyRank].ToString();
            if (Rank[ShopItemsList[i].buyRank] == "Max")
            {
                g.transform.GetChild(3).GetComponent<Button>().interactable = false;
            }
        }

        Destroy(ItemTemplate);
    }

    public void OnClickBuy()
    {
        print(EventSystem.current.currentSelectedGameObject.transform.parent.gameObject);
        print(ShopItemsList.Count);
        for (int i = 0; i < ShopItemsList.Count; i++)
        {
            print("Hi");
            if (EventSystem.current.currentSelectedGameObject.transform.parent.gameObject == contentParent.transform.GetChild(i).gameObject)
            {
                ShopItemsList[i].buyRank += 1;
                Debug.Log("Yes");
            }
        }
    }

}
*/