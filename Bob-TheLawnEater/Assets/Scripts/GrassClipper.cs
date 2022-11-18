using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GrassClipper : MonoBehaviour, IDataPersistence
{
    // Start is called before the first frame update
    public int grassClipped = 0;
    private float standardCapacity = 25;
    public float capacity;
    public Transform player;
    private float radius = 2.0f;
    public int numSegments = 200;
    public Collider[] hitColliders;
    public GameObject trashCan;
    public AudioManager audio;
    public GameObject lawnMowerBody;
    public GameObject lawnMowerWheels;
    public float timer = 0f;
    public float money = 0;
    private float[] capacityUpgrade = new float[]{1f, 2f, 4f, 7f, 15f};
    private float[] moneyUpgrade = new float[]{1f, 2f, 3f, 4f, 5f};
    public Material goldLawnWheels;
    public Shop2 shop2;
    public GameObject throwText;
    
    void Start()
    {
        DoRenderer();
        capacity = standardCapacity * capacityUpgrade[shop2.typeRank[2]];
        // Have gold if bought when game starts
        for (int i = shop2.upgrades.Length + shop2.cosmetics.Length; i < shop2.contentParent.childCount; i++) // materials
        {
            if (shop2.typeRank[i] == 1)
            {
                lawnMowerBody.GetComponent<Renderer>().material = shop2.materials[i - (shop2.upgrades.Length + shop2.cosmetics.Length)];
                lawnMowerWheels.GetComponent<Renderer>().material = shop2.materials[i - (shop2.upgrades.Length + shop2.cosmetics.Length)];
                break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        capacity = standardCapacity * capacityUpgrade[shop2.typeRank[2]];
        hitColliders = Physics.OverlapSphere(player.position, radius);
        for (var i = 0; i < hitColliders.Length; i++){
            if (hitColliders[i].tag == "TrashCan" && grassClipped > 0){
                throwText.SetActive(true);
                if(Input.GetKeyDown(KeyCode.F))
                {
                    ThrowTrash();
                }
                break;
            }
            else
            {
                throwText.SetActive(false);
            }
        }

        if(Time.realtimeSinceStartup - timer > 0.2f)
        {
            audio.Stop("Lawn-mower-cutting grass");
        }

        if(shop2.typeRank[7] == 1)
        {
            lawnMowerWheels.GetComponent<Renderer>().material = goldLawnWheels;
        }
    }

    public void LoadData(GameData data)
    {
        this.money = data.moneyCount;
    }

    public void SaveData(ref GameData data)
    {
        data.moneyCount = this.money;
    }

    public void ThrowTrash(){
        money += grassClipped * moneyUpgrade[shop2.typeRank[0]];
        audio.Play("ThrowTrash");
        grassClipped = 0;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Grass" && grassClipped < capacity)
        {
            timer = Time.realtimeSinceStartup;
            audio.Play("Lawn-mower-cutting grass");
            grassClipped += 1;
            other.transform.parent.gameObject.SetActive(false);
        }
    }

    void DoRenderer()
    {
        LineRenderer lineRenderer = trashCan.GetComponent<LineRenderer>();
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.positionCount = numSegments + 1;
        lineRenderer.useWorldSpace = false;

        float deltaTheta = (float) (2.0 * Mathf.PI) / numSegments;
        float theta = 0f;

        for (int i = 0; i < numSegments + 1; i++)
        {
            float x = radius * Mathf.Cos(theta);
            float z = radius * Mathf.Sin(theta);
            Vector3 pos = new Vector3(x, -1f, z);
            lineRenderer.SetPosition(i, pos);
            theta += deltaTheta;
        }
    }
}
