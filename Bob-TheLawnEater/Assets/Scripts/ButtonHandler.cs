using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class ButtonHandler : MonoBehaviour
{
    public GameObject defaultUI;
    public GameObject shopUI;
    public Shop2 shop2;
    public GrassClipper grassClipper;
    // Start is called before the first frame update
    public void OnClickOpenShop()
    {
        defaultUI.SetActive(false);
        shopUI.SetActive(true);
    }
    
    public void OnClickExitShop()
    {
        defaultUI.SetActive(true);
        shopUI.SetActive(false);
    }

    public void OnClickBuy()
    {
        for (int i = 0; i < shop2.contentParent.childCount; i++)
        {
            if (EventSystem.current.currentSelectedGameObject.transform.parent == shop2.contentParent.GetChild(i) && grassClipper.money >= shop2.upgradeCosts[shop2.typeRank[i]])
            {
                grassClipper.money -= shop2.upgradeCosts[shop2.typeRank[i]]; // Spend money
                shop2.typeRank[i] += 1; // Rank up
            }
        }
    }

    public void OnClickQuit()
    {
        Application.Quit();
    }
}
