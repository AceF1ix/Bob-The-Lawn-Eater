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
    public IsometricMovement isometricMovement;
    public GameObject player;
    public DataPersistenceManager dataPersistenceManager;
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
            if (EventSystem.current.currentSelectedGameObject.transform.parent == shop2.contentParent.GetChild(i))
            {
                if (i < shop2.upgrades.Length && grassClipper.money >= shop2.upgradeCosts[shop2.typeRank[i]])
                {
                    grassClipper.money -= shop2.upgradeCosts[shop2.typeRank[i]]; // Spend money on upgrade
                    shop2.typeRank[i] += 1; // Rank up
                    if (i == 3)
                    {
                        player.transform.localScale += new Vector3(isometricMovement.sizeUpgrade[shop2.typeRank[i]], isometricMovement.sizeUpgrade[shop2.typeRank[i]], isometricMovement.sizeUpgrade[shop2.typeRank[i]]);
                    }
                }
                else if (i >= shop2.upgrades.Length && i < shop2.upgrades.Length + shop2.cosmetics.Length && grassClipper.money >= shop2.singleCosts[i - shop2.upgrades.Length])
                {
                    grassClipper.money -= shop2.singleCosts[i - shop2.upgrades.Length];
                    shop2.typeRank[i] += 1;
                    shop2.cosmetics[i - shop2.upgrades.Length].SetActive(true);
                }
                else if (i >= shop2.upgrades.Length + shop2.cosmetics.Length && grassClipper.money >= shop2.singleCosts[i - shop2.upgrades.Length])
                {
                    grassClipper.money -= shop2.singleCosts[i - shop2.upgrades.Length];
                    shop2.typeRank[i] += 1;
                    grassClipper.lawnMowerBody.GetComponent<Renderer>().material = shop2.materials[i - (shop2.upgrades.Length + shop2.cosmetics.Length)];
                    grassClipper.lawnMowerWheels.GetComponent<Renderer>().material = shop2.materials[i - (shop2.upgrades.Length + shop2.cosmetics.Length)];
                }
            }
        }
    }

    public void OnClickStartGame()
    {
        SceneManager.LoadScene("Models");
    }
    
    public void OnClickMenu()
    {
        dataPersistenceManager.SaveGame();
        SceneManager.LoadScene("Menu");
    }

    public void OnClickQuit()
    {
        Application.Quit();
    }
}
